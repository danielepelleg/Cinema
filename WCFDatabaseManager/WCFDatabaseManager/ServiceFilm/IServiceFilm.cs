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
        bool AddFilm(string title, int year, string direction, int duration, DateTime releaseDate, string genre);

        [OperationContract]
        bool DeleteFilm(int filmCode);

        [OperationContract]
        Film GetFilm(int filmCode);

        [OperationContract]
        List<Film> GetFilmList();        
    }
    
}
