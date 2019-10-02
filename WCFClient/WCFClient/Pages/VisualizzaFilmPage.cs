using EasyConsole;

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

            var wcfClient = new ServiceReference1.Service1Client(); //WCF CLIENT CREATO
            Output.WriteLine("ELENCO FILM: ");

            // Richiamo la funzione server che mi mostra i film presenti al cinema in una tabella
            Output.WriteLine("{0}", wcfClient.Visualizzazione_elenco_film());

            // Naviga Indietro
            Input.ReadString("Press [Enter] to navigate back");
            Program.NavigateBack();
        }
    }
}