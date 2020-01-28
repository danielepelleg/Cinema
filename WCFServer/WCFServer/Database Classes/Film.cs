using System;
using System.Runtime.Serialization;

namespace WCFServer
{
    /*
     * Film Class
     * Store Film objects of the database
     * 
     * @author Daniele Pellegrini <daniele.pellegrini@studenti.unipr.it> - 285240
     * @author Riccardo Fava <riccardo.fava@studenti.unipr.it> - 287516
     */
    [DataContract]
    public class Film
    {
        [DataMember]
        public int filmCode { get; set; }
        [DataMember]
        public string title { get; set; }
        [DataMember]
        public int year { get; set; }
        [DataMember]
        public string direction { get; set; }
        [DataMember]
        public int duration { get; set; }
        [DataMember]
        public DateTime releaseDate { get; set; }
        [DataMember]
        public string genre { get; set; }

        /*
         * Class Constructor
         */
        public Film(int filmCode, string title, int year, string direction, int duration, DateTime releaseDate, string genre)
        {
            this.filmCode = filmCode;
            this.title = title;
            this.year = year;
            this.direction = direction;
            this.duration = duration;
            this.releaseDate = releaseDate;
            this.genre = genre;
        }


        public Film() { }

        /*
         * Show information about a film
         * 
         * @return the String with the film details
         */
        public string showFilm()
        {
            return this.filmCode + ", " +
                this.title + ", " +
                this.year.ToString() + ", " +
                this.direction + ", " +
                this.duration.ToString() + ", " +
                this.releaseDate.ToString() + ", " +
                this.genre;
        }
    }


}
