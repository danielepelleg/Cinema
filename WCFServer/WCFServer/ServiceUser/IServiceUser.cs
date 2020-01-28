using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WCFServer
{
    // NOTA: è possibile utilizzare il comando "Rinomina" del menu "Refactoring" per modificare il nome di interfaccia "IService2" nel codice e nel file di configurazione contemporaneamente.
    [ServiceContract]
    public interface IServiceUser
    {
        [OperationContract]
        bool Registration(bool isAdmin, string username, string password, string name, string surname);

        [OperationContract]
        bool Login(bool isAdmin, string username, string password);
    }
}
