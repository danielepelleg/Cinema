using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

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
        string Visualizzazione_elenco_UtentiFree();


    }
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
}
