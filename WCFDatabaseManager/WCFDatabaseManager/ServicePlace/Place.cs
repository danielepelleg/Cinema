using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace WCFDatabaseManager
{
    /*
     * Place Class
     * Store Place objects of the database
     * 
     * @author Daniele Pellegrini <daniele.pellegrini@studenti.unipr.it> - 285240
     * @author Riccardo Fava <riccardo.fava@studenti.unipr.it> - 287516
     */
    [DataContract]
    class Place
    {
        [DataMember]
        public int PlaceCode { get; set; }
        [DataMember]
        public int PlaceNumber { get; set; }
        [DataMember]
        public int HallCode { get; set; }

        /*
         * Show information about a place
         * 
         * @return the String with the place details
         */
        public string showPlaces()
        {
            return "Place code:" + this.PlaceCode +
                "Hall code:" + this.HallCode +
                ", Number: " + this.PlaceNumber;
        }
    }
}
