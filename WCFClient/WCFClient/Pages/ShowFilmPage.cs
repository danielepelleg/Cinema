using EasyConsole;
using System;
using System.Collections.Generic;
using WCFClient.ServiceReference1;

namespace WCFClient.Pages
{
    class ShowFilm : Page
    {
        public ShowFilm(Program program)
            : base("Show Film", program)
        {
        }

        public override void Display()
        {
            base.Display();

            if (SessionManager.IsAdmin())
            {
                Console.BackgroundColor = ConsoleColor.Blue;
                Output.WriteLine(ConsoleColor.White, "--------== {0} ==--------\n", base.Title);
                Console.BackgroundColor = ConsoleColor.Black;
            }
            else
            {
                Console.BackgroundColor = ConsoleColor.Magenta;
                Output.WriteLine(ConsoleColor.White, "--------== {0} ==--------\n", base.Title);
                Console.ForegroundColor = ConsoleColor.White;
                Console.BackgroundColor = ConsoleColor.Black;
            }

            /*
             * Get data from the Database
             */
            try
            {
                Output.WriteLine("FILM LIST: ");
                TablePrinter.Film(SessionManager.GetServiceClient().GetFilmList());
            }
            catch {
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