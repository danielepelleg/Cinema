using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WCFServer
{
    /*
     * Event Class
     * Store Event objects of the database
     * 
     * @author Daniele Pellegrini <daniele.pellegrini@studenti.unipr.it> - 285240
     * @author Riccardo Fava <riccardo.fava@studenti.unipr.it> - 287516
     */
    class Event
    {
        private int event_Code;
        private DateTime date_Time;
        private int hall_code;
        private string username_Admin;
        private int film_Code;
        private decimal _price;

        public int eventCode {
            get => this.event_Code;
            set => this.event_Code = value;
        }
        
        public DateTime dateTime {
            get => date_Time;
            set => date_Time = value;
        }
        
        public int hallCode {
            get => this.hall_code;
            set => this.hall_code = value;
        }
        
        public string usernameAdmin {
            get => this.username_Admin;
            set => this.username_Admin = value;
        }
        
        public int filmCode {
            get => this.film_Code;
            set => this.film_Code = value;
        }
        
        public decimal price {
            get => this._price;
            set => this._price = value;
        }

        /*
         * Show information about an event
         * 
         * @return the String with the event details
         */
        public string showEvent()
        {
            return "Event code: " + this.event_Code + 
                ", Date and time: " + this.date_Time.ToString() + 
                ", Hall code: " + this.hall_code + 
                ", Film code: " + this.film_Code + 
                ", Price: " + this._price.ToString();
        }
    }
}
