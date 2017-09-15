
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
    public partial class SMQSSOTMECoordinator : SSOTMEActorBase
    {
     
        public SMQSSOTMECoordinator(bool isAutoConnect = true)
            : base("ssotmecoordinator.all", isAutoConnect)
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

            
             if (payload.IsLexiconTerm(LexiconTermEnum.publicuser_ping_ssotmecoordinator)) 
            {
                this.OnPublicUserPingReceived(payload);
                this.Reply(payload);
            }
        
            else  if (payload.IsLexiconTerm(LexiconTermEnum.publicuser_register_ssotmecoordinator)) 
            {
                this.OnPublicUserRegisterReceived(payload);
                this.Reply(payload);
            }
        
            else  if (payload.IsLexiconTerm(LexiconTermEnum.publicuser_recover_ssotmecoordinator)) 
            {
                this.OnPublicUserRecoverReceived(payload);
                this.Reply(payload);
            }
        
            else  if (payload.IsLexiconTerm(LexiconTermEnum.publicuser_getalltranspilers_ssotmecoordinator)) 
            {
                this.OnPublicUserGetAllTranspilersReceived(payload);
                this.Reply(payload);
            }
        
            else  if (payload.IsLexiconTerm(LexiconTermEnum.publicuser_getallplatformdata_ssotmecoordinator)) 
            {
                this.OnPublicUserGetAllPlatformDataReceived(payload);
                this.Reply(payload);
            }
        
            else  if (payload.IsLexiconTerm(LexiconTermEnum.accountholder_ping_ssotmecoordinator)) 
            {
                this.OnAccountHolderPingReceived(payload);
                this.Reply(payload);
            }
        
            else  if (payload.IsLexiconTerm(LexiconTermEnum.accountholder_login_ssotmecoordinator)) 
            {
                this.OnAccountHolderLoginReceived(payload);
                this.Reply(payload);
            }
        
            else  if (payload.IsLexiconTerm(LexiconTermEnum.accountholder_logout_ssotmecoordinator)) 
            {
                this.OnAccountHolderLogoutReceived(payload);
                this.Reply(payload);
            }
        
            else  if (payload.IsLexiconTerm(LexiconTermEnum.accountholder_addtranspiler_ssotmecoordinator)) 
            {
                this.OnAccountHolderAddTranspilerReceived(payload);
                this.Reply(payload);
            }
        
            else  if (payload.IsLexiconTerm(LexiconTermEnum.accountholder_deletetranspiler_ssotmecoordinator)) 
            {
                this.OnAccountHolderDeleteTranspilerReceived(payload);
                this.Reply(payload);
            }
        
            else  if (payload.IsLexiconTerm(LexiconTermEnum.accountholder_updatetranspiler_ssotmecoordinator)) 
            {
                this.OnAccountHolderUpdateTranspilerReceived(payload);
                this.Reply(payload);
            }
        
            else  if (payload.IsLexiconTerm(LexiconTermEnum.accountholder_gettranspiler_ssotmecoordinator)) 
            {
                this.OnAccountHolderGetTranspilerReceived(payload);
                this.Reply(payload);
            }
        
            else  if (payload.IsLexiconTerm(LexiconTermEnum.accountholder_addtranspilerversion_ssotmecoordinator)) 
            {
                this.OnAccountHolderAddTranspilerVersionReceived(payload);
                this.Reply(payload);
            }
        
            else  if (payload.IsLexiconTerm(LexiconTermEnum.accountholder_deletetranspilerversion_ssotmecoordinator)) 
            {
                this.OnAccountHolderDeleteTranspilerVersionReceived(payload);
                this.Reply(payload);
            }
        
            else  if (payload.IsLexiconTerm(LexiconTermEnum.accountholder_updatetranspilerversion_ssotmecoordinator)) 
            {
                this.OnAccountHolderUpdateTranspilerVersionReceived(payload);
                this.Reply(payload);
            }
        
            else  if (payload.IsLexiconTerm(LexiconTermEnum.accountholder_gettranspilerlist_ssotmecoordinator)) 
            {
                this.OnAccountHolderGetTranspilerListReceived(payload);
                this.Reply(payload);
            }
        
            else  if (payload.IsLexiconTerm(LexiconTermEnum.accountholder_createproject_ssotmecoordinator)) 
            {
                this.OnAccountHolderCreateProjectReceived(payload);
                this.Reply(payload);
            }
        
            else  if (payload.IsLexiconTerm(LexiconTermEnum.accountholder_requesttranspile_ssotmecoordinator)) 
            {
                this.OnAccountHolderRequestTranspileReceived(payload);
                this.Reply(payload);
            }
        
            else  if (payload.IsLexiconTerm(LexiconTermEnum.accountholder_requesttranspilerhost_ssotmecoordinator)) 
            {
                this.OnAccountHolderRequestTranspilerHostReceived(payload);
                this.Reply(payload);
            }
        
            else  if (payload.IsLexiconTerm(LexiconTermEnum.accountholder_requeststopinstance_ssotmecoordinator)) 
            {
                this.OnAccountHolderRequestStopInstanceReceived(payload);
                this.Reply(payload);
            }
        
            else  if (payload.IsLexiconTerm(LexiconTermEnum.accountholder_requeststophost_ssotmecoordinator)) 
            {
                this.OnAccountHolderRequestStopHostReceived(payload);
                this.Reply(payload);
            }
        
            else  if (payload.IsLexiconTerm(LexiconTermEnum.accountholder_commandlinetranspile_ssotmecoordinator)) 
            {
                this.OnAccountHolderCommandLineTranspileReceived(payload);
                this.Reply(payload);
            }
        
            else  if (payload.IsLexiconTerm(LexiconTermEnum.transpilerhost_ping_ssotmecoordinator)) 
            {
                this.OnTranspilerHostPingReceived(payload);
                this.Reply(payload);
            }
        
            else  if (payload.IsLexiconTerm(LexiconTermEnum.transpilerhost_offline_ssotmecoordinator)) 
            {
                this.OnTranspilerHostOfflineReceived(payload);
                this.Reply(payload);
            }
        
            else  if (payload.IsLexiconTerm(LexiconTermEnum.transpilerhost_instancestarted_ssotmecoordinator)) 
            {
                this.OnTranspilerHostInstanceStartedReceived(payload);
                this.Reply(payload);
            }
        
            else  if (payload.IsLexiconTerm(LexiconTermEnum.transpilerhost_instancestopped_ssotmecoordinator)) 
            {
                this.OnTranspilerHostInstanceStoppedReceived(payload);
                this.Reply(payload);
            }
        
            else  if (payload.IsLexiconTerm(LexiconTermEnum.ssotmeadmin_ping_ssotmecoordinator)) 
            {
                this.OnSSOTMEAdminPingReceived(payload);
                this.Reply(payload);
            }
        
            else  if (payload.IsLexiconTerm(LexiconTermEnum.ssotmeadmin_addplatformcategory_ssotmecoordinator)) 
            {
                this.OnSSOTMEAdminAddPlatformCategoryReceived(payload);
                this.Reply(payload);
            }
        
            else  if (payload.IsLexiconTerm(LexiconTermEnum.ssotmeadmin_updateplatformcategory_ssotmecoordinator)) 
            {
                this.OnSSOTMEAdminUpdatePlatformCategoryReceived(payload);
                this.Reply(payload);
            }
        
            else  if (payload.IsLexiconTerm(LexiconTermEnum.ssotmeadmin_addtranspilerplatform_ssotmecoordinator)) 
            {
                this.OnSSOTMEAdminAddTranspilerPlatformReceived(payload);
                this.Reply(payload);
            }
        
            else  if (payload.IsLexiconTerm(LexiconTermEnum.ssotmeadmin_updatetranspilerplatform_ssotmecoordinator)) 
            {
                this.OnSSOTMEAdminUpdateTranspilerPlatformReceived(payload);
                this.Reply(payload);
            }
        
            // And can also hear everything which : SSOTMEAdmin hears.
            
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

        
        public event System.EventHandler<PayloadEventArgs<SSOTMEPayload>> PublicUserPingReceived;
        protected virtual void OnPublicUserPingReceived(SSOTMEPayload payload)
        {
            if (IsDebugMode) 
            {
                System.Console.WriteLine("Ping - ");
                System.Console.WriteLine("payload: " + payload.SafeToString());
            }

            var plea = new PayloadEventArgs<SSOTMEPayload>(payload);
            this.Invoke(this.PublicUserPingReceived, plea);
        }
        
        public event System.EventHandler<PayloadEventArgs<SSOTMEPayload>> PublicUserRegisterReceived;
        protected virtual void OnPublicUserRegisterReceived(SSOTMEPayload payload)
        {
            if (IsDebugMode) 
            {
                System.Console.WriteLine("Register - ");
                System.Console.WriteLine("payload: " + payload.SafeToString());
            }

            var plea = new PayloadEventArgs<SSOTMEPayload>(payload);
            this.Invoke(this.PublicUserRegisterReceived, plea);
        }
        
        public event System.EventHandler<PayloadEventArgs<SSOTMEPayload>> PublicUserRecoverReceived;
        protected virtual void OnPublicUserRecoverReceived(SSOTMEPayload payload)
        {
            if (IsDebugMode) 
            {
                System.Console.WriteLine("Recover - ");
                System.Console.WriteLine("payload: " + payload.SafeToString());
            }

            var plea = new PayloadEventArgs<SSOTMEPayload>(payload);
            this.Invoke(this.PublicUserRecoverReceived, plea);
        }
        
        public event System.EventHandler<PayloadEventArgs<SSOTMEPayload>> PublicUserGetAllTranspilersReceived;
        protected virtual void OnPublicUserGetAllTranspilersReceived(SSOTMEPayload payload)
        {
            if (IsDebugMode) 
            {
                System.Console.WriteLine("Get All Transpilers - ");
                System.Console.WriteLine("payload: " + payload.SafeToString());
            }

            var plea = new PayloadEventArgs<SSOTMEPayload>(payload);
            this.Invoke(this.PublicUserGetAllTranspilersReceived, plea);
        }
        
        public event System.EventHandler<PayloadEventArgs<SSOTMEPayload>> PublicUserGetAllPlatformDataReceived;
        protected virtual void OnPublicUserGetAllPlatformDataReceived(SSOTMEPayload payload)
        {
            if (IsDebugMode) 
            {
                System.Console.WriteLine("Get All Platform Data - ");
                System.Console.WriteLine("payload: " + payload.SafeToString());
            }

            var plea = new PayloadEventArgs<SSOTMEPayload>(payload);
            this.Invoke(this.PublicUserGetAllPlatformDataReceived, plea);
        }
        
        public event System.EventHandler<PayloadEventArgs<SSOTMEPayload>> AccountHolderPingReceived;
        protected virtual void OnAccountHolderPingReceived(SSOTMEPayload payload)
        {
            if (IsDebugMode) 
            {
                System.Console.WriteLine("Ping - ");
                System.Console.WriteLine("payload: " + payload.SafeToString());
            }

            var plea = new PayloadEventArgs<SSOTMEPayload>(payload);
            this.Invoke(this.AccountHolderPingReceived, plea);
        }
        
        public event System.EventHandler<PayloadEventArgs<SSOTMEPayload>> AccountHolderLoginReceived;
        protected virtual void OnAccountHolderLoginReceived(SSOTMEPayload payload)
        {
            if (IsDebugMode) 
            {
                System.Console.WriteLine("Login - ");
                System.Console.WriteLine("payload: " + payload.SafeToString());
            }

            var plea = new PayloadEventArgs<SSOTMEPayload>(payload);
            this.Invoke(this.AccountHolderLoginReceived, plea);
        }
        
        public event System.EventHandler<PayloadEventArgs<SSOTMEPayload>> AccountHolderLogoutReceived;
        protected virtual void OnAccountHolderLogoutReceived(SSOTMEPayload payload)
        {
            if (IsDebugMode) 
            {
                System.Console.WriteLine("Logout - ");
                System.Console.WriteLine("payload: " + payload.SafeToString());
            }

            var plea = new PayloadEventArgs<SSOTMEPayload>(payload);
            this.Invoke(this.AccountHolderLogoutReceived, plea);
        }
        
        public event System.EventHandler<PayloadEventArgs<SSOTMEPayload>> AccountHolderAddTranspilerReceived;
        protected virtual void OnAccountHolderAddTranspilerReceived(SSOTMEPayload payload)
        {
            if (IsDebugMode) 
            {
                System.Console.WriteLine("Add Transpiler - ");
                System.Console.WriteLine("payload: " + payload.SafeToString());
            }

            var plea = new PayloadEventArgs<SSOTMEPayload>(payload);
            this.Invoke(this.AccountHolderAddTranspilerReceived, plea);
        }
        
        public event System.EventHandler<PayloadEventArgs<SSOTMEPayload>> AccountHolderDeleteTranspilerReceived;
        protected virtual void OnAccountHolderDeleteTranspilerReceived(SSOTMEPayload payload)
        {
            if (IsDebugMode) 
            {
                System.Console.WriteLine("Delete Transpiler - ");
                System.Console.WriteLine("payload: " + payload.SafeToString());
            }

            var plea = new PayloadEventArgs<SSOTMEPayload>(payload);
            this.Invoke(this.AccountHolderDeleteTranspilerReceived, plea);
        }
        
        public event System.EventHandler<PayloadEventArgs<SSOTMEPayload>> AccountHolderUpdateTranspilerReceived;
        protected virtual void OnAccountHolderUpdateTranspilerReceived(SSOTMEPayload payload)
        {
            if (IsDebugMode) 
            {
                System.Console.WriteLine("Update Transpiler - ");
                System.Console.WriteLine("payload: " + payload.SafeToString());
            }

            var plea = new PayloadEventArgs<SSOTMEPayload>(payload);
            this.Invoke(this.AccountHolderUpdateTranspilerReceived, plea);
        }
        
        public event System.EventHandler<PayloadEventArgs<SSOTMEPayload>> AccountHolderGetTranspilerReceived;
        protected virtual void OnAccountHolderGetTranspilerReceived(SSOTMEPayload payload)
        {
            if (IsDebugMode) 
            {
                System.Console.WriteLine("Get Transpiler - ");
                System.Console.WriteLine("payload: " + payload.SafeToString());
            }

            var plea = new PayloadEventArgs<SSOTMEPayload>(payload);
            this.Invoke(this.AccountHolderGetTranspilerReceived, plea);
        }
        
        public event System.EventHandler<PayloadEventArgs<SSOTMEPayload>> AccountHolderAddTranspilerVersionReceived;
        protected virtual void OnAccountHolderAddTranspilerVersionReceived(SSOTMEPayload payload)
        {
            if (IsDebugMode) 
            {
                System.Console.WriteLine("Add Transpiler Version - ");
                System.Console.WriteLine("payload: " + payload.SafeToString());
            }

            var plea = new PayloadEventArgs<SSOTMEPayload>(payload);
            this.Invoke(this.AccountHolderAddTranspilerVersionReceived, plea);
        }
        
        public event System.EventHandler<PayloadEventArgs<SSOTMEPayload>> AccountHolderDeleteTranspilerVersionReceived;
        protected virtual void OnAccountHolderDeleteTranspilerVersionReceived(SSOTMEPayload payload)
        {
            if (IsDebugMode) 
            {
                System.Console.WriteLine("Delete Transpiler Version - ");
                System.Console.WriteLine("payload: " + payload.SafeToString());
            }

            var plea = new PayloadEventArgs<SSOTMEPayload>(payload);
            this.Invoke(this.AccountHolderDeleteTranspilerVersionReceived, plea);
        }
        
        public event System.EventHandler<PayloadEventArgs<SSOTMEPayload>> AccountHolderUpdateTranspilerVersionReceived;
        protected virtual void OnAccountHolderUpdateTranspilerVersionReceived(SSOTMEPayload payload)
        {
            if (IsDebugMode) 
            {
                System.Console.WriteLine("Update Transpiler Version - ");
                System.Console.WriteLine("payload: " + payload.SafeToString());
            }

            var plea = new PayloadEventArgs<SSOTMEPayload>(payload);
            this.Invoke(this.AccountHolderUpdateTranspilerVersionReceived, plea);
        }
        
        public event System.EventHandler<PayloadEventArgs<SSOTMEPayload>> AccountHolderGetTranspilerListReceived;
        protected virtual void OnAccountHolderGetTranspilerListReceived(SSOTMEPayload payload)
        {
            if (IsDebugMode) 
            {
                System.Console.WriteLine("Get Transpiler List - ");
                System.Console.WriteLine("payload: " + payload.SafeToString());
            }

            var plea = new PayloadEventArgs<SSOTMEPayload>(payload);
            this.Invoke(this.AccountHolderGetTranspilerListReceived, plea);
        }
        
        public event System.EventHandler<PayloadEventArgs<SSOTMEPayload>> AccountHolderCreateProjectReceived;
        protected virtual void OnAccountHolderCreateProjectReceived(SSOTMEPayload payload)
        {
            if (IsDebugMode) 
            {
                System.Console.WriteLine("Create Project - ");
                System.Console.WriteLine("payload: " + payload.SafeToString());
            }

            var plea = new PayloadEventArgs<SSOTMEPayload>(payload);
            this.Invoke(this.AccountHolderCreateProjectReceived, plea);
        }
        
        public event System.EventHandler<PayloadEventArgs<SSOTMEPayload>> AccountHolderRequestTranspileReceived;
        protected virtual void OnAccountHolderRequestTranspileReceived(SSOTMEPayload payload)
        {
            if (IsDebugMode) 
            {
                System.Console.WriteLine("Request Transpile - ");
                System.Console.WriteLine("payload: " + payload.SafeToString());
            }

            var plea = new PayloadEventArgs<SSOTMEPayload>(payload);
            this.Invoke(this.AccountHolderRequestTranspileReceived, plea);
        }
        
        public event System.EventHandler<PayloadEventArgs<SSOTMEPayload>> AccountHolderRequestTranspilerHostReceived;
        protected virtual void OnAccountHolderRequestTranspilerHostReceived(SSOTMEPayload payload)
        {
            if (IsDebugMode) 
            {
                System.Console.WriteLine("Request Transpiler Host - ");
                System.Console.WriteLine("payload: " + payload.SafeToString());
            }

            var plea = new PayloadEventArgs<SSOTMEPayload>(payload);
            this.Invoke(this.AccountHolderRequestTranspilerHostReceived, plea);
        }
        
        public event System.EventHandler<PayloadEventArgs<SSOTMEPayload>> AccountHolderRequestStopInstanceReceived;
        protected virtual void OnAccountHolderRequestStopInstanceReceived(SSOTMEPayload payload)
        {
            if (IsDebugMode) 
            {
                System.Console.WriteLine("Request Stop Instance - ");
                System.Console.WriteLine("payload: " + payload.SafeToString());
            }

            var plea = new PayloadEventArgs<SSOTMEPayload>(payload);
            this.Invoke(this.AccountHolderRequestStopInstanceReceived, plea);
        }
        
        public event System.EventHandler<PayloadEventArgs<SSOTMEPayload>> AccountHolderRequestStopHostReceived;
        protected virtual void OnAccountHolderRequestStopHostReceived(SSOTMEPayload payload)
        {
            if (IsDebugMode) 
            {
                System.Console.WriteLine("Request Stop Host - ");
                System.Console.WriteLine("payload: " + payload.SafeToString());
            }

            var plea = new PayloadEventArgs<SSOTMEPayload>(payload);
            this.Invoke(this.AccountHolderRequestStopHostReceived, plea);
        }
        
        public event System.EventHandler<PayloadEventArgs<SSOTMEPayload>> AccountHolderCommandLineTranspileReceived;
        protected virtual void OnAccountHolderCommandLineTranspileReceived(SSOTMEPayload payload)
        {
            if (IsDebugMode) 
            {
                System.Console.WriteLine("Command Line Transpile - ");
                System.Console.WriteLine("payload: " + payload.SafeToString());
            }

            var plea = new PayloadEventArgs<SSOTMEPayload>(payload);
            this.Invoke(this.AccountHolderCommandLineTranspileReceived, plea);
        }
        
        public event System.EventHandler<PayloadEventArgs<SSOTMEPayload>> TranspilerHostPingReceived;
        protected virtual void OnTranspilerHostPingReceived(SSOTMEPayload payload)
        {
            if (IsDebugMode) 
            {
                System.Console.WriteLine("Ping - ");
                System.Console.WriteLine("payload: " + payload.SafeToString());
            }

            var plea = new PayloadEventArgs<SSOTMEPayload>(payload);
            this.Invoke(this.TranspilerHostPingReceived, plea);
        }
        
        public event System.EventHandler<PayloadEventArgs<SSOTMEPayload>> TranspilerHostOfflineReceived;
        protected virtual void OnTranspilerHostOfflineReceived(SSOTMEPayload payload)
        {
            if (IsDebugMode) 
            {
                System.Console.WriteLine("Offline - ");
                System.Console.WriteLine("payload: " + payload.SafeToString());
            }

            var plea = new PayloadEventArgs<SSOTMEPayload>(payload);
            this.Invoke(this.TranspilerHostOfflineReceived, plea);
        }
        
        public event System.EventHandler<PayloadEventArgs<SSOTMEPayload>> TranspilerHostInstanceStartedReceived;
        protected virtual void OnTranspilerHostInstanceStartedReceived(SSOTMEPayload payload)
        {
            if (IsDebugMode) 
            {
                System.Console.WriteLine("Instance Started - ");
                System.Console.WriteLine("payload: " + payload.SafeToString());
            }

            var plea = new PayloadEventArgs<SSOTMEPayload>(payload);
            this.Invoke(this.TranspilerHostInstanceStartedReceived, plea);
        }
        
        public event System.EventHandler<PayloadEventArgs<SSOTMEPayload>> TranspilerHostInstanceStoppedReceived;
        protected virtual void OnTranspilerHostInstanceStoppedReceived(SSOTMEPayload payload)
        {
            if (IsDebugMode) 
            {
                System.Console.WriteLine("Instance Stopped - ");
                System.Console.WriteLine("payload: " + payload.SafeToString());
            }

            var plea = new PayloadEventArgs<SSOTMEPayload>(payload);
            this.Invoke(this.TranspilerHostInstanceStoppedReceived, plea);
        }
        
        public event System.EventHandler<PayloadEventArgs<SSOTMEPayload>> SSOTMEAdminPingReceived;
        protected virtual void OnSSOTMEAdminPingReceived(SSOTMEPayload payload)
        {
            if (IsDebugMode) 
            {
                System.Console.WriteLine("Ping - ");
                System.Console.WriteLine("payload: " + payload.SafeToString());
            }

            var plea = new PayloadEventArgs<SSOTMEPayload>(payload);
            this.Invoke(this.SSOTMEAdminPingReceived, plea);
        }
        
        public event System.EventHandler<PayloadEventArgs<SSOTMEPayload>> SSOTMEAdminAddPlatformCategoryReceived;
        protected virtual void OnSSOTMEAdminAddPlatformCategoryReceived(SSOTMEPayload payload)
        {
            if (IsDebugMode) 
            {
                System.Console.WriteLine("Add Platform Category - ");
                System.Console.WriteLine("payload: " + payload.SafeToString());
            }

            var plea = new PayloadEventArgs<SSOTMEPayload>(payload);
            this.Invoke(this.SSOTMEAdminAddPlatformCategoryReceived, plea);
        }
        
        public event System.EventHandler<PayloadEventArgs<SSOTMEPayload>> SSOTMEAdminUpdatePlatformCategoryReceived;
        protected virtual void OnSSOTMEAdminUpdatePlatformCategoryReceived(SSOTMEPayload payload)
        {
            if (IsDebugMode) 
            {
                System.Console.WriteLine("Update Platform Category - ");
                System.Console.WriteLine("payload: " + payload.SafeToString());
            }

            var plea = new PayloadEventArgs<SSOTMEPayload>(payload);
            this.Invoke(this.SSOTMEAdminUpdatePlatformCategoryReceived, plea);
        }
        
        public event System.EventHandler<PayloadEventArgs<SSOTMEPayload>> SSOTMEAdminAddTranspilerPlatformReceived;
        protected virtual void OnSSOTMEAdminAddTranspilerPlatformReceived(SSOTMEPayload payload)
        {
            if (IsDebugMode) 
            {
                System.Console.WriteLine("Add Transpiler Platform - ");
                System.Console.WriteLine("payload: " + payload.SafeToString());
            }

            var plea = new PayloadEventArgs<SSOTMEPayload>(payload);
            this.Invoke(this.SSOTMEAdminAddTranspilerPlatformReceived, plea);
        }
        
        public event System.EventHandler<PayloadEventArgs<SSOTMEPayload>> SSOTMEAdminUpdateTranspilerPlatformReceived;
        protected virtual void OnSSOTMEAdminUpdateTranspilerPlatformReceived(SSOTMEPayload payload)
        {
            if (IsDebugMode) 
            {
                System.Console.WriteLine("Update Transpiler Platform - ");
                System.Console.WriteLine("payload: " + payload.SafeToString());
            }

            var plea = new PayloadEventArgs<SSOTMEPayload>(payload);
            this.Invoke(this.SSOTMEAdminUpdateTranspilerPlatformReceived, plea);
        }
        
            public void SSOTMECoordinatorGetInstances(DMProxy proxy) {
                this.SSOTMECoordinatorGetInstances(this.CreatePayload(), proxy);
            }

            public void SSOTMECoordinatorGetInstances(System.String content, DMProxy proxy) {
                var payload = this.CreatePayload();
                payload.Content = content;
                this.SSOTMECoordinatorGetInstances(payload, proxy);
            }

            public void SSOTMECoordinatorGetInstances(SSOTMEPayload payload, DMProxy proxy)
            {
                payload.IsDirectMessage = true;
                this.SendMessage(payload, "Get Instances - ",
                        "ssotmecoordinatormic", "transpilerhost.general.ssotmecoordinator.getinstances", proxy.RoutingKey);
             }

 
        
            public void SSOTMECoordinatorTranspileRequested(DMProxy proxy) {
                this.SSOTMECoordinatorTranspileRequested(this.CreatePayload(), proxy);
            }

            public void SSOTMECoordinatorTranspileRequested(System.String content, DMProxy proxy) {
                var payload = this.CreatePayload();
                payload.Content = content;
                this.SSOTMECoordinatorTranspileRequested(payload, proxy);
            }

            public void SSOTMECoordinatorTranspileRequested(SSOTMEPayload payload, DMProxy proxy)
            {
                payload.IsDirectMessage = true;
                this.SendMessage(payload, "Transpile Requested - ",
                        "ssotmecoordinatormic", "transpilerhost.general.ssotmecoordinator.transpilerequested", proxy.RoutingKey);
             }

 
        
            public void SSOTMECoordinatorTranspilerOnline() {
                this.SSOTMECoordinatorTranspilerOnline(this.CreatePayload());
            }

            public void SSOTMECoordinatorTranspilerOnline(System.String content) {
                var payload = this.CreatePayload();
                payload.Content = content;
                this.SSOTMECoordinatorTranspilerOnline(payload);
            }

            public void SSOTMECoordinatorTranspilerOnline(SSOTMEPayload payload)
            {
                
                this.SendMessage(payload, "Transpiler Online - ",
                        "ssotmecoordinatormic", "publicuser.general.ssotmecoordinator.transpileronline");
             }

 
        
            public void SSOTMECoordinatorTranspilerOffline() {
                this.SSOTMECoordinatorTranspilerOffline(this.CreatePayload());
            }

            public void SSOTMECoordinatorTranspilerOffline(System.String content) {
                var payload = this.CreatePayload();
                payload.Content = content;
                this.SSOTMECoordinatorTranspilerOffline(payload);
            }

            public void SSOTMECoordinatorTranspilerOffline(SSOTMEPayload payload)
            {
                
                this.SendMessage(payload, "Transpiler Offline - ",
                        "ssotmecoordinatormic", "publicuser.general.ssotmecoordinator.transpileroffline");
             }

 
        
            public void SSOTMECoordinatorStopInstance(DMProxy proxy) {
                this.SSOTMECoordinatorStopInstance(this.CreatePayload(), proxy);
            }

            public void SSOTMECoordinatorStopInstance(System.String content, DMProxy proxy) {
                var payload = this.CreatePayload();
                payload.Content = content;
                this.SSOTMECoordinatorStopInstance(payload, proxy);
            }

            public void SSOTMECoordinatorStopInstance(SSOTMEPayload payload, DMProxy proxy)
            {
                payload.IsDirectMessage = true;
                this.SendMessage(payload, "Stop Instance - ",
                        "ssotmecoordinatormic", "transpilerhost.general.ssotmecoordinator.stopinstance", proxy.RoutingKey);
             }

 
        
            public void SSOTMECoordinatorStopHost(DMProxy proxy) {
                this.SSOTMECoordinatorStopHost(this.CreatePayload(), proxy);
            }

            public void SSOTMECoordinatorStopHost(System.String content, DMProxy proxy) {
                var payload = this.CreatePayload();
                payload.Content = content;
                this.SSOTMECoordinatorStopHost(payload, proxy);
            }

            public void SSOTMECoordinatorStopHost(SSOTMEPayload payload, DMProxy proxy)
            {
                payload.IsDirectMessage = true;
                this.SendMessage(payload, "Stop Host - ",
                        "ssotmecoordinatormic", "transpilerhost.general.ssotmecoordinator.stophost", proxy.RoutingKey);
             }

 
        
            // And can also say/hear everything which : SSOTMEAdmin hears.
            
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

 
        
    }
}

                    