using EasyConsole;
using System;

namespace WCFClient.Pages
{
    class AdminPage : MenuPage
    {
        public AdminPage(Program program)
            : base("Admin Functions", program,
                  new Option("Add Film", () => program.NavigateTo<AddFilm>()),
                  new Option("Add Event", () => program.NavigateTo<AddEvent>()),
                  new Option("Delete User", () => program.NavigateTo<DeleteUser>()),
                  new Option("Delete Film", () => program.NavigateTo<DeleteFilm>()),
                  new Option("Delete Event", () => program.NavigateTo<DeleteEvent>()),
                  new Option("Delete Prenotation", () => program.NavigateTo<DeletePrenotation>()),
                  new Option("Show Users", () => program.NavigateTo<ShowUsers>()),
                  new Option("Show Film", () => program.NavigateTo<ShowFilm>()),
                  new Option("Show Events", () => program.NavigateTo<ShowEvents>()),
                  new Option("Show Prenotations", () => program.NavigateTo<ShowPrenotations>()))
                  
        {
        }

        public override void Display()
        {
            Output.WriteLine(ConsoleColor.Blue, "--------- {0} ---------", base.Title);
            base.Display();
        }
    }
}