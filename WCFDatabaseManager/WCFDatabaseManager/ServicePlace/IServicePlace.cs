using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WCFDatabaseManager
{
    // NOTA: è possibile utilizzare il comando "Rinomina" del menu "Refactoring" per modificare il nome di interfaccia "IServicePlace" nel codice e nel file di configurazione contemporaneamente.
    [ServiceContract]
    public interface IServicePlace
    {
        [OperationContract]
        bool CheckIntFK(string value, string valueType);

        [OperationContract]
        List<Place> GetAvailablePlacesList(int eventCode);

        [OperationContract]
        bool CheckPlace(int eventCode, int placeNumber);
    }
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

        public Place(int placeNumber, int hallCode) {
            PlaceNumber = placeNumber;
            HallCode = hallCode;
        }

        public Place() {}

        /*
         * Show information about a place
         * 
         * @return the String with the place details
         */
        public string showPlaces()
        {
            return  "Place Number: " + this.PlaceNumber +
                    "Hall code:" + this.HallCode;
                
        }
    }

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

        public Reservation(int placeNumber, int prenotationCode)
        {
            PlaceNumber = placeNumber;
            PrenotationCode = prenotationCode;
        }

        public Reservation() { }
    }
}
