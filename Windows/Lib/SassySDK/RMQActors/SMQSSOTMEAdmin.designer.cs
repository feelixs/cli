
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.MessagePatterns;
using SassyMQ.Lib.RabbitMQ;
using SassyMQ.SSOTME.Lib.RabbitMQ;
using SassyMQ.Lib.RabbitMQ.Payload;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SassyMQ.SSOTME.Lib;

namespace SassyMQ.SSOTME.Lib.RMQActors
{
    public partial class SMQSSOTMEAdmin : SSOTMEActorBase
    {
     
        public SMQSSOTMEAdmin(bool isAutoConnect = true)
            : base("ssotmeadmin.all", isAutoConnect)
        {
        }
        // SSoT - SST
        public virtual bool Connect(string virtualHost, string username, string password)
        {
            return base.Connect(virtualHost, username, password);
        }   

        protected override void CheckRouting(SSOTMEPayload payload) 
        {
            this.CheckRouting(payload, false);
        }

        partial void CheckPayload(SSOTMEPayload payload);

        private void Reply(SSOTMEPayload payload)
        {
            if (!System.String.IsNullOrEmpty(payload.ReplyTo))
            {
                payload.DirectMessageQueue = this.QueueName;
                this.CheckPayload(payload);
                this.RMQChannel.BasicPublish("", payload.ReplyTo, body: Encoding.UTF8.GetBytes(payload.ToJSonString()));
            }
        }

        protected override void CheckRouting(SSOTMEPayload payload, bool isDirectMessage) 
        {
            // if (payload.IsDirectMessage && !isDirectMessage) return;

            
            // And can also hear everything which : TranspilerHost hears.
            
             if (payload.IsLexiconTerm(LexiconTermEnum.ssotmecoordinator_getinstances_transpilerhost)) 
            {
                this.OnSSOTMECoordinatorGetInstancesReceived(payload);
                this.Reply(payload);
            }
        
            else  if (payload.IsLexiconTerm(LexiconTermEnum.ssotmecoordinator_transpilerequested_transpilerhost)) 
            {
                this.OnSSOTMECoordinatorTranspileRequestedReceived(payload);
                this.Reply(payload);
            }
        
            else  if (payload.IsLexiconTerm(LexiconTermEnum.ssotmecoordinator_stopinstance_transpilerhost)) 
            {
                this.OnSSOTMECoordinatorStopInstanceReceived(payload);
                this.Reply(payload);
            }
        
            else  if (payload.IsLexiconTerm(LexiconTermEnum.ssotmecoordinator_stophost_transpilerhost)) 
            {
                this.OnSSOTMECoordinatorStopHostReceived(payload);
                this.Reply(payload);
            }
        
            // And can also hear everything which : AccountHolder hears.
            
            // And can also hear everything which : PublicUser hears.
            
             if (payload.IsLexiconTerm(LexiconTermEnum.ssotmecoordinator_transpileronline_publicuser)) 
            {
                this.OnSSOTMECoordinatorTranspilerOnlineReceived(payload);
                this.Reply(payload);
            }
        
            else  if (payload.IsLexiconTerm(LexiconTermEnum.ssotmecoordinator_transpileroffline_publicuser)) 
            {
                this.OnSSOTMECoordinatorTranspilerOfflineReceived(payload);
                this.Reply(payload);
            }
        
        }

        
            public void SSOTMEAdminPing(DMProxy proxy) {
                this.SSOTMEAdminPing(this.CreatePayload(), proxy);
            }

            public void SSOTMEAdminPing(System.String content, DMProxy proxy) {
                var payload = this.CreatePayload();
                payload.Content = content;
                this.SSOTMEAdminPing(payload, proxy);
            }

            public void SSOTMEAdminPing(SSOTMEPayload payload, DMProxy proxy)
            {
                payload.IsDirectMessage = true;
                this.SendMessage(payload, "Ping - ",
                        "ssotmeadminmic", "ssotmecoordinator.general.ssotmeadmin.ping", proxy.RoutingKey);
             }

 
        
            public void SSOTMEAdminAddPlatformCategory(DMProxy proxy) {
                this.SSOTMEAdminAddPlatformCategory(this.CreatePayload(), proxy);
            }

            public void SSOTMEAdminAddPlatformCategory(System.String content, DMProxy proxy) {
                var payload = this.CreatePayload();
                payload.Content = content;
                this.SSOTMEAdminAddPlatformCategory(payload, proxy);
            }

            public void SSOTMEAdminAddPlatformCategory(SSOTMEPayload payload, DMProxy proxy)
            {
                payload.IsDirectMessage = true;
                this.SendMessage(payload, "Add Platform Category - ",
                        "ssotmeadminmic", "ssotmecoordinator.general.ssotmeadmin.addplatformcategory", proxy.RoutingKey);
             }

 
        
            public void SSOTMEAdminUpdatePlatformCategory(DMProxy proxy) {
                this.SSOTMEAdminUpdatePlatformCategory(this.CreatePayload(), proxy);
            }

            public void SSOTMEAdminUpdatePlatformCategory(System.String content, DMProxy proxy) {
                var payload = this.CreatePayload();
                payload.Content = content;
                this.SSOTMEAdminUpdatePlatformCategory(payload, proxy);
            }

            public void SSOTMEAdminUpdatePlatformCategory(SSOTMEPayload payload, DMProxy proxy)
            {
                payload.IsDirectMessage = true;
                this.SendMessage(payload, "Update Platform Category - ",
                        "ssotmeadminmic", "ssotmecoordinator.general.ssotmeadmin.updateplatformcategory", proxy.RoutingKey);
             }

 
        
            public void SSOTMEAdminAddTranspilerPlatform(DMProxy proxy) {
                this.SSOTMEAdminAddTranspilerPlatform(this.CreatePayload(), proxy);
            }

            public void SSOTMEAdminAddTranspilerPlatform(System.String content, DMProxy proxy) {
                var payload = this.CreatePayload();
                payload.Content = content;
                this.SSOTMEAdminAddTranspilerPlatform(payload, proxy);
            }

            public void SSOTMEAdminAddTranspilerPlatform(SSOTMEPayload payload, DMProxy proxy)
            {
                payload.IsDirectMessage = true;
                this.SendMessage(payload, "Add Transpiler Platform - ",
                        "ssotmeadminmic", "ssotmecoordinator.general.ssotmeadmin.addtranspilerplatform", proxy.RoutingKey);
             }

 
        
            public void SSOTMEAdminUpdateTranspilerPlatform(DMProxy proxy) {
                this.SSOTMEAdminUpdateTranspilerPlatform(this.CreatePayload(), proxy);
            }

            public void SSOTMEAdminUpdateTranspilerPlatform(System.String content, DMProxy proxy) {
                var payload = this.CreatePayload();
                payload.Content = content;
                this.SSOTMEAdminUpdateTranspilerPlatform(payload, proxy);
            }

            public void SSOTMEAdminUpdateTranspilerPlatform(SSOTMEPayload payload, DMProxy proxy)
            {
                payload.IsDirectMessage = true;
                this.SendMessage(payload, "Update Transpiler Platform - ",
                        "ssotmeadminmic", "ssotmecoordinator.general.ssotmeadmin.updatetranspilerplatform", proxy.RoutingKey);
             }

 
        
            // And can also say/hear everything which : TranspilerHost hears.
            
        public event System.EventHandler<PayloadEventArgs<SSOTMEPayload>> SSOTMECoordinatorGetInstancesReceived;
        protected virtual void OnSSOTMECoordinatorGetInstancesReceived(SSOTMEPayload payload)
        {
            if (IsDebugMode) 
            {
                System.Console.WriteLine("Get Instances - ");
                System.Console.WriteLine("payload: " + payload.SafeToString());
            }

            var plea = new PayloadEventArgs<SSOTMEPayload>(payload);
            this.Invoke(this.SSOTMECoordinatorGetInstancesReceived, plea);
        }
        
        public event System.EventHandler<PayloadEventArgs<SSOTMEPayload>> SSOTMECoordinatorTranspileRequestedReceived;
        protected virtual void OnSSOTMECoordinatorTranspileRequestedReceived(SSOTMEPayload payload)
        {
            if (IsDebugMode) 
            {
                System.Console.WriteLine("Transpile Requested - ");
                System.Console.WriteLine("payload: " + payload.SafeToString());
            }

            var plea = new PayloadEventArgs<SSOTMEPayload>(payload);
            this.Invoke(this.SSOTMECoordinatorTranspileRequestedReceived, plea);
        }
        
        public event System.EventHandler<PayloadEventArgs<SSOTMEPayload>> SSOTMECoordinatorStopInstanceReceived;
        protected virtual void OnSSOTMECoordinatorStopInstanceReceived(SSOTMEPayload payload)
        {
            if (IsDebugMode) 
            {
                System.Console.WriteLine("Stop Instance - ");
                System.Console.WriteLine("payload: " + payload.SafeToString());
            }

            var plea = new PayloadEventArgs<SSOTMEPayload>(payload);
            this.Invoke(this.SSOTMECoordinatorStopInstanceReceived, plea);
        }
        
        public event System.EventHandler<PayloadEventArgs<SSOTMEPayload>> SSOTMECoordinatorStopHostReceived;
        protected virtual void OnSSOTMECoordinatorStopHostReceived(SSOTMEPayload payload)
        {
            if (IsDebugMode) 
            {
                System.Console.WriteLine("Stop Host - ");
                System.Console.WriteLine("payload: " + payload.SafeToString());
            }

            var plea = new PayloadEventArgs<SSOTMEPayload>(payload);
            this.Invoke(this.SSOTMECoordinatorStopHostReceived, plea);
        }
        
            public void TranspilerHostPing() {
                this.TranspilerHostPing(this.CreatePayload());
            }

            public void TranspilerHostPing(System.String content) {
                var payload = this.CreatePayload();
                payload.Content = content;
                this.TranspilerHostPing(payload);
            }

            public void TranspilerHostPing(SSOTMEPayload payload)
            {
                
                this.SendMessage(payload, "Ping - ",
                        "transpilerhostmic", "ssotmecoordinator.general.transpilerhost.ping");
             }

 
        
            public void TranspilerHostOffline(DMProxy proxy) {
                this.TranspilerHostOffline(this.CreatePayload(), proxy);
            }

            public void TranspilerHostOffline(System.String content, DMProxy proxy) {
                var payload = this.CreatePayload();
                payload.Content = content;
                this.TranspilerHostOffline(payload, proxy);
            }

            public void TranspilerHostOffline(SSOTMEPayload payload, DMProxy proxy)
            {
                payload.IsDirectMessage = true;
                this.SendMessage(payload, "Offline - ",
                        "transpilerhostmic", "ssotmecoordinator.general.transpilerhost.offline", proxy.RoutingKey);
             }

 
        
            public void TranspilerHostInstanceStarted(DMProxy proxy) {
                this.TranspilerHostInstanceStarted(this.CreatePayload(), proxy);
            }

            public void TranspilerHostInstanceStarted(System.String content, DMProxy proxy) {
                var payload = this.CreatePayload();
                payload.Content = content;
                this.TranspilerHostInstanceStarted(payload, proxy);
            }

            public void TranspilerHostInstanceStarted(SSOTMEPayload payload, DMProxy proxy)
            {
                payload.IsDirectMessage = true;
                this.SendMessage(payload, "Instance Started - ",
                        "transpilerhostmic", "ssotmecoordinator.general.transpilerhost.instancestarted", proxy.RoutingKey);
             }

 
        
            public void TranspilerHostInstanceStopped(DMProxy proxy) {
                this.TranspilerHostInstanceStopped(this.CreatePayload(), proxy);
            }

            public void TranspilerHostInstanceStopped(System.String content, DMProxy proxy) {
                var payload = this.CreatePayload();
                payload.Content = content;
                this.TranspilerHostInstanceStopped(payload, proxy);
            }

            public void TranspilerHostInstanceStopped(SSOTMEPayload payload, DMProxy proxy)
            {
                payload.IsDirectMessage = true;
                this.SendMessage(payload, "Instance Stopped - ",
                        "transpilerhostmic", "ssotmecoordinator.general.transpilerhost.instancestopped", proxy.RoutingKey);
             }

 
        
            // And can also say/hear everything which : AccountHolder hears.
            
            public void AccountHolderPing() {
                this.AccountHolderPing(this.CreatePayload());
            }

            public void AccountHolderPing(System.String content) {
                var payload = this.CreatePayload();
                payload.Content = content;
                this.AccountHolderPing(payload);
            }

            public void AccountHolderPing(SSOTMEPayload payload)
            {
                
                this.SendMessage(payload, "Ping - ",
                        "accountholdermic", "ssotmecoordinator.general.accountholder.ping");
             }

 
        
            public void AccountHolderLogin(DMProxy proxy) {
                this.AccountHolderLogin(this.CreatePayload(), proxy);
            }

            public void AccountHolderLogin(System.String content, DMProxy proxy) {
                var payload = this.CreatePayload();
                payload.Content = content;
                this.AccountHolderLogin(payload, proxy);
            }

            public void AccountHolderLogin(SSOTMEPayload payload, DMProxy proxy)
            {
                payload.IsDirectMessage = true;
                this.SendMessage(payload, "Login - ",
                        "accountholdermic", "ssotmecoordinator.general.accountholder.login", proxy.RoutingKey);
             }

 
        
            public void AccountHolderLogout(DMProxy proxy) {
                this.AccountHolderLogout(this.CreatePayload(), proxy);
            }

            public void AccountHolderLogout(System.String content, DMProxy proxy) {
                var payload = this.CreatePayload();
                payload.Content = content;
                this.AccountHolderLogout(payload, proxy);
            }

            public void AccountHolderLogout(SSOTMEPayload payload, DMProxy proxy)
            {
                payload.IsDirectMessage = true;
                this.SendMessage(payload, "Logout - ",
                        "accountholdermic", "ssotmecoordinator.general.accountholder.logout", proxy.RoutingKey);
             }

 
        
            public void AccountHolderAddTranspiler(DMProxy proxy) {
                this.AccountHolderAddTranspiler(this.CreatePayload(), proxy);
            }

            public void AccountHolderAddTranspiler(System.String content, DMProxy proxy) {
                var payload = this.CreatePayload();
                payload.Content = content;
                this.AccountHolderAddTranspiler(payload, proxy);
            }

            public void AccountHolderAddTranspiler(SSOTMEPayload payload, DMProxy proxy)
            {
                payload.IsDirectMessage = true;
                this.SendMessage(payload, "Add Transpiler - ",
                        "accountholdermic", "ssotmecoordinator.general.accountholder.addtranspiler", proxy.RoutingKey);
             }

 
        
            public void AccountHolderDeleteTranspiler(DMProxy proxy) {
                this.AccountHolderDeleteTranspiler(this.CreatePayload(), proxy);
            }

            public void AccountHolderDeleteTranspiler(System.String content, DMProxy proxy) {
                var payload = this.CreatePayload();
                payload.Content = content;
                this.AccountHolderDeleteTranspiler(payload, proxy);
            }

            public void AccountHolderDeleteTranspiler(SSOTMEPayload payload, DMProxy proxy)
            {
                payload.IsDirectMessage = true;
                this.SendMessage(payload, "Delete Transpiler - ",
                        "accountholdermic", "ssotmecoordinator.general.accountholder.deletetranspiler", proxy.RoutingKey);
             }

 
        
            public void AccountHolderUpdateTranspiler(DMProxy proxy) {
                this.AccountHolderUpdateTranspiler(this.CreatePayload(), proxy);
            }

            public void AccountHolderUpdateTranspiler(System.String content, DMProxy proxy) {
                var payload = this.CreatePayload();
                payload.Content = content;
                this.AccountHolderUpdateTranspiler(payload, proxy);
            }

            public void AccountHolderUpdateTranspiler(SSOTMEPayload payload, DMProxy proxy)
            {
                payload.IsDirectMessage = true;
                this.SendMessage(payload, "Update Transpiler - ",
                        "accountholdermic", "ssotmecoordinator.general.accountholder.updatetranspiler", proxy.RoutingKey);
             }

 
        
            public void AccountHolderGetTranspiler(DMProxy proxy) {
                this.AccountHolderGetTranspiler(this.CreatePayload(), proxy);
            }

            public void AccountHolderGetTranspiler(System.String content, DMProxy proxy) {
                var payload = this.CreatePayload();
                payload.Content = content;
                this.AccountHolderGetTranspiler(payload, proxy);
            }

            public void AccountHolderGetTranspiler(SSOTMEPayload payload, DMProxy proxy)
            {
                payload.IsDirectMessage = true;
                this.SendMessage(payload, "Get Transpiler - ",
                        "accountholdermic", "ssotmecoordinator.general.accountholder.gettranspiler", proxy.RoutingKey);
             }

 
        
            public void AccountHolderAddTranspilerVersion(DMProxy proxy) {
                this.AccountHolderAddTranspilerVersion(this.CreatePayload(), proxy);
            }

            public void AccountHolderAddTranspilerVersion(System.String content, DMProxy proxy) {
                var payload = this.CreatePayload();
                payload.Content = content;
                this.AccountHolderAddTranspilerVersion(payload, proxy);
            }

            public void AccountHolderAddTranspilerVersion(SSOTMEPayload payload, DMProxy proxy)
            {
                payload.IsDirectMessage = true;
                this.SendMessage(payload, "Add Transpiler Version - ",
                        "accountholdermic", "ssotmecoordinator.general.accountholder.addtranspilerversion", proxy.RoutingKey);
             }

 
        
            public void AccountHolderDeleteTranspilerVersion(DMProxy proxy) {
                this.AccountHolderDeleteTranspilerVersion(this.CreatePayload(), proxy);
            }

            public void AccountHolderDeleteTranspilerVersion(System.String content, DMProxy proxy) {
                var payload = this.CreatePayload();
                payload.Content = content;
                this.AccountHolderDeleteTranspilerVersion(payload, proxy);
            }

            public void AccountHolderDeleteTranspilerVersion(SSOTMEPayload payload, DMProxy proxy)
            {
                payload.IsDirectMessage = true;
                this.SendMessage(payload, "Delete Transpiler Version - ",
                        "accountholdermic", "ssotmecoordinator.general.accountholder.deletetranspilerversion", proxy.RoutingKey);
             }

 
        
            public void AccountHolderUpdateTranspilerVersion(DMProxy proxy) {
                this.AccountHolderUpdateTranspilerVersion(this.CreatePayload(), proxy);
            }

            public void AccountHolderUpdateTranspilerVersion(System.String content, DMProxy proxy) {
                var payload = this.CreatePayload();
                payload.Content = content;
                this.AccountHolderUpdateTranspilerVersion(payload, proxy);
            }

            public void AccountHolderUpdateTranspilerVersion(SSOTMEPayload payload, DMProxy proxy)
            {
                payload.IsDirectMessage = true;
                this.SendMessage(payload, "Update Transpiler Version - ",
                        "accountholdermic", "ssotmecoordinator.general.accountholder.updatetranspilerversion", proxy.RoutingKey);
             }

 
        
            public void AccountHolderGetTranspilerList(DMProxy proxy) {
                this.AccountHolderGetTranspilerList(this.CreatePayload(), proxy);
            }

            public void AccountHolderGetTranspilerList(System.String content, DMProxy proxy) {
                var payload = this.CreatePayload();
                payload.Content = content;
                this.AccountHolderGetTranspilerList(payload, proxy);
            }

            public void AccountHolderGetTranspilerList(SSOTMEPayload payload, DMProxy proxy)
            {
                payload.IsDirectMessage = true;
                this.SendMessage(payload, "Get Transpiler List - ",
                        "accountholdermic", "ssotmecoordinator.general.accountholder.gettranspilerlist", proxy.RoutingKey);
             }

 
        
            public void AccountHolderCreateProject(DMProxy proxy) {
                this.AccountHolderCreateProject(this.CreatePayload(), proxy);
            }

            public void AccountHolderCreateProject(System.String content, DMProxy proxy) {
                var payload = this.CreatePayload();
                payload.Content = content;
                this.AccountHolderCreateProject(payload, proxy);
            }

            public void AccountHolderCreateProject(SSOTMEPayload payload, DMProxy proxy)
            {
                payload.IsDirectMessage = true;
                this.SendMessage(payload, "Create Project - ",
                        "accountholdermic", "ssotmecoordinator.general.accountholder.createproject", proxy.RoutingKey);
             }

 
        
            public void AccountHolderRequestTranspile(DMProxy proxy) {
                this.AccountHolderRequestTranspile(this.CreatePayload(), proxy);
            }

            public void AccountHolderRequestTranspile(System.String content, DMProxy proxy) {
                var payload = this.CreatePayload();
                payload.Content = content;
                this.AccountHolderRequestTranspile(payload, proxy);
            }

            public void AccountHolderRequestTranspile(SSOTMEPayload payload, DMProxy proxy)
            {
                payload.IsDirectMessage = true;
                this.SendMessage(payload, "Request Transpile - ",
                        "accountholdermic", "ssotmecoordinator.general.accountholder.requesttranspile", proxy.RoutingKey);
             }

 
        
            public void AccountHolderRequestTranspilerHost(DMProxy proxy) {
                this.AccountHolderRequestTranspilerHost(this.CreatePayload(), proxy);
            }

            public void AccountHolderRequestTranspilerHost(System.String content, DMProxy proxy) {
                var payload = this.CreatePayload();
                payload.Content = content;
                this.AccountHolderRequestTranspilerHost(payload, proxy);
            }

            public void AccountHolderRequestTranspilerHost(SSOTMEPayload payload, DMProxy proxy)
            {
                payload.IsDirectMessage = true;
                this.SendMessage(payload, "Request Transpiler Host - ",
                        "accountholdermic", "ssotmecoordinator.general.accountholder.requesttranspilerhost", proxy.RoutingKey);
             }

 
        
            public void AccountHolderRequestStopInstance(DMProxy proxy) {
                this.AccountHolderRequestStopInstance(this.CreatePayload(), proxy);
            }

            public void AccountHolderRequestStopInstance(System.String content, DMProxy proxy) {
                var payload = this.CreatePayload();
                payload.Content = content;
                this.AccountHolderRequestStopInstance(payload, proxy);
            }

            public void AccountHolderRequestStopInstance(SSOTMEPayload payload, DMProxy proxy)
            {
                payload.IsDirectMessage = true;
                this.SendMessage(payload, "Request Stop Instance - ",
                        "accountholdermic", "ssotmecoordinator.general.accountholder.requeststopinstance", proxy.RoutingKey);
             }

 
        
            public void AccountHolderRequestStopHost(DMProxy proxy) {
                this.AccountHolderRequestStopHost(this.CreatePayload(), proxy);
            }

            public void AccountHolderRequestStopHost(System.String content, DMProxy proxy) {
                var payload = this.CreatePayload();
                payload.Content = content;
                this.AccountHolderRequestStopHost(payload, proxy);
            }

            public void AccountHolderRequestStopHost(SSOTMEPayload payload, DMProxy proxy)
            {
                payload.IsDirectMessage = true;
                this.SendMessage(payload, "Request Stop Host - ",
                        "accountholdermic", "ssotmecoordinator.general.accountholder.requeststophost", proxy.RoutingKey);
             }

 
        
            public void AccountHolderCommandLineTranspile(DMProxy proxy) {
                this.AccountHolderCommandLineTranspile(this.CreatePayload(), proxy);
            }

            public void AccountHolderCommandLineTranspile(System.String content, DMProxy proxy) {
                var payload = this.CreatePayload();
                payload.Content = content;
                this.AccountHolderCommandLineTranspile(payload, proxy);
            }

            public void AccountHolderCommandLineTranspile(SSOTMEPayload payload, DMProxy proxy)
            {
                payload.IsDirectMessage = true;
                this.SendMessage(payload, "Command Line Transpile - ",
                        "accountholdermic", "ssotmecoordinator.general.accountholder.commandlinetranspile", proxy.RoutingKey);
             }

 
        
            // And can also say/hear everything which : PublicUser hears.
            
        public event System.EventHandler<PayloadEventArgs<SSOTMEPayload>> SSOTMECoordinatorTranspilerOnlineReceived;
        protected virtual void OnSSOTMECoordinatorTranspilerOnlineReceived(SSOTMEPayload payload)
        {
            if (IsDebugMode) 
            {
                System.Console.WriteLine("Transpiler Online - ");
                System.Console.WriteLine("payload: " + payload.SafeToString());
            }

            var plea = new PayloadEventArgs<SSOTMEPayload>(payload);
            this.Invoke(this.SSOTMECoordinatorTranspilerOnlineReceived, plea);
        }
        
        public event System.EventHandler<PayloadEventArgs<SSOTMEPayload>> SSOTMECoordinatorTranspilerOfflineReceived;
        protected virtual void OnSSOTMECoordinatorTranspilerOfflineReceived(SSOTMEPayload payload)
        {
            if (IsDebugMode) 
            {
                System.Console.WriteLine("Transpiler Offline - ");
                System.Console.WriteLine("payload: " + payload.SafeToString());
            }

            var plea = new PayloadEventArgs<SSOTMEPayload>(payload);
            this.Invoke(this.SSOTMECoordinatorTranspilerOfflineReceived, plea);
        }
        
            public void PublicUserPing() {
                this.PublicUserPing(this.CreatePayload());
            }

            public void PublicUserPing(System.String content) {
                var payload = this.CreatePayload();
                payload.Content = content;
                this.PublicUserPing(payload);
            }

            public void PublicUserPing(SSOTMEPayload payload)
            {
                
                this.SendMessage(payload, "Ping - ",
                        "publicusermic", "ssotmecoordinator.general.publicuser.ping");
             }

 
        
            public void PublicUserRegister(DMProxy proxy) {
                this.PublicUserRegister(this.CreatePayload(), proxy);
            }

            public void PublicUserRegister(System.String content, DMProxy proxy) {
                var payload = this.CreatePayload();
                payload.Content = content;
                this.PublicUserRegister(payload, proxy);
            }

            public void PublicUserRegister(SSOTMEPayload payload, DMProxy proxy)
            {
                payload.IsDirectMessage = true;
                this.SendMessage(payload, "Register - ",
                        "publicusermic", "ssotmecoordinator.general.publicuser.register", proxy.RoutingKey);
             }

 
        
            public void PublicUserRecover(DMProxy proxy) {
                this.PublicUserRecover(this.CreatePayload(), proxy);
            }

            public void PublicUserRecover(System.String content, DMProxy proxy) {
                var payload = this.CreatePayload();
                payload.Content = content;
                this.PublicUserRecover(payload, proxy);
            }

            public void PublicUserRecover(SSOTMEPayload payload, DMProxy proxy)
            {
                payload.IsDirectMessage = true;
                this.SendMessage(payload, "Recover - ",
                        "publicusermic", "ssotmecoordinator.general.publicuser.recover", proxy.RoutingKey);
             }

 
        
            public void PublicUserGetAllTranspilers(DMProxy proxy) {
                this.PublicUserGetAllTranspilers(this.CreatePayload(), proxy);
            }

            public void PublicUserGetAllTranspilers(System.String content, DMProxy proxy) {
                var payload = this.CreatePayload();
                payload.Content = content;
                this.PublicUserGetAllTranspilers(payload, proxy);
            }

            public void PublicUserGetAllTranspilers(SSOTMEPayload payload, DMProxy proxy)
            {
                payload.IsDirectMessage = true;
                this.SendMessage(payload, "Get All Transpilers - ",
                        "publicusermic", "ssotmecoordinator.general.publicuser.getalltranspilers", proxy.RoutingKey);
             }

 
        
            public void PublicUserGetAllPlatformData(DMProxy proxy) {
                this.PublicUserGetAllPlatformData(this.CreatePayload(), proxy);
            }

            public void PublicUserGetAllPlatformData(System.String content, DMProxy proxy) {
                var payload = this.CreatePayload();
                payload.Content = content;
                this.PublicUserGetAllPlatformData(payload, proxy);
            }

            public void PublicUserGetAllPlatformData(SSOTMEPayload payload, DMProxy proxy)
            {
                payload.IsDirectMessage = true;
                this.SendMessage(payload, "Get All Platform Data - ",
                        "publicusermic", "ssotmecoordinator.general.publicuser.getallplatformdata", proxy.RoutingKey);
             }

 
        
            public void PublicUserGetAllFileTypes(DMProxy proxy) {
                this.PublicUserGetAllFileTypes(this.CreatePayload(), proxy);
            }

            public void PublicUserGetAllFileTypes(System.String content, DMProxy proxy) {
                var payload = this.CreatePayload();
                payload.Content = content;
                this.PublicUserGetAllFileTypes(payload, proxy);
            }

            public void PublicUserGetAllFileTypes(SSOTMEPayload payload, DMProxy proxy)
            {
                payload.IsDirectMessage = true;
                this.SendMessage(payload, "Get All File Types - ",
                        "publicusermic", "ssotmecoordinator.general.publicuser.getallfiletypes", proxy.RoutingKey);
             }

 
        
    }
}

                    