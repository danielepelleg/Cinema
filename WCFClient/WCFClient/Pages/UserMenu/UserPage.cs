using EasyConsole;
using System;

namespace WCFClient.Pages
{
    class UserPage : MenuPage
    {
        public UserPage(Program program)
            : base("User Functions", program,
                  new Option("Inserimento Film", () => program.NavigateTo<InserimentoFilm>()),
                  new Option("Visualizza Film", () => program.NavigateTo<VisualizzaFilm>()))
        {
        }
        public override void Display()
        {
            Output.WriteLine(ConsoleColor.Yellow, "--------- {0} ---------", base.Title);
            base.Display();
        }
    }
}