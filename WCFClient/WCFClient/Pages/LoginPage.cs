using EasyConsole;
using System;

namespace WCFClient.Pages
{
    class LoginPage : Page
    {
        public LoginPage(Program program)
            : base("Login", program)
        {
        }

        public override void Display()
        {
            base.Display();

            /*
             * Reset the Session (when the User go back in the pages)
             */
            SessionManager.Reset();

            /* 
             * Choose the type of Login (User - Admin)
             */
            UserType input = Input.ReadEnum<UserType>("Select the type of user you want to login: ");
            if (input.Equals(UserType.Back))
                Program.NavigateHome();
            SessionManager.SetAdmin(input);
            Output.WriteLine(ConsoleColor.Green, "\n{0} Login: ", input);

            /* 
             * Login Form
             * 
             * Every input must be valid and checked. The password is hashed with 
             * a MD5 algorithm before to be stored in the database, for a security reason.
             */
            Output.WriteLine("--------- LOGIN ----------");
            string username = Input.ReadString("Username: ");
            // Navigate back if User type "\\" on first input
            if (username.Contains("\\"))
                Program.NavigateBack();
            username = Controls.CheckUserInput("Username", username);
            Console.Write("Password: ");
            string password = Controls.InputPassword();
            string hashedPassword = EasyEncryption.MD5.ComputeMD5Hash(Controls.CheckUserInput("Password", password));

            /*
             * Send data to Database
             */
            try
            {
                if (SessionManager.GetServiceClient().Login(SessionManager.IsAdmin(), username, hashedPassword))
                {
                    SessionManager.SetUser(SessionManager.GetServiceClient().GetUser(SessionManager.IsAdmin(), username));
                }
            } catch {
                Console.WriteLine("Error! Retry later!");
            }


            /*
             * Go to the Menu
             */
            if (SessionManager.GetUser() != null) {                         // Check Login
                Output.WriteLine("\nLogin Successful");
                Input.ReadString("Press [Enter] to navigate to your menu");
                switch (SessionManager.IsAdmin()){                          // Check User Type
                    case true:
                        Program.NavigateTo<AdminPage>();
                        break;
                    case false:
                        Program.NavigateTo<UserPage>();
                        break;
                }
            }
            Output.WriteLine("\nLogin Failed\n");

            /*
             * Navigate back
             */
            Input.ReadString("Press [Enter] to navigate back");
            Program.NavigateHome();
        }
    }
}