using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace WCFDatabaseManager
{
    /*
     * Prenotation Class
     * Store Prenotation objects of the database
     * 
     * @author Daniele Pellegrini <daniele.pellegrini@studenti.unipr.it> - 285240
     * @author Riccardo Fava <riccardo.fava@studenti.unipr.it> - 287516
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

        public Prenotation(Prenotation prenotation)
        {
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
        
    }
}
