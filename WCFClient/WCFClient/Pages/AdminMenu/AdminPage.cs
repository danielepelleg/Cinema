using EasyConsole;
using System;

namespace WCFClient.Pages
{
    class AdminPage : MenuPage
    {
        public AdminPage(Program program)
            : base("Admin Functions", program,
                  new Option("Add Film", () => program.NavigateTo<AddFilm>()),
                  new Option("Add Evento", () => program.NavigateTo<AddEvent>()),
                  new Option("Delete Film", () => program.NavigateTo<DeleteFilm>()),
                  new Option("Show Events", () => program.NavigateTo<ShowEvents>()),
                  new Option("Show Film", () => program.NavigateTo<ShowFilm>()))
        {
        }

        public override void Display()
        {
            Output.WriteLine(ConsoleColor.Blue, "--------- {0} ---------", base.Title);
            base.Display();
        }
    }
}