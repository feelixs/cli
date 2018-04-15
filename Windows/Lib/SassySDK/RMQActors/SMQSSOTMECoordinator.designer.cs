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
    public partial class SMQSSOTMECoordinator : SSOTMEActorBase
    {
     
        public SMQSSOTMECoordinator(bool isAutoConnect = true)
            : base("ssotmecoordinator.all", isAutoConnect)
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
                this.RMQChannel.BasicPublish("", payload.ReplyTo, body: Encoding.UTF8.GetBytes(payload.ToJSonString()));
            }
        }

        protected override void CheckRouting(SSOTMEPayload payload, bool isDirectMessage) 
        {
            // if (payload.IsDirectMessage && !isDirectMessage) return;

            try {
                
             if (payload.IsLexiconTerm(LexiconTermEnum.publicuser_ping_ssotmecoordinator)) 
            {
                this.OnPublicUserPingReceived(payload);
            }
        
            else  if (payload.IsLexiconTerm(LexiconTermEnum.publicuser_register_ssotmecoordinator)) 
            {
                this.OnPublicUserRegisterReceived(payload);
            }
        
            else  if (payload.IsLexiconTerm(LexiconTermEnum.publicuser_recover_ssotmecoordinator)) 
            {
                this.OnPublicUserRecoverReceived(payload);
            }
        
            else  if (payload.IsLexiconTerm(LexiconTermEnum.publicuser_getalltranspilers_ssotmecoordinator)) 
            {
                this.OnPublicUserGetAllTranspilersReceived(payload);
            }
        
            else  if (payload.IsLexiconTerm(LexiconTermEnum.publicuser_getallplatformdata_ssotmecoordinator)) 
            {
                this.OnPublicUserGetAllPlatformDataReceived(payload);
            }
        
            else  if (payload.IsLexiconTerm(LexiconTermEnum.publicuser_getallfiletypes_ssotmecoordinator)) 
            {
                this.OnPublicUserGetAllFileTypesReceived(payload);
            }
        
            else  if (payload.IsLexiconTerm(LexiconTermEnum.accountholder_ping_ssotmecoordinator)) 
            {
                this.OnAccountHolderPingReceived(payload);
            }
        
            else  if (payload.IsLexiconTerm(LexiconTermEnum.accountholder_login_ssotmecoordinator)) 
            {
                this.OnAccountHolderLoginReceived(payload);
            }
        
            else  if (payload.IsLexiconTerm(LexiconTermEnum.accountholder_logout_ssotmecoordinator)) 
            {
                this.OnAccountHolderLogoutReceived(payload);
            }
        
            else  if (payload.IsLexiconTerm(LexiconTermEnum.accountholder_addtranspiler_ssotmecoordinator)) 
            {
                this.OnAccountHolderAddTranspilerReceived(payload);
            }
        
            else  if (payload.IsLexiconTerm(LexiconTermEnum.accountholder_deletetranspiler_ssotmecoordinator)) 
            {
                this.OnAccountHolderDeleteTranspilerReceived(payload);
            }
        
            else  if (payload.IsLexiconTerm(LexiconTermEnum.accountholder_updatetranspiler_ssotmecoordinator)) 
            {
                this.OnAccountHolderUpdateTranspilerReceived(payload);
            }
        
            else  if (payload.IsLexiconTerm(LexiconTermEnum.accountholder_gettranspiler_ssotmecoordinator)) 
            {
                this.OnAccountHolderGetTranspilerReceived(payload);
            }
        
            else  if (payload.IsLexiconTerm(LexiconTermEnum.accountholder_addtranspilerversion_ssotmecoordinator)) 
            {
                this.OnAccountHolderAddTranspilerVersionReceived(payload);
            }
        
            else  if (payload.IsLexiconTerm(LexiconTermEnum.accountholder_deletetranspilerversion_ssotmecoordinator)) 
            {
                this.OnAccountHolderDeleteTranspilerVersionReceived(payload);
            }
        
            else  if (payload.IsLexiconTerm(LexiconTermEnum.accountholder_updatetranspilerversion_ssotmecoordinator)) 
            {
                this.OnAccountHolderUpdateTranspilerVersionReceived(payload);
            }
        
            else  if (payload.IsLexiconTerm(LexiconTermEnum.accountholder_gettranspilerlist_ssotmecoordinator)) 
            {
                this.OnAccountHolderGetTranspilerListReceived(payload);
            }
        
            else  if (payload.IsLexiconTerm(LexiconTermEnum.accountholder_createproject_ssotmecoordinator)) 
            {
                this.OnAccountHolderCreateProjectReceived(payload);
            }
        
            else  if (payload.IsLexiconTerm(LexiconTermEnum.accountholder_requesttranspile_ssotmecoordinator)) 
            {
                this.OnAccountHolderRequestTranspileReceived(payload);
            }
        
            else  if (payload.IsLexiconTerm(LexiconTermEnum.accountholder_requesttranspilerhost_ssotmecoordinator)) 
            {
                this.OnAccountHolderRequestTranspilerHostReceived(payload);
            }
        
            else  if (payload.IsLexiconTerm(LexiconTermEnum.accountholder_requeststopinstance_ssotmecoordinator)) 
            {
                this.OnAccountHolderRequestStopInstanceReceived(payload);
            }
        
            else  if (payload.IsLexiconTerm(LexiconTermEnum.accountholder_requeststophost_ssotmecoordinator)) 
            {
                this.OnAccountHolderRequestStopHostReceived(payload);
            }
        
            else  if (payload.IsLexiconTerm(LexiconTermEnum.accountholder_commandlinetranspile_ssotmecoordinator)) 
            {
                this.OnAccountHolderCommandLineTranspileReceived(payload);
            }
        
            else  if (payload.IsLexiconTerm(LexiconTermEnum.transpilerhost_ping_ssotmecoordinator)) 
            {
                this.OnTranspilerHostPingReceived(payload);
            }
        
            else  if (payload.IsLexiconTerm(LexiconTermEnum.transpilerhost_offline_ssotmecoordinator)) 
            {
                this.OnTranspilerHostOfflineReceived(payload);
            }
        
            else  if (payload.IsLexiconTerm(LexiconTermEnum.transpilerhost_instancestarted_ssotmecoordinator)) 
            {
                this.OnTranspilerHostInstanceStartedReceived(payload);
            }
        
            else  if (payload.IsLexiconTerm(LexiconTermEnum.transpilerhost_instancestopped_ssotmecoordinator)) 
            {
                this.OnTranspilerHostInstanceStoppedReceived(payload);
            }
        
            else  if (payload.IsLexiconTerm(LexiconTermEnum.ssotmeadmin_ping_ssotmecoordinator)) 
            {
                this.OnSSOTMEAdminPingReceived(payload);
            }
        
            else  if (payload.IsLexiconTerm(LexiconTermEnum.ssotmeadmin_addplatformcategory_ssotmecoordinator)) 
            {
                this.OnSSOTMEAdminAddPlatformCategoryReceived(payload);
            }
        
            else  if (payload.IsLexiconTerm(LexiconTermEnum.ssotmeadmin_updateplatformcategory_ssotmecoordinator)) 
            {
                this.OnSSOTMEAdminUpdatePlatformCategoryReceived(payload);
            }
        
            else  if (payload.IsLexiconTerm(LexiconTermEnum.ssotmeadmin_addtranspilerplatform_ssotmecoordinator)) 
            {
                this.OnSSOTMEAdminAddTranspilerPlatformReceived(payload);
            }
        
            else  if (payload.IsLexiconTerm(LexiconTermEnum.ssotmeadmin_updatetranspilerplatform_ssotmecoordinator)) 
            {
                this.OnSSOTMEAdminUpdateTranspilerPlatformReceived(payload);
            }
        
            // And can also hear everything which : SSOTMEAdmin hears.
            
            // And can also hear everything which : TranspilerHost hears.
            
             if (payload.IsLexiconTerm(LexiconTermEnum.ssotmecoordinator_getinstances_transpilerhost)) 
            {
                this.OnSSOTMECoordinatorGetInstancesReceived(payload);
            }
        
            else  if (payload.IsLexiconTerm(LexiconTermEnum.ssotmecoordinator_transpilerequested_transpilerhost)) 
            {
                this.OnSSOTMECoordinatorTranspileRequestedReceived(payload);
            }
        
            else  if (payload.IsLexiconTerm(LexiconTermEnum.ssotmecoordinator_stopinstance_transpilerhost)) 
            {
                this.OnSSOTMECoordinatorStopInstanceReceived(payload);
            }
        
            else  if (payload.IsLexiconTerm(LexiconTermEnum.ssotmecoordinator_stophost_transpilerhost)) 
            {
                this.OnSSOTMECoordinatorStopHostReceived(payload);
            }
        
            // And can also hear everything which : AccountHolder hears.
            
            // And can also hear everything which : PublicUser hears.
            
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
        /// Responds to: Ping - 
        /// </summary>
        public event System.EventHandler<PayloadEventArgs<SSOTMEPayload>> PublicUserPingReceived;
        protected virtual void OnPublicUserPingReceived(SSOTMEPayload payload)
        {
            this.LogMessage(payload, "Ping - ");
            var plea = new PayloadEventArgs<SSOTMEPayload>(payload);
            this.Invoke(this.PublicUserPingReceived, plea);
        }
        
        /// <summary>
        /// Responds to: Register - 
        /// </summary>
        public event System.EventHandler<PayloadEventArgs<SSOTMEPayload>> PublicUserRegisterReceived;
        protected virtual void OnPublicUserRegisterReceived(SSOTMEPayload payload)
        {
            this.LogMessage(payload, "Register - ");
            var plea = new PayloadEventArgs<SSOTMEPayload>(payload);
            this.Invoke(this.PublicUserRegisterReceived, plea);
        }
        
        /// <summary>
        /// Responds to: Recover - 
        /// </summary>
        public event System.EventHandler<PayloadEventArgs<SSOTMEPayload>> PublicUserRecoverReceived;
        protected virtual void OnPublicUserRecoverReceived(SSOTMEPayload payload)
        {
            this.LogMessage(payload, "Recover - ");
            var plea = new PayloadEventArgs<SSOTMEPayload>(payload);
            this.Invoke(this.PublicUserRecoverReceived, plea);
        }
        
        /// <summary>
        /// Responds to: Get All Transpilers - 
        /// </summary>
        public event System.EventHandler<PayloadEventArgs<SSOTMEPayload>> PublicUserGetAllTranspilersReceived;
        protected virtual void OnPublicUserGetAllTranspilersReceived(SSOTMEPayload payload)
        {
            this.LogMessage(payload, "Get All Transpilers - ");
            var plea = new PayloadEventArgs<SSOTMEPayload>(payload);
            this.Invoke(this.PublicUserGetAllTranspilersReceived, plea);
        }
        
        /// <summary>
        /// Responds to: Get All Platform Data - 
        /// </summary>
        public event System.EventHandler<PayloadEventArgs<SSOTMEPayload>> PublicUserGetAllPlatformDataReceived;
        protected virtual void OnPublicUserGetAllPlatformDataReceived(SSOTMEPayload payload)
        {
            this.LogMessage(payload, "Get All Platform Data - ");
            var plea = new PayloadEventArgs<SSOTMEPayload>(payload);
            this.Invoke(this.PublicUserGetAllPlatformDataReceived, plea);
        }
        
        /// <summary>
        /// Responds to: Get All File Types - 
        /// </summary>
        public event System.EventHandler<PayloadEventArgs<SSOTMEPayload>> PublicUserGetAllFileTypesReceived;
        protected virtual void OnPublicUserGetAllFileTypesReceived(SSOTMEPayload payload)
        {
            this.LogMessage(payload, "Get All File Types - ");
            var plea = new PayloadEventArgs<SSOTMEPayload>(payload);
            this.Invoke(this.PublicUserGetAllFileTypesReceived, plea);
        }
        
        /// <summary>
        /// Responds to: Ping - 
        /// </summary>
        public event System.EventHandler<PayloadEventArgs<SSOTMEPayload>> AccountHolderPingReceived;
        protected virtual void OnAccountHolderPingReceived(SSOTMEPayload payload)
        {
            this.LogMessage(payload, "Ping - ");
            var plea = new PayloadEventArgs<SSOTMEPayload>(payload);
            this.Invoke(this.AccountHolderPingReceived, plea);
        }
        
        /// <summary>
        /// Responds to: Login - 
        /// </summary>
        public event System.EventHandler<PayloadEventArgs<SSOTMEPayload>> AccountHolderLoginReceived;
        protected virtual void OnAccountHolderLoginReceived(SSOTMEPayload payload)
        {
            this.LogMessage(payload, "Login - ");
            var plea = new PayloadEventArgs<SSOTMEPayload>(payload);
            this.Invoke(this.AccountHolderLoginReceived, plea);
        }
        
        /// <summary>
        /// Responds to: Logout - 
        /// </summary>
        public event System.EventHandler<PayloadEventArgs<SSOTMEPayload>> AccountHolderLogoutReceived;
        protected virtual void OnAccountHolderLogoutReceived(SSOTMEPayload payload)
        {
            this.LogMessage(payload, "Logout - ");
            var plea = new PayloadEventArgs<SSOTMEPayload>(payload);
            this.Invoke(this.AccountHolderLogoutReceived, plea);
        }
        
        /// <summary>
        /// Responds to: Add Transpiler - 
        /// </summary>
        public event System.EventHandler<PayloadEventArgs<SSOTMEPayload>> AccountHolderAddTranspilerReceived;
        protected virtual void OnAccountHolderAddTranspilerReceived(SSOTMEPayload payload)
        {
            this.LogMessage(payload, "Add Transpiler - ");
            var plea = new PayloadEventArgs<SSOTMEPayload>(payload);
            this.Invoke(this.AccountHolderAddTranspilerReceived, plea);
        }
        
        /// <summary>
        /// Responds to: Delete Transpiler - 
        /// </summary>
        public event System.EventHandler<PayloadEventArgs<SSOTMEPayload>> AccountHolderDeleteTranspilerReceived;
        protected virtual void OnAccountHolderDeleteTranspilerReceived(SSOTMEPayload payload)
        {
            this.LogMessage(payload, "Delete Transpiler - ");
            var plea = new PayloadEventArgs<SSOTMEPayload>(payload);
            this.Invoke(this.AccountHolderDeleteTranspilerReceived, plea);
        }
        
        /// <summary>
        /// Responds to: Update Transpiler - 
        /// </summary>
        public event System.EventHandler<PayloadEventArgs<SSOTMEPayload>> AccountHolderUpdateTranspilerReceived;
        protected virtual void OnAccountHolderUpdateTranspilerReceived(SSOTMEPayload payload)
        {
            this.LogMessage(payload, "Update Transpiler - ");
            var plea = new PayloadEventArgs<SSOTMEPayload>(payload);
            this.Invoke(this.AccountHolderUpdateTranspilerReceived, plea);
        }
        
        /// <summary>
        /// Responds to: Get Transpiler - 
        /// </summary>
        public event System.EventHandler<PayloadEventArgs<SSOTMEPayload>> AccountHolderGetTranspilerReceived;
        protected virtual void OnAccountHolderGetTranspilerReceived(SSOTMEPayload payload)
        {
            this.LogMessage(payload, "Get Transpiler - ");
            var plea = new PayloadEventArgs<SSOTMEPayload>(payload);
            this.Invoke(this.AccountHolderGetTranspilerReceived, plea);
        }
        
        /// <summary>
        /// Responds to: Add Transpiler Version - 
        /// </summary>
        public event System.EventHandler<PayloadEventArgs<SSOTMEPayload>> AccountHolderAddTranspilerVersionReceived;
        protected virtual void OnAccountHolderAddTranspilerVersionReceived(SSOTMEPayload payload)
        {
            this.LogMessage(payload, "Add Transpiler Version - ");
            var plea = new PayloadEventArgs<SSOTMEPayload>(payload);
            this.Invoke(this.AccountHolderAddTranspilerVersionReceived, plea);
        }
        
        /// <summary>
        /// Responds to: Delete Transpiler Version - 
        /// </summary>
        public event System.EventHandler<PayloadEventArgs<SSOTMEPayload>> AccountHolderDeleteTranspilerVersionReceived;
        protected virtual void OnAccountHolderDeleteTranspilerVersionReceived(SSOTMEPayload payload)
        {
            this.LogMessage(payload, "Delete Transpiler Version - ");
            var plea = new PayloadEventArgs<SSOTMEPayload>(payload);
            this.Invoke(this.AccountHolderDeleteTranspilerVersionReceived, plea);
        }
        
        /// <summary>
        /// Responds to: Update Transpiler Version - 
        /// </summary>
        public event System.EventHandler<PayloadEventArgs<SSOTMEPayload>> AccountHolderUpdateTranspilerVersionReceived;
        protected virtual void OnAccountHolderUpdateTranspilerVersionReceived(SSOTMEPayload payload)
        {
            this.LogMessage(payload, "Update Transpiler Version - ");
            var plea = new PayloadEventArgs<SSOTMEPayload>(payload);
            this.Invoke(this.AccountHolderUpdateTranspilerVersionReceived, plea);
        }
        
        /// <summary>
        /// Responds to: Get Transpiler List - 
        /// </summary>
        public event System.EventHandler<PayloadEventArgs<SSOTMEPayload>> AccountHolderGetTranspilerListReceived;
        protected virtual void OnAccountHolderGetTranspilerListReceived(SSOTMEPayload payload)
        {
            this.LogMessage(payload, "Get Transpiler List - ");
            var plea = new PayloadEventArgs<SSOTMEPayload>(payload);
            this.Invoke(this.AccountHolderGetTranspilerListReceived, plea);
        }
        
        /// <summary>
        /// Responds to: Create Project - 
        /// </summary>
        public event System.EventHandler<PayloadEventArgs<SSOTMEPayload>> AccountHolderCreateProjectReceived;
        protected virtual void OnAccountHolderCreateProjectReceived(SSOTMEPayload payload)
        {
            this.LogMessage(payload, "Create Project - ");
            var plea = new PayloadEventArgs<SSOTMEPayload>(payload);
            this.Invoke(this.AccountHolderCreateProjectReceived, plea);
        }
        
        /// <summary>
        /// Responds to: Request Transpile - 
        /// </summary>
        public event System.EventHandler<PayloadEventArgs<SSOTMEPayload>> AccountHolderRequestTranspileReceived;
        protected virtual void OnAccountHolderRequestTranspileReceived(SSOTMEPayload payload)
        {
            this.LogMessage(payload, "Request Transpile - ");
            var plea = new PayloadEventArgs<SSOTMEPayload>(payload);
            this.Invoke(this.AccountHolderRequestTranspileReceived, plea);
        }
        
        /// <summary>
        /// Responds to: Request Transpiler Host - 
        /// </summary>
        public event System.EventHandler<PayloadEventArgs<SSOTMEPayload>> AccountHolderRequestTranspilerHostReceived;
        protected virtual void OnAccountHolderRequestTranspilerHostReceived(SSOTMEPayload payload)
        {
            this.LogMessage(payload, "Request Transpiler Host - ");
            var plea = new PayloadEventArgs<SSOTMEPayload>(payload);
            this.Invoke(this.AccountHolderRequestTranspilerHostReceived, plea);
        }
        
        /// <summary>
        /// Responds to: Request Stop Instance - 
        /// </summary>
        public event System.EventHandler<PayloadEventArgs<SSOTMEPayload>> AccountHolderRequestStopInstanceReceived;
        protected virtual void OnAccountHolderRequestStopInstanceReceived(SSOTMEPayload payload)
        {
            this.LogMessage(payload, "Request Stop Instance - ");
            var plea = new PayloadEventArgs<SSOTMEPayload>(payload);
            this.Invoke(this.AccountHolderRequestStopInstanceReceived, plea);
        }
        
        /// <summary>
        /// Responds to: Request Stop Host - 
        /// </summary>
        public event System.EventHandler<PayloadEventArgs<SSOTMEPayload>> AccountHolderRequestStopHostReceived;
        protected virtual void OnAccountHolderRequestStopHostReceived(SSOTMEPayload payload)
        {
            this.LogMessage(payload, "Request Stop Host - ");
            var plea = new PayloadEventArgs<SSOTMEPayload>(payload);
            this.Invoke(this.AccountHolderRequestStopHostReceived, plea);
        }
        
        /// <summary>
        /// Responds to: Command Line Transpile - 
        /// </summary>
        public event System.EventHandler<PayloadEventArgs<SSOTMEPayload>> AccountHolderCommandLineTranspileReceived;
        protected virtual void OnAccountHolderCommandLineTranspileReceived(SSOTMEPayload payload)
        {
            this.LogMessage(payload, "Command Line Transpile - ");
            var plea = new PayloadEventArgs<SSOTMEPayload>(payload);
            this.Invoke(this.AccountHolderCommandLineTranspileReceived, plea);
        }
        
        /// <summary>
        /// Responds to: Ping - 
        /// </summary>
        public event System.EventHandler<PayloadEventArgs<SSOTMEPayload>> TranspilerHostPingReceived;
        protected virtual void OnTranspilerHostPingReceived(SSOTMEPayload payload)
        {
            this.LogMessage(payload, "Ping - ");
            var plea = new PayloadEventArgs<SSOTMEPayload>(payload);
            this.Invoke(this.TranspilerHostPingReceived, plea);
        }
        
        /// <summary>
        /// Responds to: Offline - 
        /// </summary>
        public event System.EventHandler<PayloadEventArgs<SSOTMEPayload>> TranspilerHostOfflineReceived;
        protected virtual void OnTranspilerHostOfflineReceived(SSOTMEPayload payload)
        {
            this.LogMessage(payload, "Offline - ");
            var plea = new PayloadEventArgs<SSOTMEPayload>(payload);
            this.Invoke(this.TranspilerHostOfflineReceived, plea);
        }
        
        /// <summary>
        /// Responds to: Instance Started - 
        /// </summary>
        public event System.EventHandler<PayloadEventArgs<SSOTMEPayload>> TranspilerHostInstanceStartedReceived;
        protected virtual void OnTranspilerHostInstanceStartedReceived(SSOTMEPayload payload)
        {
            this.LogMessage(payload, "Instance Started - ");
            var plea = new PayloadEventArgs<SSOTMEPayload>(payload);
            this.Invoke(this.TranspilerHostInstanceStartedReceived, plea);
        }
        
        /// <summary>
        /// Responds to: Instance Stopped - 
        /// </summary>
        public event System.EventHandler<PayloadEventArgs<SSOTMEPayload>> TranspilerHostInstanceStoppedReceived;
        protected virtual void OnTranspilerHostInstanceStoppedReceived(SSOTMEPayload payload)
        {
            this.LogMessage(payload, "Instance Stopped - ");
            var plea = new PayloadEventArgs<SSOTMEPayload>(payload);
            this.Invoke(this.TranspilerHostInstanceStoppedReceived, plea);
        }
        
        /// <summary>
        /// Responds to: Ping - 
        /// </summary>
        public event System.EventHandler<PayloadEventArgs<SSOTMEPayload>> SSOTMEAdminPingReceived;
        protected virtual void OnSSOTMEAdminPingReceived(SSOTMEPayload payload)
        {
            this.LogMessage(payload, "Ping - ");
            var plea = new PayloadEventArgs<SSOTMEPayload>(payload);
            this.Invoke(this.SSOTMEAdminPingReceived, plea);
        }
        
        /// <summary>
        /// Responds to: Add Platform Category - 
        /// </summary>
        public event System.EventHandler<PayloadEventArgs<SSOTMEPayload>> SSOTMEAdminAddPlatformCategoryReceived;
        protected virtual void OnSSOTMEAdminAddPlatformCategoryReceived(SSOTMEPayload payload)
        {
            this.LogMessage(payload, "Add Platform Category - ");
            var plea = new PayloadEventArgs<SSOTMEPayload>(payload);
            this.Invoke(this.SSOTMEAdminAddPlatformCategoryReceived, plea);
        }
        
        /// <summary>
        /// Responds to: Update Platform Category - 
        /// </summary>
        public event System.EventHandler<PayloadEventArgs<SSOTMEPayload>> SSOTMEAdminUpdatePlatformCategoryReceived;
        protected virtual void OnSSOTMEAdminUpdatePlatformCategoryReceived(SSOTMEPayload payload)
        {
            this.LogMessage(payload, "Update Platform Category - ");
            var plea = new PayloadEventArgs<SSOTMEPayload>(payload);
            this.Invoke(this.SSOTMEAdminUpdatePlatformCategoryReceived, plea);
        }
        
        /// <summary>
        /// Responds to: Add Transpiler Platform - 
        /// </summary>
        public event System.EventHandler<PayloadEventArgs<SSOTMEPayload>> SSOTMEAdminAddTranspilerPlatformReceived;
        protected virtual void OnSSOTMEAdminAddTranspilerPlatformReceived(SSOTMEPayload payload)
        {
            this.LogMessage(payload, "Add Transpiler Platform - ");
            var plea = new PayloadEventArgs<SSOTMEPayload>(payload);
            this.Invoke(this.SSOTMEAdminAddTranspilerPlatformReceived, plea);
        }
        
        /// <summary>
        /// Responds to: Update Transpiler Platform - 
        /// </summary>
        public event System.EventHandler<PayloadEventArgs<SSOTMEPayload>> SSOTMEAdminUpdateTranspilerPlatformReceived;
        protected virtual void OnSSOTMEAdminUpdateTranspilerPlatformReceived(SSOTMEPayload payload)
        {
            this.LogMessage(payload, "Update Transpiler Platform - ");
            var plea = new PayloadEventArgs<SSOTMEPayload>(payload);
            this.Invoke(this.SSOTMEAdminUpdateTranspilerPlatformReceived, plea);
        }
        
        /// <summary>
        /// Get Instances - 
        /// </summary>
        public void SSOTMECoordinatorGetInstances(DMProxy proxy) 
        {
            this.SSOTMECoordinatorGetInstances(this.CreatePayload(), proxy);
        }

        /// <summary>
        /// Get Instances - 
        /// </summary>
        public void SSOTMECoordinatorGetInstances(System.String content, DMProxy proxy) 
        {
            var payload = this.CreatePayload();
            payload.Content = content;
            this.SSOTMECoordinatorGetInstances(payload, proxy);
        }

        /// <summary>
        /// Get Instances - 
        /// </summary>
        public void SSOTMECoordinatorGetInstances(SSOTMEPayload payload, DMProxy proxy)
        {
            payload.IsDirectMessage = true;
            this.SendMessage(payload, "Get Instances - ",
            "ssotmecoordinatormic", "transpilerhost.general.ssotmecoordinator.getinstances", proxy.RoutingKey);
        }


        
        /// <summary>
        /// Transpile Requested - 
        /// </summary>
        public void SSOTMECoordinatorTranspileRequested(DMProxy proxy) 
        {
            this.SSOTMECoordinatorTranspileRequested(this.CreatePayload(), proxy);
        }

        /// <summary>
        /// Transpile Requested - 
        /// </summary>
        public void SSOTMECoordinatorTranspileRequested(System.String content, DMProxy proxy) 
        {
            var payload = this.CreatePayload();
            payload.Content = content;
            this.SSOTMECoordinatorTranspileRequested(payload, proxy);
        }

        /// <summary>
        /// Transpile Requested - 
        /// </summary>
        public void SSOTMECoordinatorTranspileRequested(SSOTMEPayload payload, DMProxy proxy)
        {
            payload.IsDirectMessage = true;
            this.SendMessage(payload, "Transpile Requested - ",
            "ssotmecoordinatormic", "transpilerhost.general.ssotmecoordinator.transpilerequested", proxy.RoutingKey);
        }


        
        /// <summary>
        /// Transpiler Online - 
        /// </summary>
        public void SSOTMECoordinatorTranspilerOnline() 
        {
            this.SSOTMECoordinatorTranspilerOnline(this.CreatePayload());
        }

        /// <summary>
        /// Transpiler Online - 
        /// </summary>
        public void SSOTMECoordinatorTranspilerOnline(System.String content) 
        {
            var payload = this.CreatePayload();
            payload.Content = content;
            this.SSOTMECoordinatorTranspilerOnline(payload);
        }

        /// <summary>
        /// Transpiler Online - 
        /// </summary>
        public void SSOTMECoordinatorTranspilerOnline(SSOTMEPayload payload)
        {
            
            this.SendMessage(payload, "Transpiler Online - ",
            "ssotmecoordinatormic", "publicuser.general.ssotmecoordinator.transpileronline");
        }


        
        /// <summary>
        /// Transpiler Offline - 
        /// </summary>
        public void SSOTMECoordinatorTranspilerOffline() 
        {
            this.SSOTMECoordinatorTranspilerOffline(this.CreatePayload());
        }

        /// <summary>
        /// Transpiler Offline - 
        /// </summary>
        public void SSOTMECoordinatorTranspilerOffline(System.String content) 
        {
            var payload = this.CreatePayload();
            payload.Content = content;
            this.SSOTMECoordinatorTranspilerOffline(payload);
        }

        /// <summary>
        /// Transpiler Offline - 
        /// </summary>
        public void SSOTMECoordinatorTranspilerOffline(SSOTMEPayload payload)
        {
            
            this.SendMessage(payload, "Transpiler Offline - ",
            "ssotmecoordinatormic", "publicuser.general.ssotmecoordinator.transpileroffline");
        }


        
        /// <summary>
        /// Stop Instance - 
        /// </summary>
        public void SSOTMECoordinatorStopInstance(DMProxy proxy) 
        {
            this.SSOTMECoordinatorStopInstance(this.CreatePayload(), proxy);
        }

        /// <summary>
        /// Stop Instance - 
        /// </summary>
        public void SSOTMECoordinatorStopInstance(System.String content, DMProxy proxy) 
        {
            var payload = this.CreatePayload();
            payload.Content = content;
            this.SSOTMECoordinatorStopInstance(payload, proxy);
        }

        /// <summary>
        /// Stop Instance - 
        /// </summary>
        public void SSOTMECoordinatorStopInstance(SSOTMEPayload payload, DMProxy proxy)
        {
            payload.IsDirectMessage = true;
            this.SendMessage(payload, "Stop Instance - ",
            "ssotmecoordinatormic", "transpilerhost.general.ssotmecoordinator.stopinstance", proxy.RoutingKey);
        }


        
        /// <summary>
        /// Stop Host - 
        /// </summary>
        public void SSOTMECoordinatorStopHost(DMProxy proxy) 
        {
            this.SSOTMECoordinatorStopHost(this.CreatePayload(), proxy);
        }

        /// <summary>
        /// Stop Host - 
        /// </summary>
        public void SSOTMECoordinatorStopHost(System.String content, DMProxy proxy) 
        {
            var payload = this.CreatePayload();
            payload.Content = content;
            this.SSOTMECoordinatorStopHost(payload, proxy);
        }

        /// <summary>
        /// Stop Host - 
        /// </summary>
        public void SSOTMECoordinatorStopHost(SSOTMEPayload payload, DMProxy proxy)
        {
            payload.IsDirectMessage = true;
            this.SendMessage(payload, "Stop Host - ",
            "ssotmecoordinatormic", "transpilerhost.general.ssotmecoordinator.stophost", proxy.RoutingKey);
        }


        
            // And can also say/hear everything which : SSOTMEAdmin hears.
            
        /// <summary>
        /// Ping - 
        /// </summary>
        public void SSOTMEAdminPing(DMProxy proxy) 
        {
            this.SSOTMEAdminPing(this.CreatePayload(), proxy);
        }

        /// <summary>
        /// Ping - 
        /// </summary>
        public void SSOTMEAdminPing(System.String content, DMProxy proxy) 
        {
            var payload = this.CreatePayload();
            payload.Content = content;
            this.SSOTMEAdminPing(payload, proxy);
        }

        /// <summary>
        /// Ping - 
        /// </summary>
        public void SSOTMEAdminPing(SSOTMEPayload payload, DMProxy proxy)
        {
            payload.IsDirectMessage = true;
            this.SendMessage(payload, "Ping - ",
            "ssotmeadminmic", "ssotmecoordinator.general.ssotmeadmin.ping", proxy.RoutingKey);
        }


        
        /// <summary>
        /// Add Platform Category - 
        /// </summary>
        public void SSOTMEAdminAddPlatformCategory(DMProxy proxy) 
        {
            this.SSOTMEAdminAddPlatformCategory(this.CreatePayload(), proxy);
        }

        /// <summary>
        /// Add Platform Category - 
        /// </summary>
        public void SSOTMEAdminAddPlatformCategory(System.String content, DMProxy proxy) 
        {
            var payload = this.CreatePayload();
            payload.Content = content;
            this.SSOTMEAdminAddPlatformCategory(payload, proxy);
        }

        /// <summary>
        /// Add Platform Category - 
        /// </summary>
        public void SSOTMEAdminAddPlatformCategory(SSOTMEPayload payload, DMProxy proxy)
        {
            payload.IsDirectMessage = true;
            this.SendMessage(payload, "Add Platform Category - ",
            "ssotmeadminmic", "ssotmecoordinator.general.ssotmeadmin.addplatformcategory", proxy.RoutingKey);
        }


        
        /// <summary>
        /// Update Platform Category - 
        /// </summary>
        public void SSOTMEAdminUpdatePlatformCategory(DMProxy proxy) 
        {
            this.SSOTMEAdminUpdatePlatformCategory(this.CreatePayload(), proxy);
        }

        /// <summary>
        /// Update Platform Category - 
        /// </summary>
        public void SSOTMEAdminUpdatePlatformCategory(System.String content, DMProxy proxy) 
        {
            var payload = this.CreatePayload();
            payload.Content = content;
            this.SSOTMEAdminUpdatePlatformCategory(payload, proxy);
        }

        /// <summary>
        /// Update Platform Category - 
        /// </summary>
        public void SSOTMEAdminUpdatePlatformCategory(SSOTMEPayload payload, DMProxy proxy)
        {
            payload.IsDirectMessage = true;
            this.SendMessage(payload, "Update Platform Category - ",
            "ssotmeadminmic", "ssotmecoordinator.general.ssotmeadmin.updateplatformcategory", proxy.RoutingKey);
        }


        
        /// <summary>
        /// Add Transpiler Platform - 
        /// </summary>
        public void SSOTMEAdminAddTranspilerPlatform(DMProxy proxy) 
        {
            this.SSOTMEAdminAddTranspilerPlatform(this.CreatePayload(), proxy);
        }

        /// <summary>
        /// Add Transpiler Platform - 
        /// </summary>
        public void SSOTMEAdminAddTranspilerPlatform(System.String content, DMProxy proxy) 
        {
            var payload = this.CreatePayload();
            payload.Content = content;
            this.SSOTMEAdminAddTranspilerPlatform(payload, proxy);
        }

        /// <summary>
        /// Add Transpiler Platform - 
        /// </summary>
        public void SSOTMEAdminAddTranspilerPlatform(SSOTMEPayload payload, DMProxy proxy)
        {
            payload.IsDirectMessage = true;
            this.SendMessage(payload, "Add Transpiler Platform - ",
            "ssotmeadminmic", "ssotmecoordinator.general.ssotmeadmin.addtranspilerplatform", proxy.RoutingKey);
        }


        
        /// <summary>
        /// Update Transpiler Platform - 
        /// </summary>
        public void SSOTMEAdminUpdateTranspilerPlatform(DMProxy proxy) 
        {
            this.SSOTMEAdminUpdateTranspilerPlatform(this.CreatePayload(), proxy);
        }

        /// <summary>
        /// Update Transpiler Platform - 
        /// </summary>
        public void SSOTMEAdminUpdateTranspilerPlatform(System.String content, DMProxy proxy) 
        {
            var payload = this.CreatePayload();
            payload.Content = content;
            this.SSOTMEAdminUpdateTranspilerPlatform(payload, proxy);
        }

        /// <summary>
        /// Update Transpiler Platform - 
        /// </summary>
        public void SSOTMEAdminUpdateTranspilerPlatform(SSOTMEPayload payload, DMProxy proxy)
        {
            payload.IsDirectMessage = true;
            this.SendMessage(payload, "Update Transpiler Platform - ",
            "ssotmeadminmic", "ssotmecoordinator.general.ssotmeadmin.updatetranspilerplatform", proxy.RoutingKey);
        }


        
            // And can also say/hear everything which : TranspilerHost hears.
            
        /// <summary>
        /// Responds to: Get Instances - 
        /// </summary>
        public event System.EventHandler<PayloadEventArgs<SSOTMEPayload>> SSOTMECoordinatorGetInstancesReceived;
        protected virtual void OnSSOTMECoordinatorGetInstancesReceived(SSOTMEPayload payload)
        {
            this.LogMessage(payload, "Get Instances - ");
            var plea = new PayloadEventArgs<SSOTMEPayload>(payload);
            this.Invoke(this.SSOTMECoordinatorGetInstancesReceived, plea);
        }
        
        /// <summary>
        /// Responds to: Transpile Requested - 
        /// </summary>
        public event System.EventHandler<PayloadEventArgs<SSOTMEPayload>> SSOTMECoordinatorTranspileRequestedReceived;
        protected virtual void OnSSOTMECoordinatorTranspileRequestedReceived(SSOTMEPayload payload)
        {
            this.LogMessage(payload, "Transpile Requested - ");
            var plea = new PayloadEventArgs<SSOTMEPayload>(payload);
            this.Invoke(this.SSOTMECoordinatorTranspileRequestedReceived, plea);
        }
        
        /// <summary>
        /// Responds to: Stop Instance - 
        /// </summary>
        public event System.EventHandler<PayloadEventArgs<SSOTMEPayload>> SSOTMECoordinatorStopInstanceReceived;
        protected virtual void OnSSOTMECoordinatorStopInstanceReceived(SSOTMEPayload payload)
        {
            this.LogMessage(payload, "Stop Instance - ");
            var plea = new PayloadEventArgs<SSOTMEPayload>(payload);
            this.Invoke(this.SSOTMECoordinatorStopInstanceReceived, plea);
        }
        
        /// <summary>
        /// Responds to: Stop Host - 
        /// </summary>
        public event System.EventHandler<PayloadEventArgs<SSOTMEPayload>> SSOTMECoordinatorStopHostReceived;
        protected virtual void OnSSOTMECoordinatorStopHostReceived(SSOTMEPayload payload)
        {
            this.LogMessage(payload, "Stop Host - ");
            var plea = new PayloadEventArgs<SSOTMEPayload>(payload);
            this.Invoke(this.SSOTMECoordinatorStopHostReceived, plea);
        }
        
        /// <summary>
        /// Ping - 
        /// </summary>
        public void TranspilerHostPing() 
        {
            this.TranspilerHostPing(this.CreatePayload());
        }

        /// <summary>
        /// Ping - 
        /// </summary>
        public void TranspilerHostPing(System.String content) 
        {
            var payload = this.CreatePayload();
            payload.Content = content;
            this.TranspilerHostPing(payload);
        }

        /// <summary>
        /// Ping - 
        /// </summary>
        public void TranspilerHostPing(SSOTMEPayload payload)
        {
            
            this.SendMessage(payload, "Ping - ",
            "transpilerhostmic", "ssotmecoordinator.general.transpilerhost.ping");
        }


        
        /// <summary>
        /// Offline - 
        /// </summary>
        public void TranspilerHostOffline(DMProxy proxy) 
        {
            this.TranspilerHostOffline(this.CreatePayload(), proxy);
        }

        /// <summary>
        /// Offline - 
        /// </summary>
        public void TranspilerHostOffline(System.String content, DMProxy proxy) 
        {
            var payload = this.CreatePayload();
            payload.Content = content;
            this.TranspilerHostOffline(payload, proxy);
        }

        /// <summary>
        /// Offline - 
        /// </summary>
        public void TranspilerHostOffline(SSOTMEPayload payload, DMProxy proxy)
        {
            payload.IsDirectMessage = true;
            this.SendMessage(payload, "Offline - ",
            "transpilerhostmic", "ssotmecoordinator.general.transpilerhost.offline", proxy.RoutingKey);
        }


        
        /// <summary>
        /// Instance Started - 
        /// </summary>
        public void TranspilerHostInstanceStarted(DMProxy proxy) 
        {
            this.TranspilerHostInstanceStarted(this.CreatePayload(), proxy);
        }

        /// <summary>
        /// Instance Started - 
        /// </summary>
        public void TranspilerHostInstanceStarted(System.String content, DMProxy proxy) 
        {
            var payload = this.CreatePayload();
            payload.Content = content;
            this.TranspilerHostInstanceStarted(payload, proxy);
        }

        /// <summary>
        /// Instance Started - 
        /// </summary>
        public void TranspilerHostInstanceStarted(SSOTMEPayload payload, DMProxy proxy)
        {
            payload.IsDirectMessage = true;
            this.SendMessage(payload, "Instance Started - ",
            "transpilerhostmic", "ssotmecoordinator.general.transpilerhost.instancestarted", proxy.RoutingKey);
        }


        
        /// <summary>
        /// Instance Stopped - 
        /// </summary>
        public void TranspilerHostInstanceStopped(DMProxy proxy) 
        {
            this.TranspilerHostInstanceStopped(this.CreatePayload(), proxy);
        }

        /// <summary>
        /// Instance Stopped - 
        /// </summary>
        public void TranspilerHostInstanceStopped(System.String content, DMProxy proxy) 
        {
            var payload = this.CreatePayload();
            payload.Content = content;
            this.TranspilerHostInstanceStopped(payload, proxy);
        }

        /// <summary>
        /// Instance Stopped - 
        /// </summary>
        public void TranspilerHostInstanceStopped(SSOTMEPayload payload, DMProxy proxy)
        {
            payload.IsDirectMessage = true;
            this.SendMessage(payload, "Instance Stopped - ",
            "transpilerhostmic", "ssotmecoordinator.general.transpilerhost.instancestopped", proxy.RoutingKey);
        }


        
            // And can also say/hear everything which : AccountHolder hears.
            
        /// <summary>
        /// Ping - 
        /// </summary>
        public void AccountHolderPing() 
        {
            this.AccountHolderPing(this.CreatePayload());
        }

        /// <summary>
        /// Ping - 
        /// </summary>
        public void AccountHolderPing(System.String content) 
        {
            var payload = this.CreatePayload();
            payload.Content = content;
            this.AccountHolderPing(payload);
        }

        /// <summary>
        /// Ping - 
        /// </summary>
        public void AccountHolderPing(SSOTMEPayload payload)
        {
            
            this.SendMessage(payload, "Ping - ",
            "accountholdermic", "ssotmecoordinator.general.accountholder.ping");
        }


        
        /// <summary>
        /// Login - 
        /// </summary>
        public void AccountHolderLogin(DMProxy proxy) 
        {
            this.AccountHolderLogin(this.CreatePayload(), proxy);
        }

        /// <summary>
        /// Login - 
        /// </summary>
        public void AccountHolderLogin(System.String content, DMProxy proxy) 
        {
            var payload = this.CreatePayload();
            payload.Content = content;
            this.AccountHolderLogin(payload, proxy);
        }

        /// <summary>
        /// Login - 
        /// </summary>
        public void AccountHolderLogin(SSOTMEPayload payload, DMProxy proxy)
        {
            payload.IsDirectMessage = true;
            this.SendMessage(payload, "Login - ",
            "accountholdermic", "ssotmecoordinator.general.accountholder.login", proxy.RoutingKey);
        }


        
        /// <summary>
        /// Logout - 
        /// </summary>
        public void AccountHolderLogout(DMProxy proxy) 
        {
            this.AccountHolderLogout(this.CreatePayload(), proxy);
        }

        /// <summary>
        /// Logout - 
        /// </summary>
        public void AccountHolderLogout(System.String content, DMProxy proxy) 
        {
            var payload = this.CreatePayload();
            payload.Content = content;
            this.AccountHolderLogout(payload, proxy);
        }

        /// <summary>
        /// Logout - 
        /// </summary>
        public void AccountHolderLogout(SSOTMEPayload payload, DMProxy proxy)
        {
            payload.IsDirectMessage = true;
            this.SendMessage(payload, "Logout - ",
            "accountholdermic", "ssotmecoordinator.general.accountholder.logout", proxy.RoutingKey);
        }


        
        /// <summary>
        /// Add Transpiler - 
        /// </summary>
        public void AccountHolderAddTranspiler(DMProxy proxy) 
        {
            this.AccountHolderAddTranspiler(this.CreatePayload(), proxy);
        }

        /// <summary>
        /// Add Transpiler - 
        /// </summary>
        public void AccountHolderAddTranspiler(System.String content, DMProxy proxy) 
        {
            var payload = this.CreatePayload();
            payload.Content = content;
            this.AccountHolderAddTranspiler(payload, proxy);
        }

        /// <summary>
        /// Add Transpiler - 
        /// </summary>
        public void AccountHolderAddTranspiler(SSOTMEPayload payload, DMProxy proxy)
        {
            payload.IsDirectMessage = true;
            this.SendMessage(payload, "Add Transpiler - ",
            "accountholdermic", "ssotmecoordinator.general.accountholder.addtranspiler", proxy.RoutingKey);
        }


        
        /// <summary>
        /// Delete Transpiler - 
        /// </summary>
        public void AccountHolderDeleteTranspiler(DMProxy proxy) 
        {
            this.AccountHolderDeleteTranspiler(this.CreatePayload(), proxy);
        }

        /// <summary>
        /// Delete Transpiler - 
        /// </summary>
        public void AccountHolderDeleteTranspiler(System.String content, DMProxy proxy) 
        {
            var payload = this.CreatePayload();
            payload.Content = content;
            this.AccountHolderDeleteTranspiler(payload, proxy);
        }

        /// <summary>
        /// Delete Transpiler - 
        /// </summary>
        public void AccountHolderDeleteTranspiler(SSOTMEPayload payload, DMProxy proxy)
        {
            payload.IsDirectMessage = true;
            this.SendMessage(payload, "Delete Transpiler - ",
            "accountholdermic", "ssotmecoordinator.general.accountholder.deletetranspiler", proxy.RoutingKey);
        }


        
        /// <summary>
        /// Update Transpiler - 
        /// </summary>
        public void AccountHolderUpdateTranspiler(DMProxy proxy) 
        {
            this.AccountHolderUpdateTranspiler(this.CreatePayload(), proxy);
        }

        /// <summary>
        /// Update Transpiler - 
        /// </summary>
        public void AccountHolderUpdateTranspiler(System.String content, DMProxy proxy) 
        {
            var payload = this.CreatePayload();
            payload.Content = content;
            this.AccountHolderUpdateTranspiler(payload, proxy);
        }

        /// <summary>
        /// Update Transpiler - 
        /// </summary>
        public void AccountHolderUpdateTranspiler(SSOTMEPayload payload, DMProxy proxy)
        {
            payload.IsDirectMessage = true;
            this.SendMessage(payload, "Update Transpiler - ",
            "accountholdermic", "ssotmecoordinator.general.accountholder.updatetranspiler", proxy.RoutingKey);
        }


        
        /// <summary>
        /// Get Transpiler - 
        /// </summary>
        public void AccountHolderGetTranspiler(DMProxy proxy) 
        {
            this.AccountHolderGetTranspiler(this.CreatePayload(), proxy);
        }

        /// <summary>
        /// Get Transpiler - 
        /// </summary>
        public void AccountHolderGetTranspiler(System.String content, DMProxy proxy) 
        {
            var payload = this.CreatePayload();
            payload.Content = content;
            this.AccountHolderGetTranspiler(payload, proxy);
        }

        /// <summary>
        /// Get Transpiler - 
        /// </summary>
        public void AccountHolderGetTranspiler(SSOTMEPayload payload, DMProxy proxy)
        {
            payload.IsDirectMessage = true;
            this.SendMessage(payload, "Get Transpiler - ",
            "accountholdermic", "ssotmecoordinator.general.accountholder.gettranspiler", proxy.RoutingKey);
        }


        
        /// <summary>
        /// Add Transpiler Version - 
        /// </summary>
        public void AccountHolderAddTranspilerVersion(DMProxy proxy) 
        {
            this.AccountHolderAddTranspilerVersion(this.CreatePayload(), proxy);
        }

        /// <summary>
        /// Add Transpiler Version - 
        /// </summary>
        public void AccountHolderAddTranspilerVersion(System.String content, DMProxy proxy) 
        {
            var payload = this.CreatePayload();
            payload.Content = content;
            this.AccountHolderAddTranspilerVersion(payload, proxy);
        }

        /// <summary>
        /// Add Transpiler Version - 
        /// </summary>
        public void AccountHolderAddTranspilerVersion(SSOTMEPayload payload, DMProxy proxy)
        {
            payload.IsDirectMessage = true;
            this.SendMessage(payload, "Add Transpiler Version - ",
            "accountholdermic", "ssotmecoordinator.general.accountholder.addtranspilerversion", proxy.RoutingKey);
        }


        
        /// <summary>
        /// Delete Transpiler Version - 
        /// </summary>
        public void AccountHolderDeleteTranspilerVersion(DMProxy proxy) 
        {
            this.AccountHolderDeleteTranspilerVersion(this.CreatePayload(), proxy);
        }

        /// <summary>
        /// Delete Transpiler Version - 
        /// </summary>
        public void AccountHolderDeleteTranspilerVersion(System.String content, DMProxy proxy) 
        {
            var payload = this.CreatePayload();
            payload.Content = content;
            this.AccountHolderDeleteTranspilerVersion(payload, proxy);
        }

        /// <summary>
        /// Delete Transpiler Version - 
        /// </summary>
        public void AccountHolderDeleteTranspilerVersion(SSOTMEPayload payload, DMProxy proxy)
        {
            payload.IsDirectMessage = true;
            this.SendMessage(payload, "Delete Transpiler Version - ",
            "accountholdermic", "ssotmecoordinator.general.accountholder.deletetranspilerversion", proxy.RoutingKey);
        }


        
        /// <summary>
        /// Update Transpiler Version - 
        /// </summary>
        public void AccountHolderUpdateTranspilerVersion(DMProxy proxy) 
        {
            this.AccountHolderUpdateTranspilerVersion(this.CreatePayload(), proxy);
        }

        /// <summary>
        /// Update Transpiler Version - 
        /// </summary>
        public void AccountHolderUpdateTranspilerVersion(System.String content, DMProxy proxy) 
        {
            var payload = this.CreatePayload();
            payload.Content = content;
            this.AccountHolderUpdateTranspilerVersion(payload, proxy);
        }

        /// <summary>
        /// Update Transpiler Version - 
        /// </summary>
        public void AccountHolderUpdateTranspilerVersion(SSOTMEPayload payload, DMProxy proxy)
        {
            payload.IsDirectMessage = true;
            this.SendMessage(payload, "Update Transpiler Version - ",
            "accountholdermic", "ssotmecoordinator.general.accountholder.updatetranspilerversion", proxy.RoutingKey);
        }


        
        /// <summary>
        /// Get Transpiler List - 
        /// </summary>
        public void AccountHolderGetTranspilerList(DMProxy proxy) 
        {
            this.AccountHolderGetTranspilerList(this.CreatePayload(), proxy);
        }

        /// <summary>
        /// Get Transpiler List - 
        /// </summary>
        public void AccountHolderGetTranspilerList(System.String content, DMProxy proxy) 
        {
            var payload = this.CreatePayload();
            payload.Content = content;
            this.AccountHolderGetTranspilerList(payload, proxy);
        }

        /// <summary>
        /// Get Transpiler List - 
        /// </summary>
        public void AccountHolderGetTranspilerList(SSOTMEPayload payload, DMProxy proxy)
        {
            payload.IsDirectMessage = true;
            this.SendMessage(payload, "Get Transpiler List - ",
            "accountholdermic", "ssotmecoordinator.general.accountholder.gettranspilerlist", proxy.RoutingKey);
        }


        
        /// <summary>
        /// Create Project - 
        /// </summary>
        public void AccountHolderCreateProject(DMProxy proxy) 
        {
            this.AccountHolderCreateProject(this.CreatePayload(), proxy);
        }

        /// <summary>
        /// Create Project - 
        /// </summary>
        public void AccountHolderCreateProject(System.String content, DMProxy proxy) 
        {
            var payload = this.CreatePayload();
            payload.Content = content;
            this.AccountHolderCreateProject(payload, proxy);
        }

        /// <summary>
        /// Create Project - 
        /// </summary>
        public void AccountHolderCreateProject(SSOTMEPayload payload, DMProxy proxy)
        {
            payload.IsDirectMessage = true;
            this.SendMessage(payload, "Create Project - ",
            "accountholdermic", "ssotmecoordinator.general.accountholder.createproject", proxy.RoutingKey);
        }


        
        /// <summary>
        /// Request Transpile - 
        /// </summary>
        public void AccountHolderRequestTranspile(DMProxy proxy) 
        {
            this.AccountHolderRequestTranspile(this.CreatePayload(), proxy);
        }

        /// <summary>
        /// Request Transpile - 
        /// </summary>
        public void AccountHolderRequestTranspile(System.String content, DMProxy proxy) 
        {
            var payload = this.CreatePayload();
            payload.Content = content;
            this.AccountHolderRequestTranspile(payload, proxy);
        }

        /// <summary>
        /// Request Transpile - 
        /// </summary>
        public void AccountHolderRequestTranspile(SSOTMEPayload payload, DMProxy proxy)
        {
            payload.IsDirectMessage = true;
            this.SendMessage(payload, "Request Transpile - ",
            "accountholdermic", "ssotmecoordinator.general.accountholder.requesttranspile", proxy.RoutingKey);
        }


        
        /// <summary>
        /// Request Transpiler Host - 
        /// </summary>
        public void AccountHolderRequestTranspilerHost(DMProxy proxy) 
        {
            this.AccountHolderRequestTranspilerHost(this.CreatePayload(), proxy);
        }

        /// <summary>
        /// Request Transpiler Host - 
        /// </summary>
        public void AccountHolderRequestTranspilerHost(System.String content, DMProxy proxy) 
        {
            var payload = this.CreatePayload();
            payload.Content = content;
            this.AccountHolderRequestTranspilerHost(payload, proxy);
        }

        /// <summary>
        /// Request Transpiler Host - 
        /// </summary>
        public void AccountHolderRequestTranspilerHost(SSOTMEPayload payload, DMProxy proxy)
        {
            payload.IsDirectMessage = true;
            this.SendMessage(payload, "Request Transpiler Host - ",
            "accountholdermic", "ssotmecoordinator.general.accountholder.requesttranspilerhost", proxy.RoutingKey);
        }


        
        /// <summary>
        /// Request Stop Instance - 
        /// </summary>
        public void AccountHolderRequestStopInstance(DMProxy proxy) 
        {
            this.AccountHolderRequestStopInstance(this.CreatePayload(), proxy);
        }

        /// <summary>
        /// Request Stop Instance - 
        /// </summary>
        public void AccountHolderRequestStopInstance(System.String content, DMProxy proxy) 
        {
            var payload = this.CreatePayload();
            payload.Content = content;
            this.AccountHolderRequestStopInstance(payload, proxy);
        }

        /// <summary>
        /// Request Stop Instance - 
        /// </summary>
        public void AccountHolderRequestStopInstance(SSOTMEPayload payload, DMProxy proxy)
        {
            payload.IsDirectMessage = true;
            this.SendMessage(payload, "Request Stop Instance - ",
            "accountholdermic", "ssotmecoordinator.general.accountholder.requeststopinstance", proxy.RoutingKey);
        }


        
        /// <summary>
        /// Request Stop Host - 
        /// </summary>
        public void AccountHolderRequestStopHost(DMProxy proxy) 
        {
            this.AccountHolderRequestStopHost(this.CreatePayload(), proxy);
        }

        /// <summary>
        /// Request Stop Host - 
        /// </summary>
        public void AccountHolderRequestStopHost(System.String content, DMProxy proxy) 
        {
            var payload = this.CreatePayload();
            payload.Content = content;
            this.AccountHolderRequestStopHost(payload, proxy);
        }

        /// <summary>
        /// Request Stop Host - 
        /// </summary>
        public void AccountHolderRequestStopHost(SSOTMEPayload payload, DMProxy proxy)
        {
            payload.IsDirectMessage = true;
            this.SendMessage(payload, "Request Stop Host - ",
            "accountholdermic", "ssotmecoordinator.general.accountholder.requeststophost", proxy.RoutingKey);
        }


        
        /// <summary>
        /// Command Line Transpile - 
        /// </summary>
        public void AccountHolderCommandLineTranspile(DMProxy proxy) 
        {
            this.AccountHolderCommandLineTranspile(this.CreatePayload(), proxy);
        }

        /// <summary>
        /// Command Line Transpile - 
        /// </summary>
        public void AccountHolderCommandLineTranspile(System.String content, DMProxy proxy) 
        {
            var payload = this.CreatePayload();
            payload.Content = content;
            this.AccountHolderCommandLineTranspile(payload, proxy);
        }

        /// <summary>
        /// Command Line Transpile - 
        /// </summary>
        public void AccountHolderCommandLineTranspile(SSOTMEPayload payload, DMProxy proxy)
        {
            payload.IsDirectMessage = true;
            this.SendMessage(payload, "Command Line Transpile - ",
            "accountholdermic", "ssotmecoordinator.general.accountholder.commandlinetranspile", proxy.RoutingKey);
        }


        
            // And can also say/hear everything which : PublicUser hears.
            
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

                    