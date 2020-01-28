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
        void DoWork();

        
    }
    [DataContract]
    public class User
    {
        [DataMember]
        public string username { get; set; } = string.Empty;

        [DataMember]
        public string password { get; set; } = string.Empty;

        [DataMember]
        public string name { get; set; } = string.Empty;

        [DataMember]
        public string surname { get; set; } = string.Empty;

        public User()
        {
            this.username = string.Empty;
            this.password = string.Empty;
            this.name = string.Empty;
            this.surname = string.Empty;
        }

        public User(string username, string password, string name, string surname)
        {
            this.username = username;
            this.password = password;
            this.name = name;
            this.surname = surname;
        }
    }
}
