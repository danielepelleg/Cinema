using System.Runtime.Serialization;

namespace WCFServer
{
    /*
     * User Class
     * Store User objects of the database
     * 
     * @author Daniele Pellegrini <daniele.pellegrini@studenti.unipr.it> - 285240
     * @author Riccardo Fava <riccardo.fava@studenti.unipr.it> - 287516
     */
    [DataContract]
    class User {

        [DataMember]
        public string username { get; set; }
        [DataMember]
        public string password { get; set; }
        [DataMember]
        public string name { get; set; }
        [DataMember]
        public string surname { get; set; }


        /*
         * Show information about an User
         * 
         * @return the String with the user details
         */
        public string showUser()
        {
            return "Username: " + this.username + 
                ", Name: " + this.name + 
                ", Surname: " + this.surname;
        }
    }
}
