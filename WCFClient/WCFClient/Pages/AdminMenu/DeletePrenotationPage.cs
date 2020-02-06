using EasyConsole;
using System;

namespace WCFClient.Pages
{
    class DeletePrenotation : Page
    {
        public DeletePrenotation(Program program)
            : base("Delete Prenotation", program)
        {
        }

        public override void Display()
        {
            base.Display();

            Console.BackgroundColor = ConsoleColor.Blue;
            Output.WriteLine(ConsoleColor.White, "--------== {0} ==--------\n", base.Title);
            Console.BackgroundColor = ConsoleColor.Black;

            try
            {
                if (SessionManager.GetServiceClient().GetPrenotationsList().Count != 0)
                {
                    /*
                  * Show the Admin the Prenotations
                  */
                    Output.WriteLine("PRENOTATIONS LIST: ");
                    TablePrinter.Prenotation(SessionManager.GetServiceClient().GetPrenotationsList());

                    /* 
                     * Delete Film Form
                     * 
                     * Every Primary Key input must be valid.
                     * When a Prenotation is deleted the Reservations 
                     * linked to it are deleted too.
                     */
                    Output.WriteLine("\n------ DELETE PRENOTATION ------- ");
                    string prenotation_code = Input.ReadString("Insert the Code of the Prenotation to delete: ");
                    // Navigate back if User type "\\" on first input
                    if (prenotation_code.Contains("\\"))
                        Program.NavigateBack();
                    int prenotationCode = Controls.CheckIntForeignKey(prenotation_code, "Prenotazione");

                    /*
                     * Send data to Database
                     */
                    if (SessionManager.GetServiceClient().DeletePrenotation(prenotationCode))
                        Output.WriteLine("\nPRENOTATION CANCELLATION SUCCESS!\n");
                    else Output.WriteLine("\nPRENOTATION CANCELLATION FAILED! Retry!\n");

                }
                else Console.WriteLine("There are no Prenotations in the DataBase!");
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