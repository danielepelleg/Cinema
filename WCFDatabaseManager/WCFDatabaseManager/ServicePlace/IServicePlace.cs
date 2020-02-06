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
        bool CheckIntFK(string value, string valueType);

        [OperationContract]
        List<Place> GetAvailablePlacesList(int eventCode);

        [OperationContract]
        bool CheckPlace(int eventCode, int placeNumber);
    }
}
