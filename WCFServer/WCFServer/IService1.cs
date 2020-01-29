﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WCFServer
{
    // NOTA: è possibile utilizzare il comando "Rinomina" del menu "Refactoring" per modificare il nome di interfaccia "IService1" nel codice e nel file di configurazione contemporaneamente.
    [ServiceContract]
    public interface IService1
    {
        

        

        

        [OperationContract]
        string ControlloFK(int valore, string value_type);


        [OperationContract]
        string VisualizzazionePostiDisponibili(int codice_evento);

        

        [OperationContract]
        string VerificaPosto(int codice_evento, int numero_posto);

    }
    
}
     
