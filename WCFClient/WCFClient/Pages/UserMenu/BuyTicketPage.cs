using EasyConsole;
using System;

namespace WCFClient.Pages
{
    class BuyTicket : Page
    {
        public BuyTicket(Program program)
            : base("Buy Ticket", program)
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
             * Show the User the Events 
             */
            try
            {
                Output.WriteLine("Choose the event you want to buy the ticket: ");
                Output.WriteLine("EVENTS LIST: ");
                TablePrinter.Event(SessionManager.GetServiceClient().GetEventsList());

                DateTime dateTime = DateTime.Now;
                string event_code = Input.ReadString("\nChoose the code of the event you want to buy the ticket: ");
                int eventCode = Controls.CheckInt(event_code);
                eventCode = Controls.GetPrimaryKey(Convert.ToString(eventCode), "Evento");

                /*
                 * Show the User the Hall
                 */
                Output.WriteLine("Available places in the Hall:\n{0}",
                    SessionManager.GetServiceClient().DrawHall(eventCode));

                /*
                 * Show the User the available Places
                 */
                Output.WriteLine("Available Places:");
                TablePrinter.PlaceNumber(SessionManager.GetServiceClient().GetAvailablePlacesList(eventCode));
                /*
                 * Let the User choose the place to buy
                 */
                string place_number = Input.ReadString("Choose the place you want to buy: ");
                int placeNumber = Controls.CheckInt(place_number);
                placeNumber = Controls.CheckPlace(eventCode.ToString(), placeNumber.ToString());

                /*
                 * Add a New Prenotation
                 */
                if (SessionManager.GetServiceClient().AddPrenotation(dateTime, SessionManager.GetUser().Username, eventCode, placeNumber))
                    Output.WriteLine("\nYour Prenotation request success!");
                else Output.WriteLine("\nPrenotation request failed! Retry!");
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