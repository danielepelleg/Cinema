using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

/* 
 * @author Daniele Pellegrini<daniele.pellegrini@studenti.unipr.it> - 285240
 * @author Riccardo Fava<riccardo.fava@studenti.unipr.it> - 287516
 */
namespace WCFDatabaseManager
{
    // NOTA: è possibile utilizzare il comando "Rinomina" del menu "Refactoring" per modificare il nome di interfaccia "IServiceUser" nel codice e nel file di configurazione contemporaneamente.
    [ServiceContract]
    public interface IServiceUser
    {
        [OperationContract]
        bool Registration(bool isAdmin, string username, string password, string name, string surname);

        [OperationContract]
        bool Login(bool isAdmin, string username, string password);

        [OperationContract]
        bool DeleteUser(string username);

        [OperationContract]
        bool EditUser(string oldUsername, string newUsername, string newPassword, string newName, string newSurname);

        [OperationContract]
        User GetUser(string username);

        [OperationContract]
        List<User> GetUsersList();
    }
    /*
     * User Class
     * Store User objects of the database
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
            this.Username = string.Empty;
            this.Password = string.Empty;
            this.Name = string.Empty;
            this.Surname = string.Empty;
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

        /*
         * Show information about an User
         * 
         * @return the String with the user details
         */
        public string showUser()
        {
            return "Username: " + this.Username +
                ", Name: " + this.Name +
                ", Surname: " + this.Surname;
        }
    }

    /*
     * Admin Class
     * Store Admin objects of the database
     */
     [DataContract]
    public class Admin
    {
        [DataMember]
        public string Username { get; set; } = string.Empty;

        [DataMember]
        public string Password { get; set; } = string.Empty;

        [DataMember]
        public string Name { get; set; } = string.Empty;

        [DataMember]
        public string Surname { get; set; } = string.Empty;

        public Admin()
        {
            this.Username = string.Empty;
            this.Password = string.Empty;
            this.Name = string.Empty;
            this.Surname = string.Empty;
        }

        public Admin(string username, string password, string name, string surname)
        {
            this.Username = username;
            this.Password = password;
            this.Name = name;
            this.Surname = surname;
        }

        public Admin(string username, string name, string surname)
        {
            this.Username = username;
            this.Name = name;
            this.Surname = surname;
        }

        /*
         * Show information about an User
         * 
         * @return the String with the user details
         */
        public string ShowAdmin()
        {
            return "Username: " + this.Username +
                ", Name: " + this.Name +
                ", Surname: " + this.Surname;
        }
    }
}
