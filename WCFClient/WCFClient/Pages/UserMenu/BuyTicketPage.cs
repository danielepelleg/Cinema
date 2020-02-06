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

            Console.BackgroundColor = ConsoleColor.Magenta;
            Output.WriteLine(ConsoleColor.White, "--------== {0} ==--------\n", base.Title);
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;

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
                if (SessionManager.GetServiceClient().GetShowsList().Count != 0) {
                    Output.WriteLine("SHOWS LIST: ");
                    TablePrinter.Show(SessionManager.GetServiceClient().GetShowsList());

                    DateTime dateTime = DateTime.Now;
                    string event_code = Input.ReadString("\nChoose the code of the show you want to buy the ticket: ");
                    // Navigate back if User type "\\" on first input
                    if (event_code.Contains("\\"))
                        Program.NavigateBack();
                    int eventCode = Controls.CheckIntForeignKey(event_code, "Evento");

                    /*
                     * Show the User the Hall
                     */
                    Output.WriteLine("Places in the Hall:\n{0}",
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
                    int place_Number = Controls.CheckInt(place_number);
                    int placeNumber = Controls.CheckPlace(eventCode.ToString(), place_Number.ToString());

                    /*
                     * Add a New Prenotation
                     */
                    if (SessionManager.GetServiceClient().AddPrenotation(dateTime, SessionManager.GetUser().Username, eventCode, placeNumber)) { 
                        Output.WriteLine("\nYour Prenotation request success!");
                    }
                    else Output.WriteLine("\nPrenotation request failed! Retry!");

                }
                else Console.WriteLine("There are no Shows in the DB!\n");
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