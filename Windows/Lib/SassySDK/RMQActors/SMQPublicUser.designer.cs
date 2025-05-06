using System;
using System.Linq;
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
using CoreLibrary.SassyMQ;

namespace SassyMQ.SSOTME.Lib.RMQActors
{
    public partial class SMQPublicUser : SSOTMEActorBase
    {
     
        public SMQPublicUser(bool isAutoConnect = true)
            : base("publicuser.all", isAutoConnect)
        {
        }
        // SSoTmeOST - SSOTME
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
                IBasicProperties props = this.RMQChannel.CreateBasicProperties();
                props.CorrelationId = payload.CorrelationId;
                this.RMQChannel.BasicPublish("", payload.ReplyTo, props, Encoding.UTF8.GetBytes(payload.ToJSonString()));
            }
        }

        protected override void CheckRouting(SSOTMEPayload payload, bool isDirectMessage) 
        {
            // if (payload.IsDirectMessage && !isDirectMessage) return;

            try {
                
             if (payload.IsLexiconTerm(LexiconTermEnum.ssotmecoordinator_transpileronline_publicuser)) 
            {
                this.OnSSOTMECoordinatorTranspilerOnlineReceived(payload);
            }
        
            else  if (payload.IsLexiconTerm(LexiconTermEnum.ssotmecoordinator_transpileroffline_publicuser)) 
            {
                this.OnSSOTMECoordinatorTranspilerOfflineReceived(payload);
            }
        
            } catch (Exception ex) {
                payload.Exception = ex;
            }
            this.Reply(payload);
        }

        
        /// <summary>
        /// Responds to: Transpiler Online - 
        /// </summary>
        public event System.EventHandler<PayloadEventArgs<SSOTMEPayload>> SSOTMECoordinatorTranspilerOnlineReceived;
        protected virtual void OnSSOTMECoordinatorTranspilerOnlineReceived(SSOTMEPayload payload)
        {
            this.LogMessage(payload, "Transpiler Online - ");
            var plea = new PayloadEventArgs<SSOTMEPayload>(payload);
            this.Invoke(this.SSOTMECoordinatorTranspilerOnlineReceived, plea);
        }
        
        /// <summary>
        /// Responds to: Transpiler Offline - 
        /// </summary>
        public event System.EventHandler<PayloadEventArgs<SSOTMEPayload>> SSOTMECoordinatorTranspilerOfflineReceived;
        protected virtual void OnSSOTMECoordinatorTranspilerOfflineReceived(SSOTMEPayload payload)
        {
            this.LogMessage(payload, "Transpiler Offline - ");
            var plea = new PayloadEventArgs<SSOTMEPayload>(payload);
            this.Invoke(this.SSOTMECoordinatorTranspilerOfflineReceived, plea);
        }
        
        /// <summary>
        /// Ping - 
        /// </summary>
        public void PublicUserPing() 
        {
            this.PublicUserPing(this.CreatePayload());
        }

        /// <summary>
        /// Ping - 
        /// </summary>
        public void PublicUserPing(System.String content) 
        {
            var payload = this.CreatePayload();
            payload.Content = content;
            this.PublicUserPing(payload);
        }

        /// <summary>
        /// Ping - 
        /// </summary>
        public void PublicUserPing(SSOTMEPayload payload)
        {
            
            this.SendMessage(payload, "Ping - ",
            "publicusermic", "ssotmecoordinator.general.publicuser.ping");
        }


        
        /// <summary>
        /// Register - 
        /// </summary>
        public void PublicUserRegister(DMProxy proxy) 
        {
            this.PublicUserRegister(this.CreatePayload(), proxy);
        }

        /// <summary>
        /// Register - 
        /// </summary>
        public void PublicUserRegister(System.String content, DMProxy proxy) 
        {
            var payload = this.CreatePayload();
            payload.Content = content;
            this.PublicUserRegister(payload, proxy);
        }

        /// <summary>
        /// Register - 
        /// </summary>
        public void PublicUserRegister(SSOTMEPayload payload, DMProxy proxy)
        {
            payload.IsDirectMessage = true;
            this.SendMessage(payload, "Register - ",
            "publicusermic", "ssotmecoordinator.general.publicuser.register", proxy.RoutingKey);
        }


        
        /// <summary>
        /// Authenticate - 
        /// </summary>
        public void PublicUserAuthenticate(DMProxy proxy) 
        {
            this.PublicUserAuthenticate(this.CreatePayload(), proxy);
        }

        /// <summary>
        /// Authenticate - 
        /// </summary>
        public void PublicUserAuthenticate(System.String content, DMProxy proxy) 
        {
            var payload = this.CreatePayload();
            payload.Content = content;
            this.PublicUserAuthenticate(payload, proxy);
        }

        /// <summary>
        /// Authenticate - 
        /// </summary>
        public void PublicUserAuthenticate(SSOTMEPayload payload, DMProxy proxy)
        {
            payload.IsDirectMessage = true;
            this.SendMessage(payload, "Authenticate - ",
            "publicusermic", "ssotmecoordinator.general.publicuser.authenticate", proxy.RoutingKey);
        }


        
        /// <summary>
        /// Validate Auth Token - 
        /// </summary>
        public void PublicUserValidateAuthToken(DMProxy proxy) 
        {
            this.PublicUserValidateAuthToken(this.CreatePayload(), proxy);
        }

        /// <summary>
        /// Validate Auth Token - 
        /// </summary>
        public void PublicUserValidateAuthToken(System.String content, DMProxy proxy) 
        {
            var payload = this.CreatePayload();
            payload.Content = content;
            this.PublicUserValidateAuthToken(payload, proxy);
        }

        /// <summary>
        /// Validate Auth Token - 
        /// </summary>
        public void PublicUserValidateAuthToken(SSOTMEPayload payload, DMProxy proxy)
        {
            payload.IsDirectMessage = true;
            this.SendMessage(payload, "Validate Auth Token - ",
            "publicusermic", "ssotmecoordinator.general.publicuser.validateauthtoken", proxy.RoutingKey);
        }


        
        /// <summary>
        /// Recover - 
        /// </summary>
        public void PublicUserRecover(DMProxy proxy) 
        {
            this.PublicUserRecover(this.CreatePayload(), proxy);
        }

        /// <summary>
        /// Recover - 
        /// </summary>
        public void PublicUserRecover(System.String content, DMProxy proxy) 
        {
            var payload = this.CreatePayload();
            payload.Content = content;
            this.PublicUserRecover(payload, proxy);
        }

        /// <summary>
        /// Recover - 
        /// </summary>
        public void PublicUserRecover(SSOTMEPayload payload, DMProxy proxy)
        {
            payload.IsDirectMessage = true;
            this.SendMessage(payload, "Recover - ",
            "publicusermic", "ssotmecoordinator.general.publicuser.recover", proxy.RoutingKey);
        }


        
        /// <summary>
        /// Get All Transpilers - 
        /// </summary>
        public void PublicUserGetAllTranspilers(DMProxy proxy) 
        {
            this.PublicUserGetAllTranspilers(this.CreatePayload(), proxy);
        }

        /// <summary>
        /// Get All Transpilers - 
        /// </summary>
        public void PublicUserGetAllTranspilers(System.String content, DMProxy proxy) 
        {
            var payload = this.CreatePayload();
            payload.Content = content;
            this.PublicUserGetAllTranspilers(payload, proxy);
        }

        /// <summary>
        /// Get All Transpilers - 
        /// </summary>
        public void PublicUserGetAllTranspilers(SSOTMEPayload payload, DMProxy proxy)
        {
            payload.IsDirectMessage = true;
            this.SendMessage(payload, "Get All Transpilers - ",
            "publicusermic", "ssotmecoordinator.general.publicuser.getalltranspilers", proxy.RoutingKey);
        }


        
        /// <summary>
        /// Get All Platform Data - 
        /// </summary>
        public void PublicUserGetAllPlatformData(DMProxy proxy) 
        {
            this.PublicUserGetAllPlatformData(this.CreatePayload(), proxy);
        }

        /// <summary>
        /// Get All Platform Data - 
        /// </summary>
        public void PublicUserGetAllPlatformData(System.String content, DMProxy proxy) 
        {
            var payload = this.CreatePayload();
            payload.Content = content;
            this.PublicUserGetAllPlatformData(payload, proxy);
        }

        /// <summary>
        /// Get All Platform Data - 
        /// </summary>
        public void PublicUserGetAllPlatformData(SSOTMEPayload payload, DMProxy proxy)
        {
            payload.IsDirectMessage = true;
            this.SendMessage(payload, "Get All Platform Data - ",
            "publicusermic", "ssotmecoordinator.general.publicuser.getallplatformdata", proxy.RoutingKey);
        }


        
        /// <summary>
        /// Get All File Types - 
        /// </summary>
        public void PublicUserGetAllFileTypes(DMProxy proxy) 
        {
            this.PublicUserGetAllFileTypes(this.CreatePayload(), proxy);
        }

        /// <summary>
        /// Get All File Types - 
        /// </summary>
        public void PublicUserGetAllFileTypes(System.String content, DMProxy proxy) 
        {
            var payload = this.CreatePayload();
            payload.Content = content;
            this.PublicUserGetAllFileTypes(payload, proxy);
        }

        /// <summary>
        /// Get All File Types - 
        /// </summary>
        public void PublicUserGetAllFileTypes(SSOTMEPayload payload, DMProxy proxy)
        {
            payload.IsDirectMessage = true;
            this.SendMessage(payload, "Get All File Types - ",
            "publicusermic", "ssotmecoordinator.general.publicuser.getallfiletypes", proxy.RoutingKey);
        }


        

        
        public void LogMessage(SSOTMEPayload payload, System.String msg)
        {
            if (IsDebugMode)
            {
                System.Diagnostics.Debug.WriteLine(msg);
                System.Diagnostics.Debug.WriteLine("payload: " + payload.SafeToString());
            }
        }
        
    }
}
namespace CoreLibrary.SassyMQ {}
                    