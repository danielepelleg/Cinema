﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Il codice è stato generato da uno strumento.
//     Versione runtime:4.0.30319.42000
//
//     Le modifiche apportate a questo file possono provocare un comportamento non corretto e andranno perse se
//     il codice viene rigenerato.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WCFServer.ServiceReferenceHall {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Hall", Namespace="http://schemas.datacontract.org/2004/07/WCFDatabaseManager")]
    [System.SerializableAttribute()]
    public partial class Hall : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int CapacityField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int HallCodeField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Capacity {
            get {
                return this.CapacityField;
            }
            set {
                if ((this.CapacityField.Equals(value) != true)) {
                    this.CapacityField = value;
                    this.RaisePropertyChanged("Capacity");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int HallCode {
            get {
                return this.HallCodeField;
            }
            set {
                if ((this.HallCodeField.Equals(value) != true)) {
                    this.HallCodeField = value;
                    this.RaisePropertyChanged("HallCode");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServiceReferenceHall.IServiceHall")]
    public interface IServiceHall {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceHall/GetHallsList", ReplyAction="http://tempuri.org/IServiceHall/GetHallsListResponse")]
        WCFServer.ServiceReferenceHall.Hall[] GetHallsList();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceHall/GetHallsList", ReplyAction="http://tempuri.org/IServiceHall/GetHallsListResponse")]
        System.Threading.Tasks.Task<WCFServer.ServiceReferenceHall.Hall[]> GetHallsListAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceHall/DrawHall", ReplyAction="http://tempuri.org/IServiceHall/DrawHallResponse")]
        string DrawHall(int eventCode);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceHall/DrawHall", ReplyAction="http://tempuri.org/IServiceHall/DrawHallResponse")]
        System.Threading.Tasks.Task<string> DrawHallAsync(int eventCode);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IServiceHallChannel : WCFServer.ServiceReferenceHall.IServiceHall, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class ServiceHallClient : System.ServiceModel.ClientBase<WCFServer.ServiceReferenceHall.IServiceHall>, WCFServer.ServiceReferenceHall.IServiceHall {
        
        public ServiceHallClient() {
        }
        
        public ServiceHallClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public ServiceHallClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ServiceHallClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ServiceHallClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public WCFServer.ServiceReferenceHall.Hall[] GetHallsList() {
            return base.Channel.GetHallsList();
        }
        
        public System.Threading.Tasks.Task<WCFServer.ServiceReferenceHall.Hall[]> GetHallsListAsync() {
            return base.Channel.GetHallsListAsync();
        }
        
        public string DrawHall(int eventCode) {
            return base.Channel.DrawHall(eventCode);
        }
        
        public System.Threading.Tasks.Task<string> DrawHallAsync(int eventCode) {
            return base.Channel.DrawHallAsync(eventCode);
        }
    }
}
