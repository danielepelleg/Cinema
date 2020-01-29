using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using WCFServer.ServiceReferenceUser;
using WCFServer.ServiceReferencePrenotation;
using WCFServer.ServiceReferenceFilm;
using WCFServer.ServiceReferenceEvent;
using WCFServer.ServiceReferenceHall;
using WCFServer.ServiceReferencePlace;

namespace WCFServer
{
    // NOTA: è possibile utilizzare il comando "Rinomina" del menu "Refactoring" per modificare il nome di interfaccia "IService1" nel codice e nel file di configurazione contemporaneamente.
    [ServiceContract]
    public interface IService1
    {
        // USER - ADMIN METHODS

        [OperationContract]
        bool Registration(bool isAdmin, string username, string password, string name, string surname);

        [OperationContract]
        bool Login(bool isAdmin, string username, string password);

        [OperationContract]
        bool DeleteUser(string username);

        [OperationContract]
        User GetUser(string username);

        [OperationContract]
        List<User> GetUsersList();

        // EVENTS METHODS

        [OperationContract]
        bool AddEvent(string usernameAdmin, DateTime dateTime, int filmCode, int hallCode, decimal price);

        [OperationContract]
        bool DeleteEvent(int eventCode);

        [OperationContract]
        List<Event> GetEventsList();

    }
    
}
     
