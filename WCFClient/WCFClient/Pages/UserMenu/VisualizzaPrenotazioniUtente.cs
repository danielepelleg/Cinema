using EasyConsole;
using System;

namespace WCFClient.Pages
{
    class VisualizzaPrenotazioniUtente : Page
    {
        public VisualizzaPrenotazioniUtente(Program program)
            : base("Visualizza Prenotazioni Utente", program)
        {
        }

        public override void Display()
        {
            base.Display();
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("STORICO PRENOTAZIONI:");
            try
            {
                // Richiamo la funzione server che mi mostra gli spettacoli presenti al cinema in una tabella
                Console.Out.WriteLine("{0}", Global.wcfClient.Visualizzazione_elenco_Prenotazioni(Cinema.MainProgram.Global.currentusername));
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


                                        