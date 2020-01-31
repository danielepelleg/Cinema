using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WCFDatabaseManager
{
    // NOTA: è possibile utilizzare il comando "Rinomina" del menu "Refactoring" per modificare il nome di interfaccia "IServicePrenotation" nel codice e nel file di configurazione contemporaneamente.
    [ServiceContract]
    public interface IServicePrenotation
    {
        [OperationContract]
        bool AddPrenotation(DateTime dateTime, string usernameUser, int eventCode, int placeNumber);

        [OperationContract]
        bool DeletePrenotation(int prenotationCode);

        [OperationContract]
        List<Ticket> GetTicketsList(string username);

    }
    /*
     * Prenotation Class
     * Store Prenotation objects of the database
     */
    [DataContract]
    public class Prenotation
    {
        [DataMember]
        public int PrenotationCode { get; set; }
        [DataMember]
        public DateTime DateTime { get; set; }
        [DataMember]
        public string UsernameUser { get; set; }
        [DataMember]
        public int EventCode { get; set; }
    
        public Prenotation() { }

        public Prenotation(Prenotation prenotation) {
            PrenotationCode = prenotation.PrenotationCode;
            DateTime = prenotation.DateTime;
            UsernameUser = prenotation.UsernameUser;
            EventCode = prenotation.EventCode;
        }

        public Prenotation(int prenotationCode, DateTime dateTime, string usernameUser, int eventCode)
        {
            PrenotationCode = prenotationCode;
            DateTime = dateTime;
            UsernameUser = usernameUser;
            EventCode = eventCode;
        }

        /*
         * Show information about a prenotation
         * 
         * @return the String with the prenotion details
         */
        public string showPrenotations()
        {
            return "Prenotation code:" + PrenotationCode +
                ", Date and time: " + DateTime +
                ", Username of the user: " + UsernameUser +
                ", Code of the event booked: " + EventCode;
        }
    }

    /*
     * Ticket Struct
     * Store Prenotation, Event, Film and Reservation objects 
     * as a Ticket for a User searching his event prenotations
     */
    [DataContract]
    public struct Ticket {

        public Ticket(Prenotation p, Event e, Film f, Reservation r) : this()
        {
            Prenotation = p;
            Event = e;
            Film = f;
            Reservation = r;
        }

        [DataMember]
        public Prenotation Prenotation { get; set; } 
        [DataMember]
        public Event Event { get; set; }
        [DataMember]
        public Film Film { get; set; }
        [DataMember]
        public Reservation Reservation { get; set; }

    }
}
