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

                // Assign both transaction object and connection to Command object
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
                        // on the server that would cause the rollback to fail 
                        Console.WriteLine("Rollback Exception Type: {0}", ex2.GetType());
                        Console.WriteLine("  Message: {0}", ex2.Message);
                    }

                    return new List<Hall>() { };
                }
            }
        }

        /*
         * Show the places in a hall.
         * Simple console rappresentation of the places disposition in a hall
         */ 
        public string DrawHall(int eventCode)
        {
            string drawHall = string.Empty;
            Hall h = new Hall();

            // Database connection
            using (SqlConnection connection = DatabaseHandler.GetConnection()) {
                connection.Open();

                // Start a local transaction.
                SqlTransaction transaction = connection.BeginTransaction();
                SqlCommand command = connection.CreateCommand();

                // Assign both transaction object and connection to Command object
                command.Connection = connection;
                command.Transaction = transaction;

                try {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "Cinema.RichiestaCodiceSala";
                    command.Parameters.Add("@CodiceEvento", SqlDbType.Int).Value = eventCode;

                    using (SqlDataReader reader = command.ExecuteReader()) {
                        while (reader.Read()) {
                            h.HallCode = reader.GetInt32(0);
                        }
                    }

                    // Attempt to commit the transaction.
                    transaction.Commit();

                    switch (h.HallCode) {
                        case 1:
                            drawHall = "\n  ________________________________\n  \\______________________________/\n\n      | 1 | 2 | 3 | 4 | 5 | 6 |\n" +
                                "      | 7 | 8 | 9 | 10| 11| 12|\n      | 13| 14| 15| 16| 17| 18|\n      | 19| 20| 21| 22| 23| 24|\n";
                            break;
                        case 2:
                            drawHall = "    \n________________________\n\\______________________/\n\n   | 1 | 2 | 3 | 4 |\n" +
                                "      | 5 | 6 | 7 | 8 |\n   | 9 | 10| 11| 12|\n   | 13| 14| 15| 16|\n";
                            break;
                        case 3:
                            drawHall = "    \n________________________________\n\\______________________________/\n\n   | 1 | 2 | 3 | 4 | 5 | 6 |\n" +
                                "   | 7 | 8 | 9 | 10| 11| 12|\n   | 13| 14| 15| 16| 17| 18|\n   | 19| 20| 21| 22| 23| 24|\n   | 25| 26| 27| 28| 29| 30|\n";
                            break;
                        default:
                            drawHall = "Hall not found.";
                            break;
                    }
                    return drawHall;
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
                        // on the server that would cause the rollback to fail 
                        Console.WriteLine("Rollback Exception Type: {0}", ex2.GetType());
                        Console.WriteLine("  Message: {0}", ex2.Message);
                    }

                    return drawHall;
                }
            }
        }
    }
}
