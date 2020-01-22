using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyConsole;

namespace WCFClient.Pages 
{
    class MainPage : MenuPage
    {
        public MainPage(Program program)
            :base("Main Page", program,
                 new Option("Registrazione", () => program.NavigateTo<RegistrationPage>()),
                 new Option("Login", () => program.NavigateTo<LoginPage>()))
        {
        }
    }
    // Selezione del tipo di utente che vuole registrarsi
    enum Type
    {
        User,
        Admin
    }
}
