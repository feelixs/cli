
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.MessagePatterns;
using SassyMQ.Lib.RabbitMQ;
using SassyMQ.SSOTME.Lib.RabbitMQ;
using SassyMQ.Lib.RabbitMQ.Payload;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SSoTme.OST.Lib.SassySDK;

namespace SassyMQ.SSOTME.Lib.RMQActors
{
    public partial class SMQPublicUser : SSOTMEActorBase
    {
     
        public SMQPublicUser(bool isAutoConnect = true)
            : base("publicuser.all", isAutoConnect)
        {
        }
        // SSOTME - SSOTME
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

 
        
    }
}

                    