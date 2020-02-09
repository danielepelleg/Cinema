using WCFClient.Pages;
using EasyConsole;

namespace WCFClient
{
    class CinemaProgram : Program
    {
        public CinemaProgram()
            : base("Cinema", breadcrumbHeader: true)
        {
            /*
             * Add Menu and Pages to the program
             */ 
            AddPage(new MainPage(this));
            AddPage(new RegistrationPage(this));
            AddPage(new LoginPage(this));
            AddPage(new AdminPage(this));
            AddPage(new UserPage(this));
            AddPage(new AddFilm(this));
            AddPage(new AddEvent(this));
            AddPage(new DeleteUser(this));
            AddPage(new DeleteFilm(this));
            AddPage(new DeleteEvent(this));
            AddPage(new DeletePrenotation(this));
            AddPage(new EditUserPage(this));
            AddPage(new ShowUsers(this));
            AddPage(new ShowSubscribersPage(this));
            AddPage(new ShowFilm(this));
            AddPage(new ShowEvents(this));
            AddPage(new ShowPrenotations(this));
            AddPage(new BuyTicket(this));
            AddPage(new GetSubscription(this));
            AddPage(new DeleteSubscription(this));
            AddPage(new ShowUserTickets(this));

            /*
             * Default Page when Program starts
             */ 
            SetPage<MainPage>();
        }
    }
}