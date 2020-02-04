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
            //Console.BackgroundColor = ConsoleColor.Blue;
            //Console.Clear();
            //Console.ForegroundColor = ConsoleColor.Black;
            new CinemaProgram().Run();
        }
    }

}