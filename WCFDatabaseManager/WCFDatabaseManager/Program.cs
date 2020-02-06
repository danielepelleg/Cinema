using System;
using System.Configuration;
using System.ServiceModel;

namespace WCFDatabaseManager
{
    /* Program Class
     * Starts the WCFDatabaseManager
     *
     * @author Daniele Pellegrini<daniele.pellegrini@studenti.unipr.it> - 285240
     * @author Riccardo Fava<riccardo.fava@studenti.unipr.it> - 287516
     */
    class Program
    {
        static void Main(string[] args)
        {

            try
            {
                ServiceHost svcHostUser = new ServiceHost(typeof(ServiceUser));
                ServiceHost svcHostFilm = new ServiceHost(typeof(ServiceFilm));
                ServiceHost svcHostEvent = new ServiceHost(typeof(ServiceEvent));
                ServiceHost svcHostPrenotation = new ServiceHost(typeof(ServicePrenotation));
                ServiceHost svcHostHall = new ServiceHost(typeof(ServiceHall));
                ServiceHost svcHostPlace = new ServiceHost(typeof(ServicePlace));

                svcHostUser.Open();
                svcHostFilm.Open();
                svcHostEvent.Open();
                svcHostPrenotation.Open();
                svcHostHall.Open();
                svcHostPlace.Open();

                
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.BackgroundColor = ConsoleColor.Black;
                Console.Clear();
                Console.WriteLine(ConfigurationManager.AppSettings["logo"]);
                
                Console.WriteLine("Servizio WCF Database Manager online --- premere un tasto per interrompere...");

                Console.ReadLine();
                Console.WriteLine("Servizio WCF Database Manager interrotto");

                svcHostUser.Close();
                svcHostFilm.Close();
                svcHostEvent.Close();
                svcHostPrenotation.Close();
                svcHostHall.Close();
                svcHostPlace.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Errore: " + ex.ToString());
                Console.ReadLine();
            }
        }
    }
}
