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
        void DoWork();
    }
    
    [DataContract]
    public class Event
    {
        [DataMember]
        public int filmCode { get; set; }
        [DataMember]
        private int event_Code { get; set; }
        [DataMember]
        private DateTime date_Time { get; set; }
        [DataMember]
        private int hall_code { get; set; }
        [DataMember]
        private string username_Admin { get; set; }
        [DataMember]
        private int film_Code { get; set; }
        [DataMember]
        private decimal _price { get; set; }

        public Event(int filmCode, int event_Code, DateTime date_Time, int hall_code, string username_Admin, int film_Code, decimal _price)
        {
            this.filmCode = filmCode;
            this.event_Code = event_Code;
            this.date_Time = date_Time;
            this.hall_code = hall_code;
            this.username_Admin = username_Admin;
            this.filmCode = filmCode;
            this._price = _price;
        }
    }

}
