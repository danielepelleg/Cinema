﻿using System;
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
            List<Reservation> reserveList = new List<Reservation>();

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
                            Reservation reserve = new Reservation();
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

        /* Check if the place the User wants to buy is a valid one.
         * The place must exist in that hall and must not already be reserved
         *
         * @return true if the place is a valid one, false otherwise
         */
        public bool CheckPlace(int eventCode, int placeNumber)
        {
            // Define two list, the first containing the existing place
            // the second containing the reserved ones
            List<Reservation> reservationsList = new List<Reservation>();
            List<Place> availablePlacesList = new List<Place>();

            // Define two bool value, he first check if the place exists,
            // the second whether it is already reserved
            bool placeExists = false;
            bool placeReserved = false;

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

                    // Fill the availablePlaceList with the available places for the event
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Place p = new Place();
                            p.PlaceNumber = reader.GetInt32(0);
                            p.HallCode = reader.GetInt32(1);
                            availablePlacesList.Add(p);
                        }
                    }

                    // Check if the place exists
                    for (int y = 0; y < availablePlacesList.Count; y++)
                    {
                        if (availablePlacesList[y].PlaceNumber == placeNumber) placeExists = true;
                    }

                    // Reset the parameters
                    command.Parameters.Clear();

                    // Takes the Reservations and fill the list reservationsList with booked/bought places
                    //Prelevo i posti riservati e li inserisco nella lista "lista_posti_occupati"
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "Cinema.VisualizzaPostiRiservati";
                    command.Parameters.Add("@codice_evento", SqlDbType.Int).Value = eventCode;

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Reservation r = new Reservation();
                            r.PlaceNumber = reader.GetInt32(0);
                            r.PrenotationCode = reader.GetInt32(1);
                            reservationsList.Add(r);
                        }
                    }

                    // Attempt to commit the transaction.
                    transaction.Commit();

                    // Check if the place is already in Reservation table
                    for (int z = 0; z < reservationsList.Count; z++)
                    {
                        if (reservationsList[z].PlaceNumber == placeNumber) placeReserved = true;
                    }


                    if (placeExists == true || placeReserved == true)
                    {
                        return false;
                    }
                    else return true;
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

                    return false;
                }
            }
        }

        /*
         * Check Foreign Key bond
         * 
         * @return true if the foreign key exists, false if not
         */
        public bool CheckFK(int value, string valueType)
        {
            // Datbase Connection
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
                    command.CommandText = "SELECT * FROM Cinema." + valueType + ";";

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (value == reader.GetInt32(0)) return true;
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
                        // on the server that would cause the rollback to fail, such as
                        // a closed connection.
                        Console.WriteLine("Rollback Exception Type: {0}", ex2.GetType());
                        Console.WriteLine("  Message: {0}", ex2.Message);
                    }

                    return false;
                }
            }
        }

        
    }
}
