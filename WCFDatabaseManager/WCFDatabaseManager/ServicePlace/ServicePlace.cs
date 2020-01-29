using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;

namespace WCFDatabaseManager
{
    // NOTA: è possibile utilizzare il comando "Rinomina" del menu "Refactoring" per modificare il nome di classe "ServicePlace" nel codice e nel file di configurazione contemporaneamente.
    public class ServicePlace : IServicePlace
    {
        //Visualizzazione Elenco Posti disponibili per ogni evento
        public string VisualizzazionePostiDisponibili(int codice_evento)
        {
            List<Place> lista_posti_disponibili = new List<Place>();
            List<Reserve> lista_riservati = new List<Reserve>();
            SqlTransaction tx = null;
            SqlTransaction tx1 = null;
            string elenco = string.Empty;
            int i = 0; //variabile di incremento per la lista
            int h = 0; //variabile di incremento per la lista
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
                                lista_riservati[h].placeNumber = reader.GetInt32(0);
                                lista_riservati[h].prenotationCode = reader.GetInt32(1);
                                h++;
                            }
                        }
                    }

                    for (int x = lista_posti_disponibili.Count - 1; x >= 0; x--)
                    {
                        for (int y = 0; y < lista_riservati.Count; y++)
                        {
                            if (lista_posti_disponibili[x].PlaceNumber == lista_riservati[y].placeNumber)
                            {
                                lista_posti_disponibili.Remove(lista_posti_disponibili[x]); //Se trovo due elementi uguali lo rimuovo dalla lista dei posti disponibili
                                break; //blocco il ciclo
                            }
                        }
                    }
                    for (int z = 0; z < lista_posti_disponibili.Count; z++)
                    {
                        elenco = elenco + lista_posti_disponibili[z].PlaceNumber + " ";
                    }

                    tx1.Commit();
                    conn.Close();
                }
            }
            catch (SqlException ex)
            {
                return string.Format("Connessione non riuscita: {0}", ex.ToString());
            }

            return elenco;
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
                                lista_riservati[h].placeNumber = reader.GetInt32(0);
                                lista_riservati[h].prenotationCode = reader.GetInt32(1);
                                h++;
                            }
                        }
                    }
                    for (int z = 0; z < lista_riservati.Count; z++)
                    {
                        if (lista_riservati[z].placeNumber == numero_posto)
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
