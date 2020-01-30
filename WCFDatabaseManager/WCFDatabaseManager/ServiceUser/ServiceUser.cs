using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;

namespace WCFDatabaseManager
{
    // NOTA: è possibile utilizzare il comando "Rinomina" del menu "Refactoring" per modificare il nome di classe "ServiceUser" nel codice e nel file di configurazione contemporaneamente.
    public class ServiceUser : IServiceUser
    {
        /*
         * Register an Admin or Free User in the database.
         * 
         * @return true if the operation success, false if not
         */
        public bool Registration(bool isAdmin, string username, string password, string name, string surname) {
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
                    switch (isAdmin) {
                        case true:
                            command.CommandText = "INSERT Cinema.Admin VALUES ( @username, @password, @name, @surname)";
                            break;
                        case false:
                            command.CommandText = "INSERT Cinema.UtenteFree VALUES ( @username, @password, @name, @surname)";
                            break;
                    }
                    command.Parameters.Add("@username", SqlDbType.VarChar).Value = username;
                    command.Parameters.Add("@password", SqlDbType.VarChar).Value = password;
                    command.Parameters.Add("@name", SqlDbType.VarChar).Value = name;
                    command.Parameters.Add("@surname", SqlDbType.VarChar).Value = surname;

                    // Commit the transaction.
                    transaction.Commit();

                    int result = command.ExecuteNonQuery();

                    if (result > 0) return true;
                    else {
                        command.Parameters.Clear();
                        throw new Exception("Errore: si è verificato un problema nell'aggiungere una Persona nel DB");
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
         * Sign in an Admin or Free User in the database.
         * 
         * @return true if the operation success, false if not
         */
        public bool Login(bool isAdmin, string username, string password) {
            // Database connection
            using (SqlConnection connection = DatabaseHandler.GetConnection())  {
                connection.Open();

                // Start a local transaction.
                SqlTransaction transaction = connection.BeginTransaction();
                SqlCommand command = connection.CreateCommand();

                // Must assign both transaction object and connection
                // to Command object for a pending local transaction
                command.Connection = connection;
                command.Transaction = transaction;
                try {
                    switch (isAdmin) {
                        case true:
                            command.CommandText = "SELECT * FROM Cinema.Admin WHERE UsernameAdmin = @username AND Password = @password;";
                            break;
                        case false:
                            command.CommandText = "SELECT * FROM Cinema.UtenteFree WHERE UsernameUtenteFree = @username AND Password = @password;";
                            break;
                    }
                    command.Parameters.Add("@username", SqlDbType.VarChar).Value = username;
                    command.Parameters.Add("@password", SqlDbType.VarChar).Value = password;

                    // Attempt to commit the transaction.
                    transaction.Commit();

                    using (SqlDataReader reader = command.ExecuteReader()) {
                        if (reader.Read()) return true;
                        else return false;
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
         * Delete a User from the database
         * 
         * @return true if the operation success, false if not
         */
        public bool DeleteUser(string username) {
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
                    command.CommandText = "DELETE FROM Cinema.Admin WHERE username = @username;";
                            
                    command.Parameters.Add("@username", SqlDbType.VarChar).Value = username;

                    // Commit the transaction.
                    transaction.Commit();

                    int result = command.ExecuteNonQuery();

                    if (result > 0) return true;
                    else {
                        command.Parameters.Clear();
                        throw new Exception("Errore: si è verificato un problema nell'eliminare una Persona dal DB");
                    }
                }
                catch (SqlException ex)  { 
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
         * Get the User of the database given his username
         */
        public User GetUser(string username) {
            using (SqlConnection connection = DatabaseHandler.GetConnection())  {
                connection.Open();

                // Start a local transaction.
                SqlTransaction transaction = connection.BeginTransaction();
                SqlCommand command = connection.CreateCommand();

                // Must assign both transaction object and connection
                // to Command object for a pending local transaction
                command.Connection = connection;
                command.Transaction = transaction;

                try {

                    command.CommandText = "SELECT * FROM Cinema.UtenteFree WHERE username = @username ";
                    command.Parameters.Add("@username", SqlDbType.VarChar).Value = username;

                    User user = new User();
                    using (SqlDataReader reader = command.ExecuteReader()) {
                        while (reader.Read()) {
                            user.Username = reader.GetString(0);
                            user.Name = reader.GetString(2);
                            user.Surname = reader.GetString(3);
                        }
                        
                    }
                    // Attempt to commit the transaction.
                    transaction.Commit();

                    return user;
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

                    return new User();
                }
            }
        }

 
        /*
         * Get the list containing the Users of the database
         */
        public List<User> GetUsersList() {
            // Database connection
            using (SqlConnection connection = DatabaseHandler.GetConnection()) {
                // Define a new list of Users
                List<User> usersList = new List<User>();

                connection.Open();

                // Start a local transaction.
                SqlTransaction transaction = connection.BeginTransaction();
                SqlCommand command = connection.CreateCommand();

                // Must assign both transaction object and connection
                // to Command object for a pending local transaction
                command.Connection = connection;
                command.Transaction = transaction;

                try {
                    command.CommandText = "SELECT * FROM Cinema.UtenteFree;";
                    using (SqlDataReader reader = command.ExecuteReader()) {
                        while (reader.Read()) {
                            var username = reader.GetString(0);
                            var name = reader.GetString(2);
                            var surname = reader.GetString(3);

                            usersList.Add(new User(username, name, surname));
                        }
                    }
                    // Attempt to commit the transaction.
                    transaction.Commit();

                    return usersList;
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

                    return new List<User>() { };
                }
            }
        }
    }
}
