using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;

namespace WCFDatabaseManager
{
    // NOTA: è possibile utilizzare il comando "Rinomina" del menu "Refactoring" per modificare il nome di classe "ServiceHall" nel codice e nel file di configurazione contemporaneamente.
    public class ServiceHall : IServiceHall
    {
        //Visualizzazione Elenco Sale
        public string VisualizzazioneSale()
        {
            var t = new TablePrinter(" SALA ", " CAPIENZA ");
            SqlTransaction tx = null;
            //string elenco = string.Empty;
            //int i = 0; //variabile di incremento per la lista
            try
            {
                // Connessione al DB Cinema
                using (SqlConnection conn = DatabaseHandler.GetConnection())
                {
                    conn.Open();
                    tx = conn.BeginTransaction();
                    using (SqlCommand command1 = conn.CreateCommand())
                    {
                        command1.CommandText = "SELECT * FROM Cinema.Sala;";
                        command1.Transaction = tx;
                        using (SqlDataReader reader = command1.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Hall s = new Hall();
                                s.HallCode = reader.GetInt32(0);
                                s.Capacity = reader.GetInt32(1);
                                //elenco = elenco + listSale[i].VisualizzaSala() + "\n";
                                //i++;
                                t.AddRow(s.HallCode, s.Capacity);
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
            return t.Print();
        }

        //Visualizzazione dei posti in sala
        //Funzione che mi da una semplice rappresentazione a console di come sono disposti i posti nelle varie sale del cinema
        public string RappresentaSale(int codice_evento)
        {
            SqlTransaction tx = null;
            string output = string.Empty;
            Event e1 = new Event();
            try
            {
                // Connessione al DB Cinema
                using (SqlConnection conn = DatabaseHandler.GetConnection())
                {
                    conn.Open();
                    tx = conn.BeginTransaction();
                    using (SqlCommand command1 = conn.CreateCommand())
                    {
                        command1.Transaction = tx;
                        command1.CommandType = CommandType.StoredProcedure;
                        command1.CommandText = "Cinema.RichiestaCodiceSala";
                        command1.Parameters.Add("@codice_evento", SqlDbType.Int).Value = codice_evento;
                        command1.Connection = conn;
                        command1.ExecuteNonQuery();
                        using (SqlDataReader reader = command1.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                e1.EventCode = reader.GetInt32(0);
                            }
                            switch (e1.EventCode)
                            {
                                case 1:
                                    output = "\n________________________________\n\\______________________________/\n\n   | 1 | 2 | 3 | 4 | 5 | 6 |\n" +
                                        "   | 7 | 8 | 9 | 10| 11| 12|\n   | 13| 14| 15| 16| 17| 18|\n   | 19| 20| 21| 22| 23| 24|\n";
                                    break;
                                case 2:
                                    output = "\n________________________\n\\______________________/\n\n   | 1 | 2 | 3 | 4 |\n" +
                                        "   | 5 | 6 | 7 | 8 |\n   | 9 | 10| 11| 12|\n   | 13| 14| 15| 16|\n";
                                    break;
                                case 3:
                                    output = "\n________________________________\n\\______________________________/\n\n   | 1 | 2 | 3 | 4 | 5 | 6 |\n" +
                                        "   | 7 | 8 | 9 | 10| 11| 12|\n   | 13| 14| 15| 16| 17| 18|\n   | 19| 20| 21| 22| 23| 24|\n   | 25| 26| 27| 28| 29| 30|\n";
                                    break;
                                default:
                                    output = "Errore";
                                    break;
                            }
                        }
                    }
                    tx.Commit();
                    conn.Close();
                }
            }
            catch (SqlException ex)
            {
                return string.Format("Connessione non riuscita: {0}", ex.ToString());
            }
            return output;
        }
    }
}
