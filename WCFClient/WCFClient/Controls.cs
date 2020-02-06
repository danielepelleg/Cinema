using System;
using EasyConsole;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WCFClient
{
    /* Controls Class
     * Controls and check methods for User's input.
     *
     * @author Daniele Pellegrini<daniele.pellegrini@studenti.unipr.it> - 285240
     * @author Riccardo Fava<riccardo.fava@studenti.unipr.it> - 287516
     */
    class Controls
    {
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
        public static bool CheckChar(string word)
        {
            int v = 0;
            string s = "!£$%&()=?^ì+*+§°#][@,;:.-_<>|/\\'\" ";
            foreach (char c in s)
            {
                if (word.Contains(c))
                    v += 1;
            }
            if (v == 0) return true;
            else return false;
        }

        /*
         * Check the User's attributes input
         */
        public static string CheckUserInput(string userAttribute, string value)
        {

            switch (userAttribute)
            {
                case "Username":
                    while ((value.Length < 1) || (value.Length > 30) || !CheckChar(value))
                    {
                        Console.WriteLine("\n{0} input not valid. \nThe length must be between 1 and 30 characters! Special characters are not allowed.\nRetry: ", userAttribute);
                        value = Console.ReadLine();
                    }
                    break;

                case "Password":
                    while ((value.Length < 4) || (value.Length > 32))
                    {
                        Console.WriteLine("\n{0} input not valid. \nThe length must be between 4 and 32 characters! Special characters are not allowed.\nRetry: ", userAttribute);
                        value = InputPassword();
                        Console.WriteLine("\n");

                    }
                    break;

                case "Nome":
                    while ((value.Length < 1) || (value.Length > 20) || !CheckChar(value))
                    {
                        Console.WriteLine("\n{0} input not valid. \nThe length must be between 1 and 20 characters! Special characters are not allowed.\nRetry: ", userAttribute);
                        value = Console.ReadLine();
                        Console.WriteLine("\n");

                    }
                    break;

                case "Cognome":
                    while ((value.Length < 1) || (value.Length > 20) || !CheckChar(value))
                    {
                        Console.WriteLine("\n{0} input not valid. \nThe length must be between 1 and 20 characters! Special characters are not allowed.\nRetry: ", userAttribute);
                        value = Console.ReadLine();
                        Console.WriteLine("\n");

                    }
                    break;
            }
            return value;
        }

        /*
         * Check Film's attributes input
         */
        public static string CheckFilm(string filmAttribute, string value)
        {

            switch (filmAttribute)
            {
                case "Titolo":
                    while ((value.Length < 1) || (value.Length > 50))
                    {
                        Console.WriteLine("\n{0} input not valid. \nThe length must be between 1 and 50 characters!\nRetry: ", filmAttribute);
                        value = Console.ReadLine();
                        Console.WriteLine("\n");

                    }
                    break;

                case "Regia":
                    while ((value.Length < 1) || (value.Length > 30))
                    {
                        Console.WriteLine("\n{0} input not valid. \nThe length must be between 1 and 30 characters!\nRetry: ", filmAttribute);
                        value = Console.ReadLine();
                        Console.WriteLine("\n");

                    }
                    break;

                case "Genere":
                    while ((value.Length < 1) || (value.Length > 20))
                    {
                        Console.WriteLine("\n{0} input not valid. \nThe length must be between 1 and 20 characters!\nRetry: ", filmAttribute);
                        value = Console.ReadLine();
                        Console.WriteLine("\n");
                    }
                    break;
            }

            return value;
        }

        /*
         * Check if the integer value inserted is in the DB, cheking the Foreign Key restriction
         */
        public static int CheckIntForeignKey(string value, string valueType)
        {
            while (!SessionManager.GetServiceClient().CheckIntFK(value, valueType))
            {
                Console.WriteLine("{0} not in the database. \nInsert a different value!\nRetry: ", valueType);
                value = Console.ReadLine();
                Console.WriteLine("\n");

            }
            return Convert.ToInt32(value);
        }

        /*
         * Check if the string value inserted is in the DB, cheking the Foreign Key restriction
         */
        public static string CheckStringForeignKey(string value, string valueType)
        {
            while (!SessionManager.GetServiceClient().CheckStringFK(value, valueType))
            {
                Console.WriteLine("{0} not in the database. \nInsert a different value!\nRetry: ", valueType);
                value = Console.ReadLine();
                Console.WriteLine("\n");

            }
            return value;
        }

        /*
         * Check the validity of an Int input
         */
        public static int CheckInt(string value)
        {
            while (!int.TryParse(value, out int number1))
            {
                Console.WriteLine("Not valid input. Retry: ");
                value = Console.ReadLine();
                Console.WriteLine("\n");
            }
            return Convert.ToInt32(value);
        }

        /*
         * Check the validity of a Date input
         */
        public static DateTime CheckDate(string date)
        {
            while (!DateTime.TryParse(date, out DateTime date1))
            {
                Console.WriteLine("Not valid input. Retry: ");
                date = Console.ReadLine();
                Console.WriteLine("\n");

            }
            return Convert.ToDateTime(date);
        }

        /*
         * Check the validity of a Decimal input
         */
        public static decimal CheckDecimal(string value)
        {
            while (!decimal.TryParse(value, out decimal decimal1))
            {
                Console.WriteLine("Not valid input. Retry: ");
                value = Console.ReadLine();
            }
            foreach (char c in value)
            {
                if (value.Contains("."))
                {
                    Console.WriteLine("Use comma (,) instead of dot (.)! Retry: ");
                    value = Console.ReadLine();
                    Console.WriteLine("\n");
                }
            }
            return Decimal.Round(Convert.ToDecimal(value), 2);
        }

        /*
         * Check the validity of the Place requested by the User
         * 
         */
        public static int CheckPlace(string eventCode, string place)
        {
            while (!SessionManager.GetServiceClient().CheckPlace(Convert.ToInt32(eventCode), Convert.ToInt32(place)))
            {
                Console.WriteLine("The place number {0} is not available!" +
                    " Digit a valid number!\nRetry: ", place);
                place = Console.ReadLine();
                place = Convert.ToString(CheckInt(place));
            }
            return Convert.ToInt32(place);
        }

        /*
         * Error message when Server is not available
         */ 
        public static void ErrorMessage() {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n\nConnection Error! Server Unreacheable!");
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
