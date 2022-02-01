﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DatabaseManager.ServiceReference1 {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="AnalogOutput", Namespace="http://schemas.datacontract.org/2004/07/SCADACore")]
    [System.SerializableAttribute()]
    public partial class AnalogOutput : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string DescriptionField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private double HighLimitField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string IOAdressField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int IdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private double InitialValueField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private double LowLimitField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string TagNameField;
        
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
        public string Description {
            get {
                return this.DescriptionField;
            }
            set {
                if ((object.ReferenceEquals(this.DescriptionField, value) != true)) {
                    this.DescriptionField = value;
                    this.RaisePropertyChanged("Description");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public double HighLimit {
            get {
                return this.HighLimitField;
            }
            set {
                if ((this.HighLimitField.Equals(value) != true)) {
                    this.HighLimitField = value;
                    this.RaisePropertyChanged("HighLimit");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string IOAdress {
            get {
                return this.IOAdressField;
            }
            set {
                if ((object.ReferenceEquals(this.IOAdressField, value) != true)) {
                    this.IOAdressField = value;
                    this.RaisePropertyChanged("IOAdress");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Id {
            get {
                return this.IdField;
            }
            set {
                if ((this.IdField.Equals(value) != true)) {
                    this.IdField = value;
                    this.RaisePropertyChanged("Id");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public double InitialValue {
            get {
                return this.InitialValueField;
            }
            set {
                if ((this.InitialValueField.Equals(value) != true)) {
                    this.InitialValueField = value;
                    this.RaisePropertyChanged("InitialValue");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public double LowLimit {
            get {
                return this.LowLimitField;
            }
            set {
                if ((this.LowLimitField.Equals(value) != true)) {
                    this.LowLimitField = value;
                    this.RaisePropertyChanged("LowLimit");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string TagName {
            get {
                return this.TagNameField;
            }
            set {
                if ((object.ReferenceEquals(this.TagNameField, value) != true)) {
                    this.TagNameField = value;
                    this.RaisePropertyChanged("TagName");
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
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="DigitalOutput", Namespace="http://schemas.datacontract.org/2004/07/SCADACore")]
    [System.SerializableAttribute()]
    public partial class DigitalOutput : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string IO_AdressField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int IdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string descriptionField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private double initial_ValueField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string tag_nameField;
        
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
        public string IO_Adress {
            get {
                return this.IO_AdressField;
            }
            set {
                if ((object.ReferenceEquals(this.IO_AdressField, value) != true)) {
                    this.IO_AdressField = value;
                    this.RaisePropertyChanged("IO_Adress");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Id {
            get {
                return this.IdField;
            }
            set {
                if ((this.IdField.Equals(value) != true)) {
                    this.IdField = value;
                    this.RaisePropertyChanged("Id");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string description {
            get {
                return this.descriptionField;
            }
            set {
                if ((object.ReferenceEquals(this.descriptionField, value) != true)) {
                    this.descriptionField = value;
                    this.RaisePropertyChanged("description");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public double initial_Value {
            get {
                return this.initial_ValueField;
            }
            set {
                if ((this.initial_ValueField.Equals(value) != true)) {
                    this.initial_ValueField = value;
                    this.RaisePropertyChanged("initial_Value");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string tag_name {
            get {
                return this.tag_nameField;
            }
            set {
                if ((object.ReferenceEquals(this.tag_nameField, value) != true)) {
                    this.tag_nameField = value;
                    this.RaisePropertyChanged("tag_name");
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
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServiceReference1.IAuthentication")]
    public interface IAuthentication {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAuthentication/Registration", ReplyAction="http://tempuri.org/IAuthentication/RegistrationResponse")]
        string Registration(string name, string surname, string username, string password);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAuthentication/Registration", ReplyAction="http://tempuri.org/IAuthentication/RegistrationResponse")]
        System.Threading.Tasks.Task<string> RegistrationAsync(string name, string surname, string username, string password);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAuthentication/DeleteProfile", ReplyAction="http://tempuri.org/IAuthentication/DeleteProfileResponse")]
        bool DeleteProfile(string username, string password);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAuthentication/DeleteProfile", ReplyAction="http://tempuri.org/IAuthentication/DeleteProfileResponse")]
        System.Threading.Tasks.Task<bool> DeleteProfileAsync(string username, string password);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAuthentication/Login", ReplyAction="http://tempuri.org/IAuthentication/LoginResponse")]
        string Login(string username, string password);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAuthentication/Login", ReplyAction="http://tempuri.org/IAuthentication/LoginResponse")]
        System.Threading.Tasks.Task<string> LoginAsync(string username, string password);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAuthentication/ChangePassword", ReplyAction="http://tempuri.org/IAuthentication/ChangePasswordResponse")]
        string ChangePassword(string username, string password, string update_password);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAuthentication/ChangePassword", ReplyAction="http://tempuri.org/IAuthentication/ChangePasswordResponse")]
        System.Threading.Tasks.Task<string> ChangePasswordAsync(string username, string password, string update_password);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAuthentication/Logout", ReplyAction="http://tempuri.org/IAuthentication/LogoutResponse")]
        bool Logout(string token);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAuthentication/Logout", ReplyAction="http://tempuri.org/IAuthentication/LogoutResponse")]
        System.Threading.Tasks.Task<bool> LogoutAsync(string token);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IAuthenticationChannel : DatabaseManager.ServiceReference1.IAuthentication, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class AuthenticationClient : System.ServiceModel.ClientBase<DatabaseManager.ServiceReference1.IAuthentication>, DatabaseManager.ServiceReference1.IAuthentication {
        
        public AuthenticationClient() {
        }
        
        public AuthenticationClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public AuthenticationClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public AuthenticationClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public AuthenticationClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public string Registration(string name, string surname, string username, string password) {
            return base.Channel.Registration(name, surname, username, password);
        }
        
        public System.Threading.Tasks.Task<string> RegistrationAsync(string name, string surname, string username, string password) {
            return base.Channel.RegistrationAsync(name, surname, username, password);
        }
        
        public bool DeleteProfile(string username, string password) {
            return base.Channel.DeleteProfile(username, password);
        }
        
        public System.Threading.Tasks.Task<bool> DeleteProfileAsync(string username, string password) {
            return base.Channel.DeleteProfileAsync(username, password);
        }
        
        public string Login(string username, string password) {
            return base.Channel.Login(username, password);
        }
        
        public System.Threading.Tasks.Task<string> LoginAsync(string username, string password) {
            return base.Channel.LoginAsync(username, password);
        }
        
        public string ChangePassword(string username, string password, string update_password) {
            return base.Channel.ChangePassword(username, password, update_password);
        }
        
        public System.Threading.Tasks.Task<string> ChangePasswordAsync(string username, string password, string update_password) {
            return base.Channel.ChangePasswordAsync(username, password, update_password);
        }
        
        public bool Logout(string token) {
            return base.Channel.Logout(token);
        }
        
        public System.Threading.Tasks.Task<bool> LogoutAsync(string token) {
            return base.Channel.LogoutAsync(token);
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServiceReference1.IDatabaseManager")]
    public interface IDatabaseManager {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDatabaseManager/AddAO", ReplyAction="http://tempuri.org/IDatabaseManager/AddAOResponse")]
        void AddAO(DatabaseManager.ServiceReference1.AnalogOutput AO);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDatabaseManager/AddAO", ReplyAction="http://tempuri.org/IDatabaseManager/AddAOResponse")]
        System.Threading.Tasks.Task AddAOAsync(DatabaseManager.ServiceReference1.AnalogOutput AO);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDatabaseManager/AddDO", ReplyAction="http://tempuri.org/IDatabaseManager/AddDOResponse")]
        void AddDO(DatabaseManager.ServiceReference1.DigitalOutput DO);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDatabaseManager/AddDO", ReplyAction="http://tempuri.org/IDatabaseManager/AddDOResponse")]
        System.Threading.Tasks.Task AddDOAsync(DatabaseManager.ServiceReference1.DigitalOutput DO);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDatabaseManager/LoadDataToGrid", ReplyAction="http://tempuri.org/IDatabaseManager/LoadDataToGridResponse")]
        DatabaseManager.ServiceReference1.DigitalOutput[] LoadDataToGrid();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDatabaseManager/LoadDataToGrid", ReplyAction="http://tempuri.org/IDatabaseManager/LoadDataToGridResponse")]
        System.Threading.Tasks.Task<DatabaseManager.ServiceReference1.DigitalOutput[]> LoadDataToGridAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDatabaseManager/removeDO", ReplyAction="http://tempuri.org/IDatabaseManager/removeDOResponse")]
        void removeDO(DatabaseManager.ServiceReference1.DigitalOutput DO);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDatabaseManager/removeDO", ReplyAction="http://tempuri.org/IDatabaseManager/removeDOResponse")]
        System.Threading.Tasks.Task removeDOAsync(DatabaseManager.ServiceReference1.DigitalOutput DO);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDatabaseManager/removeAO", ReplyAction="http://tempuri.org/IDatabaseManager/removeAOResponse")]
        void removeAO(DatabaseManager.ServiceReference1.AnalogOutput AO);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDatabaseManager/removeAO", ReplyAction="http://tempuri.org/IDatabaseManager/removeAOResponse")]
        System.Threading.Tasks.Task removeAOAsync(DatabaseManager.ServiceReference1.AnalogOutput AO);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDatabaseManager/LoadDataToGridAO", ReplyAction="http://tempuri.org/IDatabaseManager/LoadDataToGridAOResponse")]
        DatabaseManager.ServiceReference1.AnalogOutput[] LoadDataToGridAO();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDatabaseManager/LoadDataToGridAO", ReplyAction="http://tempuri.org/IDatabaseManager/LoadDataToGridAOResponse")]
        System.Threading.Tasks.Task<DatabaseManager.ServiceReference1.AnalogOutput[]> LoadDataToGridAOAsync();
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IDatabaseManagerChannel : DatabaseManager.ServiceReference1.IDatabaseManager, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class DatabaseManagerClient : System.ServiceModel.ClientBase<DatabaseManager.ServiceReference1.IDatabaseManager>, DatabaseManager.ServiceReference1.IDatabaseManager {
        
        public DatabaseManagerClient() {
        }
        
        public DatabaseManagerClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public DatabaseManagerClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public DatabaseManagerClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public DatabaseManagerClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public void AddAO(DatabaseManager.ServiceReference1.AnalogOutput AO) {
            base.Channel.AddAO(AO);
        }
        
        public System.Threading.Tasks.Task AddAOAsync(DatabaseManager.ServiceReference1.AnalogOutput AO) {
            return base.Channel.AddAOAsync(AO);
        }
        
        public void AddDO(DatabaseManager.ServiceReference1.DigitalOutput DO) {
            base.Channel.AddDO(DO);
        }
        
        public System.Threading.Tasks.Task AddDOAsync(DatabaseManager.ServiceReference1.DigitalOutput DO) {
            return base.Channel.AddDOAsync(DO);
        }
        
        public DatabaseManager.ServiceReference1.DigitalOutput[] LoadDataToGrid() {
            return base.Channel.LoadDataToGrid();
        }
        
        public System.Threading.Tasks.Task<DatabaseManager.ServiceReference1.DigitalOutput[]> LoadDataToGridAsync() {
            return base.Channel.LoadDataToGridAsync();
        }
        
        public void removeDO(DatabaseManager.ServiceReference1.DigitalOutput DO) {
            base.Channel.removeDO(DO);
        }
        
        public System.Threading.Tasks.Task removeDOAsync(DatabaseManager.ServiceReference1.DigitalOutput DO) {
            return base.Channel.removeDOAsync(DO);
        }
        
        public void removeAO(DatabaseManager.ServiceReference1.AnalogOutput AO) {
            base.Channel.removeAO(AO);
        }
        
        public System.Threading.Tasks.Task removeAOAsync(DatabaseManager.ServiceReference1.AnalogOutput AO) {
            return base.Channel.removeAOAsync(AO);
        }
        
        public DatabaseManager.ServiceReference1.AnalogOutput[] LoadDataToGridAO() {
            return base.Channel.LoadDataToGridAO();
        }
        
        public System.Threading.Tasks.Task<DatabaseManager.ServiceReference1.AnalogOutput[]> LoadDataToGridAOAsync() {
            return base.Channel.LoadDataToGridAOAsync();
        }
    }
}
