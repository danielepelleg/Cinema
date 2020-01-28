using WCFClient.Pages;
using EasyConsole;

namespace Cinema
{
    class CinemaProgram : Program
    {
        public CinemaProgram()
            : base("Cinema", breadcrumbHeader: true)
        {
            // Inserisco Menu e Pagine presenti nel programma
            AddPage(new MainPage(this));
            AddPage(new RegistrationPage(this));
            AddPage(new LoginPage(this));
            AddPage(new AdminPage(this));
            AddPage(new UserPage(this));
            AddPage(new InserimentoFilm(this));
            AddPage(new InserimentoEvento(this));
            AddPage(new DeleteFilm(this));
            AddPage(new VisualizzaEvento(this));
            AddPage(new VisualizzaFilm(this));
            AddPage(new PrenotazioneBiglietto(this));
            AddPage(new VisualizzaPrenotazioniUtente(this));

            // Setto la pagina di default all'avvio del programma
            SetPage<MainPage>();
        }
    }
}