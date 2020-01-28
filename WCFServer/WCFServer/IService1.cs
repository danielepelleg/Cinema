using System;
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
        bool registration(bool isAdmin, string username, string password, string name, string surname);

        [OperationContract]
        bool login(bool isAdmin, string username, string password);

        [OperationContract]
        bool addFilm(string titolo, int anno, string regia, int durata, DateTime datauscita, string genere);

        [OperationContract]
        string CancellazioneFilm(int codicefilm);

        [OperationContract]
        string InserimentoEvento(string usernameadmin, DateTime data_e_ora, int codice_film, int codice_sala, decimal prezzo);

        [OperationContract]
        string CancellazioneEvento(int codiceevento);

        [OperationContract]
        string Visualizzazione_elenco_film();

        [OperationContract]
        string Visualizzazione_elenco_eventi();

        [OperationContract]
        string VisualizzazioneSale();

        [OperationContract]
        string ControlloFK(int valore, string value_type);

        [OperationContract]
        string Visualizzazione_elenco_UtentiFree();

        [OperationContract]
        string Visualizzazione_elenco_Prenotazioni(string user);

        [OperationContract]
        string VisualizzazionePostiDisponibili(int codice_evento);

        [OperationContract]
        string InserimentoPrenotazione(DateTime dataora, string username_utentefree, int codice_evento, int numero_posto);

        [OperationContract]
        string RappresentaSale(int codice_evento);

        [OperationContract]
        string VerificaPosto(int codice_evento, int numero_posto);

    }
    
}
     
