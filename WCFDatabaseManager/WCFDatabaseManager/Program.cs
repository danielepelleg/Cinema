using System;
using System.ServiceModel;

namespace WCFDatabaseManager
{
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

                svcHostUser.Open();
                svcHostFilm.Open();
                svcHostEvent.Open();
                svcHostPrenotation.Open();

                Console.WriteLine("Servizio WCF online --- premere un tasto per interrompere...");

                Console.ReadLine();
                Console.WriteLine("Servizio WCF interrotto");

                svcHostUser.Close();
                svcHostFilm.Close();
                svcHostEvent.Close();
                svcHostPrenotation.Close();


            }
            catch (Exception ex)
            {
                Console.WriteLine("Errore: " + ex.ToString());
                Console.ReadLine();
            }
        }
    }
}
