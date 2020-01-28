using EasyConsole;
using System;

namespace WCFClient.Pages
{
    class LoginPage : Page
    {
        public LoginPage(Program program)
            : base("Login", program)
        {
        }

        public override void Display()
        {
            base.Display();
            //Imposto lo stato di Login su false nel caso in cui l'utente sia tornato indietro
            Cinema.MainProgram.Global.loggedin = false; 
            // Uso l'enumerazione creata precedentemente per far scegliere il tipo di Login (Utente - Admin)
            Type input = Input.ReadEnum<Type>("Select the type of user you want to login: ");
            bool isAdmin = false;
            if (input.ToString().Equals("Admin")) isAdmin = true;
            Output.WriteLine(ConsoleColor.Green, "\n {0} Login: ", input);

            // SCRIPT LOGIN
            string username, hashed_password, password = string.Empty;
            var wcfClient = new ServiceReference1.Service1Client(); //WCF CLIENT CREATO

            // Definisco le variabili che andranno a prendere i dati inseriti. La variabile password andrà a effettuare
            // i controlli di validità della password (sulla lunghezza) mentre la corrispondente variabile hashed
            // andrà a criptarla prima dell'inserimento nel DB attraverso un algoritmo MD5 per garantirne la sicurezza

            Output.WriteLine("--------- LOGIN ----------");
            username = Input.ReadString("Username: ");
            username = Cinema.MainProgram.Inputcheck(username, "Username");
            Console.Write("Password: ");
            password = Cinema.MainProgram.Passinsert(password);
            hashed_password = EasyEncryption.MD5.ComputeMD5Hash(Cinema.MainProgram.Inputcheck(password, "Password"));

            // Controllo Dati nel Database
            try
            {
                // Mando i dati al server per il Login
                Cinema.MainProgram.Global.loggedin = wcfClient.login(isAdmin, username, hashed_password);
                // Assegno il risultato booleano del login alla relativa variabile globale
                Cinema.MainProgram.Global.currentusername = username;
                // Reindirizzo l'Utente alla pagina corrispondente al tipo di login effettuato
                switch (input.ToString())
                {
                    case "Admin":
                        // Controllo che il login admin sia andato a buon fine
                        if (Cinema.MainProgram.Global.loggedin)
                        {
                            Output.WriteLine("\nLogin Successful");
                            Input.ReadString("Press [Enter] to navigate to your menu");
                            Program.NavigateTo<AdminPage>();
                        }
                        else
                            Output.WriteLine("\nLogin Failed");
                            Input.ReadString("Press [Enter] to navigate back");
                            Program.NavigateTo<MainPage>();
                        break;
                    case "User":
                        // Controllo che il login user sia andato a buon fine
                        if (Cinema.MainProgram.Global.loggedin)
                        {
                            Output.WriteLine("\nLogin Successful");
                            Input.ReadString("Press [Enter] to navigate to your menu");
                            Program.NavigateTo<UserPage>();
                        }
                        else
                            Output.WriteLine("\nLogin Failed");
                        Input.ReadString("Press [Enter] to navigate back");
                        Program.NavigateTo<MainPage>();
                        break;

                }
            }
            catch // WCF Server Spento
            {
                Cinema.MainProgram.Errormessage();
                Input.ReadString("Press [Enter] to navigate back");
                Program.NavigateTo<MainPage>();
            }
        }
    }

}