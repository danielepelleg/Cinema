using EasyConsole;
using System;

namespace WCFClient.Pages
{
    class UserPage : MenuPage
    {
        public UserPage(Program program)
            : base("User Functions", program,
                  new Option("Visualizza l'elenco degli eventi in programmazione", () => program.NavigateTo<VisualizzaEvento>()),
                  new Option("Prenotazione biglietto", () => program.NavigateTo<PrenotazioneBiglietto>()),
                  new Option("Visualizza prenotazioni utente", () => program.NavigateTo<VisualizzaPrenotazioniUtente>()))
        {
        }
        public override void Display()
        {
            Output.WriteLine(ConsoleColor.Yellow, "--------- {0} ---------", base.Title);
            base.Display();
        }
    }
}