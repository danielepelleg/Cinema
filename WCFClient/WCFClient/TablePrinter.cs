using BetterConsoleTables;
using EasyConsole;
using System;
using System.Collections.Generic;
using WCFClient.ServiceReference1;
using System.Text;

namespace WCFClient
{
    /*
     * TablePrinter Class
     * Draw a console table where storing data.
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
                new ColumnHeader(" USERNAME ", Alignment.Right, Alignment.Center),
                new ColumnHeader(" HASHED PASSWORD ", Alignment.Right, Alignment.Center),
                new ColumnHeader(" NAME ", Alignment.Right, Alignment.Center),
                new ColumnHeader(" SURNAME ", Alignment.Right, Alignment.Center),
            };
                Table table = new Table(headers);

                // Add the rows of the table
                foreach (User u in userList)
                {
                    table.AddRow(u.Username, u.Password, u.Name, u.Surname);
                }

                // Format the table
                table.Config = TableConfiguration.UnicodeAlt();

                Output.WriteLine(table.ToString());
            } else Console.WriteLine("There are no Users in the DataBase!");
        }

        /*
        * Draw a table of Users
        */
        public static void Subscriber(List<User> userList)
        {
            if (userList.Count != 0)
            {
                // Create the columns
                ColumnHeader[] headers = new[] {
                new ColumnHeader(" USERNAME ", Alignment.Right, Alignment.Center),
                new ColumnHeader(" NAME ", Alignment.Right, Alignment.Center),
                new ColumnHeader(" SURNAME ", Alignment.Right, Alignment.Center),
            };
                Table table = new Table(headers);

                // Add the rows of the table
                foreach (User u in userList)
                {
                    table.AddRow(u.Username, u.Name, u.Surname);
                }

                // Format the table
                table.Config = TableConfiguration.UnicodeAlt();

                Output.WriteLine(table.ToString());
            }
            else Console.WriteLine("There are no Subscribers in the DataBase!");
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
                new ColumnHeader(" # ", Alignment.Right, Alignment.Center),
                new ColumnHeader(" TITLE ", Alignment.Right, Alignment.Center),
                new ColumnHeader(" YEAR ", Alignment.Right, Alignment.Center),
                new ColumnHeader(" DIRECTION ", Alignment.Right, Alignment.Center),
                new ColumnHeader(" DURATION ", Alignment.Right, Alignment.Center),
                new ColumnHeader(" RELEASE DATE ", Alignment.Right, Alignment.Center),
                new ColumnHeader(" GENRE ", Alignment.Right, Alignment.Center),
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
                new ColumnHeader(" # ", Alignment.Right, Alignment.Center),
                new ColumnHeader(" DATE / TIME ", Alignment.Right, Alignment.Center),
                new ColumnHeader(" HALL ", Alignment.Right, Alignment.Center),
                new ColumnHeader(" # FILM ", Alignment.Right, Alignment.Center),
                new ColumnHeader(" PRICE ", Alignment.Right, Alignment.Center),
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
            } else Console.WriteLine("There are no Events in the DataBase!");
        }

        /*
         * Draw a table of Events
         */
        public static void Show(List<Show> showsList)
        {
            if (showsList.Count != 0)
            {
                // Create the columns
                ColumnHeader[] headers = new[] {
                new ColumnHeader(" # SHOW", Alignment.Right, Alignment.Center),
                new ColumnHeader(" DATE / TIME ", Alignment.Right, Alignment.Center),
                new ColumnHeader(" FILM TITLE ", Alignment.Right, Alignment.Center),
                new ColumnHeader(" # HALL ", Alignment.Right, Alignment.Center),
                new ColumnHeader(" PRICE ", Alignment.Right, Alignment.Center),
                };
                Table table = new Table(headers);

                // Add the row of the table
                foreach (Show s in showsList)
                {
                    table.AddRow(s.Event.EventCode, s.Event.DateTime.ToShortDateString() + " " + s.Event.DateTime.ToShortTimeString(),
                        s.Film.Title, s.Event.HallCode, s.Event.Price + "€");
                }

                // Format the table
                table.Config = TableConfiguration.UnicodeAlt();

                Output.WriteLine(table.ToString());
            }
            else Console.WriteLine("There are no Shows in the DataBase!");
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
                new ColumnHeader(" HALL ", Alignment.Right, Alignment.Center),
                new ColumnHeader(" CAPACITY ", Alignment.Right, Alignment.Center),
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
         * Draw a table of Places
         */
        public static void Place(List<Place> placesList)
        {
            if(placesList.Count != 0)
            {
                // Create the columns
                ColumnHeader[] headers = new[] {
                new ColumnHeader(" # PLACE NUMBER ", Alignment.Center, Alignment.Right),
                new ColumnHeader(" # HALL ", Alignment.Center, Alignment.Right),
            };
                Table table = new Table(headers);

                // Add the row of the table
                foreach (Place p in placesList)
                {
                    table.AddRow(p.PlaceNumber, p.HallCode);
                }

                // Format the table
                table.Config = TableConfiguration.UnicodeAlt();

                Output.WriteLine(table.ToString());
            } else Console.WriteLine("There are no Places in the DataBase!");
        }

        /*
         * Draw a table of Available Places
         */
        public static void PlaceNumber(List<Place> placesList)
        {
            if (placesList.Count != 0)
            {
                // Create the columns
                ColumnHeader[] headers = new[] {
                new ColumnHeader(" PLACE NUMBER ", Alignment.Right, Alignment.Center),
                };
                Table table = new Table(headers);

                // Add the row of the table
                StringBuilder result = new StringBuilder(" ");
                int counter = 0;
                foreach (Place p in placesList)
                {
                    if (counter != placesList.Count-1)
                        result.Append(p.PlaceNumber + ",  ");
                    else result.Append(p.PlaceNumber + " ");
                    counter++;
                }
                table.AddRow(result);

                // Format the table
                table.Config = TableConfiguration.MySqlSimple();

                Output.WriteLine(table.ToString());
            } else Console.WriteLine("There are no Available Places in the DataBase!");
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
                new ColumnHeader(" # ", Alignment.Right, Alignment.Center),
                new ColumnHeader(" DATE ", Alignment.Right, Alignment.Center),
                new ColumnHeader(" USER ", Alignment.Right, Alignment.Center),
                new ColumnHeader(" # EVENT ", Alignment.Right, Alignment.Center),
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
                new ColumnHeader(" # TICKET ", Alignment.Right, Alignment.Center),
                new ColumnHeader(" PRENOTATION DATE ", Alignment.Right, Alignment.Center),
                new ColumnHeader(" USER ", Alignment.Right, Alignment.Center),
                new ColumnHeader(" # EVENT ", Alignment.Right, Alignment.Center),
                new ColumnHeader(" FILM TITLE ", Alignment.Right, Alignment.Center),
                new ColumnHeader(" EVENT DATE ", Alignment.Right, Alignment.Center),
                new ColumnHeader(" HALL ", Alignment.Right, Alignment.Center),
                new ColumnHeader(" PLACE NUMBER ", Alignment.Right, Alignment.Center),
                new ColumnHeader(" PRICE ", Alignment.Right, Alignment.Center),
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
                        e.HallCode, r.PlaceNumber, e.Price + "€");
                }

                // Format the table
                table.Config = TableConfiguration.UnicodeAlt();

                Output.WriteLine(table.ToString());

            } else Console.WriteLine("There are no Tickets in the DataBase!");
        }
    }
}
