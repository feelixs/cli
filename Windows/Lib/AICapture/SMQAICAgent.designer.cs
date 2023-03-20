using System;
using System.Linq;
using RabbitMQ.Client;
using System.Text;
using Newtonsoft.Json;
using RabbitMQ.Client.Events;
using System.Threading.Tasks;

namespace AIC.SassyMQ.Lib
{
    public partial class SMQAICAgent : SMQActorBase
    {

        public SMQAICAgent(String amqpConnectionString)
            : base(amqpConnectionString, "aicagent")
        {
        }

        protected override void CheckRouting(StandardPayload payload, BasicDeliverEventArgs  bdea)
        {
            var originalAccessToken = payload.AccessToken;
            try
            {
                switch (payload.RoutingKey)
                {
                    
                    case "aicagent.custom.user.aicinstall":
                        this.OnUserAICInstallReceived(payload, bdea);
                        break;
                    
                    case "aicagent.custom.user.setdata":
                        this.OnUserSetDataReceived(payload, bdea);
                        break;
                    
                    case "aicagent.custom.user.aicreplay":
                        this.OnUserAICReplayReceived(payload, bdea);
                        break;
                    
                    case "aicagent.custom.user.getdata":
                        this.OnUserGetDataReceived(payload, bdea);
                        break;
                    
                }

            }
            catch (Exception ex)
            {
                payload.ErrorMessage = ex.Message;
            }
            var reply = payload.ReplyPayload is null ? payload  : payload.ReplyPayload;
            reply.IsHandled = payload.IsHandled;
            if (reply.AccessToken == originalAccessToken) reply.AccessToken = null;            
            this.Reply(reply, bdea.BasicProperties);
        }

        
        /// <summary>
        /// Responds to: AICInstall from User
        /// </summary>
        public event EventHandler<PayloadEventArgs> UserAICInstallReceived;
        protected virtual void OnUserAICInstallReceived(StandardPayload payload, BasicDeliverEventArgs bdea)
        {
            var plea = new PayloadEventArgs(payload, bdea);
            if (!ReferenceEquals(this.UserAICInstallReceived, null))
            {
                plea.Payload.IsHandled = true;
                this.UserAICInstallReceived(this, plea);
            }
        }

        /// <summary>
        /// Responds to: SetData from User
        /// </summary>
        public event EventHandler<PayloadEventArgs> UserSetDataReceived;
        protected virtual void OnUserSetDataReceived(StandardPayload payload, BasicDeliverEventArgs bdea)
        {
            var plea = new PayloadEventArgs(payload, bdea);
            if (!ReferenceEquals(this.UserSetDataReceived, null))
            {
                plea.Payload.IsHandled = true;
                this.UserSetDataReceived(this, plea);
            }
        }

        /// <summary>
        /// Responds to: AICReplay from User
        /// </summary>
        public event EventHandler<PayloadEventArgs> UserAICReplayReceived;
        protected virtual void OnUserAICReplayReceived(StandardPayload payload, BasicDeliverEventArgs bdea)
        {
            var plea = new PayloadEventArgs(payload, bdea);
            if (!ReferenceEquals(this.UserAICReplayReceived, null))
            {
                plea.Payload.IsHandled = true;
                this.UserAICReplayReceived(this, plea);
            }
        }

        /// <summary>
        /// Responds to: GetData from User
        /// </summary>
        public event EventHandler<PayloadEventArgs> UserGetDataReceived;
        protected virtual void OnUserGetDataReceived(StandardPayload payload, BasicDeliverEventArgs bdea)
        {
            var plea = new PayloadEventArgs(payload, bdea);
            if (!ReferenceEquals(this.UserGetDataReceived, null))
            {
                plea.Payload.IsHandled = true;
                this.UserGetDataReceived(this, plea);
            }
        }

        /// <summary>
        /// MonitoringFor - 
        /// </summary>
        public Task MonitoringFor(PayloadHandler replyHandler = null, PayloadHandler timeoutHandler = null, int waitTimeout = StandardPayload.DEFAULT_TIMEOUT)
        {
            return this.MonitoringFor(this.CreatePayload(), replyHandler, timeoutHandler, waitTimeout);
        }

        /// <summary>
        /// MonitoringFor - 
        /// </summary>
        public Task MonitoringFor(String content, PayloadHandler replyHandler = null, PayloadHandler timeoutHandler = null, int waitTimeout = StandardPayload.DEFAULT_TIMEOUT)
        {
            var payload = this.CreatePayload(content);
            return this.MonitoringFor(payload, replyHandler, timeoutHandler, waitTimeout);
        }
    
        
        /// <summary>
        /// MonitoringFor - 
        /// </summary>
        public Task MonitoringFor(StandardPayload payload, PayloadHandler replyHandler = null, PayloadHandler timeoutHandler = null, int waitTimeout = StandardPayload.DEFAULT_TIMEOUT)
        {
            return this.SendMessage("crudcoordinator.custom.aicagent.monitoringfor", payload, replyHandler, timeoutHandler, waitTimeout);
        }
        
        
        /// <summary>
        /// ProjectChanged - 
        /// </summary>
        public Task ProjectChanged(DMProxy dmp, PayloadHandler replyHandler = null, PayloadHandler timeoutHandler = null, int waitTimeout = StandardPayload.DEFAULT_TIMEOUT)
        {
            return this.ProjectChanged(dmp, this.CreatePayload(), replyHandler, timeoutHandler, waitTimeout);
        }

        /// <summary>
        /// ProjectChanged - 
        /// </summary>
        public Task ProjectChanged(DMProxy dmp, String content, PayloadHandler replyHandler = null, PayloadHandler timeoutHandler = null, int waitTimeout = StandardPayload.DEFAULT_TIMEOUT)
        {
            var payload = this.CreatePayload(content);
            return this.ProjectChanged(dmp, payload, replyHandler, timeoutHandler, waitTimeout);
        }
        
        /// <summary>
        /// ProjectChanged - 
        /// </summary>
        public Task ProjectChanged(DMProxy dmp, StandardPayload payload, PayloadHandler replyHandler = null, PayloadHandler timeoutHandler = null, int waitTimeout = StandardPayload.DEFAULT_TIMEOUT)
        {
            return this.SendMessage("user.custom.aicagent.projectchanged", payload, replyHandler, timeoutHandler, waitTimeout, dmp.RoutingKey);
        }
        
        
    }
}

                    
