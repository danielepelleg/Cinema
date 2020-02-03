using EasyConsole;
using System;

namespace WCFClient.Pages
{
    class DeleteUser : Page
    {

        public DeleteUser(Program program)
            : base("Delete User", program)
        {
        }

        public override void Display()
        {
            base.Display();

            try
            {
                /*
                 * Show the Admin the Film
                 */
                Output.WriteLine("USERS LIST: ");
                TablePrinter.User(SessionManager.GetServiceClient().GetUsersList());

                /* 
                 * Delete User Form
                 * 
                 * When a User is deleted, the prenotation and the reservations
                 * linked to him are deleted too
                 */
                Output.WriteLine("\n------ DELETE USER ------- ");
                string username = Input.ReadString("Insert the Username of the User to delete: ");
                username = Controls.CheckUserInput("Username", username);

                /*
                 * Send data to Database
                 */
                if (SessionManager.GetServiceClient().DeleteUser(username))
                    Output.WriteLine("\nUSER CANCELLATION SUCCESS!\n");
                else Output.WriteLine("\nUSER CANCELLATION FAILED! Retry!\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception Type: {0}", ex.GetType());
                Console.WriteLine("Message: {0}", ex.Message);
            }

            /*
             * Navigate back
             */
            Input.ReadString("Press [Enter] to navigate back");
            Program.NavigateBack();
        }
    }

}
