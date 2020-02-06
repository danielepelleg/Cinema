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
    public class Place
    {
        [DataMember]
        public int PlaceNumber { get; set; }
        [DataMember]
        public int HallCode { get; set; }

        public Place(int placeNumber, int hallCode)
        {
            PlaceNumber = placeNumber;
            HallCode = hallCode;
        }

        public Place() { }
        
    }
}
