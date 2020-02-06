using EasyConsole;
using System;
using System.Configuration;

namespace WCFClient
{
    /* MainProgram Class
     * Starts the WCFClient
     *
     * @author Daniele Pellegrini<daniele.pellegrini@studenti.unipr.it> - 285240
     * @author Riccardo Fava<riccardo.fava@studenti.unipr.it> - 287516
     */
    class MainProgram
    {
        static void Main(string[] args)
        { 
            new CinemaProgram().Run();
        }
    }

}