using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WCFServer
{
    /*
     * Film Class
     * Store Film objects of the database
     * 
     * @author Daniele Pellegrini <daniele.pellegrini@studenti.unipr.it> - 285240
     * @author Riccardo Fava <riccardo.fava@studenti.unipr.it> - 287516
     */
    public class Film
    {
        private int film_Code;
        private string _title;
        private int _year;
        private string _direction;
        private int _duration;
        private DateTime release_Date;
        private string _genre;

        public int filmCode {
            get => this.film_Code;
            set => this.film_Code = value;
        }
        
        public string title {
            get => this._title;
            set => this._title = value;
        }
        
        public int year {
            get => this._year;
            set => this._year = value;
        }
        
        public string direction {
            get => this._direction;
            set => this._direction = value;
        }
        
        public int duration {
            get => this._duration;
            set => this._duration = value;
        }
        
        public DateTime releaseDate {
            get => this.release_Date;
            set => this.release_Date = value; }
        
        public string genre {
            get => this._genre;
            set => this._genre = value;
        }

        /*
         * Show information about a film
         * 
         * @return the String with the film details
         */
        public string showFilm()
        {
            return this.film_Code + ", " + 
                this._title + ", " + 
                this._year.ToString() + ", " + 
                this._direction + ", " + 
                this._duration.ToString() + ", " + 
                this.release_Date.ToString() + ", " + 
                this._genre;
        }

    }
}
