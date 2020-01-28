using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WCFDatabaseManager
{
    // NOTA: è possibile utilizzare il comando "Rinomina" del menu "Refactoring" per modificare il nome di classe "ServicePrenotation" nel codice e nel file di configurazione contemporaneamente.
    public class ServicePrenotation : IServicePrenotation
    {
        public void DoWork()
        {
        }

        public Prenotation MakePrenotation()
        {
            Prenotation p = new Prenotation(1,DateTime.Now,"corso",12);
            return p;
        }
    }
}
