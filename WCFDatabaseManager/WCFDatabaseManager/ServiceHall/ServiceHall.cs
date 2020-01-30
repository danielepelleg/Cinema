using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;

namespace WCFDatabaseManager
{
    // NOTA: è possibile utilizzare il comando "Rinomina" del menu "Refactoring" per modificare il nome di classe "ServiceHall" nel codice e nel file di configurazione contemporaneamente.
    public class ServiceHall : IServiceHall
    {
        /*
         * Get the list containing the Halls of the database
         */
        public List<Hall> GetHallsList() {
            // Database connection
            using (SqlConnection connection = DatabaseHandler.GetConnection()) {
                // Define a new list of Users
                List<Hall> hallsList = new List<Hall>();

                connection.Open();

                // Start a local transaction.
                SqlTransaction transaction = connection.BeginTransaction();
                SqlCommand command = connection.CreateCommand();

                // Must assign both transaction object and connection
                // to Command object for a pending local transaction
                command.Connection = connection;
                command.Transaction = transaction;

                try {
                    command.CommandText = "SELECT * FROM Cinema.Sala;";
                    using (SqlDataReader reader = command.ExecuteReader()) {
                        while (reader.Read()) {
                            var hallCode = reader.GetInt32(0);
                            var capacity = reader.GetInt32(1);

                            hallsList.Add(new Hall(hallCode, capacity));
                        }
                    }
                    // Attempt to commit the transaction.
                    transaction.Commit();

                    return hallsList;
                }
                catch (Exception ex) {
                    Console.WriteLine("Commit Exception Type: {0}", ex.GetType());
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

                    return new List<Hall>() { };
                }
            }
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
