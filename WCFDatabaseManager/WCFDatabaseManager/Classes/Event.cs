using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace WCFDatabaseManager
{
    /*
     * Event Class
     * Store Event objects of the database
     * 
     * @author Daniele Pellegrini <daniele.pellegrini@studenti.unipr.it> - 285240
     * @author Riccardo Fava <riccardo.fava@studenti.unipr.it> - 287516
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

        public Event(int eventCode, DateTime dateTime, int filmCode, int hallCode, string usernameAdmin, decimal price)
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
}
