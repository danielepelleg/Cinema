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
         * Add an event into the database
         * 
         * @return true if the operation success, false if not
         */
        public bool AddEvent(string usernameAdmin, DateTime dateTime, int filmCode, int hallCode, decimal price) {
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
                    // Check if in the hall is already present a film proiection in the same dateTime.
                    // If so, return false, add the event otherwise.
                    command.CommandText = "SELECT * FROM Cinema.Evento;";

                    using (SqlDataReader reader = command.ExecuteReader()) {
                        while (reader.Read()) {
                            if (dateTime == reader.GetDateTime(1) && hallCode == reader.GetInt32(4)) {
                                return false;
                            }
                        }
                    }
                    // Reset the parameters
                    command.Parameters.Clear();

                    // Insert the event in the database
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "Cinema.AddNewEvento";
                    command.Parameters.Add("@DataOra", SqlDbType.DateTime).Value = dateTime;
                    command.Parameters.Add("@Codice_Film", SqlDbType.Int).Value = filmCode;
                    command.Parameters.Add("@Codice_Sala", SqlDbType.Int).Value = hallCode;
                    command.Parameters.Add("@Username_Admin", SqlDbType.VarChar).Value = usernameAdmin;
                    command.Parameters.Add("@Prezzo", SqlDbType.Decimal).Value = price;
                    command.Parameters.Add("@CodiceEvento", SqlDbType.Int).Direction = ParameterDirection.Output;
                    command.ExecuteNonQuery();

                    // Initialize a int value to check if the Stored Procedure success
                    var returnParameter = command.Parameters.Add("@ReturnVal", SqlDbType.Int);
                    returnParameter.Direction = ParameterDirection.ReturnValue;

                    // Commit the transaction
                    transaction.Commit();

                    // If the int value > 0 the Stored Procedure success
                    if (returnParameter.Direction > 0) return true;
                    else {
                        command.Parameters.Clear();
                        throw new Exception("Error: There is a problem adding the Event into the DB!");
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

        /*
         * Delete an event from the database
         * 
         * @return true if the operation success, false if not
         */
        public bool DeleteEvent(int eventCode) {

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
                    // Insert an Event
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "Cinema.DeleteEvento";
                    command.Parameters.Add("@CodiceEvento", SqlDbType.Int).Value = eventCode;
                    command.ExecuteNonQuery();

                    // Initialize a int value to check if the Stored Procedure success
                    var returnParameter = command.Parameters.Add("@ReturnVal", SqlDbType.Int);
                    returnParameter.Direction = ParameterDirection.ReturnValue;

                    // Commit the transaction.
                    transaction.Commit();

                    // If the int value > 0 the Stored Procedure success
                    if (returnParameter.Direction > 0) return true;
                    else {
                        command.Parameters.Clear();
                        throw new Exception("Errore: si è verificato un problema nell'eliminare un Evento dal DB");
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

        /*
         * Get the Event of the database given its code
         */
        public Event GetEvent(int eventCode) {
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

                    command.CommandText = "SELECT * FROM Cinema.Evento WHERE CodiceEvento = @eventCode ";
                    command.Parameters.Add("@eventCode", SqlDbType.Int).Value = eventCode;

                    Event _event = new Event();
                    using (SqlDataReader reader = command.ExecuteReader()) {
                        while (reader.Read()) {
                            _event.EventCode = reader.GetInt32(0);
                            _event.DateTime = reader.GetDateTime(1);
                            _event.FilmCode = reader.GetInt32(2);
                            _event.HallCode = reader.GetInt32(3);
                            _event.UsernameAdmin = reader.GetString(4);
                            _event.Price = reader.GetDecimal(5);
                        }

                    }
                    // Attempt to commit the transaction.
                    transaction.Commit();

                    return _event;
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

                    return new Event();
                }
            }
        }

        /*
         * Get the list containing the Events of the database
         */
        public List<Event> GetEventsList() {
            // Database connection
            using (SqlConnection connection = DatabaseHandler.GetConnection()) {
                // Define a new list of Users
                List<Event> eventsList = new List<Event>();

                connection.Open();

                // Start a local transaction.
                SqlTransaction transaction = connection.BeginTransaction();
                SqlCommand command = connection.CreateCommand();

                // Must assign both transaction object and connection
                // to Command object for a pending local transaction
                command.Connection = connection;
                command.Transaction = transaction;

                try {
                    command.CommandText = "SELECT * FROM Cinema.Evento;";
                    using (SqlDataReader reader = command.ExecuteReader()) {
                        while (reader.Read()) {
                            var eventCode = reader.GetInt32(0);
                            var dateTime = reader.GetDateTime(1);
                            var price = reader.GetDecimal(2);
                            var filmCode = reader.GetInt32(3);
                            var hallCode = reader.GetInt32(4);
                            var usernameAdmin = reader.GetString(5);
                            
                            eventsList.Add(new Event(eventCode, dateTime, filmCode, hallCode, usernameAdmin, price));
                        }
                    }
                    // Attempt to commit the transaction.
                    transaction.Commit();

                    return eventsList;
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

                    return new List<Event>() { };
                }
            }
        }
    }
}
