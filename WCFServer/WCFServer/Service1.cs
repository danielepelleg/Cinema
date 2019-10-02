using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace WCFServer
{
    // NOTA: è possibile utilizzare il comando "Rinomina" del menu "Refactoring" per modificare il nome di classe "Service1" nel codice e nel file di configurazione contemporaneamente.
    public class Service1 : IService1
    {
        List<Film> listFilm = new List<Film>();
        List<Evento> listEventi = new List<Evento>();
        List<Sala> listSale = new List<Sala>();
        List<UtenteFree> listUtentiFree = new List<UtenteFree>();
        List<Prenotazione> listPrenotazioni = new List<Prenotazione>();
        Admin a1 = new Admin();
        UtenteFree u1 = new UtenteFree();
        Film f1 = new Film();
        Evento e1 = new Evento();
        Prenotazione p1 = new Prenotazione();


        //Registrazione Admin e UtenteFree
        public string Registration(string user_type, string username, string password, string nome, string cognome)
        {
            using (SqlConnection connection = new SqlConnection("Server = localhost\\SQLEXPRESS; Database = Prova1; Integrated Security = SSPI"))
            {
                connection.Open();

                // Start a local transaction.
                SqlTransaction sqlTran = connection.BeginTransaction();

                // Enlist a command in the current transaction.
                SqlCommand command = connection.CreateCommand();
                command.Transaction = sqlTran;
                try
                {
                    if (user_type == "Admin")
                    {
                        command.CommandText = "INSERT Cinema.Admin (UsernameAdmin, Password, Nome, Cognome) VALUES ('" + username + "','" + password + "','" + nome + "','" + cognome + "')";
                        command.ExecuteNonQuery();
                    }
                    else if (user_type == "User")
                    {
                        command.CommandText = "INSERT Cinema.UtenteFree (UsernameUtenteFree, Password, Nome, Cognome) VALUES ('" + username + "','" + password + "','" + nome + "','" + cognome + "')";
                        command.ExecuteNonQuery();
                    }
                    // Commit the transaction.
                    sqlTran.Commit();
                    return "REGISTRAZIONE AVVENUTA CON SUCCESSO\nPROCEDERE CON IL LOGIN\n";
                }
                catch
                {
                    return "ERRORE REGISTRAZIONE UTENTE\n RIPROVARE!\n";

                }
            }

        }

        // Login (Administrator - Utente)
        public bool Login(string user_type, string username, string password)
        {
            bool check = false; // Varabile booleana che controlla il successo del Login
            // Controllo tipo di Login
            if (user_type == "Admin")
            {
                a1.UsernameAdmin = username;
                a1.Password = password;
                user_type = "admin";
            }
            else if (user_type == "User")
            {
                u1.UsernameUtenteFree = username;
                u1.Password = password;
                user_type = "utentefree";
            }
            string result = string.Empty;
            try
            {
                SqlTransaction tx = null;
                // Connessione al DB Cinema
                using (SqlConnection conn = new SqlConnection("Server=localhost\\SQLEXPRESS;Database=Prova1;Integrated Security=SSPI"))
                {
                    conn.Open();
                    tx = conn.BeginTransaction();
                    using (SqlCommand command1 = conn.CreateCommand())
                    {
                        command1.CommandText = "SELECT * FROM Cinema." + user_type + ";";
                        command1.Transaction = tx;
                        using (SqlDataReader reader = command1.ExecuteReader())
                        {
                            {
                                // Definisco una ariabile booleana di controllo. 
                                // I dati forniti dall'utente devono soddisfare quelli presenti nel Database
                                bool found = false; 
                                while (reader.Read())
                                {
                                    if ((username == reader.GetString(0)) && (password == reader.GetString(1)))
                                    {
                                        found = true;
                                        check = true;
                                    }
                                }
                                if (found == false)
                                {
                                    check = false;
                                }
                            }
                        }
                    }
                    tx.Commit();
                }
            }
            catch (SqlException ex)
            {
                result = string.Format("Connessione non riuscita: {0}", ex.ToString());
            }
            return check;
        }

        // Inserimento Film
        public string InserimentoFilm(string titolo, int anno, string regia, int durata, DateTime datauscita, string genere)
        {
            // Definisco la variabile t per la creazione della tabella
            var t = new TablePrinter("ID", "Titolo", "Anno", "Regia", "Durata", "Data di Uscita", "Genere");
            using (SqlConnection connection = new SqlConnection("Server = localhost\\SQLEXPRESS; Database = Prova1; Integrated Security = SSPI"))
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
                    cmd.CommandText = "Cinema.AddNewFilm";
                    cmd.Parameters.Add("@Titolo", SqlDbType.VarChar).Value = titolo;
                    cmd.Parameters.Add("@Anno", SqlDbType.Int).Value = anno;
                    cmd.Parameters.Add("@Regia", SqlDbType.VarChar).Value = regia;
                    cmd.Parameters.Add("@Durata", SqlDbType.Int).Value = durata;
                    cmd.Parameters.Add("@Data_Uscita", SqlDbType.DateTime).Value = datauscita;
                    cmd.Parameters.Add("@Genere", SqlDbType.VarChar).Value = genere;
                    cmd.Parameters.Add("@CodiceFilm", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Connection = connection;
                    cmd.ExecuteNonQuery();
                    // CodiceFilm is an IDENTITY value from the database.
                    f1.CodiceFilm = Convert.ToInt32(cmd.Parameters["@CodiceFilm"].Value.ToString());
                    f1.Titolo = titolo;
                    f1.Anno = anno;
                    f1.Regia = regia;
                    f1.Durata = durata;
                    f1.DataUscita = datauscita;
                    f1.Genere = genere;
                    t.AddRow(f1.CodiceFilm, f1.Titolo, f1.Anno, f1.Regia, f1.Durata+"'", f1.DataUscita.ToShortDateString(), f1.Genere);
                    // Commit the transaction.
                    sqlTran.Commit();
                    return "\nINSERIMENTO DEL FILM AVVENUTO CON SUCCESSO" +"\nFilm Inserito: \n \n" + t.Print() + "\n";
                }
                catch (SqlException ex)
                {
                    return ex.Message;
                }
            }

        }

        // Cancellazione Film
        public string CancellazioneFilm(int codicefilm)
        {

            using (SqlConnection connection = new SqlConnection("Server = localhost\\SQLEXPRESS; Database = Prova1; Integrated Security = SSPI"))
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
                    cmd.CommandText = "Cinema.DeleteFilm";
                    cmd.Parameters.Add("@CodiceFilm", SqlDbType.Int).Value = codicefilm;
                    cmd.Connection = connection;
                    cmd.ExecuteNonQuery();
                    // Commit the transaction.
                    sqlTran.Commit();
                    return "CANCELLAZIONE DEL FILM DAL DATABASE AVVENUTA CON SUCCESSO\n";

                }
                catch (SqlException ex)
                {
                    return ex.Message;
                }
            }

        }

        // Cancellazione Evento
        public string CancellazioneEvento(int codiceevento)
        {

            using (SqlConnection connection = new SqlConnection("Server = localhost\\SQLEXPRESS; Database = Prova1; Integrated Security = SSPI"))
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

        // Inserimento Evento
        public string InserimentoEvento(string usernameadmin, DateTime data_e_ora, int codice_film, int codice_sala, decimal prezzo)
        {

            using (SqlConnection connection = new SqlConnection("Server = localhost\\SQLEXPRESS; Database = Prova1; Integrated Security = SSPI"))
            {
                var t = new TablePrinter("ID EVENTO", "Data e Ora", "Sala", "ID FILM", "Prezzo");
                SqlTransaction tx = null;
                connection.Open();
                tx = connection.BeginTransaction();
                using (SqlCommand command1 = connection.CreateCommand()) //controllo che non ci siano sovvrapposizioni di eventi nella stessa sala
                {
                    command1.CommandText = "SELECT * FROM Cinema.Evento;";
                    command1.Transaction = tx;
                    using (SqlDataReader reader = command1.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (data_e_ora == reader.GetDateTime(1) && codice_sala == reader.GetInt32(4))
                            {
                                return "\nNella sala inserita è gia presente una proiezione di un film nella data e all'orario che hai appena inserito!\nImpossibile effettuare l'inserimento! Riprovare!\n";
                            }
                        }
                    }
                }
                tx.Commit();

                // Start a local transaction.
                SqlTransaction sqlTran = connection.BeginTransaction();

                // Enlist a command in the current transaction.
                SqlCommand cmd = connection.CreateCommand();
                cmd.Transaction = sqlTran;

                try
                {
                    // Esecuzione Inserimento
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Cinema.AddNewEvento";
                    cmd.Parameters.Add("@DataOra", SqlDbType.DateTime).Value = data_e_ora;
                    cmd.Parameters.Add("@Codice_Film", SqlDbType.Int).Value = codice_film;
                    cmd.Parameters.Add("@Codice_Sala", SqlDbType.Int).Value = codice_sala;
                    cmd.Parameters.Add("@Username_Admin", SqlDbType.VarChar).Value = usernameadmin;
                    cmd.Parameters.Add("@Prezzo", SqlDbType.Decimal).Value = prezzo;
                    cmd.Parameters.Add("@CodiceEvento", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Connection = connection;
                    cmd.ExecuteNonQuery();
                    // CodiceEvento is an IDENTITY value from the database.
                    e1.CodiceEvento = Convert.ToInt32(cmd.Parameters["@CodiceEvento"].Value.ToString());
                    e1.Data_e_ora = data_e_ora;
                    e1.CodiceFilm = codice_film;
                    e1.CodiceSala = codice_sala;
                    e1.UsernameAdmin = usernameadmin;
                    e1.Prezzo = prezzo;
                    t.AddRow(e1.CodiceEvento, e1.Data_e_ora, e1.CodiceSala, e1.CodiceFilm, e1.Prezzo + "€");
                    // Commit the transaction.
                    sqlTran.Commit();
                    connection.Close();
                    return "INSERIMENTO EVENTO AVVENUTO CON SUCCESSO\nEvento Inserito: \n" + t.Print() + "\n";

                }
                catch (SqlException ex)
                {
                    return string.Format("{0}", ex.ToString());
                }
            }

        }

        //Inserimento Prenotazioni
        public string InserimentoPrenotazione(DateTime dataora, string username_utentefree, int codice_evento, int numero_posto)
        {
            using (SqlConnection connection = new SqlConnection("Server = localhost\\SQLEXPRESS; Database = Prova1; Integrated Security = SSPI"))
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
                    p1.CodicePrenotazione = Convert.ToInt32(cmd.Parameters["@CodicePrenotazione"].Value.ToString());
                    p1.Dataora = dataora;
                    p1.Username_UtenteFree = username_utentefree;
                    p1.Codice_Evento = codice_evento;
                    // Commit the transaction.
                    sqlTran.Commit();
                    //Inserisco i valori nella tabella Riserva
                    SqlTransaction sqlTran1 = connection.BeginTransaction();
                    SqlCommand cmd1 = connection.CreateCommand();
                    cmd1.Transaction = sqlTran1;
                    cmd1.CommandType = CommandType.StoredProcedure;
                    cmd1.CommandText = "Cinema.AddNewRiserva";
                    cmd1.Parameters.Add("@numero_posto", SqlDbType.Int).Value = numero_posto;
                    cmd1.Parameters.Add("@codice_prenotazione", SqlDbType.Int).Value = p1.CodicePrenotazione;
                    cmd1.Connection = connection;
                    cmd1.ExecuteNonQuery();
                    sqlTran1.Commit();
                    connection.Close();
                    return "PRENOTAZIONE EFFETTUATA CON SUCCESSO!\nBiglietto acquistato: \n" + p1.VisualizzaPrenotazioni() + "\n";
                }
                catch (SqlException ex)
                {
                    return string.Format("{0}", ex.ToString());
                }
            }

        }

        //Visualizzazione Elenco Film
        public string Visualizzazione_elenco_film()
        {
            var t = new TablePrinter("ID", "Titolo", "Anno", "Regia", "Durata", "Data di Uscita", "Genere");
            SqlTransaction tx = null;
            string elenco = string.Empty;
            int i = 0; //variabile di incremento per la lista
            try
            {
                // Connessione al DB Cinema
                
                using (SqlConnection conn = new SqlConnection("Server=localhost\\SQLEXPRESS;Database=Prova1;Integrated Security=SSPI"))
                {
                    listFilm.Clear(); //pulisco la lista di film in modo tale da riempirla nuovamente e non avere problemi se sono avvenute modifiche
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
                                listFilm.Add(f);
                                listFilm[i].CodiceFilm = reader.GetInt32(0);
                                listFilm[i].Titolo = reader.GetString(1);
                                listFilm[i].Anno = reader.GetInt32(2);
                                listFilm[i].Regia = reader.GetString(3);
                                listFilm[i].Durata = reader.GetInt32(4);
                                listFilm[i].DataUscita = reader.GetDateTime(5);
                                listFilm[i].Genere = reader.GetString(6);
                                elenco = elenco + listFilm[i].VisualizzaFilm() + "\n";
                                t.AddRow(listFilm[i].CodiceFilm, listFilm[i].Titolo, listFilm[i].Anno, listFilm[i].Regia, listFilm[i].Durata+"'", listFilm[i].DataUscita.ToShortDateString(), listFilm[i].Genere);
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
                return "Non sono ancora presenti film all'interno del database. \n";
            else
                return t.Print();
        }

        //Visualizzazione Elenco Eventi
        public string Visualizzazione_elenco_eventi()
        {
            var t = new TablePrinter("ID EVENTO", "Data e Ora", "Sala", "ID FILM", "Prezzo");
            SqlTransaction tx = null;
            string elenco = string.Empty;
            int i = 0; //variabile di incremento per la lista
            try
            {
                // Connessione al DB Cinema
                using (SqlConnection conn = new SqlConnection("Server=localhost\\SQLEXPRESS;Database=Prova1;Integrated Security=SSPI"))
                {
                    listEventi.Clear(); //pulisco la lista di film in modo tale da riempirla nuovamente e non avere problemi se sono avvenute modifiche
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
                                Evento e = new Evento();
                                listEventi.Add(e);
                                listEventi[i].CodiceEvento = reader.GetInt32(0);
                                listEventi[i].Data_e_ora = reader.GetDateTime(1);
                                listEventi[i].CodiceFilm = reader.GetInt32(2);
                                listEventi[i].CodiceSala = reader.GetInt32(3);
                                listEventi[i].UsernameAdmin = reader.GetString(4);
                                listEventi[i].Prezzo = reader.GetDecimal(5);
                                elenco = elenco + listEventi[i].VisualizzaEventi() + "\n";
                                t.AddRow(listEventi[i].CodiceEvento, listEventi[i].Data_e_ora, listEventi[i].CodiceSala, listEventi[i].CodiceFilm, listEventi[i].Prezzo + "€");
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
            if (elenco == string.Empty)
                return "Non sono presenti film in programmazione in questo momento. \n";
            else
                return t.Print();
        }


        //Visualizzazione Elenco Sale
        public string VisualizzazioneSale()
        {
            var t = new TablePrinter(" SALA ", " CAPIENZA ");
            SqlTransaction tx = null;
            string elenco = string.Empty;
            int i = 0; //variabile di incremento per la lista
            try
            {
                // Connessione al DB Cinema
                using (SqlConnection conn = new SqlConnection("Server=localhost\\SQLEXPRESS;Database=Prova1;Integrated Security=SSPI"))
                {
                    listSale.Clear(); //pulisco la lista di film in modo tale da riempirla nuovamente e non avere problemi se sono avvenute modifiche
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
                                Sala s = new Sala();
                                listSale.Add(s);
                                listSale[i].CodiceSala = reader.GetInt32(0);
                                listSale[i].Capienza = reader.GetInt32(1);
                                elenco = elenco + listSale[i].VisualizzaSala() + "\n";
                                i++;
                                t.AddRow(reader.GetInt32(0), reader.GetInt32(1));
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
            List<Posto> lista_posti_disponibili = new List<Posto>();
            List<Riserva> lista_riservati = new List<Riserva>();
            SqlTransaction tx = null;
            SqlTransaction tx1 = null;
            string elenco = string.Empty;
            int i = 0; //variabile di incremento per la lista
            int h = 0; //variabile di incremento per la lista
            try
            {
                // Connessione al DB Cinema
                using (SqlConnection conn = new SqlConnection("Server=localhost\\SQLEXPRESS;Database=Prova1;Integrated Security=SSPI"))
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
                                Posto p = new Posto();
                                lista_posti_disponibili.Add(p);
                                lista_posti_disponibili[i].NumeroPosto = reader.GetInt32(0);
                                lista_posti_disponibili[i].Codice_Sala = reader.GetInt32(1);
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
                                Riserva r = new Riserva();
                                lista_riservati.Add(r);
                                lista_riservati[h].NumeroPosto = reader.GetInt32(0);
                                lista_riservati[h].CodicePrenotazione = reader.GetInt32(1);
                                h++;
                            }
                        }
                    }

                    for (int x = lista_posti_disponibili.Count - 1; x >= 0; x--)
                    {
                        for (int y = 0; y < lista_riservati.Count; y++)
                        {
                            if (lista_posti_disponibili[x].NumeroPosto == lista_riservati[y].NumeroPosto)
                            {
                                lista_posti_disponibili.Remove(lista_posti_disponibili[x]); //Se trovo due elementi uguali lo rimuovo dalla lista dei posti disponibili
                                break; //blocco il ciclo
                            }
                        }
                    }
                    for (int z = 0; z < lista_posti_disponibili.Count; z++)
                    {
                        elenco = elenco + lista_posti_disponibili[z].NumeroPosto + " ";
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
                using (SqlConnection conn = new SqlConnection("Server=localhost\\SQLEXPRESS;Database=Prova1;Integrated Security=SSPI"))
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
                                UtenteFree u = new UtenteFree();
                                listUtentiFree.Add(u);
                                listUtentiFree[i].UsernameUtenteFree = reader.GetString(0);
                                listUtentiFree[i].Nome = reader.GetString(2);
                                listUtentiFree[i].Cognome = reader.GetString(3);
                                elenco = elenco + listUtentiFree[i].VisualizzaUtentiFree() + "\n";
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
            SqlTransaction tx = null;
            string elenco = string.Empty;
            int i = 0; //variabile di incremento per la lista
            try
            {
                // Connessione al DB Cinema
                using (SqlConnection conn = new SqlConnection("Server=localhost\\SQLEXPRESS;Database=Prova1;Integrated Security=SSPI"))
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
                                Prenotazione p = new Prenotazione();
                                listPrenotazioni.Add(p);
                                listPrenotazioni[i].CodicePrenotazione = reader.GetInt32(0);
                                listPrenotazioni[i].Dataora = reader.GetDateTime(1);
                                listPrenotazioni[i].Username_UtenteFree = reader.GetString(2);
                                listPrenotazioni[i].Codice_Evento = reader.GetInt32(3);
                                elenco = elenco + listPrenotazioni[i].VisualizzaPrenotazioni() + "\n" + "Titolo: " + reader.GetString(4) + ", Data e Ora dell'evento: " + reader.GetDateTime(5) +
                                    ", Sala: " + reader.GetInt32(6) + "\nPrezzo: " + reader.GetDecimal(7) + ", Numero posto:" + reader.GetInt32(8) + "\n";
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
            if (elenco == string.Empty)
                return "Non sono state ancora effettuate prenotazioni \n";
            else
                return elenco;
        }

        //Visualizzazione dei posti in sala
        //Funzione che mi da una semplice rappresentazione a console di come sono disposti i posti nelle varie sale del cinema
        public string RappresentaSale(int codice_evento)
        {
            SqlTransaction tx = null;
            string output = string.Empty;
            Evento e1 = new Evento();
            try
            {
                // Connessione al DB Cinema
                using (SqlConnection conn = new SqlConnection("Server=localhost\\SQLEXPRESS;Database=Prova1;Integrated Security=SSPI"))
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
                                e1.CodiceEvento = reader.GetInt32(0);
                            }
                            switch (e1.CodiceEvento)
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
                using (SqlConnection conn = new SqlConnection("Server=localhost\\SQLEXPRESS;Database=Prova1;Integrated Security=SSPI"))
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
            List<Riserva> lista_riservati = new List<Riserva>();
            SqlTransaction tx1 = null;
            List<Posto> lista_posti_disponibili = new List<Posto>();
            SqlTransaction tx = null;
            bool condizione1 = true;
            bool condizione2 = false;
            int i = 0;
            int h = 0;
            try
            {
                // Connessione al DB Cinema
                using (SqlConnection conn = new SqlConnection("Server=localhost\\SQLEXPRESS;Database=Prova1;Integrated Security=SSPI"))
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
                                Posto p = new Posto();
                                lista_posti_disponibili.Add(p);
                                lista_posti_disponibili[i].NumeroPosto = reader.GetInt32(0);
                                lista_posti_disponibili[i].Codice_Sala = reader.GetInt32(1);
                                i++;
                            }
                        }
                    }
                    for (int y = 0; y < lista_posti_disponibili.Count; y++)
                    {
                        if (lista_posti_disponibili[y].NumeroPosto == numero_posto) condizione1 = false;
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
                                Riserva r = new Riserva();
                                lista_riservati.Add(r);
                                lista_riservati[h].NumeroPosto = reader.GetInt32(0);
                                lista_riservati[h].CodicePrenotazione = reader.GetInt32(1);
                                h++;
                            }
                        }
                    }
                    for (int z = 0; z < lista_riservati.Count; z++)
                    {
                        if (lista_riservati[z].NumeroPosto == numero_posto)
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

        // CLASSI
        public class Film
        {
            private int codiceFilm;
            public int CodiceFilm { get => codiceFilm; set => codiceFilm = value; }
            private string titolo;
            public string Titolo { get => titolo; set => titolo = value; }
            private int anno;
            public int Anno { get => anno; set => anno = value; }
            private string regia;
            public string Regia { get => regia; set => regia = value; }
            private int durata;
            public int Durata { get => durata; set => durata = value; }
            private DateTime dataUscita;
            public DateTime DataUscita { get => dataUscita; set => dataUscita = value; }
            private string genere;
            public string Genere { get => genere; set => genere = value; }

            public string VisualizzaFilm()
            {
                return CodiceFilm + ", " + Titolo + ", " + Anno.ToString() + ", " + Regia + ", " + Durata.ToString() + ", " + DataUscita.ToString() + ", " + Genere;
            }

        }

        public class Evento
        {
            private int codiceEvento;
            public int CodiceEvento { get => codiceEvento; set => codiceEvento = value; }
            private DateTime data_e_ora;
            public DateTime Data_e_ora { get => data_e_ora; set => data_e_ora = value; }
            private int codiceSala;
            public int CodiceSala { get => codiceSala; set => codiceSala = value; }
            private string usernameAdmin;
            public string UsernameAdmin { get => usernameAdmin; set => usernameAdmin = value; }
            private int codiceFilm;
            public int CodiceFilm { get => codiceFilm; set => codiceFilm = value; }
            private decimal prezzo;
            public decimal Prezzo { get => prezzo; set => prezzo = value; }

            public string VisualizzaEventi()
            {
                return "Codice Evento: " + CodiceEvento + ", Data e Ora: " + Data_e_ora.ToString() + ", Codice sala: " + CodiceSala + ", Codice film: " + CodiceFilm + ", Prezzo: " + Prezzo.ToString();
            }
        }

        public class Admin
        {
            private string usernameAdmin;
            public string UsernameAdmin { get => usernameAdmin; set => usernameAdmin = value; }
            private string password;
            public string Password { get => password; set => password = value; }
            private string nome;
            public string Nome { get => nome; set => nome = value; }
            private string cognome;
            public string Cognome { get => cognome; set => cognome = value; }

        }

        public class UtenteFree
        {
            private string usernameUtenteFree;
            public string UsernameUtenteFree { get => usernameUtenteFree; set => usernameUtenteFree = value; }
            private string password;
            public string Password { get => password; set => password = value; }
            private string nome;
            public string Nome { get => nome; set => nome = value; }
            private string cognome;
            public string Cognome { get => cognome; set => cognome = value; }
            public string VisualizzaUtentiFree()
            {
                return "Username: " + UsernameUtenteFree + ", Nome: " + Nome + ", Cognome: " + Cognome;
            }
        }

        public class Sala
        {
            private int codiceSala;
            public int CodiceSala { get => codiceSala; set => codiceSala = value; }
            private int capienza;
            public int Capienza { get => capienza; set => capienza = value; }

            public string VisualizzaSala()
            {
                return "Codice Sala:" + CodiceSala + ", Capienza:" + Capienza;
            }
        }

        public class Prenotazione
        {
            private int codicePrenotazione;
            public int CodicePrenotazione { get => codicePrenotazione; set => codicePrenotazione = value; }
            private DateTime dataora;
            public DateTime Dataora { get => dataora; set => dataora = value; }
            private string username_UtenteFree;
            public string Username_UtenteFree { get => username_UtenteFree; set => username_UtenteFree = value; }
            private int codice_Evento;
            public int Codice_Evento { get => codice_Evento; set => codice_Evento = value; }

            public string VisualizzaPrenotazioni()
            {
                return "Codice Prenotazione:" + CodicePrenotazione + ", Data e Ora della Prenotazione: " + Dataora + ", Username dell'utente che ha svolto la prenotazione: " + Username_UtenteFree +
                    ", Codice dell'evento prenotato: " + Codice_Evento;
            }
        }

        public class Posto
        {
            private int codicePosto;
            public int CodicePosto { get => numeroPosto; set => numeroPosto = value; }
            private int numeroPosto;
            public int NumeroPosto { get => numeroPosto; set => numeroPosto = value; }
            private int codice_Sala;
            public int Codice_Sala { get => codice_Sala; set => codice_Sala = value; }

            public string VisualizzaPosti()
            {
                return "Codice del posto:" + CodicePosto + "Codice della sala:" + Codice_Sala + ", Numero posto: " + NumeroPosto;
            }
        }

        public class Riserva
        {
            private int numeroPosto;
            public int NumeroPosto { get => numeroPosto; set => numeroPosto = value; }
            private int codicePrenotazione;
            public int CodicePrenotazione { get => codicePrenotazione; set => codicePrenotazione = value; }
        }

        public class TablePrinter
        {
            private readonly string[] titles;
            private readonly List<int> lengths;
            private readonly List<string[]> rows = new List<string[]>();

            public TablePrinter(params string[] titles)
            {
                this.titles = titles;
                lengths = titles.Select(t => t.Length).ToList();
            }

            public void AddRow(params object[] row)
            {
                if (row.Length != titles.Length)
                {
                    throw new System.Exception($"Added row length [{row.Length}] is not equal to title row length [{titles.Length}]");
                }
                rows.Add(row.Select(o => o.ToString()).ToArray());
                for (int i = 0; i < titles.Length; i++)
                {
                    if (rows.Last()[i].Length > lengths[i])
                    {
                        lengths[i] = rows.Last()[i].Length;
                    }
                }
            }

            public string Print()
            {
                string s = string.Empty;
                // Creo il bordo inferiore degli indici della tabella e gli angoli
                lengths.ForEach(l => s += ("+-" + new string('-', l) + '-'));
                s += ("+" + "\n");

                string line = "";
                // Aggiungo un separatore per ogni titolo
                for (int i = 0; i < titles.Length; i++)
                {
                    line += "| " + titles[i].PadRight(lengths[i]) + ' ';
                }
                s += (line + "|" + "\n");

                // Creo il bordo inferiore degli indici della tabella e gli angoli
                lengths.ForEach(l => s+= ("+-" + new string('-', l) + '-'));
                s+= ("+" + "\n");

                //Creo il bordo separatore tra le colonne,per ciascuna riga
                foreach (var row in rows)
                {
                    line = "";
                    for (int i = 0; i < row.Length; i++)
                    {
                        if (int.TryParse(row[i], out int n))
                        {
                            line += "| " + row[i].PadLeft(lengths[i]) + ' ';  // numbers are padded to the left
                        }
                        else
                        {
                            line += "| " + row[i].PadRight(lengths[i]) + ' ';
                        }
                    }
                    s += (line + "|" + "\n");
                }
                // Creo ultimo bordo inferiore della tabella
                lengths.ForEach(l => s+= ("+-" + new string('-', l) + '-'));
                s += ("+" + "\n");
                return s;
            }
        }
    }
}