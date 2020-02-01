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

            /*
             * Change output encoding to allow special 
             * characters output such as € 
             */ 
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            /*
             * Get data from the Database
             */
            Output.WriteLine("EVENTS LIST: ");
            TablePrinter.Event(SessionManager.GetServiceClient().GetEventsList());

            /*
             * Navigate back
             */
            Input.ReadString("Press [Enter] to navigate back");
            Program.NavigateBack();
        }
    }
}