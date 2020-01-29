using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;

namespace WCFDatabaseManager
{
    // NOTA: è possibile utilizzare il comando "Rinomina" del menu "Refactoring" per modificare il nome di classe "ServiceFilm" nel codice e nel file di configurazione contemporaneamente.
    public class ServiceFilm : IServiceFilm
    {
        /*
          * Add a film into the database.
          */
        public bool AddFilm(string title, int year, string direction, int duration, DateTime releaseDate, string genre)
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
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "Cinema.AddNewFilm";
                    command.Parameters.Add("@Titolo", SqlDbType.VarChar).Value = title;
                    command.Parameters.Add("@Anno", SqlDbType.Int).Value = year;
                    command.Parameters.Add("@Regia", SqlDbType.VarChar).Value = direction;
                    command.Parameters.Add("@Durata", SqlDbType.Int).Value = duration;
                    command.Parameters.Add("@Data_Uscita", SqlDbType.DateTime).Value = releaseDate;
                    command.Parameters.Add("@Genere", SqlDbType.VarChar).Value = genre;
                    command.Parameters.Add("@CodiceFilm", SqlDbType.Int).Direction = ParameterDirection.Output;

                    // Initialize a int value to check if the query success
                    var returnParameter = command.Parameters.Add("@ReturnVal", SqlDbType.Int);
                    returnParameter.Direction = ParameterDirection.ReturnValue;

                    // Commit the transaction.
                    transaction.Commit();

                    if (returnParameter.Direction > 0) return true;
                    else
                    {
                        command.Parameters.Clear();
                        throw new Exception("Errore: si è verificato un problema nell'aggiungere un Film nel DB");
                    }
                    /**
                     * // Definisco la variabile t per la creazione della tabella
            \       var t = new TablePrinter("ID", "Titolo", "Anno", "Regia", "Durata", "Data di Uscita", "Genere");
                    Film f1 = new Film();
                    f1.filmCode = Convert.ToInt32(cmd.Parameters["@CodiceFilm"].Value.ToString());
                    f1.title = title;
                    f1.year = year;
                    f1.direction = direction;
                    f1.duration = duration;
                    f1.releaseDate = releaseDate;
                    f1.genre = genre;
                    t.AddRow(f1.filmCode, f1.title, f1.year, f1.direction, f1.duration+"'", f1.releaseDate.ToShortDateString(), f1.genre);
                    */
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
         * Delete a film from the database.
         */
        public bool DeleteFilm(int filmCode)
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
                    // Call the stored procedure to delete the film 
                    // given the filmCode (@primaryKey)
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "Cinema.DeleteFilm";
                    command.Parameters.Add("@CodiceFilm", SqlDbType.Int).Value = filmCode;
                    command.Connection = connection;
                    command.ExecuteNonQuery();

                    // Initialize a int value to check if the query success
                    var returnParameter = command.Parameters.Add("@ReturnVal", SqlDbType.Int);
                    returnParameter.Direction = ParameterDirection.ReturnValue;

                    // Commit the transaction.
                    transaction.Commit();

                    if (returnParameter.Direction > 0) return true;
                    else
                    {
                        command.Parameters.Clear();
                        throw new Exception("Errore: si è verificato un problema nell'aggiungere un Film nel DB");
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

        //Visualizzazione Elenco Film
        public string Visualizzazione_elenco_film()
        {
            var t = new TablePrinter("ID", "Titolo", "Anno", "Regia", "Durata", "Data di Uscita", "Genere");
            SqlTransaction tx = null;
            string elenco = string.Empty;
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
                        command1.CommandText = "SELECT * FROM Cinema.Film;";
                        command1.Transaction = tx;
                        using (SqlDataReader reader = command1.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Film f = new Film();
                                f.FilmCode = reader.GetInt32(0);
                                f.Title = reader.GetString(1);
                                f.Year = reader.GetInt32(2);
                                f.Direction = reader.GetString(3);
                                f.Duration = reader.GetInt32(4);
                                f.ReleaseDate = reader.GetDateTime(5);
                                f.Genre = reader.GetString(6);
                                //elenco = elenco + listFilm[i].VisualizzaFilm() + "\n";
                                t.AddRow(f.FilmCode, f.Title, f.Year, f.Direction, f.Duration + "'", f.ReleaseDate.ToShortDateString(), f.Genre);
                                //i++;
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
            /*if (elenco == string.Empty)
                return "Non sono ancora presenti film all'interno del database. \n";
            else*/
            return t.Print();
        }

        public Film makeFilm()
        {
            Film f = new Film(1, "Avatar", 2010, "sd", 122, DateTime.Now, "fantasy");
            return f;
        }
    }
}
