using EasyConsole;
using System;

namespace WCFClient.Pages
{
    class AddFilm : Page
    {
        public AddFilm(Program program)
            : base("Inserimento Film", program)
        {
        }

        public override void Display()
        {
            base.Display();

            /* 
             * Add Film Form
             * 
             * Every input must be valid and checked.
             */
            Output.WriteLine("------ ADD NEW FILM ------- ");
            string title = Input.ReadString("Title (max 50 characters): ");
            title = Controls.CheckFilm("Titolo", title);
            string _year = Input.ReadString("Year: ");
            int year = Controls.CheckInt(_year);
            string director = Input.ReadString("Regia (max 30 caratteri): ");
            director = Controls.CheckFilm("Regia", director);
            string _duration = Input.ReadString("Durata: ");
            int duration = Controls.CheckInt(_duration);
            string release_date = Input.ReadString("Data di Uscita: ");
            DateTime releaseDate = Controls.CheckDate(release_date);
            string genre = Input.ReadString("Genere (max 20 caratteri): ");
            genre = Controls.CheckFilm("Genere", genre);

            /*
             * Send data to Database
             */
            if(SessionManager.wcfClient.AddFilm(title, year, director, duration, releaseDate, genre))
                Output.WriteLine("\nFILM INSERTION SUCCESSFUL\n");
            else Output.WriteLine("\nFILM INSERTION FAILED!\n");

            /*
             * Navigate back
             */
            Input.ReadString("Press [Enter] to navigate back");
            Program.NavigateBack();
        }
    }
}