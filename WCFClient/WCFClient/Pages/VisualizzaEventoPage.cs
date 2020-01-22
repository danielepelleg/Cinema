using EasyConsole;
using System;

namespace WCFClient.Pages
{
    class VisualizzaEvento : Page
    {
        public VisualizzaEvento(Program program)
            : base("Visualizza Film", program)
        {
        }

        public override void Display()
        {
            base.Display();

            Console.OutputEncoding = System.Text.Encoding.UTF8;
            var wcfClient = new ServiceReference1.Service1Client(); //WCF CLIENT CREATO
            Output.WriteLine("ELENCO EVENTI: ");
            try
            {
                // Richiamo la funzione server che mi mostra gli spettacoli presenti al cinema in una tabella
                Console.Out.WriteLine("{0}", wcfClient.Visualizzazione_elenco_eventi());
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