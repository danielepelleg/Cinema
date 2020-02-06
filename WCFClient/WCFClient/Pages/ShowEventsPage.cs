using EasyConsole;
using System;

namespace WCFClient.Pages
{
    class ShowEvents : Page
    {
        public ShowEvents(Program program)
            : base("Show Events", program)
        {
        }

        public override void Display()
        {
            base.Display();

            if (SessionManager.IsAdmin()) {
                Console.BackgroundColor = ConsoleColor.Blue;
                Output.WriteLine(ConsoleColor.White, "--------== {0} ==--------\n", base.Title);
                Console.BackgroundColor = ConsoleColor.Black;
            }
            else {
                Console.BackgroundColor = ConsoleColor.Magenta;
                Output.WriteLine(ConsoleColor.White, "--------== {0} ==--------\n", base.Title);
                Console.ForegroundColor = ConsoleColor.White;
                Console.BackgroundColor = ConsoleColor.Black;
            }

            /*
             * Change output encoding to allow special 
             * characters output such as € 
             */ 
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            /*
             * Get data from the Database
             */
            try
            {
                Output.WriteLine("EVENTS LIST: ");
                TablePrinter.Event(SessionManager.GetServiceClient().GetEventsList());
            
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