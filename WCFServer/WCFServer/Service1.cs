using System;
using System.Linq;
using System.Collections.Generic;
using WCFServer.ServiceReferenceUser;
using WCFServer.ServiceReferencePrenotation;
using WCFServer.ServiceReferenceFilm;
using WCFServer.ServiceReferenceEvent;
using WCFServer.ServiceReferenceHall;
using WCFServer.ServiceReferencePlace;


namespace WCFServer
{
    // NOTA: è possibile utilizzare il comando "Rinomina" del menu "Refactoring" per modificare il nome di classe "Service1" nel codice e nel file di configurazione contemporaneamente.
    public class Service1 : IService1
    {
        private ServiceUserClient serviceUser = new ServiceReferenceUser.ServiceUserClient();
        private ServicePrenotationClient servicePrenotation = new ServiceReferencePrenotation.ServicePrenotationClient();
        private ServiceFilmClient serviceFilm = new ServiceReferenceFilm.ServiceFilmClient();
        private ServiceEventClient serviceEvent = new ServiceReferenceEvent.ServiceEventClient();
        private ServiceHallClient serviceHall = new ServiceReferenceHall.ServiceHallClient();
        private ServicePlaceClient servicePlace = new ServiceReferencePlace.ServicePlaceClient();

        // USER - ADMIN METHODS 

        public bool Registration(bool isAdmin, string username, string password, string name, string surname) {
            return serviceUser.Registration(isAdmin, username, password, name, surname);
        }

        public bool Login(bool isAdmin, string username, string password) {
            return serviceUser.Login(isAdmin, username, password);
        }

        public bool DeleteUser(string username) {
            return serviceUser.DeleteUser(username);
        }

        public User GetUser(string username) {
            return serviceUser.GetUser(username);
        }

        public List<User> GetUsersList() {
            return serviceUser.GetUsersList().ToList();
        }

        // EVENTS METHODS

        public bool AddEvent(string usernameAdmin, DateTime dateTime, int filmCode, int hallCode, decimal price) {
            return serviceEvent.AddEvent(usernameAdmin, dateTime, filmCode, hallCode, price);
        }

        public bool DeleteEvent(int eventCode) {
            return serviceEvent.DeleteEvent(eventCode);
        }

        public List<Event> GetEventsList() {
            return serviceEvent.GetEventsList().ToList();
        }




    }
}