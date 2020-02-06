using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace WCFDatabaseManager
{
    /*
     * Hall Class
     * Store Hall objects of the database
     * 
     * @author Daniele Pellegrini <daniele.pellegrini@studenti.unipr.it> - 285240
     * @author Riccardo Fava <riccardo.fava@studenti.unipr.it> - 287516
     */
    [DataContract]
    public class Hall
    {
        [DataMember]
        public int HallCode { get; set; }
        [DataMember]
        public int Capacity { get; set; }

        public Hall() { }

        public Hall(int hallCode, int capacity)
        {
            HallCode = hallCode;
            Capacity = capacity;
        }

        /*
         * Show information about a hall
         * 
         * @return the String with the hall details
         */
        public string showHall()
        {
            return "Hall Code:" + this.HallCode +
                ", Capacity:" + this.Capacity;
        }
    }
}
