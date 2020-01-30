using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;

namespace WCFDatabaseManager
{
    // NOTA: è possibile utilizzare il comando "Rinomina" del menu "Refactoring" per modificare il nome di classe "ServicePlace" nel codice e nel file di configurazione contemporaneamente.
    public class ServicePlace : IServicePlace
    {
        /*
         * Get the list containing the avilable Places for an Event of the database
         */
        public List<Place> GetAvailablePlacesList(int eventCode)
        {
            // Define two list: one for available places, 
            //  the second for the booked ones
            List<Place> availablePlacesList = new List<Place>();
            List<Reserve> reserveList = new List<Reserve>();

            // Connessione al DB Cinema
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
                    command.CommandText = "Cinema.VisualizzaPostiSala";
                    command.Parameters.Add("@codice_evento", SqlDbType.Int).Value = eventCode;
                    command.ExecuteNonQuery();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Place place = new Place();
                            place.PlaceNumber = reader.GetInt32(0);
                            place.HallCode = reader.GetInt32(1);
                            availablePlacesList.Add(place);
                        }
                    }
                    // Reset the parameters
                    command.Parameters.Clear();

                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "Cinema.VisualizzaPostiRiservati";
                    command.Parameters.Add("@codice_evento", SqlDbType.Int).Value = eventCode;
                    command.ExecuteNonQuery();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Reserve reserve = new Reserve();
                            reserve.PlaceNumber = reader.GetInt32(0);
                            reserve.PrenotationCode = reader.GetInt32(1);
                            reserveList.Add(reserve);
                        }
                    }

                    // Attempt to commit the transaction.
                    transaction.Commit();

                    for (int x = availablePlacesList.Count - 1; x >= 0; x--)
                    {
                        for (int y = 0; y < reserveList.Count; y++)
                        {
                            if (availablePlacesList[x].PlaceNumber == reserveList[y].PlaceNumber)
                            {
                                // If an element is in both the lists remove it from the availablePlaceList
                                availablePlacesList.Remove(availablePlacesList[x]);
                                break; // Stop the cycle
                            }
                        }
                    }

                    return availablePlacesList;
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
                        // on the server that would cause the rollback to fail, such as
                        // a closed connection.
                        Console.WriteLine("Rollback Exception Type: {0}", ex2.GetType());
                        Console.WriteLine("  Message: {0}", ex2.Message);
                    }

                    return new List<Place>() { };
                }
            }
        }

        //Controllo vincolo Foreign key
        public string ControlloFK(int valore, string value_type)
        {
            string result = string.Empty;
            try
            {
                SqlTransaction tx = null;
                // Connessione al DB Cinema
                using (SqlConnection conn = DatabaseHandler.GetConnection())
                {
                    conn.Open();
                    tx = conn.BeginTransaction();
                    using (SqlCommand command1 = conn.CreateCommand())
                    {
                        command1.CommandText = "SELECT * FROM Cinema." + value_type + ";";
                        command1.Transaction = tx;
                        using (SqlDataReader reader = command1.ExecuteReader())
                        {
                            {
                                bool found = false;
                                while (reader.Read())
                                {
                                    if (valore == reader.GetInt32(0))
                                    {
                                        found = true;
                                        result = "Trovato";
                                    }
                                }
                                if (found == false)
                                    result = "Non Presente";
                            }
                        }
                    }
                    tx.Commit();
                }
            }
            catch (Exception ex)
            {
                result = string.Format("{0}", ex.ToString());
            }
            return result;
        }

        //Controllo che il posto inserito dall'utente non sia gia stato prenotato e quindi sia già presente nella tabella Riserva del DB
        //Controllo inoltre che il numero posto inserito dall'utente non sia presente all'interno della sala in cui avverrà la presentazione
        public string VerificaPosto(int codice_evento, int numero_posto)
        {
            string result = string.Empty;
            List<Reserve> lista_riservati = new List<Reserve>();
            SqlTransaction tx1 = null;
            List<Place> lista_posti_disponibili = new List<Place>();
            SqlTransaction tx = null;
            bool condizione1 = true;
            bool condizione2 = false;
            int i = 0;
            int h = 0;
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
                        command1.CommandText = "Cinema.VisualizzaPostiSala";
                        command1.Parameters.Add("@codice_evento", SqlDbType.Int).Value = codice_evento;
                        command1.Connection = conn;
                        command1.ExecuteNonQuery();
                        using (SqlDataReader reader = command1.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Place p = new Place();
                                lista_posti_disponibili.Add(p);
                                lista_posti_disponibili[i].PlaceNumber = reader.GetInt32(0);
                                lista_posti_disponibili[i].HallCode = reader.GetInt32(1);
                                i++;
                            }
                        }
                    }
                    for (int y = 0; y < lista_posti_disponibili.Count; y++)
                    {
                        if (lista_posti_disponibili[y].PlaceNumber == numero_posto) condizione1 = false;
                    }
                    tx.Commit();
                    //Prelevo i posti riservati e li inserisco nella lista "lista_posti_occupati"
                    tx1 = conn.BeginTransaction();
                    using (SqlCommand command = conn.CreateCommand())
                    {
                        command.Transaction = tx1;
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "Cinema.VisualizzaPostiRiservati";
                        command.Parameters.Add("@codice_evento", SqlDbType.Int).Value = codice_evento;
                        command.Connection = conn;
                        command.ExecuteNonQuery();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Reserve r = new Reserve();
                                lista_riservati.Add(r);
                                lista_riservati[h].PlaceNumber = reader.GetInt32(0);
                                lista_riservati[h].PrenotationCode = reader.GetInt32(1);
                                h++;
                            }
                        }
                    }
                    for (int z = 0; z < lista_riservati.Count; z++)
                    {
                        if (lista_riservati[z].PlaceNumber == numero_posto)
                        {
                            condizione2 = true;
                        }
                    }
                    tx1.Commit();
                    if (condizione1 == true || condizione2 == true)
                    {
                        result = "Valore non valido";
                    }
                }
            }
            catch (SqlException ex)
            {
                return string.Format("Connessione non riuscita: {0}", ex.ToString());
            }
            return result;
        }
    }
}
