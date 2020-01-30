using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace WCFDatabaseManager
{
    /*
     * TablePrinter Class
     * Draw a console table where storing data.
     * 
     * The class has 3 attribute. The first stay for the table header, the titles of the columns, 
     * the second attribute is a List that contains the titles numbers of character for the table creation,
     * and the third attribute stores the numbers of rows.
     * 
     * @author Daniele Pellegrini <daniele.pellegrini@studenti.unipr.it> - 285240
     * @author Riccardo Fava <riccardo.fava@studenti.unipr.it> - 287516
     */
     [DataContract]
    public class TablePrinter
    {
        [DataMember]
        private readonly string[] titles;
        [DataMember]
        private readonly List<int> lengths;
        [DataMember]
        private readonly List<string[]> rows = new List<string[]>();

        /*
         * Class Constructor
         */
        public TablePrinter(params string[] titles)
        {
            this.titles = titles;
            lengths = titles.Select(t => t.Length).ToList();
        }

        /*
         * Add a row containing the object to display.
         * Each objects must have a numbers of parameters equal to the numbers of the titles,
         * if not the system thorws an Exception.
         * 
         * @Exception The length of the row added is not equal to the title row length. 
         */
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

        /*
         * Draw the table in a string
         * 
         * @return the string
         */
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
            lengths.ForEach(l => s += ("+-" + new string('-', l) + '-'));
            s += ("+" + "\n");

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
            lengths.ForEach(l => s += ("+-" + new string('-', l) + '-'));
            s += ("+" + "\n");
            return s;
        }

        /*
         * Draw a table of users
         */
        public string TableUser(List<User> userList)
        {
            // Create the columns
            var table = new TablePrinter(" NOME ", " COGNOME ", " USERNAME ");

            // Add the row of the table
            foreach (User u in userList)
            {
                table.AddRow(u.Name, u.Surname, u.Username);
            }

            return table.Print();
        }

        /*
         * Draw a table of film
         */
        public string TableFilm(List<Film> filmList)
        {
            // Create the columns
            var table = new TablePrinter(" ID ", " TITOLO ", " ANNO ", " REGIA ", " DURATA ", " DATA DI USCITA ", " GENERE ");

            // Add the row of the table
            foreach (Film f in filmList)
            {
                table.AddRow(f.FilmCode, f.Title, f.Year, f.Direction,
                    f.Duration + "'", f.ReleaseDate.ToShortDateString(), f.Genre);
            }

            return table.Print();
        }

        /*
         * Draw a table of events
         */
        public string TableEvent(List<Event> eventsList)
        {
            // Create the columns
            var table = new TablePrinter("ID EVENTO", " DATA E ORA ", " SALA ", " ID FILM ", " PREZZO ");

            // Add the row of the table
            foreach (Event e in eventsList)
            {
                table.AddRow(e.EventCode, e.DateTime.ToShortDateString() + " " + e.DateTime.ToShortTimeString(), 
                    e.HallCode, e.FilmCode, e.Price + "€");
            }

            return table.Print();
        }

        /*
         * Draw a table of halls
         */
        public string TableHall(List<Hall> hallsList)
        {
            // Create the columns
            var table = new TablePrinter(" SALA ", " CAPIENZA ");

            // Add the row of the table
            foreach (Hall h in hallsList)
            {
                table.AddRow(h.HallCode, h.Capacity);
            }

            return table.Print();
        }

        /*
         * Draw a table of prenotations
         */
        public string TablePrenotation(List<Prenotation> prenotationsList)
        {
            // Create the columns
            var table = new TablePrinter("ID PRENOTAZIONE", " DATA ", " UTENTE ", "CODICE EVENTO");

            // Add the row of the table
            foreach (Prenotation p in prenotationsList)
            {
                table.AddRow(p.PrenotationCode, p.DateTime, p.UsernameUser, p.EventCode);
            }

            return table.Print();
        }
    }
}
