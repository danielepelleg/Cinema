using EasyConsole;
using System;

namespace WCFClient.Pages
{
    class AdminPage : MenuPage
    {
        public AdminPage(Program program)
            : base("Admin Functions", program,
                  new Option("Inserimento Film", () => program.NavigateTo<InserimentoFilm>()),
                  new Option("Inserimento Evento", () => program.NavigateTo<InserimentoEvento>()),
                  new Option("Visualizza Eventi", () => program.NavigateTo<VisualizzaEvento>()),
                  new Option("Visualizza Film", () => program.NavigateTo<VisualizzaFilm>()))
        {
        }

        public override void Display()
        {
            Output.WriteLine(ConsoleColor.Blue, "--------- {0} ---------", base.Title);
            base.Display();
        }
    }
}