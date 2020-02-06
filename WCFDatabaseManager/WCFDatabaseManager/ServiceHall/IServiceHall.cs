using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WCFDatabaseManager
{
    // NOTA: è possibile utilizzare il comando "Rinomina" del menu "Refactoring" per modificare il nome di interfaccia "IServiceHall" nel codice e nel file di configurazione contemporaneamente.
    [ServiceContract]
    public interface IServiceHall
    {
        [OperationContract]
        List<Hall> GetHallsList();

        [OperationContract]
        string DrawHall(int eventCode);
    }

    
}
