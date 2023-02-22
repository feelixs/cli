using System;
using System.Linq;
using RabbitMQ.Client;
using System.Text;
using Newtonsoft.Json;
using RabbitMQ.Client.Events;
using System.Threading.Tasks;

namespace AIC.SassyMQ.Lib
{
    public partial class SMQCRUDCoordinator : SMQActorBase
    {

        public SMQCRUDCoordinator(String amqpConnectionString)
            : base(amqpConnectionString, "crudcoordinator")
        {
        }

        protected override void CheckRouting(StandardPayload payload, BasicDeliverEventArgs  bdea)
        {
            var originalAccessToken = payload.AccessToken;
            try
            {
                switch (bdea.RoutingKey)
                {
                    
                    case "crudcoordinator.general.guest.requesttoken":
                        this.OnGuestRequestTokenReceived(payload, bdea);
                        break;
                    
                    case "crudcoordinator.general.guest.validatetemporaryaccesstoken":
                        this.OnGuestValidateTemporaryAccessTokenReceived(payload, bdea);
                        break;
                    
                    case "crudcoordinator.general.guest.whoami":
                        this.OnGuestWhoAmIReceived(payload, bdea);
                        break;
                    
                    case "crudcoordinator.general.guest.whoareyou":
                        this.OnGuestWhoAreYouReceived(payload, bdea);
                        break;
                    
                    case "crudcoordinator.utlity.guest.storetempfile":
                        this.OnGuestStoreTempFileReceived(payload, bdea);
                        break;
                    
                    case "crudcoordinator.general.crudcoordinator.resetrabbitsassymqconfiguration":
                        this.OnCRUDCoordinatorResetRabbitSassyMQConfigurationReceived(payload, bdea);
                        break;
                    
                    case "crudcoordinator.general.crudcoordinator.resetjwtsecretkey":
                        this.OnCRUDCoordinatorResetJWTSecretKeyReceived(payload, bdea);
                        break;
                    
                    case "crudcoordinator.custom.aicagent.monitoringfor":
                        this.OnAICAgentMonitoringForReceived(payload, bdea);
                        break;
                    
                    case "crudcoordinator.crud.admin.addappuser":
                        this.OnAdminAddAppUserReceived(payload, bdea);
                        break;
                    
                    case "crudcoordinator.crud.admin.getappusers":
                        this.OnAdminGetAppUsersReceived(payload, bdea);
                        break;
                    
                    case "crudcoordinator.crud.admin.updateappuser":
                        this.OnAdminUpdateAppUserReceived(payload, bdea);
                        break;
                    
                    case "crudcoordinator.crud.admin.deleteappuser":
                        this.OnAdminDeleteAppUserReceived(payload, bdea);
                        break;
                    
                    case "crudcoordinator.crud.user.addaicconversation":
                        this.OnUserAddAICConversationReceived(payload, bdea);
                        break;
                    
                    case "crudcoordinator.crud.user.getaicconversations":
                        this.OnUserGetAICConversationsReceived(payload, bdea);
                        break;
                    
                    case "crudcoordinator.crud.user.updateaicconversation":
                        this.OnUserUpdateAICConversationReceived(payload, bdea);
                        break;
                    
                    case "crudcoordinator.crud.admin.addaicconversation":
                        this.OnAdminAddAICConversationReceived(payload, bdea);
                        break;
                    
                    case "crudcoordinator.crud.admin.getaicconversations":
                        this.OnAdminGetAICConversationsReceived(payload, bdea);
                        break;
                    
                    case "crudcoordinator.crud.admin.updateaicconversation":
                        this.OnAdminUpdateAICConversationReceived(payload, bdea);
                        break;
                    
                    case "crudcoordinator.crud.admin.deleteaicconversation":
                        this.OnAdminDeleteAICConversationReceived(payload, bdea);
                        break;
                    
                    case "crudcoordinator.crud.user.addaicproject":
                        this.OnUserAddAICProjectReceived(payload, bdea);
                        break;
                    
                    case "crudcoordinator.crud.user.getaicprojects":
                        this.OnUserGetAICProjectsReceived(payload, bdea);
                        break;
                    
                    case "crudcoordinator.crud.user.updateaicproject":
                        this.OnUserUpdateAICProjectReceived(payload, bdea);
                        break;
                    
                    case "crudcoordinator.crud.admin.addaicproject":
                        this.OnAdminAddAICProjectReceived(payload, bdea);
                        break;
                    
                    case "crudcoordinator.crud.admin.getaicprojects":
                        this.OnAdminGetAICProjectsReceived(payload, bdea);
                        break;
                    
                    case "crudcoordinator.crud.admin.updateaicproject":
                        this.OnAdminUpdateAICProjectReceived(payload, bdea);
                        break;
                    
                    case "crudcoordinator.crud.admin.deleteaicproject":
                        this.OnAdminDeleteAICProjectReceived(payload, bdea);
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
        /// Responds to: RequestToken from Guest
        /// </summary>
        public event EventHandler<PayloadEventArgs> GuestRequestTokenReceived;
        protected virtual void OnGuestRequestTokenReceived(StandardPayload payload, BasicDeliverEventArgs bdea)
        {
            var plea = new PayloadEventArgs(payload, bdea);
            if (!ReferenceEquals(this.GuestRequestTokenReceived, null))
            {
                plea.Payload.IsHandled = true;
                this.GuestRequestTokenReceived(this, plea);
            }
        }

        /// <summary>
        /// Responds to: ValidateTemporaryAccessToken from Guest
        /// </summary>
        public event EventHandler<PayloadEventArgs> GuestValidateTemporaryAccessTokenReceived;
        protected virtual void OnGuestValidateTemporaryAccessTokenReceived(StandardPayload payload, BasicDeliverEventArgs bdea)
        {
            var plea = new PayloadEventArgs(payload, bdea);
            if (!ReferenceEquals(this.GuestValidateTemporaryAccessTokenReceived, null))
            {
                plea.Payload.IsHandled = true;
                this.GuestValidateTemporaryAccessTokenReceived(this, plea);
            }
        }

        /// <summary>
        /// Responds to: WhoAmI from Guest
        /// </summary>
        public event EventHandler<PayloadEventArgs> GuestWhoAmIReceived;
        protected virtual void OnGuestWhoAmIReceived(StandardPayload payload, BasicDeliverEventArgs bdea)
        {
            var plea = new PayloadEventArgs(payload, bdea);
            if (!ReferenceEquals(this.GuestWhoAmIReceived, null))
            {
                plea.Payload.IsHandled = true;
                this.GuestWhoAmIReceived(this, plea);
            }
        }

        /// <summary>
        /// Responds to: WhoAreYou from Guest
        /// </summary>
        public event EventHandler<PayloadEventArgs> GuestWhoAreYouReceived;
        protected virtual void OnGuestWhoAreYouReceived(StandardPayload payload, BasicDeliverEventArgs bdea)
        {
            var plea = new PayloadEventArgs(payload, bdea);
            if (!ReferenceEquals(this.GuestWhoAreYouReceived, null))
            {
                plea.Payload.IsHandled = true;
                this.GuestWhoAreYouReceived(this, plea);
            }
        }

        /// <summary>
        /// Responds to: StoreTempFile from Guest
        /// </summary>
        public event EventHandler<PayloadEventArgs> GuestStoreTempFileReceived;
        protected virtual void OnGuestStoreTempFileReceived(StandardPayload payload, BasicDeliverEventArgs bdea)
        {
            var plea = new PayloadEventArgs(payload, bdea);
            if (!ReferenceEquals(this.GuestStoreTempFileReceived, null))
            {
                plea.Payload.IsHandled = true;
                this.GuestStoreTempFileReceived(this, plea);
            }
        }

        /// <summary>
        /// Responds to: ResetRabbitSassyMQConfiguration from CRUDCoordinator
        /// </summary>
        public event EventHandler<PayloadEventArgs> CRUDCoordinatorResetRabbitSassyMQConfigurationReceived;
        protected virtual void OnCRUDCoordinatorResetRabbitSassyMQConfigurationReceived(StandardPayload payload, BasicDeliverEventArgs bdea)
        {
            var plea = new PayloadEventArgs(payload, bdea);
            if (!ReferenceEquals(this.CRUDCoordinatorResetRabbitSassyMQConfigurationReceived, null))
            {
                plea.Payload.IsHandled = true;
                this.CRUDCoordinatorResetRabbitSassyMQConfigurationReceived(this, plea);
            }
        }

        /// <summary>
        /// Responds to: ResetJWTSecretKey from CRUDCoordinator
        /// </summary>
        public event EventHandler<PayloadEventArgs> CRUDCoordinatorResetJWTSecretKeyReceived;
        protected virtual void OnCRUDCoordinatorResetJWTSecretKeyReceived(StandardPayload payload, BasicDeliverEventArgs bdea)
        {
            var plea = new PayloadEventArgs(payload, bdea);
            if (!ReferenceEquals(this.CRUDCoordinatorResetJWTSecretKeyReceived, null))
            {
                plea.Payload.IsHandled = true;
                this.CRUDCoordinatorResetJWTSecretKeyReceived(this, plea);
            }
        }

        /// <summary>
        /// Responds to: MonitoringFor from AICAgent
        /// </summary>
        public event EventHandler<PayloadEventArgs> AICAgentMonitoringForReceived;
        protected virtual void OnAICAgentMonitoringForReceived(StandardPayload payload, BasicDeliverEventArgs bdea)
        {
            var plea = new PayloadEventArgs(payload, bdea);
            if (!ReferenceEquals(this.AICAgentMonitoringForReceived, null))
            {
                plea.Payload.IsHandled = true;
                this.AICAgentMonitoringForReceived(this, plea);
            }
        }

        /// <summary>
        /// Responds to: AddAppUser from Admin
        /// </summary>
        public event EventHandler<PayloadEventArgs> AdminAddAppUserReceived;
        protected virtual void OnAdminAddAppUserReceived(StandardPayload payload, BasicDeliverEventArgs bdea)
        {
            var plea = new PayloadEventArgs(payload, bdea);
            if (!ReferenceEquals(this.AdminAddAppUserReceived, null))
            {
                plea.Payload.IsHandled = true;
                this.AdminAddAppUserReceived(this, plea);
            }
        }

        /// <summary>
        /// Responds to: GetAppUsers from Admin
        /// </summary>
        public event EventHandler<PayloadEventArgs> AdminGetAppUsersReceived;
        protected virtual void OnAdminGetAppUsersReceived(StandardPayload payload, BasicDeliverEventArgs bdea)
        {
            var plea = new PayloadEventArgs(payload, bdea);
            if (!ReferenceEquals(this.AdminGetAppUsersReceived, null))
            {
                plea.Payload.IsHandled = true;
                this.AdminGetAppUsersReceived(this, plea);
            }
        }

        /// <summary>
        /// Responds to: UpdateAppUser from Admin
        /// </summary>
        public event EventHandler<PayloadEventArgs> AdminUpdateAppUserReceived;
        protected virtual void OnAdminUpdateAppUserReceived(StandardPayload payload, BasicDeliverEventArgs bdea)
        {
            var plea = new PayloadEventArgs(payload, bdea);
            if (!ReferenceEquals(this.AdminUpdateAppUserReceived, null))
            {
                plea.Payload.IsHandled = true;
                this.AdminUpdateAppUserReceived(this, plea);
            }
        }

        /// <summary>
        /// Responds to: DeleteAppUser from Admin
        /// </summary>
        public event EventHandler<PayloadEventArgs> AdminDeleteAppUserReceived;
        protected virtual void OnAdminDeleteAppUserReceived(StandardPayload payload, BasicDeliverEventArgs bdea)
        {
            var plea = new PayloadEventArgs(payload, bdea);
            if (!ReferenceEquals(this.AdminDeleteAppUserReceived, null))
            {
                plea.Payload.IsHandled = true;
                this.AdminDeleteAppUserReceived(this, plea);
            }
        }

        /// <summary>
        /// Responds to: AddAICConversation from User
        /// </summary>
        public event EventHandler<PayloadEventArgs> UserAddAICConversationReceived;
        protected virtual void OnUserAddAICConversationReceived(StandardPayload payload, BasicDeliverEventArgs bdea)
        {
            var plea = new PayloadEventArgs(payload, bdea);
            if (!ReferenceEquals(this.UserAddAICConversationReceived, null))
            {
                plea.Payload.IsHandled = true;
                this.UserAddAICConversationReceived(this, plea);
            }
        }

        /// <summary>
        /// Responds to: GetAICConversations from User
        /// </summary>
        public event EventHandler<PayloadEventArgs> UserGetAICConversationsReceived;
        protected virtual void OnUserGetAICConversationsReceived(StandardPayload payload, BasicDeliverEventArgs bdea)
        {
            var plea = new PayloadEventArgs(payload, bdea);
            if (!ReferenceEquals(this.UserGetAICConversationsReceived, null))
            {
                plea.Payload.IsHandled = true;
                this.UserGetAICConversationsReceived(this, plea);
            }
        }

        /// <summary>
        /// Responds to: UpdateAICConversation from User
        /// </summary>
        public event EventHandler<PayloadEventArgs> UserUpdateAICConversationReceived;
        protected virtual void OnUserUpdateAICConversationReceived(StandardPayload payload, BasicDeliverEventArgs bdea)
        {
            var plea = new PayloadEventArgs(payload, bdea);
            if (!ReferenceEquals(this.UserUpdateAICConversationReceived, null))
            {
                plea.Payload.IsHandled = true;
                this.UserUpdateAICConversationReceived(this, plea);
            }
        }

        /// <summary>
        /// Responds to: AddAICConversation from Admin
        /// </summary>
        public event EventHandler<PayloadEventArgs> AdminAddAICConversationReceived;
        protected virtual void OnAdminAddAICConversationReceived(StandardPayload payload, BasicDeliverEventArgs bdea)
        {
            var plea = new PayloadEventArgs(payload, bdea);
            if (!ReferenceEquals(this.AdminAddAICConversationReceived, null))
            {
                plea.Payload.IsHandled = true;
                this.AdminAddAICConversationReceived(this, plea);
            }
        }

        /// <summary>
        /// Responds to: GetAICConversations from Admin
        /// </summary>
        public event EventHandler<PayloadEventArgs> AdminGetAICConversationsReceived;
        protected virtual void OnAdminGetAICConversationsReceived(StandardPayload payload, BasicDeliverEventArgs bdea)
        {
            var plea = new PayloadEventArgs(payload, bdea);
            if (!ReferenceEquals(this.AdminGetAICConversationsReceived, null))
            {
                plea.Payload.IsHandled = true;
                this.AdminGetAICConversationsReceived(this, plea);
            }
        }

        /// <summary>
        /// Responds to: UpdateAICConversation from Admin
        /// </summary>
        public event EventHandler<PayloadEventArgs> AdminUpdateAICConversationReceived;
        protected virtual void OnAdminUpdateAICConversationReceived(StandardPayload payload, BasicDeliverEventArgs bdea)
        {
            var plea = new PayloadEventArgs(payload, bdea);
            if (!ReferenceEquals(this.AdminUpdateAICConversationReceived, null))
            {
                plea.Payload.IsHandled = true;
                this.AdminUpdateAICConversationReceived(this, plea);
            }
        }

        /// <summary>
        /// Responds to: DeleteAICConversation from Admin
        /// </summary>
        public event EventHandler<PayloadEventArgs> AdminDeleteAICConversationReceived;
        protected virtual void OnAdminDeleteAICConversationReceived(StandardPayload payload, BasicDeliverEventArgs bdea)
        {
            var plea = new PayloadEventArgs(payload, bdea);
            if (!ReferenceEquals(this.AdminDeleteAICConversationReceived, null))
            {
                plea.Payload.IsHandled = true;
                this.AdminDeleteAICConversationReceived(this, plea);
            }
        }

        /// <summary>
        /// Responds to: AddAICProject from User
        /// </summary>
        public event EventHandler<PayloadEventArgs> UserAddAICProjectReceived;
        protected virtual void OnUserAddAICProjectReceived(StandardPayload payload, BasicDeliverEventArgs bdea)
        {
            var plea = new PayloadEventArgs(payload, bdea);
            if (!ReferenceEquals(this.UserAddAICProjectReceived, null))
            {
                plea.Payload.IsHandled = true;
                this.UserAddAICProjectReceived(this, plea);
            }
        }

        /// <summary>
        /// Responds to: GetAICProjects from User
        /// </summary>
        public event EventHandler<PayloadEventArgs> UserGetAICProjectsReceived;
        protected virtual void OnUserGetAICProjectsReceived(StandardPayload payload, BasicDeliverEventArgs bdea)
        {
            var plea = new PayloadEventArgs(payload, bdea);
            if (!ReferenceEquals(this.UserGetAICProjectsReceived, null))
            {
                plea.Payload.IsHandled = true;
                this.UserGetAICProjectsReceived(this, plea);
            }
        }

        /// <summary>
        /// Responds to: UpdateAICProject from User
        /// </summary>
        public event EventHandler<PayloadEventArgs> UserUpdateAICProjectReceived;
        protected virtual void OnUserUpdateAICProjectReceived(StandardPayload payload, BasicDeliverEventArgs bdea)
        {
            var plea = new PayloadEventArgs(payload, bdea);
            if (!ReferenceEquals(this.UserUpdateAICProjectReceived, null))
            {
                plea.Payload.IsHandled = true;
                this.UserUpdateAICProjectReceived(this, plea);
            }
        }

        /// <summary>
        /// Responds to: AddAICProject from Admin
        /// </summary>
        public event EventHandler<PayloadEventArgs> AdminAddAICProjectReceived;
        protected virtual void OnAdminAddAICProjectReceived(StandardPayload payload, BasicDeliverEventArgs bdea)
        {
            var plea = new PayloadEventArgs(payload, bdea);
            if (!ReferenceEquals(this.AdminAddAICProjectReceived, null))
            {
                plea.Payload.IsHandled = true;
                this.AdminAddAICProjectReceived(this, plea);
            }
        }

        /// <summary>
        /// Responds to: GetAICProjects from Admin
        /// </summary>
        public event EventHandler<PayloadEventArgs> AdminGetAICProjectsReceived;
        protected virtual void OnAdminGetAICProjectsReceived(StandardPayload payload, BasicDeliverEventArgs bdea)
        {
            var plea = new PayloadEventArgs(payload, bdea);
            if (!ReferenceEquals(this.AdminGetAICProjectsReceived, null))
            {
                plea.Payload.IsHandled = true;
                this.AdminGetAICProjectsReceived(this, plea);
            }
        }

        /// <summary>
        /// Responds to: UpdateAICProject from Admin
        /// </summary>
        public event EventHandler<PayloadEventArgs> AdminUpdateAICProjectReceived;
        protected virtual void OnAdminUpdateAICProjectReceived(StandardPayload payload, BasicDeliverEventArgs bdea)
        {
            var plea = new PayloadEventArgs(payload, bdea);
            if (!ReferenceEquals(this.AdminUpdateAICProjectReceived, null))
            {
                plea.Payload.IsHandled = true;
                this.AdminUpdateAICProjectReceived(this, plea);
            }
        }

        /// <summary>
        /// Responds to: DeleteAICProject from Admin
        /// </summary>
        public event EventHandler<PayloadEventArgs> AdminDeleteAICProjectReceived;
        protected virtual void OnAdminDeleteAICProjectReceived(StandardPayload payload, BasicDeliverEventArgs bdea)
        {
            var plea = new PayloadEventArgs(payload, bdea);
            if (!ReferenceEquals(this.AdminDeleteAICProjectReceived, null))
            {
                plea.Payload.IsHandled = true;
                this.AdminDeleteAICProjectReceived(this, plea);
            }
        }

        /// <summary>
        /// ResetRabbitSassyMQConfiguration - 
        /// </summary>
        public Task ResetRabbitSassyMQConfiguration(PayloadHandler replyHandler = null, PayloadHandler timeoutHandler = null, int waitTimeout = StandardPayload.DEFAULT_TIMEOUT)
        {
            return this.ResetRabbitSassyMQConfiguration(this.CreatePayload(), replyHandler, timeoutHandler, waitTimeout);
        }

        /// <summary>
        /// ResetRabbitSassyMQConfiguration - 
        /// </summary>
        public Task ResetRabbitSassyMQConfiguration(String content, PayloadHandler replyHandler = null, PayloadHandler timeoutHandler = null, int waitTimeout = StandardPayload.DEFAULT_TIMEOUT)
        {
            var payload = this.CreatePayload(content);
            return this.ResetRabbitSassyMQConfiguration(payload, replyHandler, timeoutHandler, waitTimeout);
        }
    
        
        /// <summary>
        /// ResetRabbitSassyMQConfiguration - 
        /// </summary>
        public Task ResetRabbitSassyMQConfiguration(StandardPayload payload, PayloadHandler replyHandler = null, PayloadHandler timeoutHandler = null, int waitTimeout = StandardPayload.DEFAULT_TIMEOUT)
        {
            return this.SendMessage("crudcoordinator.general.crudcoordinator.resetrabbitsassymqconfiguration", payload, replyHandler, timeoutHandler, waitTimeout);
        }
        
        
        /// <summary>
        /// ResetJWTSecretKey - 
        /// </summary>
        public Task ResetJWTSecretKey(PayloadHandler replyHandler = null, PayloadHandler timeoutHandler = null, int waitTimeout = StandardPayload.DEFAULT_TIMEOUT)
        {
            return this.ResetJWTSecretKey(this.CreatePayload(), replyHandler, timeoutHandler, waitTimeout);
        }

        /// <summary>
        /// ResetJWTSecretKey - 
        /// </summary>
        public Task ResetJWTSecretKey(String content, PayloadHandler replyHandler = null, PayloadHandler timeoutHandler = null, int waitTimeout = StandardPayload.DEFAULT_TIMEOUT)
        {
            var payload = this.CreatePayload(content);
            return this.ResetJWTSecretKey(payload, replyHandler, timeoutHandler, waitTimeout);
        }
    
        
        /// <summary>
        /// ResetJWTSecretKey - 
        /// </summary>
        public Task ResetJWTSecretKey(StandardPayload payload, PayloadHandler replyHandler = null, PayloadHandler timeoutHandler = null, int waitTimeout = StandardPayload.DEFAULT_TIMEOUT)
        {
            return this.SendMessage("crudcoordinator.general.crudcoordinator.resetjwtsecretkey", payload, replyHandler, timeoutHandler, waitTimeout);
        }
        
        
    }
}

                    
