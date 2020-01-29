using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    class TablePrinter
    {
        private readonly string[] titles;
        private readonly List<int> lengths;
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
    }
}
