using EasyConsole;
using System;
using System.Collections.Generic;
using WCFClient.ServiceReference1;

namespace WCFClient.Pages
{
    class ShowSubscribersPage : Page {
        public ShowSubscribersPage(Program program)
            : base("Show Subscribers", program)
        {
        }

        public override void Display() {

            base.Display();

            Console.BackgroundColor = ConsoleColor.Blue;
            Output.WriteLine(ConsoleColor.White, "--------== {0} ==--------\n", base.Title);
            Console.BackgroundColor = ConsoleColor.Black;

            /*
             * Get data from the Database
             */
            try {
                Output.WriteLine("SUBSCRIBERS LIST: ");
                TablePrinter.Subscriber(SessionManager.GetServiceClient().GetSubscribersList());
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