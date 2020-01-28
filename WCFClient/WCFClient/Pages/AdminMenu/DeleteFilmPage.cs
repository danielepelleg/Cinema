using EasyConsole;
using System;

namespace WCFClient.Pages
{
    class DeleteFilm : Page
    {
        public DeleteFilm(Program program)
            : base("Inserimento Film", program)
        {
        }

        public override void Display()
        {
            base.Display();

            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Output.WriteLine("ELENCO EVENTI: ");
            try {
                // Richiamo la funzione server che mi mostra gli spettacoli presenti al cinema in una tabella
                Console.Out.WriteLine("{0}", Global.wcfClient.Visualizzazione_elenco_film());
            }
            catch {
                Cinema.MainProgram.Errormessage();
            }
            
            Output.WriteLine("\n------ DELETE FILM ------- ");


            string filmCode1 = Input.ReadString("Inserisci il codice del film da eliminare: ");
            int filmCode = Cinema.MainProgram.Intcheck(filmCode1);

            // Inserimento Film nel Database.
            try
            {
                bool success = Global.wcfClient.DeleteFilm(filmCode);
                if (success) Output.WriteLine("CANCELLAZIONE FILM AVVENUTO CON SUCCESSO\n");
                else Output.WriteLine("ERRORE CANCELLAZIONE FILM\n RIPROVARE!\n");
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