using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace WCFDatabaseManager
{
    /*
     * Reserve Class
     * Store Reserve objects of the database
     * 
     * @author Daniele Pellegrini <daniele.pellegrini@studenti.unipr.it> - 285240
     * @author Riccardo Fava <riccardo.fava@studenti.unipr.it> - 287516
     */
     [DataContract]
    public class Reservation
    {
        [DataMember]
        public int PlaceNumber { get; set; }
        [DataMember]
        public int PrenotationCode { get; set; }
    }
}