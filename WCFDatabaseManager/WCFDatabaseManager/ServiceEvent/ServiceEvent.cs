using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;

namespace WCFDatabaseManager
{
    // NOTA: è possibile utilizzare il comando "Rinomina" del menu "Refactoring" per modificare il nome di classe "ServiceEvent" nel codice e nel file di configurazione contemporaneamente.
    public class ServiceEvent : IServiceEvent
    {
        /*
         * Add an event into the database.
         */
        public bool AddEvent(string usernameAdmin, DateTime dateTime, int filmCode, int hallCode, decimal price)
        {
            using (SqlConnection connection = DatabaseHandler.GetConnection())
            {
                connection.Open();
                // Start a local transaction.
                SqlTransaction transaction = connection.BeginTransaction();
                SqlCommand command = connection.CreateCommand();

                // Must assign both transaction object and connection
                // to Command object for a pending local transaction
                command.Connection = connection;
                command.Transaction = transaction;

                command.CommandText = "SELECT * FROM Cinema.Evento;";
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    if (dateTime == reader.GetDateTime(1) && hallCode == reader.GetInt32(4))
                    {
                        return false;
                        //return "\nNella sala inserita è gia presente una proiezione di un film nella data e all'orario che hai appena inserito!\nImpossibile effettuare l'inserimento! Riprovare!\n";
                    }
                }
                reader.Close();
                // Commit the transaction
                transaction.Commit();

                // Start a local transaction.
                SqlTransaction transaction2 = connection.BeginTransaction();
                SqlCommand command2 = connection.CreateCommand();

                // Must assign both transaction object and connection
                // to Command object for a pending local transaction
                command2.Connection = connection;
                command2.Transaction = transaction2;

                try
                {
                    // Insert the event in the database
                    command2.CommandType = CommandType.StoredProcedure;
                    command2.CommandText = "Cinema.AddNewEvento";
                    command2.Parameters.Add("@DataOra", SqlDbType.DateTime).Value = dateTime;
                    command2.Parameters.Add("@Codice_Film", SqlDbType.Int).Value = filmCode;
                    command2.Parameters.Add("@Codice_Sala", SqlDbType.Int).Value = hallCode;
                    command2.Parameters.Add("@Username_Admin", SqlDbType.VarChar).Value = usernameAdmin;
                    command2.Parameters.Add("@Prezzo", SqlDbType.Decimal).Value = price;
                    command2.Parameters.Add("@CodiceEvento", SqlDbType.Int).Direction = ParameterDirection.Output;
                    command2.Connection = connection;
                    command2.ExecuteNonQuery();

                    // Initialize a int value to check if the query success
                    var returnParameter = command.Parameters.Add("@ReturnVal", SqlDbType.Int);
                    returnParameter.Direction = ParameterDirection.ReturnValue;

                    // Commit the transaction
                    transaction2.Commit();

                    if (returnParameter.Direction > 0) return true;
                    else
                    {
                        command.Parameters.Clear();
                        throw new Exception("Errore: si è verificato un problema nell'aggiungere un Film nel DB");
                    }
                    /*

                    // CodiceEvento is an IDENTITY value from the database.
                    e1.eventCode = Convert.ToInt32(command2.Parameters["@CodiceEvento"].Value.ToString());
                    e1.dateTime = data_e_ora;
                    e1.filmCode = codice_film;
                    e1.hallCode = codice_sala;
                    e1.usernameAdmin = usernameadmin;
                    e1.price = prezzo;
                    
                    var t = new TablePrinter("ID EVENTO", "Data e Ora", "Sala", "ID FILM", "Prezzo");
                    t.AddRow(e1.eventCode, e1.dateTime.ToShortDateString() + " " + e1.dateTime.ToShortTimeString(), e1.hallCode, e1.filmCode, e1.price + "€");
                    */
                    // Commit the transaction.

                }
                catch (SqlException ex)
                { // TODO toglibile (?)
                    Console.WriteLine("\nCommit Exception Type: {0}", ex.GetType());
                    Console.WriteLine("  Message: {0}", ex.Message);

                    // Attempt to roll back the transaction.
                    try
                    {
                        transaction.Rollback();
                    }
                    catch (Exception ex2)
                    {
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

        /*
         * Delete an event from the database.
         */
        public string CancellazioneEvento(int codiceevento)
        {

            using (SqlConnection connection = DatabaseHandler.GetConnection())
            {
                connection.Open();

                // Start a local transaction.
                SqlTransaction sqlTran = connection.BeginTransaction();

                // Enlist a command in the current transaction.
                SqlCommand cmd = connection.CreateCommand();
                cmd.Transaction = sqlTran;


                try
                {
                    // Esecuzione Inserimento
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Cinema.DeleteEvento";
                    cmd.Parameters.Add("@CodiceEvento", SqlDbType.Int).Value = codiceevento;
                    cmd.Connection = connection;
                    cmd.ExecuteNonQuery();
                    // Commit the transaction.
                    sqlTran.Commit();
                    connection.Close(); // Chiusura Connessione
                    return "CANCELLAZIONE DELL'EVENTO DALLA PROGRAMMAZIONE AVVENUTA CON SUCCESSO\n";

                }
                catch (SqlException ex)
                {
                    return ex.Message;
                }
            }

        }

        //Visualizzazione Elenco Eventi
        public string Visualizzazione_elenco_eventi()
        {
            var t = new TablePrinter("ID EVENTO", "Data e Ora", "Sala", "ID FILM", "Prezzo");
            SqlTransaction tx = null;
            //string elenco = string.Empty;
            //int i = 0; 
            try
            {
                // Connessione al DB Cinema
                using (SqlConnection conn = DatabaseHandler.GetConnection())
                {
                    conn.Open();
                    tx = conn.BeginTransaction();
                    using (SqlCommand command1 = conn.CreateCommand())
                    {
                        command1.CommandText = "SELECT * FROM Cinema.Evento;";
                        command1.Transaction = tx;
                        using (SqlDataReader reader = command1.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Event e = new Event();
                                e.EventCode = reader.GetInt32(0);
                                e.DateTime = reader.GetDateTime(1);
                                e.FilmCode = reader.GetInt32(2);
                                e.HallCode = reader.GetInt32(3);
                                e.UsernameAdmin = reader.GetString(4);
                                e.Price = reader.GetDecimal(5);
                                //elenco = elenco + listEventi[i].VisualizzaEventi() + "\n";
                                t.AddRow(e.EventCode, e.DateTime.ToShortDateString() + " " + e.DateTime.ToShortTimeString(), e.HallCode, e.FilmCode, e.Price + "€");
                                //i++;
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
                return "Non sono presenti film in programmazione in questo momento. \n";
            else*/
            return t.Print();
        }
    }
}
