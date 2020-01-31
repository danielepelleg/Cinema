using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WCFClient;

namespace Cinema
{
    class MainProgram
    {
        static void Main(string[] args)
        {
            new CinemaProgram().Run();
        }

        /*
         * Hide Password Insertion
         */
        public static string InputPassword()
        {
            string password = string.Empty;
            do
            {
                ConsoleKeyInfo key = Console.ReadKey(true);
                // Backspace Should Not Work
                // Stops Receving Keys Once Enter is Pressed
                if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
                {
                    password += key.KeyChar;
                    Console.Write("*");
                }
                else
                {
                    if (key.Key == ConsoleKey.Backspace && password.Length > 0)
                    {
                        password = password.Substring(0, (password.Length - 1));
                        Console.Write("\b \b");
                    }
                    else if (key.Key == ConsoleKey.Enter)
                    {
                        break;
                    }
                }
            } while (true);

            return password;
        }

        /*
         * Check if the string contains special characters
         * 
         * @return true if the string is valid, false if not
         */ 
        public static bool Charcheck(string word) {
            int v = 0;
            string s = "!£$%&()=?^ì+*+§°#][@,;:.-_<>|/\\'\" ";
            foreach (char c in s) {
                if (word.Contains(c))
                    v += 1;
            }
            if (v == 0) return true;
            else return false;
        }

        /*
         * Check the User's attributes input
         */ 
        public static string CheckUserInput(string userAttribute, string value) {

            switch (userAttribute) {
                case "Username":
                    while ((value.Length < 1) || (value.Length > 30) || !Charcheck(value)) {
                        Console.WriteLine("\n{0} non accettabile. \nInserire un numero di caratteri compreso tra 1 e 30! I caratteri speciali non sono ammessi.\nRiprovare: ", userAttribute);
                        userAttribute = Console.ReadLine();
                    } break;

                case "Password":
                    while ((value.Length < 4) || (value.Length > 32)) {
                        Console.WriteLine("\n{0} non accettabile. \nInserire un numero di caratteri compreso tra 4 e 32!\nRiprovare: ", userAttribute);
                        userAttribute = InputPassword();
                    } break;

                case "Nome":
                    while ((value.Length < 1) || (value.Length > 20) || !Charcheck(value)) {
                        Console.WriteLine("\n{0} non accettabile. \nInserire un numero di caratteri compreso tra 1 e 20! I caratteri speciali non sono ammessi.\nRiprovare: ", userAttribute);
                        userAttribute = Console.ReadLine();
                    } break;

                case "Cognome":
                    while ((value.Length < 1) || (value.Length > 20) || !Charcheck(value)) {
                        Console.WriteLine("\n{0} non accettabile. \nInserire un numero di caratteri compreso tra 1 e 20! I caratteri speciali non sono ammessi.\nRiprovare: ", userAttribute);
                        userAttribute = Console.ReadLine();
                    } break;
            }
            return userAttribute;
        }

        /*
         * Check Film's attributes input
         */ 
        public static string CheckFilm(string filmAttribute, string value) {

            switch (filmAttribute) {
                case "Titolo":
                    while ((value.Length < 1) || (value.Length > 50))  {
                        Console.WriteLine("{0} non accettabile. \nInserire un numero di caratteri compreso tra 1 e 50!\nRiprovare: ", filmAttribute);
                        value = Console.ReadLine();
                    } break;

                case "Regia":
                    while ((value.Length < 1) || (value.Length > 30)) {
                        Console.WriteLine("{0} non accettabile. \nInserire un numero di caratteri compreso tra 1 e 30!\nRiprovare: ", filmAttribute);
                        value = Console.ReadLine();
                    } break;

                case "Genere":
                    while ((value.Length < 1) || (value.Length > 20)) {
                        Console.WriteLine("{0} non accettabile. \nInserire un numero di caratteri compreso tra 1 e 20!\nRiprovare: ", filmAttribute);
                        value = Console.ReadLine();
                    }  break;
            }

            return value;
        }

        /*
         * Get the Primary Key of a Table, given a value
         */ 
        public static int GetPrimaryKey(string value, string valueType) {
            while (!Global.wcfClient.CheckFK(Convert.ToInt32(value), valueType)) {
                Console.WriteLine("{0} non presente. \nInserire un valore accettabile!\nRiprovare: ", valueType);
                value = Console.ReadLine();
            }
            return Convert.ToInt32(value);
        }

        /*
         * Check the validity of an Int input
         */ 
        public static int Intcheck(string value) {
            while (!int.TryParse(value, out int number1)) {
                Console.WriteLine("Immissione non valida. Riprovare: ");
                value = Console.ReadLine();
            }
            return Convert.ToInt32(value);
        }

        /*
         * Check the validity of a Date input
         */
        public static DateTime CheckDate(string date) {
            while (!DateTime.TryParse(date, out DateTime date1)) {
                Console.WriteLine("Immissione non valida. Riprovare: ");
                date = Console.ReadLine();
            }
            return Convert.ToDateTime(date);
        }

        /*
         * Check the validity of a Decimal input
         */
        public static decimal CheckDecimal(string value) {
            while (!decimal.TryParse(value, out decimal decimal1)) {
                Console.WriteLine("Immissione non valida. Riprovare: ");
                value = Console.ReadLine();
            }
            foreach (char c in value) {
                if (value.Contains(".")) {
                    Console.WriteLine("Inserire la virgola anzichè il punto! Riprovare: ");
                    value = Console.ReadLine();
                }
            }
            return Decimal.Round(Convert.ToDecimal(value), 2);
        }

        /*
         * Check the validity of the Place requested by the User
         * 
         */ 
        public static int CheckPlace(string eventCode, string place) {
            while (!Global.wcfClient.CheckPlace(Convert.ToInt32(eventCode), Convert.ToInt32(place)))  {
                Console.WriteLine("Il posto {0} non è disponibile!" +
                    " Inserine uno disponibile!\nRiprovare: ", place);
                place = Console.ReadLine();
                place = Convert.ToString(Intcheck(place));
            }
            return Convert.ToInt32(place);
        }

        // Messaggio di errore collegamento al DB
        public static void Errormessage()
        {
            Console.WriteLine("\nIl Database non è raggiungibile al momento.");
        }
    }

}