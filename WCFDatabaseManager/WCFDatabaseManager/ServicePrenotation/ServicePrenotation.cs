using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;

namespace WCFDatabaseManager
{
    // NOTA: è possibile utilizzare il comando "Rinomina" del menu "Refactoring" per modificare il nome di classe "ServicePrenotation" nel codice e nel file di configurazione contemporaneamente.
    public class ServicePrenotation : IServicePrenotation
    {
        /*
         * Add a Prenotation into the database.
         */
        public bool AddPrenotation(DateTime dateTime, string usernameUser, int eventCode, int placeNumber) {
            using (SqlConnection connection = DatabaseHandler.GetConnection()) {
                connection.Open();

                // Start a local transaction.
                SqlTransaction transaction = connection.BeginTransaction();
                SqlCommand command = connection.CreateCommand();

                // Must assign both transaction object and connection
                // to Command object for a pending local transaction
                command.Connection = connection;
                command.Transaction = transaction;

                try {
                    // Insert the Prenotation into the database
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "Cinema.AddNewPrenotazione";
                    command.Parameters.Add("@DataOra", SqlDbType.DateTime).Value = dateTime;
                    command.Parameters.Add("@Username_UtenteFree", SqlDbType.VarChar).Value = usernameUser;
                    command.Parameters.Add("@Codice_Evento", SqlDbType.Int).Value = eventCode;
                    command.Parameters.Add("@CodicePrenotazione", SqlDbType.Int).Direction = ParameterDirection.Output;
                    command.ExecuteNonQuery();

                    // Create a Prenotation Object
                    Prenotation prenotation = new Prenotation(Convert.ToInt32(command.Parameters["@CodicePrenotazione"].Value.ToString()), 
                        dateTime, usernameUser, eventCode);

                    // Initialize a int value to check if the Stored Procedure success
                    var returnParameter1 = command.Parameters.Add("@ReturnVal", SqlDbType.Int);
                    returnParameter1.Direction = ParameterDirection.ReturnValue;

                    // Commit the transaction
                    transaction.Commit();

                    //Reset the parameters
                    command.Parameters.Clear();

                    // Insert data in Reserve table in the database
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "Cinema.AddNewRiserva";
                    command.Parameters.Add("@numero_posto", SqlDbType.Int).Value = placeNumber;
                    command.Parameters.Add("@codice_prenotazione", SqlDbType.Int).Value = prenotation.PrenotationCode;
                    command.ExecuteNonQuery();

                    // Initialize a int value to check if the Stored Procedure success
                    var returnParameter2 = command.Parameters.Add("@ReturnVal", SqlDbType.Int);
                    returnParameter2.Direction = ParameterDirection.ReturnValue;

                    // Commit the transaction
                    transaction.Commit();

                    // If both the int value > 0 the Stored Procedure both success
                    if (returnParameter1.Direction > 0 && returnParameter2.Direction > 0) return true;
                    else {
                        command.Parameters.Clear();
                        throw new Exception("Errore: si è verificato un problema nell'aggiungere un Evento nel DB");
                    }
                }
                catch (SqlException ex) {
                    Console.WriteLine("\nCommit Exception Type: {0}", ex.GetType());
                    Console.WriteLine("  Message: {0}", ex.Message);

                    // Attempt to roll back the transaction.
                    try {
                        transaction.Rollback();
                    }
                    catch (Exception ex2) {
                        // This catch block will handle any errors that may have occurred
                        // on the server that would cause the rollback to fail, such as
                        // a closed connection.
                        Console.WriteLine("Rollback Exception Type: {0}", ex2.GetType());
                        Console.WriteLine("  Message: {0}", ex2.Message);
                    }

                    return false;
                }
            }

        }

        //Visualizzazione Elenco Prenotazioni filtrato per Utente
        public string Visualizzazione_elenco_Prenotazioni(string user)
        {
            var t = new TablePrinter("Prenotation ID", "Prenotation Date", "User", "Event Code", "Price, Title", "Event Date", "Hall", "Price", "Place Number");
            SqlTransaction tx = null;
            string elenco = string.Empty;
            int i = 0; //variabile di incremento per la lista
            try
            {
                // Connessione al DB Cinema
                using (SqlConnection conn = DatabaseHandler.GetConnection())
                {conn.Open();
                    tx = conn.BeginTransaction();
                    using (SqlCommand command1 = conn.CreateCommand())
                    {
                        command1.Transaction = tx;
                        command1.CommandType = CommandType.StoredProcedure;
                        command1.CommandText = "Cinema.VisualizzaPrenotazione";
                        command1.Parameters.Add("@user", SqlDbType.VarChar).Value = user;
                        command1.Connection = conn;
                        command1.ExecuteNonQuery();

                        using (SqlDataReader reader = command1.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Prenotation p = new Prenotation();
                                p.PrenotationCode = reader.GetInt32(0);
                                p.DateTime = reader.GetDateTime(1);
                                p.UsernameUser = reader.GetString(2);
                                p.EventCode = reader.GetInt32(3);
                                /*elenco = elenco + listPrenotazioni[i].VisualizzaPrenotazioni() + "\n" + "Titolo: " + reader.GetString(4) + ", Data e Ora dell'evento: " + reader.GetDateTime(5) +
                                    ", Sala: " + reader.GetInt32(6) + "\nPrezzo: " + reader.GetDecimal(7) + ", Numero posto:" + reader.GetInt32(8) + "\n";*/
                                t.AddRow(p.PrenotationCode, p.DateTime.ToShortDateString() + " " + p.DateTime.ToShortTimeString(), p.UsernameUser, p.EventCode, reader.GetString(4), reader.GetDateTime(5), reader.GetInt32(6), reader.GetDecimal(7) + "€", reader.GetInt32(8));
                                i++;
                            }
                        }
                    }
                    tx.Commit();
                }
            }
            catch (SqlException ex)
            {
                return string.Format("Connessione non riuscita: {0}", ex.ToString());
            }
            /*if (elenco == string.Empty)
                return "Non sono state ancora effettuate prenotazioni \n";
            else*/
            return t.Print();
        }
    }
}
