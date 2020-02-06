using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

/* 
 * @author Daniele Pellegrini<daniele.pellegrini@studenti.unipr.it> - 285240
 * @author Riccardo Fava<riccardo.fava@studenti.unipr.it> - 287516
 */
namespace WCFDatabaseManager
{
    // NOTA: è possibile utilizzare il comando "Rinomina" del menu "Refactoring" per modificare il nome di interfaccia "IServiceUser" nel codice e nel file di configurazione contemporaneamente.
    [ServiceContract]
    public interface IServiceUser
    {
        [OperationContract]
        bool Registration(bool isAdmin, string username, string password, string name, string surname);

        [OperationContract]
        bool Login(bool isAdmin, string username, string password);

        [OperationContract]
        bool DeleteUser(string username);

        [OperationContract]
        bool EditUser(string oldUsername, string newUsername, string newPassword, string newName, string newSurname);

        [OperationContract]
        User GetUser(bool isAdmin, string username);

        [OperationContract]
        List<User> GetUsersList();

        [OperationContract]
        bool CheckStringFK(string value, string valueType);
    }
}
