using BetterConsoleTables;
using EasyConsole;
using System;
using System.Collections.Generic;
using WCFClient.ServiceReference1;

namespace WCFClient
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

    public abstract class TablePrinter{

        /*
         * Draw a table of Users
         */
        public static void User(List<User> userList)
        {
            if(userList.Count != 0)
            {
                // Create the columns
                ColumnHeader[] headers = new[] {
                new ColumnHeader(" NAME ", Alignment.Center, Alignment.Right),
                new ColumnHeader(" SURNAME ", Alignment.Center, Alignment.Right),
                new ColumnHeader(" USERNAME ", Alignment.Center, Alignment.Right),
            };
                Table table = new Table(headers);

                // Add the rows of the table
                foreach (User u in userList)
                {
                    table.AddRow(u.Name, u.Surname, u.Username);
                }

                // Format the table
                table.Config = TableConfiguration.UnicodeAlt();

                Output.WriteLine(table.ToString());
            } else Console.WriteLine("There are no Users in the DataBase!");
        }

        /*
         * Draw a table of Film
         */
        public static void Film(List<Film> filmList)
        {
            if(filmList.Count != 0)
            {
                // Create the columns
                ColumnHeader[] headers = new[] {
                new ColumnHeader(" # ", Alignment.Center, Alignment.Right),
                new ColumnHeader(" TITLE ", Alignment.Center, Alignment.Center),
                new ColumnHeader(" YEAR ", Alignment.Center, Alignment.Right),
                new ColumnHeader(" DIRECTION ", Alignment.Center, Alignment.Center),
                new ColumnHeader(" DURATION ", Alignment.Center, Alignment.Right),
                new ColumnHeader(" RELEASE DATE ", Alignment.Center, Alignment.Right),
                new ColumnHeader(" GENRE ", Alignment.Center, Alignment.Center),
            };
                Table table = new Table(headers);

                // Add the row of the table
                foreach (Film f in filmList)
                {
                    table.AddRow(f.FilmCode, f.Title, f.Year, f.Direction,
                        f.Duration + "'", f.ReleaseDate.ToShortDateString(), f.Genre);
                }

                // Format the table
                table.Config = TableConfiguration.UnicodeAlt();

                Output.WriteLine(table.ToString());
            } else Console.WriteLine("There are no Films in the DataBase!");
        }

        /*
         * Draw a table of Events
         */
        public static void Event(List<Event> eventsList)
        {
            if(eventsList.Count != 0)
            {
                // Create the columns
                ColumnHeader[] headers = new[] {
                new ColumnHeader(" # ", Alignment.Center, Alignment.Right),
                new ColumnHeader(" DATE / TIME ", Alignment.Center, Alignment.Right),
                new ColumnHeader(" HALL ", Alignment.Center, Alignment.Right),
                new ColumnHeader(" # FILM ", Alignment.Center, Alignment.Right),
                new ColumnHeader(" PRICE ", Alignment.Center, Alignment.Right),
                };
                Table table = new Table(headers);

                // Add the row of the table
                foreach (Event e in eventsList)
                {
                    table.AddRow(e.EventCode, e.DateTime.ToShortDateString() + " " + e.DateTime.ToShortTimeString(),
                        e.HallCode, e.FilmCode, e.Price + "€");
                }

                // Format the table
                table.Config = TableConfiguration.UnicodeAlt();

                Output.WriteLine(table.ToString());
            } else Console.WriteLine("There are no Event in the DataBase!");
        }

        /*
         * Draw a table of Halls
         */
        public static void Hall(List<Hall> hallsList)
        {
            if (hallsList.Count != 0)
            {
                // Create the columns
                ColumnHeader[] headers = new[] {
                new ColumnHeader(" HALL ", Alignment.Center, Alignment.Right),
                new ColumnHeader(" CAPACITY ", Alignment.Center, Alignment.Right),
            };
                Table table = new Table(headers);

                // Add the row of the table
                foreach (Hall h in hallsList)
                {
                    table.AddRow(h.HallCode, h.Capacity);
                }

                // Format the table
                table.Config = TableConfiguration.UnicodeAlt();

                Output.WriteLine(table.ToString());
            } else Console.WriteLine("There are no Halls in the DataBase!");
        }

        /*
         * Draw a table of Halls
         */
        public static void Place(List<Place> placesList)
        {
            if(placesList.Count != 0)
            {
                // Create the columns
                ColumnHeader[] headers = new[] {
                new ColumnHeader(" # ", Alignment.Center, Alignment.Right),
                new ColumnHeader(" PLACE NUMBER ", Alignment.Center, Alignment.Right),
                new ColumnHeader(" # HALL ", Alignment.Center, Alignment.Right),
            };
                Table table = new Table(headers);

                // Add the row of the table
                foreach (Place p in placesList)
                {
                    table.AddRow(p.PlaceCode, p.PlaceNumber, p.HallCode);
                }

                // Format the table
                table.Config = TableConfiguration.UnicodeAlt();

                Output.WriteLine(table.ToString());
            } else Console.WriteLine("There are no Places in the DataBase!");
        }

        /*
         * Draw a table of Halls
         */
        public static void PlaceNumber(List<Place> placesList)
        {
            if (placesList.Count != 0)
            {
                // Create the columns
                ColumnHeader[] headers = new[] {
                new ColumnHeader(" PLACE NUMBER ", Alignment.Center, Alignment.Right),
            };
                Table table = new Table(headers);

                // Add the row of the table
                foreach (Place p in placesList)
                {
                    table.AddRow(p.PlaceNumber);
                }

                // Format the table
                table.Config = TableConfiguration.MySqlSimple();

                Output.WriteLine(table.ToString());
            } else Console.WriteLine("There are no Places in the DataBase!");
        }

        /*
         * Draw a table of Prenotations
         */
        public static void Prenotation(List<Prenotation> prenotationsList)
        {
            if (prenotationsList.Count != 0)
            {
                // Create the columns
                ColumnHeader[] headers = new[] {
                new ColumnHeader(" # ", Alignment.Center, Alignment.Right),
                new ColumnHeader(" DATE ", Alignment.Center, Alignment.Right),
                new ColumnHeader(" USER ", Alignment.Center, Alignment.Right),
                new ColumnHeader(" # EVENT ", Alignment.Center, Alignment.Right),
            };
                Table table = new Table(headers);

                // Add the row of the table
                foreach (Prenotation p in prenotationsList)
                {
                    table.AddRow(p.PrenotationCode, p.DateTime, p.UsernameUser, p.EventCode);
                }

                // Format the table
                table.Config = TableConfiguration.UnicodeAlt();

                Output.WriteLine(table.ToString());
            } else Console.WriteLine("There are no Prenotations in the DataBase!");
        }

        /*
         * Draw a table of Tickets
         */
        public static void Ticket(List<Ticket> ticketsList)
        {
            if (ticketsList.Count != 0)
            {
                // Create the columns
                ColumnHeader[] headers = new[] {
                new ColumnHeader(" # PRENOTATION ", Alignment.Center, Alignment.Right),
                new ColumnHeader(" PRENOTATION DATE ", Alignment.Center, Alignment.Right),
                new ColumnHeader(" USER ", Alignment.Center, Alignment.Right),
                new ColumnHeader(" # EVENT ", Alignment.Center, Alignment.Right),
                new ColumnHeader(" FILM TITLE ", Alignment.Center, Alignment.Right),
                new ColumnHeader(" EVENT DATE ", Alignment.Center, Alignment.Right),
                new ColumnHeader(" HALL ", Alignment.Center, Alignment.Right),
                new ColumnHeader(" PRICE ", Alignment.Center, Alignment.Right),
                new ColumnHeader(" PLACE NUMBER ", Alignment.Center, Alignment.Right),
            };
                Table table = new Table(headers);

                // Add the row of the table
                foreach (Ticket t in ticketsList)
                {
                    Prenotation p = t.Prenotation;
                    Event e = t.Event;
                    Film f = t.Film;
                    Reservation r = t.Reservation;

                    table.AddRow(p.PrenotationCode, p.DateTime, p.UsernameUser, p.EventCode,
                        f.Title, e.DateTime.ToShortDateString() + " " + e.DateTime.ToShortTimeString(),
                        e.HallCode, e.Price + "€", r.PlaceNumber);
                }

                // Format the table
                table.Config = TableConfiguration.UnicodeAlt();

                Output.WriteLine(table.ToString());

            } else Console.WriteLine("There are no Tickets in the DataBase!");
        }
    }
}
