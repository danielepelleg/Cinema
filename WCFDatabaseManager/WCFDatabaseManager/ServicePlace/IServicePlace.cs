using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WCFDatabaseManager
{
    // NOTA: è possibile utilizzare il comando "Rinomina" del menu "Refactoring" per modificare il nome di interfaccia "IServicePlace" nel codice e nel file di configurazione contemporaneamente.
    [ServiceContract]
    public interface IServicePlace
    {
        [OperationContract]
        string ControlloFK(int valore, string value_type);

        [OperationContract]
        List<Place> GetAvailablePlacesList(int eventCode);

        [OperationContract]
        string VerificaPosto(int codice_evento, int numero_posto);
    }
}
