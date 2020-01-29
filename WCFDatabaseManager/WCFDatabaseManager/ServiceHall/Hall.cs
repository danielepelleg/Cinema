using System.Runtime.Serialization;

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
    class Hall
    {
        [DataMember]
        public int HallCode { get; set; }
        [DataMember]
        public int Capacity { get; set; }

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
