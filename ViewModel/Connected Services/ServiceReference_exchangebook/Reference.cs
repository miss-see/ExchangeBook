﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

namespace ViewModel.ServiceReference_exchangebook {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServiceReference_exchangebook.ExchangeBookServiceSoap")]
    public interface ExchangeBookServiceSoap {
        
        // CODEGEN: 命名空间 http://tempuri.org/ 的元素名称 HelloWorldResult 以后生成的消息协定未标记为 nillable
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/HelloWorld", ReplyAction="*")]
        ViewModel.ServiceReference_exchangebook.HelloWorldResponse HelloWorld(ViewModel.ServiceReference_exchangebook.HelloWorldRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/HelloWorld", ReplyAction="*")]
        System.Threading.Tasks.Task<ViewModel.ServiceReference_exchangebook.HelloWorldResponse> HelloWorldAsync(ViewModel.ServiceReference_exchangebook.HelloWorldRequest request);
        
        // CODEGEN: 命名空间 http://tempuri.org/ 的元素名称 name 以后生成的消息协定未标记为 nillable
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/Login", ReplyAction="*")]
        ViewModel.ServiceReference_exchangebook.LoginResponse Login(ViewModel.ServiceReference_exchangebook.LoginRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/Login", ReplyAction="*")]
        System.Threading.Tasks.Task<ViewModel.ServiceReference_exchangebook.LoginResponse> LoginAsync(ViewModel.ServiceReference_exchangebook.LoginRequest request);
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class HelloWorldRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="HelloWorld", Namespace="http://tempuri.org/", Order=0)]
        public ViewModel.ServiceReference_exchangebook.HelloWorldRequestBody Body;
        
        public HelloWorldRequest() {
        }
        
        public HelloWorldRequest(ViewModel.ServiceReference_exchangebook.HelloWorldRequestBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute()]
    public partial class HelloWorldRequestBody {
        
        public HelloWorldRequestBody() {
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class HelloWorldResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="HelloWorldResponse", Namespace="http://tempuri.org/", Order=0)]
        public ViewModel.ServiceReference_exchangebook.HelloWorldResponseBody Body;
        
        public HelloWorldResponse() {
        }
        
        public HelloWorldResponse(ViewModel.ServiceReference_exchangebook.HelloWorldResponseBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class HelloWorldResponseBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string HelloWorldResult;
        
        public HelloWorldResponseBody() {
        }
        
        public HelloWorldResponseBody(string HelloWorldResult) {
            this.HelloWorldResult = HelloWorldResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class LoginRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="Login", Namespace="http://tempuri.org/", Order=0)]
        public ViewModel.ServiceReference_exchangebook.LoginRequestBody Body;
        
        public LoginRequest() {
        }
        
        public LoginRequest(ViewModel.ServiceReference_exchangebook.LoginRequestBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class LoginRequestBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string name;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public string pwd;
        
        public LoginRequestBody() {
        }
        
        public LoginRequestBody(string name, string pwd) {
            this.name = name;
            this.pwd = pwd;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class LoginResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="LoginResponse", Namespace="http://tempuri.org/", Order=0)]
        public ViewModel.ServiceReference_exchangebook.LoginResponseBody Body;
        
        public LoginResponse() {
        }
        
        public LoginResponse(ViewModel.ServiceReference_exchangebook.LoginResponseBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class LoginResponseBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string LoginResult;
        
        public LoginResponseBody() {
        }
        
        public LoginResponseBody(string LoginResult) {
            this.LoginResult = LoginResult;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ExchangeBookServiceSoapChannel : ViewModel.ServiceReference_exchangebook.ExchangeBookServiceSoap, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class ExchangeBookServiceSoapClient : System.ServiceModel.ClientBase<ViewModel.ServiceReference_exchangebook.ExchangeBookServiceSoap>, ViewModel.ServiceReference_exchangebook.ExchangeBookServiceSoap {
        
        public ExchangeBookServiceSoapClient() {
        }
        
        public ExchangeBookServiceSoapClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public ExchangeBookServiceSoapClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ExchangeBookServiceSoapClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ExchangeBookServiceSoapClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        ViewModel.ServiceReference_exchangebook.HelloWorldResponse ViewModel.ServiceReference_exchangebook.ExchangeBookServiceSoap.HelloWorld(ViewModel.ServiceReference_exchangebook.HelloWorldRequest request) {
            return base.Channel.HelloWorld(request);
        }
        
        public string HelloWorld() {
            ViewModel.ServiceReference_exchangebook.HelloWorldRequest inValue = new ViewModel.ServiceReference_exchangebook.HelloWorldRequest();
            inValue.Body = new ViewModel.ServiceReference_exchangebook.HelloWorldRequestBody();
            ViewModel.ServiceReference_exchangebook.HelloWorldResponse retVal = ((ViewModel.ServiceReference_exchangebook.ExchangeBookServiceSoap)(this)).HelloWorld(inValue);
            return retVal.Body.HelloWorldResult;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<ViewModel.ServiceReference_exchangebook.HelloWorldResponse> ViewModel.ServiceReference_exchangebook.ExchangeBookServiceSoap.HelloWorldAsync(ViewModel.ServiceReference_exchangebook.HelloWorldRequest request) {
            return base.Channel.HelloWorldAsync(request);
        }
        
        public System.Threading.Tasks.Task<ViewModel.ServiceReference_exchangebook.HelloWorldResponse> HelloWorldAsync() {
            ViewModel.ServiceReference_exchangebook.HelloWorldRequest inValue = new ViewModel.ServiceReference_exchangebook.HelloWorldRequest();
            inValue.Body = new ViewModel.ServiceReference_exchangebook.HelloWorldRequestBody();
            return ((ViewModel.ServiceReference_exchangebook.ExchangeBookServiceSoap)(this)).HelloWorldAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        ViewModel.ServiceReference_exchangebook.LoginResponse ViewModel.ServiceReference_exchangebook.ExchangeBookServiceSoap.Login(ViewModel.ServiceReference_exchangebook.LoginRequest request) {
            return base.Channel.Login(request);
        }
        
        public string Login(string name, string pwd) {
            ViewModel.ServiceReference_exchangebook.LoginRequest inValue = new ViewModel.ServiceReference_exchangebook.LoginRequest();
            inValue.Body = new ViewModel.ServiceReference_exchangebook.LoginRequestBody();
            inValue.Body.name = name;
            inValue.Body.pwd = pwd;
            ViewModel.ServiceReference_exchangebook.LoginResponse retVal = ((ViewModel.ServiceReference_exchangebook.ExchangeBookServiceSoap)(this)).Login(inValue);
            return retVal.Body.LoginResult;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<ViewModel.ServiceReference_exchangebook.LoginResponse> ViewModel.ServiceReference_exchangebook.ExchangeBookServiceSoap.LoginAsync(ViewModel.ServiceReference_exchangebook.LoginRequest request) {
            return base.Channel.LoginAsync(request);
        }
        
        public System.Threading.Tasks.Task<ViewModel.ServiceReference_exchangebook.LoginResponse> LoginAsync(string name, string pwd) {
            ViewModel.ServiceReference_exchangebook.LoginRequest inValue = new ViewModel.ServiceReference_exchangebook.LoginRequest();
            inValue.Body = new ViewModel.ServiceReference_exchangebook.LoginRequestBody();
            inValue.Body.name = name;
            inValue.Body.pwd = pwd;
            return ((ViewModel.ServiceReference_exchangebook.ExchangeBookServiceSoap)(this)).LoginAsync(inValue);
        }
    }
}