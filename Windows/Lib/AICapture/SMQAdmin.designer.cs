using System;
using System.Linq;
using RabbitMQ.Client;
using System.Text;
using Newtonsoft.Json;
using RabbitMQ.Client.Events;
using System.Threading.Tasks;

namespace AIC.SassyMQ.Lib
{
    public partial class SMQAdmin : SMQActorBase
    {

        public SMQAdmin(String amqpConnectionString)
            : base(amqpConnectionString, "admin")
        {
        }

        protected override void CheckRouting(StandardPayload payload, BasicDeliverEventArgs  bdea)
        {
            var originalAccessToken = payload.AccessToken;
            try
            {
                switch (bdea.RoutingKey)
                {
                    
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
        /// AddAppUser - 
        /// </summary>
        public Task AddAppUser(PayloadHandler replyHandler = null, PayloadHandler timeoutHandler = null, int waitTimeout = StandardPayload.DEFAULT_TIMEOUT)
        {
            return this.AddAppUser(this.CreatePayload(), replyHandler, timeoutHandler, waitTimeout);
        }

        /// <summary>
        /// AddAppUser - 
        /// </summary>
        public Task AddAppUser(String content, PayloadHandler replyHandler = null, PayloadHandler timeoutHandler = null, int waitTimeout = StandardPayload.DEFAULT_TIMEOUT)
        {
            var payload = this.CreatePayload(content);
            return this.AddAppUser(payload, replyHandler, timeoutHandler, waitTimeout);
        }
    
        
        /// <summary>
        /// AddAppUser - 
        /// </summary>
        public Task AddAppUser(StandardPayload payload, PayloadHandler replyHandler = null, PayloadHandler timeoutHandler = null, int waitTimeout = StandardPayload.DEFAULT_TIMEOUT)
        {
            return this.SendMessage("crudcoordinator.crud.admin.addappuser", payload, replyHandler, timeoutHandler, waitTimeout);
        }
        
        
        /// <summary>
        /// GetAppUsers - 
        /// </summary>
        public Task GetAppUsers(PayloadHandler replyHandler = null, PayloadHandler timeoutHandler = null, int waitTimeout = StandardPayload.DEFAULT_TIMEOUT)
        {
            return this.GetAppUsers(this.CreatePayload(), replyHandler, timeoutHandler, waitTimeout);
        }

        /// <summary>
        /// GetAppUsers - 
        /// </summary>
        public Task GetAppUsers(String content, PayloadHandler replyHandler = null, PayloadHandler timeoutHandler = null, int waitTimeout = StandardPayload.DEFAULT_TIMEOUT)
        {
            var payload = this.CreatePayload(content);
            return this.GetAppUsers(payload, replyHandler, timeoutHandler, waitTimeout);
        }
    
        
        /// <summary>
        /// GetAppUsers - 
        /// </summary>
        public Task GetAppUsers(StandardPayload payload, PayloadHandler replyHandler = null, PayloadHandler timeoutHandler = null, int waitTimeout = StandardPayload.DEFAULT_TIMEOUT)
        {
            return this.SendMessage("crudcoordinator.crud.admin.getappusers", payload, replyHandler, timeoutHandler, waitTimeout);
        }
        
        
        /// <summary>
        /// UpdateAppUser - 
        /// </summary>
        public Task UpdateAppUser(PayloadHandler replyHandler = null, PayloadHandler timeoutHandler = null, int waitTimeout = StandardPayload.DEFAULT_TIMEOUT)
        {
            return this.UpdateAppUser(this.CreatePayload(), replyHandler, timeoutHandler, waitTimeout);
        }

        /// <summary>
        /// UpdateAppUser - 
        /// </summary>
        public Task UpdateAppUser(String content, PayloadHandler replyHandler = null, PayloadHandler timeoutHandler = null, int waitTimeout = StandardPayload.DEFAULT_TIMEOUT)
        {
            var payload = this.CreatePayload(content);
            return this.UpdateAppUser(payload, replyHandler, timeoutHandler, waitTimeout);
        }
    
        
        /// <summary>
        /// UpdateAppUser - 
        /// </summary>
        public Task UpdateAppUser(StandardPayload payload, PayloadHandler replyHandler = null, PayloadHandler timeoutHandler = null, int waitTimeout = StandardPayload.DEFAULT_TIMEOUT)
        {
            return this.SendMessage("crudcoordinator.crud.admin.updateappuser", payload, replyHandler, timeoutHandler, waitTimeout);
        }
        
        
        /// <summary>
        /// DeleteAppUser - 
        /// </summary>
        public Task DeleteAppUser(PayloadHandler replyHandler = null, PayloadHandler timeoutHandler = null, int waitTimeout = StandardPayload.DEFAULT_TIMEOUT)
        {
            return this.DeleteAppUser(this.CreatePayload(), replyHandler, timeoutHandler, waitTimeout);
        }

        /// <summary>
        /// DeleteAppUser - 
        /// </summary>
        public Task DeleteAppUser(String content, PayloadHandler replyHandler = null, PayloadHandler timeoutHandler = null, int waitTimeout = StandardPayload.DEFAULT_TIMEOUT)
        {
            var payload = this.CreatePayload(content);
            return this.DeleteAppUser(payload, replyHandler, timeoutHandler, waitTimeout);
        }
    
        
        /// <summary>
        /// DeleteAppUser - 
        /// </summary>
        public Task DeleteAppUser(StandardPayload payload, PayloadHandler replyHandler = null, PayloadHandler timeoutHandler = null, int waitTimeout = StandardPayload.DEFAULT_TIMEOUT)
        {
            return this.SendMessage("crudcoordinator.crud.admin.deleteappuser", payload, replyHandler, timeoutHandler, waitTimeout);
        }
        
        
        /// <summary>
        /// AddAICConversation - 
        /// </summary>
        public Task AddAICConversation(PayloadHandler replyHandler = null, PayloadHandler timeoutHandler = null, int waitTimeout = StandardPayload.DEFAULT_TIMEOUT)
        {
            return this.AddAICConversation(this.CreatePayload(), replyHandler, timeoutHandler, waitTimeout);
        }

        /// <summary>
        /// AddAICConversation - 
        /// </summary>
        public Task AddAICConversation(String content, PayloadHandler replyHandler = null, PayloadHandler timeoutHandler = null, int waitTimeout = StandardPayload.DEFAULT_TIMEOUT)
        {
            var payload = this.CreatePayload(content);
            return this.AddAICConversation(payload, replyHandler, timeoutHandler, waitTimeout);
        }
    
        
        /// <summary>
        /// AddAICConversation - 
        /// </summary>
        public Task AddAICConversation(StandardPayload payload, PayloadHandler replyHandler = null, PayloadHandler timeoutHandler = null, int waitTimeout = StandardPayload.DEFAULT_TIMEOUT)
        {
            return this.SendMessage("crudcoordinator.crud.admin.addaicconversation", payload, replyHandler, timeoutHandler, waitTimeout);
        }
        
        
        /// <summary>
        /// GetAICConversations - 
        /// </summary>
        public Task GetAICConversations(PayloadHandler replyHandler = null, PayloadHandler timeoutHandler = null, int waitTimeout = StandardPayload.DEFAULT_TIMEOUT)
        {
            return this.GetAICConversations(this.CreatePayload(), replyHandler, timeoutHandler, waitTimeout);
        }

        /// <summary>
        /// GetAICConversations - 
        /// </summary>
        public Task GetAICConversations(String content, PayloadHandler replyHandler = null, PayloadHandler timeoutHandler = null, int waitTimeout = StandardPayload.DEFAULT_TIMEOUT)
        {
            var payload = this.CreatePayload(content);
            return this.GetAICConversations(payload, replyHandler, timeoutHandler, waitTimeout);
        }
    
        
        /// <summary>
        /// GetAICConversations - 
        /// </summary>
        public Task GetAICConversations(StandardPayload payload, PayloadHandler replyHandler = null, PayloadHandler timeoutHandler = null, int waitTimeout = StandardPayload.DEFAULT_TIMEOUT)
        {
            return this.SendMessage("crudcoordinator.crud.admin.getaicconversations", payload, replyHandler, timeoutHandler, waitTimeout);
        }
        
        
        /// <summary>
        /// UpdateAICConversation - 
        /// </summary>
        public Task UpdateAICConversation(PayloadHandler replyHandler = null, PayloadHandler timeoutHandler = null, int waitTimeout = StandardPayload.DEFAULT_TIMEOUT)
        {
            return this.UpdateAICConversation(this.CreatePayload(), replyHandler, timeoutHandler, waitTimeout);
        }

        /// <summary>
        /// UpdateAICConversation - 
        /// </summary>
        public Task UpdateAICConversation(String content, PayloadHandler replyHandler = null, PayloadHandler timeoutHandler = null, int waitTimeout = StandardPayload.DEFAULT_TIMEOUT)
        {
            var payload = this.CreatePayload(content);
            return this.UpdateAICConversation(payload, replyHandler, timeoutHandler, waitTimeout);
        }
    
        
        /// <summary>
        /// UpdateAICConversation - 
        /// </summary>
        public Task UpdateAICConversation(StandardPayload payload, PayloadHandler replyHandler = null, PayloadHandler timeoutHandler = null, int waitTimeout = StandardPayload.DEFAULT_TIMEOUT)
        {
            return this.SendMessage("crudcoordinator.crud.admin.updateaicconversation", payload, replyHandler, timeoutHandler, waitTimeout);
        }
        
        
        /// <summary>
        /// DeleteAICConversation - 
        /// </summary>
        public Task DeleteAICConversation(PayloadHandler replyHandler = null, PayloadHandler timeoutHandler = null, int waitTimeout = StandardPayload.DEFAULT_TIMEOUT)
        {
            return this.DeleteAICConversation(this.CreatePayload(), replyHandler, timeoutHandler, waitTimeout);
        }

        /// <summary>
        /// DeleteAICConversation - 
        /// </summary>
        public Task DeleteAICConversation(String content, PayloadHandler replyHandler = null, PayloadHandler timeoutHandler = null, int waitTimeout = StandardPayload.DEFAULT_TIMEOUT)
        {
            var payload = this.CreatePayload(content);
            return this.DeleteAICConversation(payload, replyHandler, timeoutHandler, waitTimeout);
        }
    
        
        /// <summary>
        /// DeleteAICConversation - 
        /// </summary>
        public Task DeleteAICConversation(StandardPayload payload, PayloadHandler replyHandler = null, PayloadHandler timeoutHandler = null, int waitTimeout = StandardPayload.DEFAULT_TIMEOUT)
        {
            return this.SendMessage("crudcoordinator.crud.admin.deleteaicconversation", payload, replyHandler, timeoutHandler, waitTimeout);
        }
        
        
        /// <summary>
        /// AddAICProject - 
        /// </summary>
        public Task AddAICProject(PayloadHandler replyHandler = null, PayloadHandler timeoutHandler = null, int waitTimeout = StandardPayload.DEFAULT_TIMEOUT)
        {
            return this.AddAICProject(this.CreatePayload(), replyHandler, timeoutHandler, waitTimeout);
        }

        /// <summary>
        /// AddAICProject - 
        /// </summary>
        public Task AddAICProject(String content, PayloadHandler replyHandler = null, PayloadHandler timeoutHandler = null, int waitTimeout = StandardPayload.DEFAULT_TIMEOUT)
        {
            var payload = this.CreatePayload(content);
            return this.AddAICProject(payload, replyHandler, timeoutHandler, waitTimeout);
        }
    
        
        /// <summary>
        /// AddAICProject - 
        /// </summary>
        public Task AddAICProject(StandardPayload payload, PayloadHandler replyHandler = null, PayloadHandler timeoutHandler = null, int waitTimeout = StandardPayload.DEFAULT_TIMEOUT)
        {
            return this.SendMessage("crudcoordinator.crud.admin.addaicproject", payload, replyHandler, timeoutHandler, waitTimeout);
        }
        
        
        /// <summary>
        /// GetAICProjects - 
        /// </summary>
        public Task GetAICProjects(PayloadHandler replyHandler = null, PayloadHandler timeoutHandler = null, int waitTimeout = StandardPayload.DEFAULT_TIMEOUT)
        {
            return this.GetAICProjects(this.CreatePayload(), replyHandler, timeoutHandler, waitTimeout);
        }

        /// <summary>
        /// GetAICProjects - 
        /// </summary>
        public Task GetAICProjects(String content, PayloadHandler replyHandler = null, PayloadHandler timeoutHandler = null, int waitTimeout = StandardPayload.DEFAULT_TIMEOUT)
        {
            var payload = this.CreatePayload(content);
            return this.GetAICProjects(payload, replyHandler, timeoutHandler, waitTimeout);
        }
    
        
        /// <summary>
        /// GetAICProjects - 
        /// </summary>
        public Task GetAICProjects(StandardPayload payload, PayloadHandler replyHandler = null, PayloadHandler timeoutHandler = null, int waitTimeout = StandardPayload.DEFAULT_TIMEOUT)
        {
            return this.SendMessage("crudcoordinator.crud.admin.getaicprojects", payload, replyHandler, timeoutHandler, waitTimeout);
        }
        
        
        /// <summary>
        /// UpdateAICProject - 
        /// </summary>
        public Task UpdateAICProject(PayloadHandler replyHandler = null, PayloadHandler timeoutHandler = null, int waitTimeout = StandardPayload.DEFAULT_TIMEOUT)
        {
            return this.UpdateAICProject(this.CreatePayload(), replyHandler, timeoutHandler, waitTimeout);
        }

        /// <summary>
        /// UpdateAICProject - 
        /// </summary>
        public Task UpdateAICProject(String content, PayloadHandler replyHandler = null, PayloadHandler timeoutHandler = null, int waitTimeout = StandardPayload.DEFAULT_TIMEOUT)
        {
            var payload = this.CreatePayload(content);
            return this.UpdateAICProject(payload, replyHandler, timeoutHandler, waitTimeout);
        }
    
        
        /// <summary>
        /// UpdateAICProject - 
        /// </summary>
        public Task UpdateAICProject(StandardPayload payload, PayloadHandler replyHandler = null, PayloadHandler timeoutHandler = null, int waitTimeout = StandardPayload.DEFAULT_TIMEOUT)
        {
            return this.SendMessage("crudcoordinator.crud.admin.updateaicproject", payload, replyHandler, timeoutHandler, waitTimeout);
        }
        
        
        /// <summary>
        /// DeleteAICProject - 
        /// </summary>
        public Task DeleteAICProject(PayloadHandler replyHandler = null, PayloadHandler timeoutHandler = null, int waitTimeout = StandardPayload.DEFAULT_TIMEOUT)
        {
            return this.DeleteAICProject(this.CreatePayload(), replyHandler, timeoutHandler, waitTimeout);
        }

        /// <summary>
        /// DeleteAICProject - 
        /// </summary>
        public Task DeleteAICProject(String content, PayloadHandler replyHandler = null, PayloadHandler timeoutHandler = null, int waitTimeout = StandardPayload.DEFAULT_TIMEOUT)
        {
            var payload = this.CreatePayload(content);
            return this.DeleteAICProject(payload, replyHandler, timeoutHandler, waitTimeout);
        }
    
        
        /// <summary>
        /// DeleteAICProject - 
        /// </summary>
        public Task DeleteAICProject(StandardPayload payload, PayloadHandler replyHandler = null, PayloadHandler timeoutHandler = null, int waitTimeout = StandardPayload.DEFAULT_TIMEOUT)
        {
            return this.SendMessage("crudcoordinator.crud.admin.deleteaicproject", payload, replyHandler, timeoutHandler, waitTimeout);
        }
        
        
    }
}

                    
