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
        void DoWork();

        [OperationContract]
        Prenotation MakePrenotation();

        [OperationContract]
        string InserimentoPrenotazione(DateTime dateTime, string UsernameUser, int eventCode, int placeNumber);

        [OperationContract]
        string Visualizzazione_elenco_Prenotazioni(string user);

    }

    [DataContract]
    public class Prenotation
    {
        public int PrenotationCode { get; set; }
        public DateTime DateTime { get; set; }
        public string UsernameUser { get; set; }
        public int EventCode { get; set; }
    
        public Prenotation() { }

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
}
