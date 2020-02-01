using EasyConsole;
using System;

namespace WCFClient.Pages
{
    class ShowUserTickets : Page
    {
        public ShowUserTickets(Program program)
            : base("Show User Tickets", program)
        {
        }

        public override void Display()
        {
            base.Display();

            /*
             * Change output encoding to allow special 
             * characters output such as € 
             */
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            /*
             * Get data from the Database
             */
            Output.WriteLine("{0} TICKETS: ", SessionManager.GetUser().Username);
            TablePrinter.Ticket(SessionManager.GetServiceClient().GetTicketsList(SessionManager.GetUser().Username));

            /*
             * Navigate back
             */
            Input.ReadString("Press [Enter] to navigate back");
            Program.NavigateBack();
        }
    }
}


                                        