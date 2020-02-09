using EasyConsole;
using System;

namespace WCFClient.Pages
{
    class DeleteSubscription : Page
    {
        public DeleteSubscription(Program program)
            : base("Delete the Subscription", program)
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
            
            Decision input = Input.ReadEnum<Decision>("\nWould you like to delete your subscription?");

            if (input.ToString().Equals("YES")) {
                try {
                    SessionManager.SetSubscription(SessionManager.GetServiceClient().GetSubscription(SessionManager.GetUser().Username));
                    if (SessionManager.IsSubscribed()) {
                        if (SessionManager.GetServiceClient().DeleteSubscription(SessionManager.GetUser().Username)) {
                            SessionManager.SetSubscription(SessionManager.GetServiceClient().GetSubscription(SessionManager.GetUser().Username));
                            Output.WriteLine("\nUNSUBSCRIPTION SUCCESSFUL\n");
                        }
                        else Output.WriteLine("\nUNSUBSCRIPTION FAILED!\n");
                    }
                    else Output.WriteLine("\nYou don't have a subscription!\n");
                }
                catch {
                    Console.WriteLine("Error! Retry later!");
                }
            }

            /*
             * Navigate back
             */
            Input.ReadString("\nPress [Enter] to navigate back");
            Program.NavigateBack();
        }
    }
}