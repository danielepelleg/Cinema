using System;
using WCFClient.ServiceReference1;

namespace WCFClient
{
    /*
     * Global Class
     * 
     * Store global data and attribute available everywhere
     */ 
    class Global
    {
        // Creation of WCF Client
        public static Service1Client wcfClient = new ServiceReference1.Service1Client();
    }
}
