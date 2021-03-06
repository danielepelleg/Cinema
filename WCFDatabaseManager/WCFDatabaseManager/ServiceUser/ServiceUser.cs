﻿using System;
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

                // Assign both transaction object and connection to Command object
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

                // Assign both transaction object and connection to Command object
                command.Connection = connection;
                command.Transaction = transaction;
                try {
                    switch (isAdmin) {
                        case true:
                            command.CommandText = "SELECT * FROM Cinema.Admin WHERE UsernameAdmin = @username AND Password = @password";
                            break;
                        case false:
                            command.CommandText = "SELECT * FROM Cinema.UtenteFree WHERE UsernameUtenteFree = @username AND Password = @password";
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
                        // on the server that would cause the rollback to fail 
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

                // Assign both transaction object and connection to Command object
                command.Connection = connection;
                command.Transaction = transaction;
                try {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "Cinema.DeleteUser";
                    command.Parameters.Add("@Username", SqlDbType.VarChar).Value = username;
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
                        return false;
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
                        // on the server that would cause the rollback to fail 
                        Console.WriteLine("Rollback Exception Type: {0}", ex2.GetType());
                        Console.WriteLine("  Message: {0}", ex2.Message);
                    }

                    return false;
                }
            }
        }

        /*
         * Edit a User in the database
         * 
         * @return true if the operation success, false if not
         */
        public bool EditUser(string oldUsername, string newUsername, string newPassword, string newName, string newSurname)
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
                    // Update an User
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "Cinema.EditUser";
                    command.Parameters.Add("@OldUsername", SqlDbType.VarChar).Value = oldUsername;
                    command.Parameters.Add("@NewUsername", SqlDbType.VarChar).Value = newUsername;
                    command.Parameters.Add("@NewPassword", SqlDbType.VarChar).Value = newPassword;
                    command.Parameters.Add("@NewNome", SqlDbType.VarChar).Value = newName;
                    command.Parameters.Add("@NewCognome", SqlDbType.VarChar).Value = newSurname;
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
         * Get the User of the database given his username
         */
        public User GetUser(bool isAdmin, string username) {
            using (SqlConnection connection = DatabaseHandler.GetConnection())  {
                connection.Open();

                // Start a local transaction.
                SqlTransaction transaction = connection.BeginTransaction();
                SqlCommand command = connection.CreateCommand();

                // Assign both transaction object and connection to Command object
                command.Connection = connection;
                command.Transaction = transaction;
                try {
                    switch (isAdmin)
                    {
                        case true:
                            command.CommandText = "SELECT * FROM Cinema.Admin WHERE Cinema.Admin.UsernameAdmin = @username";
                            break;
                        case false:
                            command.CommandText = "SELECT * FROM Cinema.UtenteFree WHERE Cinema.UtenteFree.UsernameUtenteFree = @username";
                            break;
                    }
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
                        // on the server that would cause the rollback to fail 
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

                // Assign both transaction object and connection to Command object
                command.Connection = connection;
                command.Transaction = transaction;

                try {
                    command.CommandText = "SELECT * FROM Cinema.UtenteFree;";
                    using (SqlDataReader reader = command.ExecuteReader()) {
                        while (reader.Read()) {
                            var username = reader.GetString(0);
                            var hashedPassword = reader.GetString(1);
                            var name = reader.GetString(2);
                            var surname = reader.GetString(3);
                            

                            usersList.Add(new User(username, hashedPassword, name, surname));
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
                        // on the server that would cause the rollback to fail 
                        Console.WriteLine("Rollback Exception Type: {0}", ex2.GetType());
                        Console.WriteLine("  Message: {0}", ex2.Message);
                    }

                    return new List<User>() { };
                }
            }
        }

        /*
         * Get the list containing the User who has signed up for a subscription in the Cinema
         */
        public List<User> GetSubscribersList()
        {
            // Database connection
            using (SqlConnection connection = DatabaseHandler.GetConnection())
            {
                // Define a new list of Users
                List<User> subscribersList = new List<User>();

                connection.Open();

                // Start a local transaction.
                SqlTransaction transaction = connection.BeginTransaction();
                SqlCommand command = connection.CreateCommand();

                // Assign both transaction object and connection to Command object
                command.Connection = connection;
                command.Transaction = transaction;

                try
                {
                    command.CommandText = "SELECT Cinema.Abbonamento.Username, Cinema.UtenteFree.Nome, Cinema.UtenteFree.Cognome " +
                        "FROM Cinema.Abbonamento, Cinema.UtenteFree WHERE Cinema.Abbonamento.Username = Cinema.UtenteFree.UsernameUtenteFree;";
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var username = reader.GetString(0);
                            var name = reader.GetString(1);
                            var surname = reader.GetString(2);


                            subscribersList.Add(new User(username, name, surname));
                        }
                    }
                    // Attempt to commit the transaction.
                    transaction.Commit();

                    return subscribersList;
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

                    return new List<User>() { };
                }
            }
        }

        /*
         * Check Foreign Key bond
         * 
         * @return true if the foreign key exists, false if not
         */
        public bool CheckStringFK(string value, string valueType)
        {
            // Datbase Connection
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
                    command.CommandText = "SELECT * FROM Cinema." + valueType + ";";

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (value == reader.GetString(0)) return true;
                        }
                        return false;
                    }
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

                    return false;
                }
            }
        }

        /*
         * Function that allows the server to check the connection with the Database Manager
         * 
         */
        public void CheckConnection() { }
    }
}
