using EasyConsole;
using System;

namespace WCFClient.Pages
{
    class AddEventPage : Page
    {
        public AddEventPage(Program program)
            : base("Inserimento Evento", program)
        {
        }

        public override void Display()
        {
            base.Display();
            Output.WriteLine("------ ADD EVENT ------- ");

            // Definisco le variabili che andranno a prendere i dati inseriti. Essi verranno controllati nel tipo e nella
            // validità (ad esempio lunghezza) prima di essere passati al database.
            string data_e_ora1, codice_film1, codice_sala1, prezzo1 = string.Empty;

            Output.WriteLine("ELENCO FILM:");
            Output.WriteLine("{0}", SessionManager.wcfClient.Visualizzazione_elenco_film() + "\n");
            Console.WriteLine("ELENCO SALE:");
            Console.WriteLine("{0}", SessionManager.wcfClient.VisualizzazioneSale());

            Console.WriteLine("--------- Inserimento Dati Evento ----------");
            data_e_ora1 = Input.ReadString("Data e Ora dell'Evento: ");
            DateTime data_e_ora = Cinema.MainProgram.CheckDate(data_e_ora1);
            codice_film1 = Input.ReadString("Codice Film Proiettato: ");
            int codice_film = Cinema.MainProgram.CheckInt(codice_film1);
            codice_film = Cinema.MainProgram.GetPrimaryKey(Convert.ToString(codice_film), "Film");
            codice_sala1 = Input.ReadString("Codice Sala Spettacolo: ");
            int codice_sala = Cinema.MainProgram.CheckInt(codice_sala1);
            codice_sala = Cinema.MainProgram.GetPrimaryKey(Convert.ToString(codice_sala), "Sala");
            prezzo1 = Input.ReadString("Inserire il Prezzo del biglietto (esempio: 8,50): ");
            decimal prezzo = Cinema.MainProgram.CheckDecimal(prezzo1);
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            // Inserimento Evento nel Database.
            try
            {
                Console.Out.WriteLine("{0}", SessionManager.wcfClient.AddEvent(SessionManager.currentusername, data_e_ora, codice_film, codice_sala, prezzo));
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