using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using WCFServer.ServiceReferenceUser;
using WCFServer.ServiceReferencePrenotation;
using WCFServer.ServiceReferenceFilm;
using WCFServer.ServiceReferenceEvent;
using WCFServer.ServiceReferenceHall;
using WCFServer.ServiceReferencePlace;

namespace WCFServer
{
    // NOTA: è possibile utilizzare il comando "Rinomina" del menu "Refactoring" per modificare il nome di classe "Service1" nel codice e nel file di configurazione contemporaneamente.
    public class Service1 : IService1
    {
        private ServiceUserClient serviceUser = new ServiceReferenceUser.ServiceUserClient();
        private ServicePrenotationClient servicePrenotation = new ServiceReferencePrenotation.ServicePrenotationClient();
        private ServiceFilmClient serviceFilm = new ServiceReferenceFilm.ServiceFilmClient();
        private ServiceEventClient serviceEvent = new ServiceReferenceEvent.ServiceEventClient();
        private ServiceHallClient serviceHall = new ServiceReferenceHall.ServiceHallClient();
        private ServicePlaceClient servicePlace = new ServiceReferencePlace.ServicePlaceClient();





    }
}