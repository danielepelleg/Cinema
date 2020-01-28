using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WCFServer.ServiceFilm
{
    // NOTA: è possibile utilizzare il comando "Rinomina" del menu "Refactoring" per modificare il nome di interfaccia "IService1" nel codice e nel file di configurazione contemporaneamente.
    [ServiceContract]
    public interface IServiceFilm
    {
        [OperationContract]
        bool AddFilm(string titolo, int anno, string regia, int durata, DateTime datauscita, string genere);

        [OperationContract]
        bool DeleteFilm(int codicefilm);

        [OperationContract]
        string Visualizzazione_elenco_film();
    }
}
