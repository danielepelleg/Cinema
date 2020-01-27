using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WCFServer
{
    /*
     * Hall Class
     * Store Hall objects of the database
     * 
     * @author Daniele Pellegrini <daniele.pellegrini@studenti.unipr.it> - 285240
     * @author Riccardo Fava <riccardo.fava@studenti.unipr.it> - 287516
     */
    class Hall
    {
        
        private int hall_Code;
        private int _capacity;

        public int hallCode { get => this.hall_Code; set => this.hall_Code = value; }
       
        public int capacity { get => this._capacity; set => this._capacity = value; }

        /*
         * Show information about a hall
         * 
         * @return the String with the hall details
         */
        public string showHall()
        {
            return "Hall Code:" + this.hall_Code + 
                ", Capacity:" + this._capacity;
        }
    }
}
