using EasyConsole;
using System;

namespace WCFClient.Pages
{
    class DeleteEvent : Page
    {
        public DeleteEvent(Program program)
            : base("Delete Event", program)
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

            try {
                /*
                 * Show the Admin the Events
                 */
                if (SessionManager.GetServiceClient().GetEventsList().Count != 0)
                {
                    Output.WriteLine("EVENTS LIST: ");
                    TablePrinter.Event(SessionManager.GetServiceClient().GetEventsList());

                    /* 
                     * Delete Event Form
                     * 
                     * Every Primary Key input must be valid.
                     * When a Event is deleted, the Prenotations and the Reservations
                     * linked to it are deleted too.
                     */
                    Output.WriteLine("\n------ DELETE EVENT ------- ");
                    string event_code = Input.ReadString("Insert the Code of the Event to delete: ");
                    int event_Code = Controls.CheckInt(event_code);
                    int eventCode = Controls.CheckIntForeignKey(event_Code.ToString(), "Evento");

                    /*
                     * Send data to Database
                     */
                    if (SessionManager.GetServiceClient().DeleteEvent(eventCode))
                        Output.WriteLine("\nEVENT CANCELLATION SUCCESS!\n");
                    else Output.WriteLine("\nEVENT CANCELLATION FAILED! Retry!\n");

                } else Console.WriteLine("There are no Events in the DataBase!");
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