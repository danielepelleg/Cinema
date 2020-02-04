using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WCFDatabaseManager
{
    // NOTA: è possibile utilizzare il comando "Rinomina" del menu "Refactoring" per modificare il nome di interfaccia "IServiceEvent" nel codice e nel file di configurazione contemporaneamente.
    [ServiceContract]
    public interface IServiceEvent
    {
        [OperationContract]
        bool AddEvent(string usernameAdmin, DateTime dateTime, int filmCode, int hallCode, decimal price);

        [OperationContract]
        bool DeleteEvent(int eventCode);

        [OperationContract]
        List<Event> GetEventsList();

        [OperationContract]
        List<Show> GetShowsList();
    }

    /*
     * User Class
     * Store User objects of the database
     */
    [DataContract]
    public class Event
    {
        [DataMember]
        public int EventCode { get; set; }
        [DataMember]
        public DateTime DateTime { get; set; }
        [DataMember]
        public int FilmCode { get; set; }
        [DataMember]
        public int HallCode { get; set; }
        [DataMember]
        public string UsernameAdmin { get; set; }
        [DataMember]
        public decimal Price { get; set; }

        public Event() { }

        public Event(int eventCode, DateTime dateTime, int filmCode,  int hallCode, string usernameAdmin, decimal price)
        {
            EventCode = eventCode;
            DateTime = dateTime;
            FilmCode = filmCode;
            HallCode = hallCode;
            UsernameAdmin = usernameAdmin;
            FilmCode = filmCode;
            Price = price;
        }
    }

    /*
     * Ticket Struct
     * Store Prenotation, Event, Film and Reservation objects 
     * as a Ticket for a User searching his event prenotations
     */
    [DataContract]
    public struct Show {

        public Show(Event e, Film f) : this() {
            Event = e;
            Film = f;
        }

        public Show(int eventCode, DateTime dateTime, string title, int hallCode, decimal price) : this()
        {
            Event.EventCode = eventCode;
            Event.DateTime = dateTime;
            Film.Title = title;
            Event.HallCode = hallCode;
            Event.Price = price;
        }

        [DataMember]
        public Event Event { get; set; }
        [DataMember]
        public Film Film { get; set; }
    }
}
