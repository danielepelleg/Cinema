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
        public static Service1Client wcfClient = new Service1Client();

        // Store the logged user
        private static User user = null;

        private static bool admin;

        public static void Reset() {
            user = null;
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