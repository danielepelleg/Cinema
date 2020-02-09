using EasyConsole;
using System;

namespace WCFClient.Pages
{
    class GetSubscription : Page
    {
        public GetSubscription(Program program)
            : base("Commit to a Subscription", program)
        {
        }

        public override void Display() {
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

            Output.WriteLine("The Cinema give you the opportunity to commit to a subscription. Buying an EasyCard you " +
                "can get all the films\n you want at 6,40€ each.");
            Decision input = Input.ReadEnum<Decision>("\nWould you like to sign up for a subscription?");

            if (input.ToString().Equals("YES")) {
                try{
                    SessionManager.SetSubscription(SessionManager.GetServiceClient().GetSubscription(SessionManager.GetUser().Username));
                    if (!SessionManager.IsSubscribed()){
                        if (SessionManager.GetServiceClient().AddSubscription(SessionManager.GetUser().Username)) {
                            SessionManager.SetSubscription(SessionManager.GetServiceClient().GetSubscription(SessionManager.GetUser().Username));
                            Output.WriteLine("\nSUBSCRIPTION SUCCESSFUL\n");
                        }
                        else Output.WriteLine("\nSUBSCRIPTION FAILED!\n");
                    }
                    else Output.WriteLine("\nYou already have a subscription!\n");
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