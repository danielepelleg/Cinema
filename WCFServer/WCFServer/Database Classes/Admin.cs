using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WCFServer
{
    /*
     * Admin Class
     * Store Admin objects of the database
     * 
     * @author Daniele Pellegrini <daniele.pellegrini@studenti.unipr.it> - 285240
     * @author Riccardo Fava <riccardo.fava@studenti.unipr.it> - 287516
     */
    class Admin
    {
        private string _username;
        private string _password;
        private string _name;
        private string _surname;

        public string username {
            get => this._username;
            set => this._username = value;
        }
        
        public string password {
            get => this._password;
            set => this._password = value;
        }
        
        public string name {
            get => this._name;
            set => this._name = value;
        }
        
        public string surname {
            get => this._surname;
            set => this._surname = value;
        }

        /*
         * Show information about an Admin
         * 
         * @return the String with the admin details
         */
        public string showAdmin()
        {
            return "Username: " + this._username +
                ", Name: " + this._name +
                ", Surname: " + this._surname;
        }
    }
}
