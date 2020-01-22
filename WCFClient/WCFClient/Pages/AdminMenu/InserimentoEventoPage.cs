using EasyConsole;
using System;

namespace WCFClient.Pages
{
    class InserimentoEvento : Page
    {
        public InserimentoEvento(Program program)
            : base("Inserimento Evento", program)
        {
        }

        public override void Display()
        {
            base.Display();

            var wcfClient = new ServiceReference1.Service1Client(); //WCF CLIENT CREATO
            Output.WriteLine("------ INSERIMENTO EVENTO ------- ");

            // Definisco le variabili che andranno a prendere i dati inseriti. Essi verranno controllati nel tipo e nella
            // validità (ad esempio lunghezza) prima di essere passati al database.
            string data_e_ora1, codice_film1, codice_sala1, prezzo1 = string.Empty;

            Output.WriteLine("ELENCO FILM:");
            Output.WriteLine("{0}", wcfClient.Visualizzazione_elenco_film() + "\n");
            Console.WriteLine("ELENCO SALE:");
            Console.WriteLine("{0}", wcfClient.VisualizzazioneSale());

            Console.WriteLine("--------- Inserimento Dati Evento ----------");
            data_e_ora1 = Input.ReadString("Data e Ora dell'Evento: ");
            DateTime data_e_ora = Cinema.MainProgram.Datecheck(data_e_ora1);
            codice_film1 = Input.ReadString("Codice Film Proiettato: ");
            int codice_film = Cinema.MainProgram.Intcheck(codice_film1);
            codice_film = Cinema.MainProgram.FKCheck(Convert.ToString(codice_film), "Film");
            codice_sala1 = Input.ReadString("Codice Sala Spettacolo: ");
            int codice_sala = Cinema.MainProgram.Intcheck(codice_sala1);
            codice_sala = Cinema.MainProgram.FKCheck(Convert.ToString(codice_sala), "Sala");
            prezzo1 = Input.ReadString("Inserire il Prezzo del biglietto (esempio: 8,50): ");
            decimal prezzo = Cinema.MainProgram.Decimalcheck(prezzo1);
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            // Inserimento Evento nel Database.
            try
            {
                Console.Out.WriteLine("{0}", wcfClient.InserimentoEvento(Cinema.MainProgram.Global.currentusername, data_e_ora, codice_film, codice_sala, prezzo));
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