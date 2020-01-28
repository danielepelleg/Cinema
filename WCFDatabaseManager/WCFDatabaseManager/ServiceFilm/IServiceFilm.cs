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
        void DoWork();

        [OperationContract]
        Film makeFilm();
    }
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
    }
    }
