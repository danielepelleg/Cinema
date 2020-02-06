using EasyConsole;
using System;
using System.Collections.Generic;
using WCFClient.ServiceReference1;

namespace WCFClient.Pages
{
    class ShowUsers : Page
    {
        public ShowUsers(Program program)
            : base("Show Users", program)
        {
        }

        public override void Display()
        {
            base.Display();

            Console.BackgroundColor = ConsoleColor.Blue;
            Output.WriteLine(ConsoleColor.White, "--------== {0} ==--------\n", base.Title);
            Console.BackgroundColor = ConsoleColor.Black;

            /*
             * Get data from the Database
             */
            try
            {
                Output.WriteLine("USERS LIST: ");
                TablePrinter.User(SessionManager.GetServiceClient().GetUsersList());
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