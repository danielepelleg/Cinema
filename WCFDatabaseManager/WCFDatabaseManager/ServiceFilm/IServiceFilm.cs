using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WCFDatabaseManager
{
    // NOTA: è possibile utilizzare il comando "Rinomina" del menu "Refactoring" per modificare il nome di interfaccia "IServiceFilm" nel codice e nel file di configurazione contemporaneamente.
    [ServiceContract]
    public interface IServiceFilm
    {
        [OperationContract]
        bool AddFilm(string titolo, int anno, string regia, int durata, DateTime datauscita, string genere);

        [OperationContract]
        bool DeleteFilm(int codicefilm);

        [OperationContract]
        string Visualizzazione_elenco_film();

        [OperationContract]
        Film makeFilm();
    }
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
            this.FilmCode = filmCode;
            this.Title = title;
            this.Year = year;
            this.Direction = direction;
            this.Duration = duration;
            this.ReleaseDate = releaseDate;
            this.Genre = genre;
        }
    }
    }
