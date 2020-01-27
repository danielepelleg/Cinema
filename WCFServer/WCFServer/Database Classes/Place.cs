using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WCFServer
{
    /*
     * Place Class
     * Store Place objects of the database
     * 
     * @author Daniele Pellegrini <daniele.pellegrini@studenti.unipr.it> - 285240
     * @author Riccardo Fava <riccardo.fava@studenti.unipr.it> - 287516
     */
    class Place
    {
        private int place_Code;
        private int place_Number;
        private int hall_Code;

        public int placeCode {
            get => this.place_Code;
            set => this.place_Code = value;
        }
        
        public int placeNumber {
            get => this.place_Number;
            set => this.place_Number = value;
        }
        
        public int hallCode {
            get => this.hall_Code;
            set => this.hall_Code = value;
        }

        /*
         * Show information about a place
         * 
         * @return the String with the place details
         */
        public string showPlaces()
        {
            return "Place code:" + this.place_Code + 
                "Hall code:" + this.hall_Code + 
                ", Number: " + this.place_Number;
        }
    }
}
