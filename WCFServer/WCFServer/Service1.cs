using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;

namespace WCFServer
{
    // NOTA: è possibile utilizzare il comando "Rinomina" del menu "Refactoring" per modificare il nome di classe "Service1" nel codice e nel file di configurazione contemporaneamente.
    public class Service1 : IService1
    {
        List<User> listUtentiFree = new List<User>();
        List<Prenotation> listPrenotazioni = new List<Prenotation>();
        Admin a1 = new Admin();
        User u1 = new User();
        Event e1 = new Event();

        Prenotation p1 = new Prenotation();
        
        /*
         * Add an event into the database.
         */
        public bool AddEvent(string usernameAdmin, DateTime dateTime, int filmCode, int hallCode, decimal price)
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

                command.CommandText = "SELECT * FROM Cinema.Evento;";
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read()) {
                    if (dateTime == reader.GetDateTime(1) && hallCode == reader.GetInt32(4)){
                        return false;
                        //return "\nNella sala inserita è gia presente una proiezione di un film nella data e all'orario che hai appena inserito!\nImpossibile effettuare l'inserimento! Riprovare!\n";
                    }
                }
                reader.Close();
                // Commit the transaction
                transaction.Commit();

                // Start a local transaction.
                SqlTransaction transaction2 = connection.BeginTransaction();
                SqlCommand command2 = connection.CreateCommand();

                // Must assign both transaction object and connection
                // to Command object for a pending local transaction
                command2.Connection = connection;
                command2.Transaction = transaction2;

                try
                {
                    // Insert the event in the database
                    command2.CommandType = CommandType.StoredProcedure;
                    command2.CommandText = "Cinema.AddNewEvento";
                    command2.Parameters.Add("@DataOra", SqlDbType.DateTime).Value = dateTime;
                    command2.Parameters.Add("@Codice_Film", SqlDbType.Int).Value = filmCode;
                    command2.Parameters.Add("@Codice_Sala", SqlDbType.Int).Value = hallCode;
                    command2.Parameters.Add("@Username_Admin", SqlDbType.VarChar).Value = usernameAdmin;
                    command2.Parameters.Add("@Prezzo", SqlDbType.Decimal).Value = price;
                    command2.Parameters.Add("@CodiceEvento", SqlDbType.Int).Direction = ParameterDirection.Output;
                    command2.Connection = connection;
                    command2.ExecuteNonQuery();

                    // Initialize a int value to check if the query success
                    var returnParameter = command.Parameters.Add("@ReturnVal", SqlDbType.Int);
                    returnParameter.Direction = ParameterDirection.ReturnValue;

                    // Commit the transaction
                    transaction2.Commit();

                    if (returnParameter.Direction > 0) return true;
                    else
                    {
                        command.Parameters.Clear();
                        throw new Exception("Errore: si è verificato un problema nell'aggiungere un Film nel DB");
                    }
                    /*

                    // CodiceEvento is an IDENTITY value from the database.
                    e1.eventCode = Convert.ToInt32(command2.Parameters["@CodiceEvento"].Value.ToString());
                    e1.dateTime = data_e_ora;
                    e1.filmCode = codice_film;
                    e1.hallCode = codice_sala;
                    e1.usernameAdmin = usernameadmin;
                    e1.price = prezzo;
                    
                    var t = new TablePrinter("ID EVENTO", "Data e Ora", "Sala", "ID FILM", "Prezzo");
                    t.AddRow(e1.eventCode, e1.dateTime.ToShortDateString() + " " + e1.dateTime.ToShortTimeString(), e1.hallCode, e1.filmCode, e1.price + "€");
                    */
                    // Commit the transaction.

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
         * Delete an event from the database.
         */
        public string CancellazioneEvento(int codiceevento)
        {

            using (SqlConnection connection = DatabaseHandler.GetConnection())
            {
                connection.Open();

                // Start a local transaction.
                SqlTransaction sqlTran = connection.BeginTransaction();

                // Enlist a command in the current transaction.
                SqlCommand cmd = connection.CreateCommand();
                cmd.Transaction = sqlTran;


                try
                {
                    // Esecuzione Inserimento
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Cinema.DeleteEvento";
                    cmd.Parameters.Add("@CodiceEvento", SqlDbType.Int).Value = codiceevento;
                    cmd.Connection = connection;
                    cmd.ExecuteNonQuery();
                    // Commit the transaction.
                    sqlTran.Commit();
                    connection.Close(); // Chiusura Connessione
                    return "CANCELLAZIONE DELL'EVENTO DALLA PROGRAMMAZIONE AVVENUTA CON SUCCESSO\n";

                }
                catch (SqlException ex)
                {
                    return ex.Message;
                }
            }

        }

        //Inserimento Prenotazioni
        public string InserimentoPrenotazione(DateTime dataora, string username_utentefree, int codice_evento, int numero_posto)
        {
            var t = new TablePrinter("Numero Prenotazione", "ID Evento", "Username", "Data e Ora Prenotazione");
            using (SqlConnection connection = DatabaseHandler.GetConnection())
            {
                connection.Open();
                // Start a local transaction.
                SqlTransaction sqlTran = connection.BeginTransaction();

                // Enlist a command in the current transaction.
                SqlCommand cmd = connection.CreateCommand();
                cmd.Transaction = sqlTran;

                try
                {
                    // Esecuzione Inserimento
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Cinema.AddNewPrenotazione";
                    cmd.Parameters.Add("@DataOra", SqlDbType.DateTime).Value = dataora;
                    cmd.Parameters.Add("@Username_UtenteFree", SqlDbType.VarChar).Value = username_utentefree;
                    cmd.Parameters.Add("@Codice_Evento", SqlDbType.Int).Value = codice_evento;
                    cmd.Parameters.Add("@CodicePrenotazione", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Connection = connection;
                    cmd.ExecuteNonQuery();
                    // CodiceEvento is an IDENTITY value from the database.
                    p1.prenotationCode = Convert.ToInt32(cmd.Parameters["@CodicePrenotazione"].Value.ToString());
                    p1.dateTime = dataora;
                    p1.usernameUser = username_utentefree;
                    p1.eventCode = codice_evento;
                    t.AddRow(p1.prenotationCode, p1.eventCode, p1.usernameUser, p1.dateTime.ToShortDateString() + " " + p1.dateTime.ToShortTimeString());
                    // Commit the transaction.
                    sqlTran.Commit();
                    //Inserisco i valori nella tabella Riserva
                    SqlTransaction sqlTran1 = connection.BeginTransaction();
                    SqlCommand cmd1 = connection.CreateCommand();
                    cmd1.Transaction = sqlTran1;
                    cmd1.CommandType = CommandType.StoredProcedure;
                    cmd1.CommandText = "Cinema.AddNewRiserva";
                    cmd1.Parameters.Add("@numero_posto", SqlDbType.Int).Value = numero_posto;
                    cmd1.Parameters.Add("@codice_prenotazione", SqlDbType.Int).Value = p1.prenotationCode;
                    cmd1.Connection = connection;
                    cmd1.ExecuteNonQuery();
                    sqlTran1.Commit();
                    return "PRENOTAZIONE EFFETTUATA CON SUCCESSO!\nBiglietto acquistato: \n" + t.Print() + "\n";
                }
                catch (SqlException ex)
                {
                    return string.Format("{0}", ex.ToString());
                }
            }

        }  

        //Visualizzazione Elenco Eventi
        public string Visualizzazione_elenco_eventi()
        {
            var t = new TablePrinter("ID EVENTO", "Data e Ora", "Sala", "ID FILM", "Prezzo");
            SqlTransaction tx = null;
            //string elenco = string.Empty;
            //int i = 0; 
            try
            {
                // Connessione al DB Cinema
                using (SqlConnection conn = DatabaseHandler.GetConnection())
                {
                    conn.Open();
                    tx = conn.BeginTransaction();
                    using (SqlCommand command1 = conn.CreateCommand())
                    {
                        command1.CommandText = "SELECT * FROM Cinema.Evento;";
                        command1.Transaction = tx;
                        using (SqlDataReader reader = command1.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Event e = new Event();
                                e.eventCode = reader.GetInt32(0);
                                e.dateTime = reader.GetDateTime(1);
                                e.filmCode = reader.GetInt32(2);
                                e.hallCode = reader.GetInt32(3);
                                e.usernameAdmin = reader.GetString(4);
                                e.price = reader.GetDecimal(5);
                                //elenco = elenco + listEventi[i].VisualizzaEventi() + "\n";
                                t.AddRow(e.eventCode, e.dateTime.ToShortDateString() + " " + e.dateTime.ToShortTimeString(), e.hallCode, e.filmCode, e.price + "€");
                                //i++;
                            }

                        }
                    }
                    tx.Commit();
                }
            }
            catch (SqlException ex)
            {
                return string.Format("Connessione non riuscita: {0}", ex.ToString());
            }
            /*if (elenco == string.Empty)
                return "Non sono presenti film in programmazione in questo momento. \n";
            else*/
                return t.Print();
        }

        //Visualizzazione Elenco Sale
        public string VisualizzazioneSale()
        {
            var t = new TablePrinter(" SALA ", " CAPIENZA ");
            SqlTransaction tx = null;
            //string elenco = string.Empty;
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
                        command1.CommandText = "SELECT * FROM Cinema.Sala;";
                        command1.Transaction = tx;
                        using (SqlDataReader reader = command1.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Hall s = new Hall();
                                s.hallCode = reader.GetInt32(0);
                                s.capacity = reader.GetInt32(1);
                                //elenco = elenco + listSale[i].VisualizzaSala() + "\n";
                                //i++;
                                t.AddRow(s.hallCode, s.capacity);
                            }

                        }
                    }
                    tx.Commit();
                }
            }
            catch (SqlException ex)
            {
                return string.Format("Connessione non riuscita: {0}", ex.ToString());
            }
            return t.Print();
        }

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
                                lista_posti_disponibili[i].placeNumber = reader.GetInt32(0);
                                lista_posti_disponibili[i].hallCode = reader.GetInt32(1);
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
                            if (lista_posti_disponibili[x].placeNumber == lista_riservati[y].placeNumber)
                            {
                                lista_posti_disponibili.Remove(lista_posti_disponibili[x]); //Se trovo due elementi uguali lo rimuovo dalla lista dei posti disponibili
                                break; //blocco il ciclo
                            }
                        }
                    }
                    for (int z = 0; z < lista_posti_disponibili.Count; z++)
                    {
                        elenco = elenco + lista_posti_disponibili[z].placeNumber + " ";
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
                                listUtentiFree[i].username = reader.GetString(0);
                                listUtentiFree[i].name = reader.GetString(2);
                                listUtentiFree[i].surname = reader.GetString(3);
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

        //Visualizzazione Elenco Prenotazioni filtrato per Utente
        public string Visualizzazione_elenco_Prenotazioni(string user)
        {
            var t = new TablePrinter("Prenotation ID", "Prenotation Date", "User", "Event Code", "Price, Title", "Event Date", "Hall", "Price", "Place Number");
            SqlTransaction tx = null;
            string elenco = string.Empty;
            int i = 0; //variabile di incremento per la lista
            try
            {
                // Connessione al DB Cinema
                using (SqlConnection conn = DatabaseHandler.GetConnection())
                {
                    listPrenotazioni.Clear(); //pulisco la lista di film in modo tale da riempirla nuovamente e non avere problemi se sono avvenute modifiche
                    conn.Open();
                    tx = conn.BeginTransaction();
                    using (SqlCommand command1 = conn.CreateCommand())
                    {
                        command1.Transaction = tx;
                        command1.CommandType = CommandType.StoredProcedure;
                        command1.CommandText = "Cinema.VisualizzaPrenotazione";
                        command1.Parameters.Add("@user", SqlDbType.VarChar).Value = user;
                        command1.Connection = conn;
                        command1.ExecuteNonQuery();

                        using (SqlDataReader reader = command1.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Prenotation p = new Prenotation();
                                p.prenotationCode = reader.GetInt32(0);
                                p.dateTime = reader.GetDateTime(1);
                                p.usernameUser = reader.GetString(2);
                                p.eventCode = reader.GetInt32(3);
                                /*elenco = elenco + listPrenotazioni[i].VisualizzaPrenotazioni() + "\n" + "Titolo: " + reader.GetString(4) + ", Data e Ora dell'evento: " + reader.GetDateTime(5) +
                                    ", Sala: " + reader.GetInt32(6) + "\nPrezzo: " + reader.GetDecimal(7) + ", Numero posto:" + reader.GetInt32(8) + "\n";*/
                                t.AddRow(p.prenotationCode, p.dateTime.ToShortDateString() + " " + p.dateTime.ToShortTimeString(), p.usernameUser, p.eventCode, reader.GetString(4), reader.GetDateTime(5), reader.GetInt32(6), reader.GetDecimal(7) + "€", reader.GetInt32(8));
                                i++;
                            }
                        }
                    }
                    tx.Commit();
                }
            }
            catch (SqlException ex)
            {
                return string.Format("Connessione non riuscita: {0}", ex.ToString());
            }
            /*if (elenco == string.Empty)
                return "Non sono state ancora effettuate prenotazioni \n";
            else*/
                return t.Print();
        }

        //Visualizzazione dei posti in sala
        //Funzione che mi da una semplice rappresentazione a console di come sono disposti i posti nelle varie sale del cinema
        public string RappresentaSale(int codice_evento)
        {
            SqlTransaction tx = null;
            string output = string.Empty;
            Event e1 = new Event();
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
                        command1.CommandText = "Cinema.RichiestaCodiceSala";
                        command1.Parameters.Add("@codice_evento", SqlDbType.Int).Value = codice_evento;
                        command1.Connection = conn;
                        command1.ExecuteNonQuery();
                        using (SqlDataReader reader = command1.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                e1.eventCode = reader.GetInt32(0);
                            }
                            switch (e1.eventCode)
                            {
                                case 1:
                                    output = "\n________________________________\n\\______________________________/\n\n   | 1 | 2 | 3 | 4 | 5 | 6 |\n" +
                                        "   | 7 | 8 | 9 | 10| 11| 12|\n   | 13| 14| 15| 16| 17| 18|\n   | 19| 20| 21| 22| 23| 24|\n";
                                    break;
                                case 2:
                                    output = "\n________________________\n\\______________________/\n\n   | 1 | 2 | 3 | 4 |\n" +
                                        "   | 5 | 6 | 7 | 8 |\n   | 9 | 10| 11| 12|\n   | 13| 14| 15| 16|\n";
                                    break;
                                case 3:
                                    output = "\n________________________________\n\\______________________________/\n\n   | 1 | 2 | 3 | 4 | 5 | 6 |\n" +
                                        "   | 7 | 8 | 9 | 10| 11| 12|\n   | 13| 14| 15| 16| 17| 18|\n   | 19| 20| 21| 22| 23| 24|\n   | 25| 26| 27| 28| 29| 30|\n";
                                    break;
                                default:
                                    output = "Errore";
                                    break;
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
            return output;
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
                                lista_posti_disponibili[i].placeNumber = reader.GetInt32(0);
                                lista_posti_disponibili[i].hallCode = reader.GetInt32(1);
                                i++;
                            }
                        }
                    }
                    for (int y = 0; y < lista_posti_disponibili.Count; y++)
                    {
                        if (lista_posti_disponibili[y].placeNumber == numero_posto) condizione1 = false;
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