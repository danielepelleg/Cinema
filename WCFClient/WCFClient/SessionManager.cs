using System;
using System.Configuration;
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

        // Set the bool value for User's privilege
        private static bool admin;

        // Set the bool value for User's subscription
        private static bool subscription;

        // Reset the User when Logout
        public static void Reset() {
            user = null;
            subscription = false;
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
        public static Service1Client Connect() {              
            Service1Client wcfClient = new Service1Client();
            try {
                //Check if the Database Manager is reacheable
                if (wcfClient.CheckConnection())
                    return wcfClient;
                if (!wcfClient.CheckConnection()) {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\n\nConnection Error! Database Manager Unreacheable!");
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
            catch {
                Controls.ErrorMessage();
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

        public static bool IsSubscribed() {
            return subscription;
        }

        public static string GetSubscribed(){
            if (subscription)
                return "Yes";
            else return "No";
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

        public static void SetSubscription(bool value)
        {
            subscription = value;
        }
    }
}