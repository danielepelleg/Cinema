using EasyConsole;
using System;

namespace WCFClient.Pages
{
    class AddEvent : Page
    {
        public AddEvent(Program program)
            : base("Add New Event", program)
        {
        }

        public override void Display()
        {
            base.Display();

            Console.BackgroundColor = ConsoleColor.Blue;
            Output.WriteLine(ConsoleColor.White, "--------== {0} ==--------\n", base.Title);
            Console.BackgroundColor = ConsoleColor.Black;

            try
            {
                /*
                 * Show the user the Film and the Hall List
                 */

                Output.WriteLine("LIST OF EVENT ALREADY INSERTED: ");
                TablePrinter.Event(SessionManager.GetServiceClient().GetEventsList());
                Output.WriteLine("FILM LIST:");
                TablePrinter.Film(SessionManager.GetServiceClient().GetFilmList());
                Console.WriteLine("\nHALL LIST:");
                TablePrinter.Hall(SessionManager.GetServiceClient().GetHallsList());

                /* 
                 * Add Event Form
                 * 
                 * Every input must be valid and checked.
                 */
                Output.WriteLine("------ ADD NEW EVENT ------- ");
                string date_time = Input.ReadString("Data and Time of the Event: ");
                // Navigate back if User type "\\" on first input
                if (date_time.Contains("\\"))
                    Program.NavigateBack();
                DateTime dateTime = Controls.CheckDate(date_time);
                string film_code = Input.ReadString("Code of the Film: ");
                int filmCode = Controls.CheckIntForeignKey(film_code, "Film");
                string hall_code = Input.ReadString("Hall code of the Event: ");
                int hallCode = Controls.CheckIntForeignKey(hall_code, "Sala");
                string _price = Input.ReadString("Insert the Price (ex: 8,50): ");
                decimal price = Controls.CheckDecimal(_price);
                
                /*
                 * Send data to Database
                 */
                if (SessionManager.GetServiceClient().AddEvent(SessionManager.GetUser().Username, dateTime, filmCode, hallCode, price))
                    Output.WriteLine("\nEVENT INSERTION SUCCESSFUL\n");
                else Output.WriteLine("\nEVENT INSERTION FAILED!\nCheck that there are no other events at the same time, in the same date and in the same hall!");

            } catch {
                Console.WriteLine("Error! Retry later!");
            }

            /*
             * Navigate back
             */
            Input.ReadString("Press [Enter] to navigate back");
            Program.NavigateBack();
        }

    }
}