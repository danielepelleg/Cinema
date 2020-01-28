﻿using EasyConsole;

namespace WCFClient.Pages
{
    class VisualizzaFilm : Page
    {
        public VisualizzaFilm(Program program)
            : base("Visualizza Film", program)
        {
        }

        public override void Display()
        {
            base.Display();

            Output.WriteLine("ELENCO FILM: ");
            try
            {
                // Richiamo la funzione server che mi mostra i film presenti al cinema in una tabella
                Output.WriteLine("{0}", Global.wcfClient.Visualizzazione_elenco_film());

            }
            catch
            {
                Cinema.MainProgram.Errormessage();
            }
            // Naviga Indietro
            Input.ReadString("Press [Enter] to navigate back");
            Program.NavigateBack();
        }
    }
}