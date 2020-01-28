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
    }

    [DataContract]
    public class Prenotation
    {
        private int prenotation_Code { get; set; }
        private DateTime date_Time { get; set; }
        private string username_User { get; set; }
        private int event_Code { get; set; }
    
        public Prenotation(int prenotation_Code, DateTime date_Time, string username_User, int event_Code)
        {
            this.prenotation_Code = prenotation_Code;
            this.date_Time = date_Time;
            this.username_User = username_User;
            this.event_Code = event_Code;
        }
    }
}
