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
        bool AddSubscription(string username);

        [OperationContract]
        bool DeleteSubscription(string username);

        [OperationContract]
        bool GetSubscription(string username);

        [OperationContract]
        List<Prenotation> GetPrenotationsList();

        [OperationContract]
        List<Ticket> GetTicketsList(string username);

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
