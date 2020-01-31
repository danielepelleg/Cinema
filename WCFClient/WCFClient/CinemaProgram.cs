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
            AddPage(new AddFilm(this));
            AddPage(new AddEvent(this));
            AddPage(new DeleteFilm(this));
            AddPage(new ShowEvents(this));
            AddPage(new ShowFilm(this));
            AddPage(new TicketPrenotation(this));
            AddPage(new ShowUserTickets(this));

            // Setto la pagina di default all'avvio del programma
            SetPage<MainPage>();
        }
    }
}