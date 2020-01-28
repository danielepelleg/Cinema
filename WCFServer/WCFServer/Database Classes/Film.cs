using System;
using System.Runtime.Serialization;

namespace WCFServer
{
    [DataContract]
    public class Film
    {
        [DataMember]
        public int FilmCode { get; set; }
        [DataMember]
        public string Title { get; set; }
        [DataMember]
        public int Year { get; set; }
        [DataMember]
        public string Direction { get; set; }
        [DataMember]
        public int Duration { get; set; }
        [DataMember]
        public DateTime ReleaseDate { get; set; }
        [DataMember]
        public string Genre { get; set; }

        /*
         * Class Constructor
         */
        public Film(int filmCode, string title, int year, string direction, int duration, DateTime releaseDate, string genre)
        {
            this.FilmCode = filmCode;
            this.Title = title;
            this.Year = year;
            this.Direction = direction;
            this.Duration = duration;
            this.ReleaseDate = releaseDate;
            this.Genre = genre;
        }


        public Film() { }

        /*
         * Show information about a film
         * 
         * @return the String with the film details
         */
        public string showFilm()
        {
            return this.FilmCode + ", " +
                this.Title + ", " +
                this.Year.ToString() + ", " +
                this.Direction + ", " +
                this.Duration.ToString() + ", " +
                this.ReleaseDate.ToString() + ", " +
                this.Genre;
        }
    }


}
