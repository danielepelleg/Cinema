﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Il codice è stato generato da uno strumento.
//     Versione runtime:4.0.30319.42000
//
//     Le modifiche apportate a questo file possono provocare un comportamento non corretto e andranno perse se
//     il codice viene rigenerato.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WCFServer.ServiceReferenceEvent {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServiceReferenceEvent.IServiceEvent")]
    public interface IServiceEvent {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceEvent/AddEvent", ReplyAction="http://tempuri.org/IServiceEvent/AddEventResponse")]
        bool AddEvent(string usernameAdmin, System.DateTime dateTime, int filmCode, int hallCode, decimal price);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceEvent/AddEvent", ReplyAction="http://tempuri.org/IServiceEvent/AddEventResponse")]
        System.Threading.Tasks.Task<bool> AddEventAsync(string usernameAdmin, System.DateTime dateTime, int filmCode, int hallCode, decimal price);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceEvent/CancellazioneEvento", ReplyAction="http://tempuri.org/IServiceEvent/CancellazioneEventoResponse")]
        string CancellazioneEvento(int codiceevento);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceEvent/CancellazioneEvento", ReplyAction="http://tempuri.org/IServiceEvent/CancellazioneEventoResponse")]
        System.Threading.Tasks.Task<string> CancellazioneEventoAsync(int codiceevento);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceEvent/Visualizzazione_elenco_eventi", ReplyAction="http://tempuri.org/IServiceEvent/Visualizzazione_elenco_eventiResponse")]
        string Visualizzazione_elenco_eventi();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceEvent/Visualizzazione_elenco_eventi", ReplyAction="http://tempuri.org/IServiceEvent/Visualizzazione_elenco_eventiResponse")]
        System.Threading.Tasks.Task<string> Visualizzazione_elenco_eventiAsync();
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IServiceEventChannel : WCFServer.ServiceReferenceEvent.IServiceEvent, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class ServiceEventClient : System.ServiceModel.ClientBase<WCFServer.ServiceReferenceEvent.IServiceEvent>, WCFServer.ServiceReferenceEvent.IServiceEvent {
        
        public ServiceEventClient() {
        }
        
        public ServiceEventClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public ServiceEventClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ServiceEventClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ServiceEventClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public bool AddEvent(string usernameAdmin, System.DateTime dateTime, int filmCode, int hallCode, decimal price) {
            return base.Channel.AddEvent(usernameAdmin, dateTime, filmCode, hallCode, price);
        }
        
        public System.Threading.Tasks.Task<bool> AddEventAsync(string usernameAdmin, System.DateTime dateTime, int filmCode, int hallCode, decimal price) {
            return base.Channel.AddEventAsync(usernameAdmin, dateTime, filmCode, hallCode, price);
        }
        
        public string CancellazioneEvento(int codiceevento) {
            return base.Channel.CancellazioneEvento(codiceevento);
        }
        
        public System.Threading.Tasks.Task<string> CancellazioneEventoAsync(int codiceevento) {
            return base.Channel.CancellazioneEventoAsync(codiceevento);
        }
        
        public string Visualizzazione_elenco_eventi() {
            return base.Channel.Visualizzazione_elenco_eventi();
        }
        
        public System.Threading.Tasks.Task<string> Visualizzazione_elenco_eventiAsync() {
            return base.Channel.Visualizzazione_elenco_eventiAsync();
        }
    }
}
