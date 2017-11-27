
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
    public partial class SMQAccountHolder : SSOTMEActorBase
    {
     
        public SMQAccountHolder(bool isAutoConnect = true)
            : base("accountholder.all", isAutoConnect)
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

                    