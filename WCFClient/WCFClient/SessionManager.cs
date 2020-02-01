using System;
using WCFClient.ServiceReference1;

namespace WCFClient
{
    /*
     * SessionManager Class
     * 
     * Store session variables and attribute available everywhere
     */
    class SessionManager
    {
        // Creation of WCF Client
        private static Service1Client wcfClient = null;

        // Store the logged user
        private static User user = null;

        private static bool admin;

        public static void Reset() {
            user = null;
        }

        /*
        * @return the connection to the WCFServer
        */
        public static Service1Client GetServiceClient()
        {
            if (wcfClient != null) return wcfClient;
            return Connect();
        }

        /*
         * Connect to WCFServer.
         * @return the Service1Client
         */
        public static Service1Client Connect() {              // TODO Fix Exception when 
            Service1Client wcfClient = new Service1Client();  // Server not available
            try {
                wcfClient.GetFilmList();
            }
            catch (Exception ex) {
                Console.WriteLine("\nCommit Exception Type: {0}", ex.GetType());
                Console.WriteLine("  Message: {0}", ex.Message);
            }
            return wcfClient;
        }

        public static User GetUser() {
            return user;
        }

        public static void SetUser(User client) {
            user = client;
        }

        public static bool IsAdmin() {
            return admin;
        }

        public static void SetAdmin(Enum UserType)
        {
            switch (UserType.ToString()) {
                case "User": admin = false;
                    break;
                case "Admin": admin = true;
                    break;
            }
        }
    }
}