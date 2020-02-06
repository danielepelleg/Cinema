using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyConsole;

namespace WCFClient.Pages
{
    class MainPage : MenuPage
    {
        public MainPage(Program program)
            : base(" Main Page ", program,
                 new Option("Registration", () => program.NavigateTo<RegistrationPage>()),
                 new Option("Login", () => program.NavigateTo<LoginPage>()))
        {
        }

        public override void Display()
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Output.WriteLine(ConfigurationManager.AppSettings["logo"].Replace("\\n", "\n") + "\n");
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            base.Display();
        }
    }

    /*
     * User Type Enumeration
     * 
     * Select the type of User, if Admin or not
     */
    enum UserType
    {
        User,
        Admin,
        Back

    }

    
}
