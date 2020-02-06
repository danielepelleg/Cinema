using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace WCFDatabaseManager
{
    /*
     * User Class
     * Store User objects of the database
     * 
     * @author Daniele Pellegrini <daniele.pellegrini@studenti.unipr.it> - 285240
     * @author Riccardo Fava <riccardo.fava@studenti.unipr.it> - 287516
     */ 
    [DataContract]
    public class User
    {
        [DataMember]
        public string Username { get; set; } = string.Empty;

        [DataMember]
        public string Password { get; set; } = string.Empty;

        [DataMember]
        public string Name { get; set; } = string.Empty;

        [DataMember]
        public string Surname { get; set; } = string.Empty;

        public User()
        {
        }

        public User(string username, string password, string name, string surname)
        {
            this.Username = username;
            this.Password = password;
            this.Name = name;
            this.Surname = surname;
        }

        public User(string username, string name, string surname)
        {
            this.Username = username;
            this.Name = name;
            this.Surname = surname;
        }

    }
}
