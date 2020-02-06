using EasyConsole;
using System;

namespace WCFClient.Pages
{
    class RegistrationPage : Page
    {
        
        public RegistrationPage(Program program)
            : base("Registration", program)
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
             * Choose the type of Registration (User - Admin)
             */
            UserType input = Input.ReadEnum<UserType>("Select the type of user you want to register: ");
            if (input.Equals(UserType.Back))
                Program.NavigateHome();
            SessionManager.SetAdmin(input);
            Output.WriteLine(ConsoleColor.Green, "\n{0} Sign In: ", input);

            /* 
             * Registration Form
             * 
             * Every input must be valid and checked. The password is hashed with 
             * a MD5 algorithm before to be stored in the database, for a security reason.
             */
            Output.WriteLine("--------- REGISTRATION ----------");
            string username = Input.ReadString("Username (max 30 characters): ");
            // Navigate back if User type "\\" on first input
            if (username.Contains("\\"))
                Program.NavigateBack();
            username = Controls.CheckUserInput("Username", username);  
            string password = Input.ReadString("Password (max 32 characters): ");
            string hashedPassword = EasyEncryption.MD5.ComputeMD5Hash(Controls.CheckUserInput("Password", password));
            string name = Input.ReadString("Name (max 20 characters): ");
            name = Controls.CheckUserInput("Nome", name);
            string surname = Input.ReadString("Surname (max 20 characters): ");
            surname = Controls.CheckUserInput("Cognome", surname);

            /*
             * Send data to Database
             */
            try
            {
                if (SessionManager.GetServiceClient().Registration(SessionManager.IsAdmin(), username, hashedPassword, name, surname))
                    Output.WriteLine("REGISTRATION SUCCESS!\n Come back to login\n");
                else Output.WriteLine("REGISTRATION ERROR\n Retry!\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception Type: {0}", ex.GetType());
                Console.WriteLine("Message: {0}", ex.Message);
            }
            
            /*
             * Navigate back
             */
            Input.ReadString("Press [Enter] to navigate home");
            Program.NavigateHome();
        }
    }

}
