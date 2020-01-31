using EasyConsole;
using System;

namespace WCFClient.Pages
{
    class PrenotazioneBiglietto : Page
    {
        public PrenotazioneBiglietto(Program program)
            : base("Prenotazione Biglietto", program)
        {
        }
        
        public override void Display()
        {
            base.Display();
   
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Output.WriteLine("Selezionare l'evento di cui si vuole acquistare il biglietto: ");
            Output.WriteLine("ELENCO EVENTI IN PROGRAMMAZIONE:");
            try
            {
                Output.WriteLine("{0}", Global.wcfClient.Visualizzazione_elenco_eventi());
            }
            catch
            {
                Cinema.MainProgram.Errormessage();
            }
            string codice_evento1, numero_posto1 = string.Empty;
            DateTime data_e_ora = DateTime.Now;
            codice_evento1 = Input.ReadString("Scegliere il codice corrispondente all'evento che si vuole prenotare: ");
            int codice_evento = Cinema.MainProgram.Intcheck(codice_evento1);
            codice_evento = Cinema.MainProgram.GetFK(Convert.ToString(codice_evento), "Evento");
            try
            {
                Output.WriteLine("Rappresentazione della disposizione dei posti in sala:{0}", Global.wcfClient.RappresentaSale(codice_evento));
            }
            catch
            {
                Cinema.MainProgram.Errormessage();
            }
            Output.WriteLine("Elenco dei posti disponibili:");
            try
            {
                Output.WriteLine("{0}", Global.wcfClient.VisualizzazionePostiDisponibili(codice_evento));
            }
            catch
            {
                Cinema.MainProgram.Errormessage();
            }
            numero_posto1 = Input.ReadString("Scegliere il numero del posto che si vuole prenotare: ");
            int numero_posto = Cinema.MainProgram.Intcheck(numero_posto1);
            numero_posto = Cinema.MainProgram.CheckPlace(Convert.ToString(codice_evento), Convert.ToString(numero_posto));

            // Inserimento Film nel Database.
            try
            {
                Console.Out.WriteLine("{0}", Global.wcfClient.InserimentoPrenotazione(data_e_ora, Cinema.MainProgram.Global.currentusername, codice_evento, numero_posto));
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