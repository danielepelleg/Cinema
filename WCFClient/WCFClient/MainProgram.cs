using EasyConsole;
using System;
using System.Configuration;

namespace WCFClient
{
    class MainProgram
    {
        static void Main(string[] args)
        {
            //Console.BackgroundColor = ConsoleColor.DarkRed;
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Output.WriteLine(ConfigurationManager.AppSettings["logo"].Replace("\\n", "\n"));
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            //Console.BackgroundColor = ConsoleColor.Blue;
            //Console.Clear();
            //Console.ForegroundColor = ConsoleColor.Black;
            new CinemaProgram().Run();
        }
    }

}