using EasyConsole;
using System;
using System.Configuration;

namespace WCFClient
{
    class MainProgram
    {
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Output.WriteLine(ConfigurationManager.AppSettings["logo"].Replace("\\n", "\n"));
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            new CinemaProgram().Run();
        }
    }

}