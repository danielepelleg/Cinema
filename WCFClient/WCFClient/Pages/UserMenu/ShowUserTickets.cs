using EasyConsole;
using System;

namespace WCFClient.Pages
{
    class ShowUserTickets : Page
    {
        public ShowUserTickets(Program program)
            : base("My Tickets", program)
        {
        }

        public override void Display()
        {
            base.Display();
            Console.BackgroundColor = ConsoleColor.Magenta;
            Output.WriteLine(ConsoleColor.White, "--------== {0} ==--------\n", base.Title);
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            

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
                Output.WriteLine("{0} Tickets: ", SessionManager.GetUser().Username);
                TablePrinter.Ticket(SessionManager.GetServiceClient().GetTicketsList(SessionManager.GetUser().Username));
            }
            catch {
                Console.WriteLine("Server Unreacheable, Retry later!");
            }


            /*
             * Navigate back
             */
            Input.ReadString("Press [Enter] to navigate back");
            Program.NavigateBack();
        }
    }
}


                                        