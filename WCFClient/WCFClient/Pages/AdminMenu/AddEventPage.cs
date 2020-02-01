using EasyConsole;
using System;

namespace WCFClient.Pages
{
    class AddEvent : Page
    {
        public AddEvent(Program program)
            : base("Add new Event", program)
        {
        }

        public override void Display()
        {
            base.Display();

            try
            {
           /*
            * Show the user the Film and the Hall List
            */
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
                DateTime dateTime = Controls.CheckDate(date_time);
                string film_code = Input.ReadString("Codice Film Proiettato: ");
                int filmCode = Controls.CheckInt(film_code);
                filmCode = Controls.GetPrimaryKey(filmCode.ToString(), "Film");
                string hall_code = Input.ReadString("Hall code of the Event: ");
                int hallCode = Controls.CheckInt(hall_code);
                hallCode = Controls.GetPrimaryKey(hallCode.ToString(), "Sala");
                string _price = Input.ReadString("Insert the Price (ex: 8,50): ");
                decimal price = Controls.CheckDecimal(_price);

                /*
                 * Send data to Database
                 */
                if (SessionManager.GetServiceClient().AddEvent(SessionManager.GetUser().Username, dateTime, filmCode, hallCode, price))
                    Output.WriteLine("\nEVENT INSERTION SUCCESSFUL\n");
                else Output.WriteLine("\nEVENT INSERTION FAILED!\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception Type: {0}", ex.GetType());
                Console.WriteLine("Message: {0}", ex.Message);
            }
           
            /*
             * Navigate back
             */
            Input.ReadString("Press [Enter] to navigate back");
            Program.NavigateBack();
        }

    }
}