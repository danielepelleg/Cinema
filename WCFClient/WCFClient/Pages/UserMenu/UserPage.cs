using EasyConsole;
using System;

namespace WCFClient.Pages
{
    class UserPage : MenuPage {
        public UserPage(Program program)
            : base("User Functions", program,
                  new Option("Show Events Scheduled ", () => program.NavigateTo<ShowEvents>()),
                  new Option("Show Film Scheduled ", () => program.NavigateTo<ShowFilm>()),
                  new Option("Buy a Ticket", () => program.NavigateTo<BuyTicket>()),
                  new Option("My Tickets", () => program.NavigateTo<ShowUserTickets>()))
        {
        }
        public override void Display() {
            Console.BackgroundColor = ConsoleColor.DarkMagenta;
            Output.WriteLine(ConsoleColor.White, "--------== {0} ==--------", base.Title);
            Output.WriteLine(ConsoleColor.White, "\n | User: {0} \n", SessionManager.GetUser().Username);
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
            base.Display();
            
        }
    }
}