using System;
using System.Linq;
using System.Collections.Generic;
using WCFServer.ServiceReferenceUser;
using WCFServer.ServiceReferencePrenotation;
using WCFServer.ServiceReferenceFilm;
using WCFServer.ServiceReferenceEvent;
using WCFServer.ServiceReferenceHall;
using WCFServer.ServiceReferencePlace;
using Film = WCFServer.ServiceReferenceFilm.Film;
using Event = WCFServer.ServiceReferenceEvent.Event;

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

        public bool EditUser(string oldUsername, string newUsername, string newPassword, string newName, string newSurname) {
            return serviceUser.EditUser(oldUsername, newUsername, newPassword, newName, newSurname);
        }

        public User GetUser(bool isAdmin ,string username) {
            return serviceUser.GetUser(isAdmin, username);
        }

        public List<User> GetUsersList() {
            return serviceUser.GetUsersList().ToList();
        }

        public bool CheckStringFK(string value, string valueType) {
            return serviceUser.CheckStringFK(value, valueType);
        }

        // FILM METHODS

        public bool AddFilm(string title, int year, string direction, int duration, DateTime releaseDate, string genre) {
            return serviceFilm.AddFilm(title, year, direction, duration, releaseDate, genre);
        }

        public bool DeleteFilm(int filmCode) {
            return serviceFilm.DeleteFilm(filmCode);
        }

        public Film GetFilm(int filmCode) {
            return serviceFilm.GetFilm(filmCode);
        }

        public List<Film> GetFilmList() {
            return serviceFilm.GetFilmList().ToList();
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

        public List<Show> GetShowsList()
        {
            return serviceEvent.GetShowsList().ToList();
        }

        // PRENOTATION METHODS

        public bool AddPrenotation(DateTime dateTime, string usernameUser, int eventCode, int placeNumber) {
            return servicePrenotation.AddPrenotation(dateTime, usernameUser, eventCode, placeNumber);
        }

        public bool DeletePrenotation(int prenotationCode) {
            return servicePrenotation.DeletePrenotation(prenotationCode);
        }

        public List<Prenotation> GetPrenotationsList()
        {
            return servicePrenotation.GetPrenotationsList().ToList();
        }


        public List<Ticket> GetTicketsList(string username) {
            return servicePrenotation.GetTicketsList(username).ToList();
        }

        // HALL METHODS

        public List<Hall> GetHallsList() {
            return serviceHall.GetHallsList().ToList();
        }

        public string DrawHall(int eventCode) {
            return serviceHall.DrawHall(eventCode);
        }

        // PLACE METHODS

        public bool CheckIntFK(string value, string valueType) {
            return servicePlace.CheckIntFK(value, valueType);
        }

        public List<Place> GetAvailablePlacesList(int eventCode) {
            return servicePlace.GetAvailablePlacesList(eventCode).ToList();
        }


        public bool CheckPlace(int eventCode, int placeNumber) {
            return servicePlace.CheckPlace(eventCode, placeNumber);
        }



    }
}