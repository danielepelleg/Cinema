using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;

namespace WCFDatabaseManager
{
    // NOTA: è possibile utilizzare il comando "Rinomina" del menu "Refactoring" per modificare il nome di classe "ServiceUser" nel codice e nel file di configurazione contemporaneamente.
    public class ServiceUser : IServiceUser
    {

        List<User> listUtentiFree = new List<User>();
        User u1 = new User();

        /*
         * Register an Admin or Free User in the database.
         */
        public bool Registration(bool isAdmin, string username, string password, string name, string surname)
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
                try
                {
                    switch (isAdmin)
                    {
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
                    else
                    {
                        command.Parameters.Clear();
                        throw new Exception("Errore: si è verificato un problema nell'aggiungere una Persona nel DB");
                    }
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
         * Sign in an Admin or Free User in the database.
         */
        public bool Login(bool isAdmin, string username, string password)
        {
            // Database connection
            using (SqlConnection connection = 
DatabaseHandler.GetConnection())
            {
                connection.Open();

                // Start a local transaction.
                SqlTransaction transaction = connection.BeginTransaction();
                SqlCommand command = connection.CreateCommand();

                // Must assign both transaction object and connection
                // to Command object for a pending local transaction
                command.Connection = connection;
                command.Transaction = transaction;
                try
                {
                    switch (isAdmin)
                    {
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

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read()) return true;
                        else return false;
                    }
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

        //Visualizzazione Utenti
        public string Visualizzazione_elenco_UtentiFree()
        {
            SqlTransaction tx = null;
            string elenco = string.Empty;
            int i = 0; //variabile di incremento per la lista
            try
            {
                // Connessione al DB Cinema
                using (SqlConnection conn = DatabaseHandler.GetConnection())
                {
                    listUtentiFree.Clear(); //pulisco la lista di film in modo tale da riempirla nuovamente e non avere problemi se sono avvenute modifiche
                    conn.Open();
                    tx = conn.BeginTransaction();
                    using (SqlCommand command1 = conn.CreateCommand())
                    {
                        command1.CommandText = "SELECT * FROM Cinema.UtenteFree;";
                        command1.Transaction = tx;
                        using (SqlDataReader reader = command1.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                User u = new User();
                                listUtentiFree.Add(u);
                                listUtentiFree[i].Username = reader.GetString(0);
                                listUtentiFree[i].Name = reader.GetString(2);
                                listUtentiFree[i].Surname = reader.GetString(3);
                                elenco = elenco + listUtentiFree[i].showUser() + "\n";
                                i++;
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
            if (elenco == string.Empty)
                return "Non sono ancora presenti utenti censiti all'interno del database. \n";
            else
                return elenco;
        }

    }
}
