using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace WCFDatabaseManager
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

        public Film() { }

        /*
         * Class Constructor
         */
        public Film(int filmCode, string title, int year, string direction, int duration, DateTime releaseDate, string genre)
        {
            FilmCode = filmCode;
            Title = title;
            Year = year;
            Direction = direction;
            Duration = duration;
            ReleaseDate = releaseDate;
            Genre = genre;
        }
    }
}
