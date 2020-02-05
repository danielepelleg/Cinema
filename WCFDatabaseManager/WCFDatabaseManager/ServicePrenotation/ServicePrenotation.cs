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
         * 
         * @return true if the operation success, false if not
         */
        public bool AddPrenotation(DateTime dateTime, string usernameUser, int eventCode, int placeNumber) {
            using (SqlConnection connection = DatabaseHandler.GetConnection()) {
                connection.Open();

                // Start a local transaction.
                SqlTransaction transaction = connection.BeginTransaction();
                SqlCommand command = connection.CreateCommand();

                // Assign both transaction object and connection to Command object
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
                    Prenotation prenotation = new Prenotation(Convert.ToInt32(command.Parameters["@CodicePrenotazione"].Value), 
                        dateTime, usernameUser, eventCode);

                    // Initialize a int value to check if the Stored Procedure success
                    var returnParameter1 = command.Parameters.Add("@ReturnVal", SqlDbType.Int);
                    returnParameter1.Direction = ParameterDirection.ReturnValue;

                    //Reset the parameters
                    command.Parameters.Clear();

                    // Insert data in Reserve table in the database
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "Cinema.AddNewRiserva";
                    command.Parameters.Add("@Numero_Posto", SqlDbType.Int).Value = placeNumber;
                    command.Parameters.Add("@Codice_Prenotazione", SqlDbType.Int).Value = prenotation.PrenotationCode;
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
                        //throw new Exception("Errore: si è verificato un problema nell'aggiungere un Evento nel DB");
                        return false;
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
                        // on the server that would cause the rollback to fail 
                        Console.WriteLine("Rollback Exception Type: {0}", ex2.GetType());
                        Console.WriteLine("  Message: {0}", ex2.Message);
                    }

                    return false;
                }
            }

        }

        /*
         * Delete a Prenotation and the Reservation record from the database.
         * 
         * @return true if the operation success, false if not
         */
        public bool DeletePrenotation(int prenotationCode)
        {

            using (SqlConnection connection = DatabaseHandler.GetConnection())
            {
                connection.Open();
                // Start a local transaction.
                SqlTransaction transaction = connection.BeginTransaction();
                SqlCommand command = connection.CreateCommand();

                // Assign both transaction object and connection to Command object
                command.Connection = connection;
                command.Transaction = transaction;

                try
                {
                    // Insert an Event
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "Cinema.DeletePrenotazione";
                    command.Parameters.Add("@CodicePrenotazione", SqlDbType.Int).Value = prenotationCode;
                    command.ExecuteNonQuery();

                    // Initialize a int value to check if the Stored Procedure success
                    var returnParameter = command.Parameters.Add("@ReturnVal", SqlDbType.Int);
                    returnParameter.Direction = ParameterDirection.ReturnValue;

                    // Commit the transaction.
                    transaction.Commit();

                    // If the int value > 0 the Stored Procedure success
                    if (returnParameter.Direction > 0) return true;
                    else
                    {
                        command.Parameters.Clear();
                        //throw new Exception("Errore: si è verificato un problema nell'eliminare una Prenotazione e una Riserva dal DB");
                        return false;
                    }

                }
                catch (SqlException ex)
                {

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
                        // on the server that would cause the rollback to fail 
                        Console.WriteLine("Rollback Exception Type: {0}", ex2.GetType());
                        Console.WriteLine("  Message: {0}", ex2.Message);
                    }

                    return false;
                }
            }

        }

        /*
         * Get the list containing the Prenotations of the database
         */
        public List<Prenotation> GetPrenotationsList()
        {
            // Database connection
            using (SqlConnection connection = DatabaseHandler.GetConnection())
            {
                // Define a new list of Users
                List<Prenotation> prenotationsList = new List<Prenotation>();

                connection.Open();

                // Start a local transaction.
                SqlTransaction transaction = connection.BeginTransaction();
                SqlCommand command = connection.CreateCommand();

                // Assign both transaction object and connection to Command object
                command.Connection = connection;
                command.Transaction = transaction;

                try
                {
                    command.CommandText = "SELECT * FROM Cinema.Prenotazione;";
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var prenotationCode = reader.GetInt32(0);
                            var dateTime = reader.GetDateTime(1);
                            var username = reader.GetString(2);
                            var eventCode = reader.GetInt32(3);

                            prenotationsList.Add(new Prenotation(prenotationCode, dateTime, username, eventCode));
                        }
                    }
                    // Attempt to commit the transaction.
                    transaction.Commit();

                    return prenotationsList;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Commit Exception Type: {0}", ex.GetType());
                    Console.WriteLine("  Message: {0}", ex.Message);

                    // Attempt to roll back the transaction.
                    try
                    {
                        transaction.Rollback();
                    }
                    catch (Exception ex2)
                    {
                        // This catch block will handle any errors that may have occurred
                        // on the server that would cause the rollback to fail 
                        Console.WriteLine("Rollback Exception Type: {0}", ex2.GetType());
                        Console.WriteLine("  Message: {0}", ex2.Message);
                    }

                    return new List<Prenotation>() { };
                }
            }
        }

        /*
         * Get the list containing the tickets of a user
         */
        public List<Ticket> GetTicketsList(string username)
        {
            // Database connection
            using (SqlConnection connection = DatabaseHandler.GetConnection())
            {

                // Define a new list of Users
                List<Ticket> ticketsList = new List<Ticket>();

                connection.Open();

                // Start a local transaction.
                SqlTransaction transaction = connection.BeginTransaction();
                SqlCommand command = connection.CreateCommand();

                // Assign both transaction object and connection to Command object
                command.Connection = connection;
                command.Transaction = transaction;

                try
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "Cinema.VisualizzaPrenotazione";
                    command.Parameters.Add("@Username", SqlDbType.VarChar).Value = username;

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Prenotation p = new Prenotation();
                            p.PrenotationCode = reader.GetInt32(0);
                            p.DateTime = reader.GetDateTime(1);
                            p.UsernameUser = reader.GetString(2);
                            p.EventCode = reader.GetInt32(3);

                            Event e = new Event();
                            e.EventCode = reader.GetInt32(3);
                            e.DateTime = reader.GetDateTime(5);
                            e.HallCode = reader.GetInt32(6);
                            e.Price = reader.GetDecimal(7);

                            Film f = new Film();
                            f.Title = reader.GetString(4);

                            Reservation r = new Reservation();
                            r.PlaceNumber = reader.GetInt32(8);

                            ticketsList.Add(new Ticket(p, e, f, r));
                        }
                    }
                    // Attempt to commit the transaction.
                    transaction.Commit();

                    return ticketsList;
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

                    return new List<Ticket>() { };
                }
            }
        }
    }
}
