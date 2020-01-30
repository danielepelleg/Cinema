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

using Event = WCFServer.ServiceReferenceEvent.Event;
using Film = WCFServer.ServiceReferenceFilm.Film;

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
        bool EditUser(string oldUsername, string newUsername, string newPassword, string newName, string newSurname);

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

        // PRENOTATION METHODS

        [OperationContract]
        bool AddPrenotation(DateTime dateTime, string usernameUser, int eventCode, int placeNumber);

        [OperationContract]
        bool DeletePrenotation(int prenotationCode);

        [OperationContract]
        List<Ticket> GetTicketsList(string username);

        // FILM METHODS

        [OperationContract]
        bool AddFilm(string title, int year, string direction, int duration, DateTime releaseDate, string genre);

        [OperationContract]
        bool DeleteFilm(int filmCode);

        [OperationContract]
        Film GetFilm(int filmCode);

        [OperationContract]
        List<Film> GetFilmList();

        // HALL METHODS

        [OperationContract]
        List<Hall> GetHallsList();

        [OperationContract]
        string DrawHall(int eventCode);

        // PLACE METHODS 

        [OperationContract]
        bool CheckFK(int value, string valueType);

        [OperationContract]
        List<Place> GetAvailablePlacesList(int eventCode);

        [OperationContract]
        bool CheckPlace(int eventCode, int placeNumber);
    }
    
}
     
