using EasyConsole;
using System;

namespace WCFClient.Pages
{
    class RegistrationPage : Page
    {
        
        public RegistrationPage(Program program)
            : base("Registration", program)
        {
        }
       
        public override void Display()
        {
            base.Display();

            //Imposto lo stato di Login su false nel caso in cui l'utente sia tornato indietro
            Cinema.MainProgram.Global.loggedin = false;

            // Uso l'enumerazione creata precedentemente per far scegliere il tipo di Registrazione (Utente - Admin)
            Type input = Input.ReadEnum<Type>("Select the type of user you want to register: ");
            bool isAdmin = false;
            if (input.ToString().Equals("Admin")) isAdmin = true;
            Output.WriteLine(ConsoleColor.Green, "\n {0} Sign In: ", input);

            // SCRIPT REGISTRAZIONE
            string user, password, hashed_password, nome, cognome = string.Empty;
            var wcfClient = new ServiceReference1.Service1Client(); //WCF CLIENT CREATO

            // Definisco le variabili che andranno a prendere i dati inseriti. La variabile password andrà a effettuare
            // i controlli di validità della password (sulla lunghezza) mentre la corrispondente variabile hashed
            // andrà a criptarla prima dell'inserimento nel DB attraverso un algoritmo MD5 per garantirne la sicurezza 

            Output.WriteLine("--------- REGISTRAZIONE ----------");
            user = Input.ReadString("Username (max 30 caratteri): ");
            user = Cinema.MainProgram.Inputcheck(user, "Username");  
            password = Input.ReadString("Password (max 32 caratteri): ");
            hashed_password = EasyEncryption.MD5.ComputeMD5Hash(Cinema.MainProgram.Inputcheck(password, "Password"));
            nome = Input.ReadString("Nome (max 20 caratteri): ");
            nome = Cinema.MainProgram.Inputcheck(nome, "Nome");
            cognome = Input.ReadString("Cognome (max 20 caratteri): ");
            cognome = Cinema.MainProgram.Inputcheck(cognome, "Cognome");

            // Registrazione dati utente (admin o user) nel Database
            try
            {
                bool success = wcfClient.registration(isAdmin, user, hashed_password, nome, cognome);
                if (success) Output.WriteLine("REGISTRAZIONE AVVENUTA CON SUCCESSO\nPROCEDERE CON IL LOGIN\n");
                else Output.WriteLine("ERRORE REGISTRAZIONE UTENTE\n RIPROVARE!\n");
            }
            catch
            {
                Cinema.MainProgram.Errormessage();
            }

            Input.ReadString("Press [Enter] to navigate home");
            Program.NavigateHome();
        }
    }

}
