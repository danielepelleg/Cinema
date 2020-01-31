using EasyConsole;
using System;

namespace WCFClient.Pages
{
    class InserimentoFilm : Page
    {
        public InserimentoFilm(Program program)
            : base("Inserimento Film", program)
        {
        }

        public override void Display()
        {
            base.Display();
            Output.WriteLine("------ INSERIMENTO FILM ------- ");

            // Definisco le variabili che andranno a prendere i dati inseriti. Essi verranno controllati nel tipo e nella
            // validità (ad esempio lunghezza) prima di essere passati al database.
            string titolo, anno1, regia, durata1, datauscita1, genere = string.Empty;

            titolo = Input.ReadString("Titolo (max 50 caratteri): ");
            titolo = Cinema.MainProgram.CheckFilm("Titolo", titolo);
            anno1 = Input.ReadString("Anno di Produzione: ");
            int anno = Cinema.MainProgram.Intcheck(anno1);
            regia = Input.ReadString("Regia (max 30 caratteri): ");
            regia = Cinema.MainProgram.CheckFilm("Regia", regia);
            durata1 = Input.ReadString("Durata: ");
            int durata = Cinema.MainProgram.Intcheck(durata1);
            datauscita1 = Input.ReadString("Data di Uscita: ");
            DateTime datauscita = Cinema.MainProgram.CheckDate(datauscita1);
            genere = Input.ReadString("Genere (max 20 caratteri): ");
            genere = Cinema.MainProgram.CheckFilm("Genere", genere);

            // Inserimento Film nel Database.
            try
            {
                bool success = Global.wcfClient.AddFilm(titolo, anno, regia, durata, datauscita, genere);
                if (success) Output.WriteLine("INSERIMENTO FILM AVVENUTO CON SUCCESSO\n");
                else Output.WriteLine("ERRORE INSERIMENTO FILM\n RIPROVARE!\n");
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