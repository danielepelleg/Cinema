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
         * 
         * @return true if the operation success, false if not
         */
        public bool AddFilm(string title, int year, string direction, int duration, DateTime releaseDate, string genre) {
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
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "Cinema.AddNewFilm";
                    command.Parameters.Add("@Titolo", SqlDbType.VarChar).Value = title;
                    command.Parameters.Add("@Anno", SqlDbType.Int).Value = year;
                    command.Parameters.Add("@Regia", SqlDbType.VarChar).Value = direction;
                    command.Parameters.Add("@Durata", SqlDbType.Int).Value = duration;
                    command.Parameters.Add("@Data_Uscita", SqlDbType.DateTime).Value = releaseDate;
                    command.Parameters.Add("@Genere", SqlDbType.VarChar).Value = genre;
                    command.Parameters.Add("@CodiceFilm", SqlDbType.Int).Direction = ParameterDirection.Output;
                    command.ExecuteNonQuery();

                    // Initialize a int value to check if the Stored Procedure success
                    var returnParameter = command.Parameters.Add("@ReturnVal", SqlDbType.Int);
                    returnParameter.Direction = ParameterDirection.ReturnValue;

                    // Commit the transaction.
                    transaction.Commit();

                    // If the int value > 0 the Stored Procedure success
                    if (returnParameter.Direction > 0) return true;
                    else  {
                        command.Parameters.Clear();
                        throw new Exception("Errore: si è verificato un problema nell'aggiungere un Film nel DB");
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
         * Delete a film from the database.
         * 
         * @return true if the operation success, false if not
         */
        public bool DeleteFilm(int filmCode) {
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
                    // Call the stored procedure to delete the film 
                    // given the filmCode (@primaryKey)
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "Cinema.DeleteFilm";
                    command.Parameters.Add("@CodiceFilm", SqlDbType.Int).Value = filmCode;
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
                        throw new Exception("Errore: there was a problem removing the Movie!");
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
         * Get the Film of the database given its code
         */
        public Film GetFilm(int filmCode) {
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
                    command.CommandText = "SELECT * FROM Cinema.Film WHERE CodiceFilm = @filmCode ";
                    command.Parameters.Add("@filmCode", SqlDbType.Int).Value = filmCode;

                    Film film = new Film();
                    using (SqlDataReader reader = command.ExecuteReader()) {
                        while (reader.Read()) {
                            film.FilmCode = reader.GetInt32(0);
                            film.Title = reader.GetString(1);
                            film.Year = reader.GetInt32(2);
                            film.Direction = reader.GetString(3);
                            film.Duration = reader.GetInt32(4);
                            film.ReleaseDate = reader.GetDateTime(5);
                            film.Genre = reader.GetString(6);
                        }

                    }
                    // Attempt to commit the transaction.
                    transaction.Commit();

                    return film;
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

                    return new Film();
                }
            }
        }

        /*
         * Get the list containing the Film of the database
         */
        public List<Film> GetFilmList()
        {
            // Database connection
            using (SqlConnection connection = DatabaseHandler.GetConnection()) {
                // Define a new list of Users
                List<Film> filmList = new List<Film>();

                connection.Open();

                // Start a local transaction.
                SqlTransaction transaction = connection.BeginTransaction();
                SqlCommand command = connection.CreateCommand();

                // Must assign both transaction object and connection
                // to Command object for a pending local transaction
                command.Connection = connection;
                command.Transaction = transaction;

                try {
                    command.CommandText = "SELECT * FROM Cinema.Film;";
                    using (SqlDataReader reader = command.ExecuteReader()) {
                        while (reader.Read()) {
                            var filmCode = reader.GetInt32(0);
                            var title = reader.GetString(1);
                            var year = reader.GetInt32(2);
                            var direction = reader.GetString(3);
                            var duration = reader.GetInt32(4);
                            var releaseDate = reader.GetDateTime(5);
                            var genre = reader.GetString(6);

                            filmList.Add(new Film(filmCode, title, year, direction, duration, releaseDate, genre));
                        }
                    }
                    // Attempt to commit the transaction.
                    transaction.Commit();

                    return filmList;
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

                    return new List<Film>() { };
                }
            }
        }
    }
}
