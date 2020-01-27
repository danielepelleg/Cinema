using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WCFServer
{
    /*
     * Prenotation Class
     * Store Prenotation objects of the database
     * 
     * @author Daniele Pellegrini <daniele.pellegrini@studenti.unipr.it> - 285240
     * @author Riccardo Fava <riccardo.fava@studenti.unipr.it> - 287516
     */
    class Prenotation
    {
        private int prenotation_Code;
        private DateTime date_Time;
        private string username_User;
        private int event_Code;

        public int prenotationCode {
            get => this.prenotation_Code;
            set => this.prenotation_Code = value;
        }

        public DateTime dateTime {
            get => this.date_Time;
            set => this.date_Time = value;
        }
        
        public string usernameUser {
            get => this.username_User;
            set => this.username_User = value;
        }
        
        public int eventCode {
            get => this.event_Code;
            set => this.event_Code = value;
        }

        /*
         * Show information about a prenotation
         * 
         * @return the String with the prenotion details
         */ 
        public string showPrenotations()
        {
            return "Prenotation code:" + this.prenotation_Code + 
                ", Date and time: " + this.date_Time + 
                ", Username of the user: " + this.username_User +
                ", Code of the event booked: " + this.event_Code;
        }
    }
}
