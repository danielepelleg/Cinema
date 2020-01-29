using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;

namespace WCFDatabaseManager
{
    // NOTA: è possibile utilizzare il comando "Rinomina" del menu "Refactoring" per modificare il nome di classe "ServicePrenotation" nel codice e nel file di configurazione contemporaneamente.
    public class ServicePrenotation : IServicePrenotation
    {
        List<Prenotation> listPrenotazioni = new List<Prenotation>();
        Prenotation p1 = new Prenotation();
        public void DoWork()
        {
        }

        public Prenotation MakePrenotation()
        {
            Prenotation p = new Prenotation(1,DateTime.Now,"corso",12);
            return p;
        }

        //Inserimento Prenotazioni
        public string InserimentoPrenotazione(DateTime dateTime, string UsernameUser, int eventCode, int placeNumber)
        {
            var t = new TablePrinter("Numero Prenotazione", "ID Evento", "Username", "Data e Ora Prenotazione");
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
                    cmd.CommandText = "Cinema.AddNewPrenotazione";
                    cmd.Parameters.Add("@DataOra", SqlDbType.DateTime).Value = dateTime;
                    cmd.Parameters.Add("@Username_UtenteFree", SqlDbType.VarChar).Value = UsernameUser;
                    cmd.Parameters.Add("@Codice_Evento", SqlDbType.Int).Value = eventCode;
                    cmd.Parameters.Add("@CodicePrenotazione", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Connection = connection;
                    cmd.ExecuteNonQuery();
                    // CodiceEvento is an IDENTITY value from the database.
                    p1.PrenotationCode = Convert.ToInt32(cmd.Parameters["@CodicePrenotazione"].Value.ToString());
                    p1.DateTime = dateTime;
                    p1.UsernameUser = UsernameUser;
                    p1.EventCode = eventCode;
                    t.AddRow(p1.PrenotationCode, p1.EventCode, p1.UsernameUser, p1.DateTime.ToShortDateString() + " " + p1.DateTime.ToShortTimeString());
                    // Commit the transaction.
                    sqlTran.Commit();
                    //Inserisco i valori nella tabella Riserva
                    SqlTransaction sqlTran1 = connection.BeginTransaction();
                    SqlCommand cmd1 = connection.CreateCommand();
                    cmd1.Transaction = sqlTran1;
                    cmd1.CommandType = CommandType.StoredProcedure;
                    cmd1.CommandText = "Cinema.AddNewRiserva";
                    cmd1.Parameters.Add("@numero_posto", SqlDbType.Int).Value = placeNumber;
                    cmd1.Parameters.Add("@codice_prenotazione", SqlDbType.Int).Value = p1.PrenotationCode;
                    cmd1.Connection = connection;
                    cmd1.ExecuteNonQuery();
                    sqlTran1.Commit();
                    return "PRENOTAZIONE EFFETTUATA CON SUCCESSO!\nBiglietto acquistato: \n" + t.Print() + "\n";
                }
                catch (SqlException ex)
                {
                    return string.Format("{0}", ex.ToString());
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
                {
                    listPrenotazioni.Clear(); //pulisco la lista di film in modo tale da riempirla nuovamente e non avere problemi se sono avvenute modifiche
                    conn.Open();
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
