using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema
{
    class MainProgram
    {
        static void Main(string[] args)
        {
            new CinemaProgram().Run();
        }
        public static class Global
        {
            public static string currentusername = "default";
            public static bool loggedin = false;
        }

        //Nascondere Inserimento Password
        public static string Passinsert(string a)
        {
            ConsoleKeyInfo key;
            do
            {
                key = Console.ReadKey(true);

                // Backspace Should Not Work
                if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
                {
                    a += key.KeyChar;
                    Console.Write("*");
                }
            }
            // Stops Receving Keys Once Enter is Pressed
            while (key.Key != ConsoleKey.Enter);
            return a;
        }

        //Controllo Caratteri Speciali
        public static bool Charcheck(string a)
        {
            int v = 0;
            string s = "!£$%&()=?^ì+*+§°#][@,;:.-_<>|/\\'\" ";
            foreach (char c in s)
            {
                if (a.Contains(c))
                    v += 1;
            }
            if (v == 0)
                return true;
            else
                return false;
        }

        //Controllo Validità Input (Variabile da controllare, Nome formale della Variabile)
        public static string Inputcheck(string a, string b)
        {
            string c = string.Empty;
            switch (b)
            {
                case "Username":
                    while ((a.Length < 1) || (a.Length > 30) || Charcheck(a) == false)
                    {
                        Console.WriteLine("\n{0} non accettabile. Inserire un numero di caratteri compreso tra 1 e 30! I caratteri speciali non sono ammessi.\nRiprovare: ", b);
                        a = Console.ReadLine();
                    }
                    break;
                case "Password":
                    while ((a.Length < 4) || (a.Length > 32))
                    {
                        Console.WriteLine("\n{0} non accettabile. Inserire un numero di caratteri compreso tra 4 e 32!\nRiprovare: ", b);
                        a = Passinsert(c);
                    }
                    break;
                case "Nome":
                    while ((a.Length < 1) || (a.Length > 20) || Charcheck(a) == false)
                    {
                        Console.WriteLine("\n{0} non accettabile. Inserire un numero di caratteri compreso tra 1 e 20! I caratteri speciali non sono ammessi.\nRiprovare: ", b);
                        a = Console.ReadLine();
                    }
                    break;
                case "Cognome":
                    while ((a.Length < 1) || (a.Length > 20) || Charcheck(a) == false)
                    {
                        Console.WriteLine("\n{0} non accettabile. Inserire un numero di caratteri compreso tra 1 e 20! I caratteri speciali non sono ammessi.\nRiprovare: ", b);
                        a = Console.ReadLine();
                    }
                    break;
            }
            return a;
        }

        //Controllo Validità Dati Film
        public static string Filmcheck(string a, string b)
        {
            switch (b)
            {
                case "Titolo":
                    while ((a.Length < 1) || (a.Length > 50))
                    {
                        Console.WriteLine("{0} non accettabile. Inserire un numero di caratteri compreso tra 1 e 50!\nRiprovare: ", b);
                        a = Console.ReadLine();
                    }
                    break;
                case "Regia":
                    while ((a.Length < 1) || (a.Length > 30))
                    {
                        Console.WriteLine("{0} non accettabile. Inserire un numero di caratteri compreso tra 1 e 30!\nRiprovare: ", b);
                        a = Console.ReadLine();
                    }
                    break;
                case "Genere":
                    while ((a.Length < 1) || (a.Length > 20))
                    {
                        Console.WriteLine("{0} non accettabile. Inserire un numero di caratteri compreso tra 1 e 20!\nRiprovare: ", b);
                        a = Console.ReadLine();
                    }
                    break;
            }
            return a;
        }

        //Controllo Inserimento valore con Foreign key
        public static int FKCheck(string valore, string tipo)
        {
            var wcfClient = new WCFClient.ServiceReference1.Service1Client();
            while (wcfClient.ControlloFK(Convert.ToInt32(valore), tipo) != "Trovato")
            {
                Console.WriteLine("{0} non presente. Inserire un valore accettabile!\nRiprovare: ", tipo);
                valore = Console.ReadLine();
            }
            return Convert.ToInt32(valore);
        }

        //Controllo Validità Numero Intero
        public static int Intcheck(string a)
        {
            int b;
            while (int.TryParse(a, out int number1) == false)
            {
                Console.WriteLine("Immissione non valida. Riprovare: ");
                a = Console.ReadLine();
            }
            return b = Convert.ToInt32(a);
        }

        //Controllo Validità Data
        public static DateTime Datecheck(string a)
        {
            DateTime b;
            while (DateTime.TryParse(a, out DateTime date1) == false)
            {
                Console.WriteLine("Immissione non valida. Riprovare: ");
                a = Console.ReadLine();
            }
            return b = Convert.ToDateTime(a);
        }

        //Controllo Validità Decimal
        public static decimal Decimalcheck(string a)
        {
            decimal b;
            while (decimal.TryParse(a, out decimal decimal1) == false)
            {
                Console.WriteLine("Immissione non valida. Riprovare: ");
                a = Console.ReadLine();
            }
            foreach (char c in a)
            {
                if (a.Contains("."))
                {
                    Console.WriteLine("Inserire la virgola anzichè il punto! Riprovare: ");
                    a = Console.ReadLine();
                }
            }
            return b = Convert.ToDecimal(a);
        }

        //Controllo che il posto inserito non sia gia stato prenotato
        public static int ControlloPosto(string codice_evento, string posto)
        {
            var wcfClient = new WCFClient.ServiceReference1.Service1Client();
            while (wcfClient.VerificaPosto(Convert.ToInt32(codice_evento), Convert.ToInt32(posto)) == "Valore non valido")
            {
                Console.WriteLine("Il posto {0} non è disponibile!" +
                    " Inserine uno disponibile!\nRiprovare: ", posto);
                posto = Console.ReadLine();
            }
            return Convert.ToInt32(posto);
        }

        // Messaggio di errore collegamento al DB
        public static void Errormessage()
        {
            Console.WriteLine("\nIl Database non è raggiungibile al momento.");
        }
    }

}