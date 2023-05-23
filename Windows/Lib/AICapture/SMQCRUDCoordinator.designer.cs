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
                    
                    case "crudcoordinator.custom.user.notifymehere":
                        this.OnUserNotifyMeHereReceived(payload, bdea);
                        break;
                    
                    case "crudcoordinator.custom.aicagent.monitoringfor":
                        this.OnAICAgentMonitoringForReceived(payload, bdea);
                        break;
                    
                    case "crudcoordinator.crud.aicsuperagent.addhelpfulprompt":
                        this.OnAICSuperAgentAddHelpfulPromptReceived(payload, bdea);
                        break;
                    
                    case "crudcoordinator.crud.aicsuperagent.gethelpfulprompts":
                        this.OnAICSuperAgentGetHelpfulPromptsReceived(payload, bdea);
                        break;
                    
                    case "crudcoordinator.crud.aicsuperagent.updatehelpfulprompt":
                        this.OnAICSuperAgentUpdateHelpfulPromptReceived(payload, bdea);
                        break;
                    
                    case "crudcoordinator.crud.aicsuperagent.deletehelpfulprompt":
                        this.OnAICSuperAgentDeleteHelpfulPromptReceived(payload, bdea);
                        break;
                    
                    case "crudcoordinator.crud.admin.addhelpfulprompt":
                        this.OnAdminAddHelpfulPromptReceived(payload, bdea);
                        break;
                    
                    case "crudcoordinator.crud.admin.gethelpfulprompts":
                        this.OnAdminGetHelpfulPromptsReceived(payload, bdea);
                        break;
                    
                    case "crudcoordinator.crud.admin.updatehelpfulprompt":
                        this.OnAdminUpdateHelpfulPromptReceived(payload, bdea);
                        break;
                    
                    case "crudcoordinator.crud.admin.deletehelpfulprompt":
                        this.OnAdminDeleteHelpfulPromptReceived(payload, bdea);
                        break;
                    
                    case "crudcoordinator.crud.aicsuperagent.addaimodel":
                        this.OnAICSuperAgentAddAIModelReceived(payload, bdea);
                        break;
                    
                    case "crudcoordinator.crud.aicsuperagent.getaimodels":
                        this.OnAICSuperAgentGetAIModelsReceived(payload, bdea);
                        break;
                    
                    case "crudcoordinator.crud.aicsuperagent.updateaimodel":
                        this.OnAICSuperAgentUpdateAIModelReceived(payload, bdea);
                        break;
                    
                    case "crudcoordinator.crud.aicsuperagent.deleteaimodel":
                        this.OnAICSuperAgentDeleteAIModelReceived(payload, bdea);
                        break;
                    
                    case "crudcoordinator.crud.admin.addaimodel":
                        this.OnAdminAddAIModelReceived(payload, bdea);
                        break;
                    
                    case "crudcoordinator.crud.admin.getaimodels":
                        this.OnAdminGetAIModelsReceived(payload, bdea);
                        break;
                    
                    case "crudcoordinator.crud.admin.updateaimodel":
                        this.OnAdminUpdateAIModelReceived(payload, bdea);
                        break;
                    
                    case "crudcoordinator.crud.admin.deleteaimodel":
                        this.OnAdminDeleteAIModelReceived(payload, bdea);
                        break;
                    
                    case "crudcoordinator.crud.user.getshareddirectories":
                        this.OnUserGetSharedDirectoriesReceived(payload, bdea);
                        break;
                    
                    case "crudcoordinator.crud.aicsuperagent.addshareddirectory":
                        this.OnAICSuperAgentAddSharedDirectoryReceived(payload, bdea);
                        break;
                    
                    case "crudcoordinator.crud.aicsuperagent.getshareddirectories":
                        this.OnAICSuperAgentGetSharedDirectoriesReceived(payload, bdea);
                        break;
                    
                    case "crudcoordinator.crud.aicsuperagent.updateshareddirectory":
                        this.OnAICSuperAgentUpdateSharedDirectoryReceived(payload, bdea);
                        break;
                    
                    case "crudcoordinator.crud.aicsuperagent.deleteshareddirectory":
                        this.OnAICSuperAgentDeleteSharedDirectoryReceived(payload, bdea);
                        break;
                    
                    case "crudcoordinator.crud.admin.addshareddirectory":
                        this.OnAdminAddSharedDirectoryReceived(payload, bdea);
                        break;
                    
                    case "crudcoordinator.crud.admin.getshareddirectories":
                        this.OnAdminGetSharedDirectoriesReceived(payload, bdea);
                        break;
                    
                    case "crudcoordinator.crud.admin.updateshareddirectory":
                        this.OnAdminUpdateSharedDirectoryReceived(payload, bdea);
                        break;
                    
                    case "crudcoordinator.crud.admin.deleteshareddirectory":
                        this.OnAdminDeleteSharedDirectoryReceived(payload, bdea);
                        break;
                    
                    case "crudcoordinator.crud.aicsuperagent.addaicdirectory":
                        this.OnAICSuperAgentAddAICDirectoryReceived(payload, bdea);
                        break;
                    
                    case "crudcoordinator.crud.aicsuperagent.getaicdirectories":
                        this.OnAICSuperAgentGetAICDirectoriesReceived(payload, bdea);
                        break;
                    
                    case "crudcoordinator.crud.aicsuperagent.updateaicdirectory":
                        this.OnAICSuperAgentUpdateAICDirectoryReceived(payload, bdea);
                        break;
                    
                    case "crudcoordinator.crud.aicsuperagent.deleteaicdirectory":
                        this.OnAICSuperAgentDeleteAICDirectoryReceived(payload, bdea);
                        break;
                    
                    case "crudcoordinator.crud.admin.addaicdirectory":
                        this.OnAdminAddAICDirectoryReceived(payload, bdea);
                        break;
                    
                    case "crudcoordinator.crud.admin.getaicdirectories":
                        this.OnAdminGetAICDirectoriesReceived(payload, bdea);
                        break;
                    
                    case "crudcoordinator.crud.admin.updateaicdirectory":
                        this.OnAdminUpdateAICDirectoryReceived(payload, bdea);
                        break;
                    
                    case "crudcoordinator.crud.admin.deleteaicdirectory":
                        this.OnAdminDeleteAICDirectoryReceived(payload, bdea);
                        break;
                    
                    case "crudcoordinator.crud.aicsuperagent.addratelimit":
                        this.OnAICSuperAgentAddRateLimitReceived(payload, bdea);
                        break;
                    
                    case "crudcoordinator.crud.aicsuperagent.getratelimits":
                        this.OnAICSuperAgentGetRateLimitsReceived(payload, bdea);
                        break;
                    
                    case "crudcoordinator.crud.aicsuperagent.updateratelimit":
                        this.OnAICSuperAgentUpdateRateLimitReceived(payload, bdea);
                        break;
                    
                    case "crudcoordinator.crud.aicsuperagent.deleteratelimit":
                        this.OnAICSuperAgentDeleteRateLimitReceived(payload, bdea);
                        break;
                    
                    case "crudcoordinator.crud.admin.addratelimit":
                        this.OnAdminAddRateLimitReceived(payload, bdea);
                        break;
                    
                    case "crudcoordinator.crud.admin.getratelimits":
                        this.OnAdminGetRateLimitsReceived(payload, bdea);
                        break;
                    
                    case "crudcoordinator.crud.admin.updateratelimit":
                        this.OnAdminUpdateRateLimitReceived(payload, bdea);
                        break;
                    
                    case "crudcoordinator.crud.admin.deleteratelimit":
                        this.OnAdminDeleteRateLimitReceived(payload, bdea);
                        break;
                    
                    case "crudcoordinator.crud.aicsuperagent.addappuser":
                        this.OnAICSuperAgentAddAppUserReceived(payload, bdea);
                        break;
                    
                    case "crudcoordinator.crud.aicsuperagent.getappusers":
                        this.OnAICSuperAgentGetAppUsersReceived(payload, bdea);
                        break;
                    
                    case "crudcoordinator.crud.aicsuperagent.updateappuser":
                        this.OnAICSuperAgentUpdateAppUserReceived(payload, bdea);
                        break;
                    
                    case "crudcoordinator.crud.aicsuperagent.deleteappuser":
                        this.OnAICSuperAgentDeleteAppUserReceived(payload, bdea);
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
                    
                    case "crudcoordinator.crud.user.getaicworkspaces":
                        this.OnUserGetAICWorkspacesReceived(payload, bdea);
                        break;
                    
                    case "crudcoordinator.crud.user.updateaicworkspace":
                        this.OnUserUpdateAICWorkspaceReceived(payload, bdea);
                        break;
                    
                    case "crudcoordinator.crud.aicsuperagent.addaicworkspace":
                        this.OnAICSuperAgentAddAICWorkspaceReceived(payload, bdea);
                        break;
                    
                    case "crudcoordinator.crud.aicsuperagent.getaicworkspaces":
                        this.OnAICSuperAgentGetAICWorkspacesReceived(payload, bdea);
                        break;
                    
                    case "crudcoordinator.crud.aicsuperagent.updateaicworkspace":
                        this.OnAICSuperAgentUpdateAICWorkspaceReceived(payload, bdea);
                        break;
                    
                    case "crudcoordinator.crud.aicsuperagent.deleteaicworkspace":
                        this.OnAICSuperAgentDeleteAICWorkspaceReceived(payload, bdea);
                        break;
                    
                    case "crudcoordinator.crud.admin.addaicworkspace":
                        this.OnAdminAddAICWorkspaceReceived(payload, bdea);
                        break;
                    
                    case "crudcoordinator.crud.admin.getaicworkspaces":
                        this.OnAdminGetAICWorkspacesReceived(payload, bdea);
                        break;
                    
                    case "crudcoordinator.crud.admin.updateaicworkspace":
                        this.OnAdminUpdateAICWorkspaceReceived(payload, bdea);
                        break;
                    
                    case "crudcoordinator.crud.admin.deleteaicworkspace":
                        this.OnAdminDeleteAICWorkspaceReceived(payload, bdea);
                        break;
                    
                    case "crudcoordinator.crud.aicsuperagent.addrepo":
                        this.OnAICSuperAgentAddRepoReceived(payload, bdea);
                        break;
                    
                    case "crudcoordinator.crud.aicsuperagent.getrepos":
                        this.OnAICSuperAgentGetReposReceived(payload, bdea);
                        break;
                    
                    case "crudcoordinator.crud.aicsuperagent.updaterepo":
                        this.OnAICSuperAgentUpdateRepoReceived(payload, bdea);
                        break;
                    
                    case "crudcoordinator.crud.aicsuperagent.deleterepo":
                        this.OnAICSuperAgentDeleteRepoReceived(payload, bdea);
                        break;
                    
                    case "crudcoordinator.crud.admin.addrepo":
                        this.OnAdminAddRepoReceived(payload, bdea);
                        break;
                    
                    case "crudcoordinator.crud.admin.getrepos":
                        this.OnAdminGetReposReceived(payload, bdea);
                        break;
                    
                    case "crudcoordinator.crud.admin.updaterepo":
                        this.OnAdminUpdateRepoReceived(payload, bdea);
                        break;
                    
                    case "crudcoordinator.crud.admin.deleterepo":
                        this.OnAdminDeleteRepoReceived(payload, bdea);
                        break;
                    
                    case "crudcoordinator.crud.aicsuperagent.addaiccontextcategory":
                        this.OnAICSuperAgentAddAICContextCategoryReceived(payload, bdea);
                        break;
                    
                    case "crudcoordinator.crud.aicsuperagent.getaiccontextcategories":
                        this.OnAICSuperAgentGetAICContextCategoriesReceived(payload, bdea);
                        break;
                    
                    case "crudcoordinator.crud.aicsuperagent.updateaiccontextcategory":
                        this.OnAICSuperAgentUpdateAICContextCategoryReceived(payload, bdea);
                        break;
                    
                    case "crudcoordinator.crud.aicsuperagent.deleteaiccontextcategory":
                        this.OnAICSuperAgentDeleteAICContextCategoryReceived(payload, bdea);
                        break;
                    
                    case "crudcoordinator.crud.admin.addaiccontextcategory":
                        this.OnAdminAddAICContextCategoryReceived(payload, bdea);
                        break;
                    
                    case "crudcoordinator.crud.admin.getaiccontextcategories":
                        this.OnAdminGetAICContextCategoriesReceived(payload, bdea);
                        break;
                    
                    case "crudcoordinator.crud.admin.updateaiccontextcategory":
                        this.OnAdminUpdateAICContextCategoryReceived(payload, bdea);
                        break;
                    
                    case "crudcoordinator.crud.admin.deleteaiccontextcategory":
                        this.OnAdminDeleteAICContextCategoryReceived(payload, bdea);
                        break;
                    
                    case "crudcoordinator.crud.aicsuperagent.addaicplan":
                        this.OnAICSuperAgentAddAICPlanReceived(payload, bdea);
                        break;
                    
                    case "crudcoordinator.crud.aicsuperagent.getaicplans":
                        this.OnAICSuperAgentGetAICPlansReceived(payload, bdea);
                        break;
                    
                    case "crudcoordinator.crud.aicsuperagent.updateaicplan":
                        this.OnAICSuperAgentUpdateAICPlanReceived(payload, bdea);
                        break;
                    
                    case "crudcoordinator.crud.aicsuperagent.deleteaicplan":
                        this.OnAICSuperAgentDeleteAICPlanReceived(payload, bdea);
                        break;
                    
                    case "crudcoordinator.crud.admin.addaicplan":
                        this.OnAdminAddAICPlanReceived(payload, bdea);
                        break;
                    
                    case "crudcoordinator.crud.admin.getaicplans":
                        this.OnAdminGetAICPlansReceived(payload, bdea);
                        break;
                    
                    case "crudcoordinator.crud.admin.updateaicplan":
                        this.OnAdminUpdateAICPlanReceived(payload, bdea);
                        break;
                    
                    case "crudcoordinator.crud.admin.deleteaicplan":
                        this.OnAdminDeleteAICPlanReceived(payload, bdea);
                        break;
                    
                    case "crudcoordinator.crud.aicsuperagent.addaicmodelpricing":
                        this.OnAICSuperAgentAddAICModelPricingReceived(payload, bdea);
                        break;
                    
                    case "crudcoordinator.crud.aicsuperagent.getaicmodelpricings":
                        this.OnAICSuperAgentGetAICModelPricingsReceived(payload, bdea);
                        break;
                    
                    case "crudcoordinator.crud.aicsuperagent.updateaicmodelpricing":
                        this.OnAICSuperAgentUpdateAICModelPricingReceived(payload, bdea);
                        break;
                    
                    case "crudcoordinator.crud.aicsuperagent.deleteaicmodelpricing":
                        this.OnAICSuperAgentDeleteAICModelPricingReceived(payload, bdea);
                        break;
                    
                    case "crudcoordinator.crud.admin.addaicmodelpricing":
                        this.OnAdminAddAICModelPricingReceived(payload, bdea);
                        break;
                    
                    case "crudcoordinator.crud.admin.getaicmodelpricings":
                        this.OnAdminGetAICModelPricingsReceived(payload, bdea);
                        break;
                    
                    case "crudcoordinator.crud.admin.updateaicmodelpricing":
                        this.OnAdminUpdateAICModelPricingReceived(payload, bdea);
                        break;
                    
                    case "crudcoordinator.crud.admin.deleteaicmodelpricing":
                        this.OnAdminDeleteAICModelPricingReceived(payload, bdea);
                        break;
                    
                    case "crudcoordinator.crud.aicsuperagent.addlanguage":
                        this.OnAICSuperAgentAddLanguageReceived(payload, bdea);
                        break;
                    
                    case "crudcoordinator.crud.aicsuperagent.getlanguages":
                        this.OnAICSuperAgentGetLanguagesReceived(payload, bdea);
                        break;
                    
                    case "crudcoordinator.crud.aicsuperagent.updatelanguage":
                        this.OnAICSuperAgentUpdateLanguageReceived(payload, bdea);
                        break;
                    
                    case "crudcoordinator.crud.aicsuperagent.deletelanguage":
                        this.OnAICSuperAgentDeleteLanguageReceived(payload, bdea);
                        break;
                    
                    case "crudcoordinator.crud.admin.addlanguage":
                        this.OnAdminAddLanguageReceived(payload, bdea);
                        break;
                    
                    case "crudcoordinator.crud.admin.getlanguages":
                        this.OnAdminGetLanguagesReceived(payload, bdea);
                        break;
                    
                    case "crudcoordinator.crud.admin.updatelanguage":
                        this.OnAdminUpdateLanguageReceived(payload, bdea);
                        break;
                    
                    case "crudcoordinator.crud.admin.deletelanguage":
                        this.OnAdminDeleteLanguageReceived(payload, bdea);
                        break;
                    
                    case "crudcoordinator.crud.aicsuperagent.addappuserusage":
                        this.OnAICSuperAgentAddAppUserUsageReceived(payload, bdea);
                        break;
                    
                    case "crudcoordinator.crud.aicsuperagent.getappuserusages":
                        this.OnAICSuperAgentGetAppUserUsagesReceived(payload, bdea);
                        break;
                    
                    case "crudcoordinator.crud.aicsuperagent.updateappuserusage":
                        this.OnAICSuperAgentUpdateAppUserUsageReceived(payload, bdea);
                        break;
                    
                    case "crudcoordinator.crud.aicsuperagent.deleteappuserusage":
                        this.OnAICSuperAgentDeleteAppUserUsageReceived(payload, bdea);
                        break;
                    
                    case "crudcoordinator.crud.admin.addappuserusage":
                        this.OnAdminAddAppUserUsageReceived(payload, bdea);
                        break;
                    
                    case "crudcoordinator.crud.admin.getappuserusages":
                        this.OnAdminGetAppUserUsagesReceived(payload, bdea);
                        break;
                    
                    case "crudcoordinator.crud.admin.updateappuserusage":
                        this.OnAdminUpdateAppUserUsageReceived(payload, bdea);
                        break;
                    
                    case "crudcoordinator.crud.admin.deleteappuserusage":
                        this.OnAdminDeleteAppUserUsageReceived(payload, bdea);
                        break;
                    
                    case "crudcoordinator.crud.aicsuperagent.addaiprovider":
                        this.OnAICSuperAgentAddAIProviderReceived(payload, bdea);
                        break;
                    
                    case "crudcoordinator.crud.aicsuperagent.getaiproviders":
                        this.OnAICSuperAgentGetAIProvidersReceived(payload, bdea);
                        break;
                    
                    case "crudcoordinator.crud.aicsuperagent.updateaiprovider":
                        this.OnAICSuperAgentUpdateAIProviderReceived(payload, bdea);
                        break;
                    
                    case "crudcoordinator.crud.aicsuperagent.deleteaiprovider":
                        this.OnAICSuperAgentDeleteAIProviderReceived(payload, bdea);
                        break;
                    
                    case "crudcoordinator.crud.admin.addaiprovider":
                        this.OnAdminAddAIProviderReceived(payload, bdea);
                        break;
                    
                    case "crudcoordinator.crud.admin.getaiproviders":
                        this.OnAdminGetAIProvidersReceived(payload, bdea);
                        break;
                    
                    case "crudcoordinator.crud.admin.updateaiprovider":
                        this.OnAdminUpdateAIProviderReceived(payload, bdea);
                        break;
                    
                    case "crudcoordinator.crud.admin.deleteaiprovider":
                        this.OnAdminDeleteAIProviderReceived(payload, bdea);
                        break;
                    
                    case "crudcoordinator.crud.aicsuperagent.addaicskill":
                        this.OnAICSuperAgentAddAICSkillReceived(payload, bdea);
                        break;
                    
                    case "crudcoordinator.crud.aicsuperagent.getaicskills":
                        this.OnAICSuperAgentGetAICSkillsReceived(payload, bdea);
                        break;
                    
                    case "crudcoordinator.crud.aicsuperagent.updateaicskill":
                        this.OnAICSuperAgentUpdateAICSkillReceived(payload, bdea);
                        break;
                    
                    case "crudcoordinator.crud.aicsuperagent.deleteaicskill":
                        this.OnAICSuperAgentDeleteAICSkillReceived(payload, bdea);
                        break;
                    
                    case "crudcoordinator.crud.admin.addaicskill":
                        this.OnAdminAddAICSkillReceived(payload, bdea);
                        break;
                    
                    case "crudcoordinator.crud.admin.getaicskills":
                        this.OnAdminGetAICSkillsReceived(payload, bdea);
                        break;
                    
                    case "crudcoordinator.crud.admin.updateaicskill":
                        this.OnAdminUpdateAICSkillReceived(payload, bdea);
                        break;
                    
                    case "crudcoordinator.crud.admin.deleteaicskill":
                        this.OnAdminDeleteAICSkillReceived(payload, bdea);
                        break;
                    
                    case "crudcoordinator.crud.aicsuperagent.addaiccontext":
                        this.OnAICSuperAgentAddAICContextReceived(payload, bdea);
                        break;
                    
                    case "crudcoordinator.crud.aicsuperagent.getaiccontexts":
                        this.OnAICSuperAgentGetAICContextsReceived(payload, bdea);
                        break;
                    
                    case "crudcoordinator.crud.aicsuperagent.updateaiccontext":
                        this.OnAICSuperAgentUpdateAICContextReceived(payload, bdea);
                        break;
                    
                    case "crudcoordinator.crud.aicsuperagent.deleteaiccontext":
                        this.OnAICSuperAgentDeleteAICContextReceived(payload, bdea);
                        break;
                    
                    case "crudcoordinator.crud.admin.addaiccontext":
                        this.OnAdminAddAICContextReceived(payload, bdea);
                        break;
                    
                    case "crudcoordinator.crud.admin.getaiccontexts":
                        this.OnAdminGetAICContextsReceived(payload, bdea);
                        break;
                    
                    case "crudcoordinator.crud.admin.updateaiccontext":
                        this.OnAdminUpdateAICContextReceived(payload, bdea);
                        break;
                    
                    case "crudcoordinator.crud.admin.deleteaiccontext":
                        this.OnAdminDeleteAICContextReceived(payload, bdea);
                        break;
                    
                    case "crudcoordinator.crud.aicsuperagent.addfriendrequest":
                        this.OnAICSuperAgentAddFriendRequestReceived(payload, bdea);
                        break;
                    
                    case "crudcoordinator.crud.aicsuperagent.getfriendrequests":
                        this.OnAICSuperAgentGetFriendRequestsReceived(payload, bdea);
                        break;
                    
                    case "crudcoordinator.crud.aicsuperagent.updatefriendrequest":
                        this.OnAICSuperAgentUpdateFriendRequestReceived(payload, bdea);
                        break;
                    
                    case "crudcoordinator.crud.aicsuperagent.deletefriendrequest":
                        this.OnAICSuperAgentDeleteFriendRequestReceived(payload, bdea);
                        break;
                    
                    case "crudcoordinator.crud.admin.addfriendrequest":
                        this.OnAdminAddFriendRequestReceived(payload, bdea);
                        break;
                    
                    case "crudcoordinator.crud.admin.getfriendrequests":
                        this.OnAdminGetFriendRequestsReceived(payload, bdea);
                        break;
                    
                    case "crudcoordinator.crud.admin.updatefriendrequest":
                        this.OnAdminUpdateFriendRequestReceived(payload, bdea);
                        break;
                    
                    case "crudcoordinator.crud.admin.deletefriendrequest":
                        this.OnAdminDeleteFriendRequestReceived(payload, bdea);
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
                    
                    case "crudcoordinator.crud.aicsuperagent.addaicconversation":
                        this.OnAICSuperAgentAddAICConversationReceived(payload, bdea);
                        break;
                    
                    case "crudcoordinator.crud.aicsuperagent.getaicconversations":
                        this.OnAICSuperAgentGetAICConversationsReceived(payload, bdea);
                        break;
                    
                    case "crudcoordinator.crud.aicsuperagent.updateaicconversation":
                        this.OnAICSuperAgentUpdateAICConversationReceived(payload, bdea);
                        break;
                    
                    case "crudcoordinator.crud.aicsuperagent.deleteaicconversation":
                        this.OnAICSuperAgentDeleteAICConversationReceived(payload, bdea);
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
                    
                    case "crudcoordinator.crud.aicsuperagent.addentity":
                        this.OnAICSuperAgentAddEntityReceived(payload, bdea);
                        break;
                    
                    case "crudcoordinator.crud.aicsuperagent.getentities":
                        this.OnAICSuperAgentGetEntitiesReceived(payload, bdea);
                        break;
                    
                    case "crudcoordinator.crud.aicsuperagent.updateentity":
                        this.OnAICSuperAgentUpdateEntityReceived(payload, bdea);
                        break;
                    
                    case "crudcoordinator.crud.aicsuperagent.deleteentity":
                        this.OnAICSuperAgentDeleteEntityReceived(payload, bdea);
                        break;
                    
                    case "crudcoordinator.crud.admin.addentity":
                        this.OnAdminAddEntityReceived(payload, bdea);
                        break;
                    
                    case "crudcoordinator.crud.admin.getentities":
                        this.OnAdminGetEntitiesReceived(payload, bdea);
                        break;
                    
                    case "crudcoordinator.crud.admin.updateentity":
                        this.OnAdminUpdateEntityReceived(payload, bdea);
                        break;
                    
                    case "crudcoordinator.crud.admin.deleteentity":
                        this.OnAdminDeleteEntityReceived(payload, bdea);
                        break;
                    
                    case "crudcoordinator.crud.aicsuperagent.addaicskillversion":
                        this.OnAICSuperAgentAddAICSkillVersionReceived(payload, bdea);
                        break;
                    
                    case "crudcoordinator.crud.aicsuperagent.getaicskillversions":
                        this.OnAICSuperAgentGetAICSkillVersionsReceived(payload, bdea);
                        break;
                    
                    case "crudcoordinator.crud.aicsuperagent.updateaicskillversion":
                        this.OnAICSuperAgentUpdateAICSkillVersionReceived(payload, bdea);
                        break;
                    
                    case "crudcoordinator.crud.aicsuperagent.deleteaicskillversion":
                        this.OnAICSuperAgentDeleteAICSkillVersionReceived(payload, bdea);
                        break;
                    
                    case "crudcoordinator.crud.admin.addaicskillversion":
                        this.OnAdminAddAICSkillVersionReceived(payload, bdea);
                        break;
                    
                    case "crudcoordinator.crud.admin.getaicskillversions":
                        this.OnAdminGetAICSkillVersionsReceived(payload, bdea);
                        break;
                    
                    case "crudcoordinator.crud.admin.updateaicskillversion":
                        this.OnAdminUpdateAICSkillVersionReceived(payload, bdea);
                        break;
                    
                    case "crudcoordinator.crud.admin.deleteaicskillversion":
                        this.OnAdminDeleteAICSkillVersionReceived(payload, bdea);
                        break;
                    
                    case "crudcoordinator.crud.aicsuperagent.addaicmessage":
                        this.OnAICSuperAgentAddAICMessageReceived(payload, bdea);
                        break;
                    
                    case "crudcoordinator.crud.aicsuperagent.getaicmessages":
                        this.OnAICSuperAgentGetAICMessagesReceived(payload, bdea);
                        break;
                    
                    case "crudcoordinator.crud.aicsuperagent.updateaicmessage":
                        this.OnAICSuperAgentUpdateAICMessageReceived(payload, bdea);
                        break;
                    
                    case "crudcoordinator.crud.aicsuperagent.deleteaicmessage":
                        this.OnAICSuperAgentDeleteAICMessageReceived(payload, bdea);
                        break;
                    
                    case "crudcoordinator.crud.admin.addaicmessage":
                        this.OnAdminAddAICMessageReceived(payload, bdea);
                        break;
                    
                    case "crudcoordinator.crud.admin.getaicmessages":
                        this.OnAdminGetAICMessagesReceived(payload, bdea);
                        break;
                    
                    case "crudcoordinator.crud.admin.updateaicmessage":
                        this.OnAdminUpdateAICMessageReceived(payload, bdea);
                        break;
                    
                    case "crudcoordinator.crud.admin.deleteaicmessage":
                        this.OnAdminDeleteAICMessageReceived(payload, bdea);
                        break;
                    
                    case "crudcoordinator.crud.aicsuperagent.addrepobranch":
                        this.OnAICSuperAgentAddRepoBranchReceived(payload, bdea);
                        break;
                    
                    case "crudcoordinator.crud.aicsuperagent.getrepobranches":
                        this.OnAICSuperAgentGetRepoBranchesReceived(payload, bdea);
                        break;
                    
                    case "crudcoordinator.crud.aicsuperagent.updaterepobranch":
                        this.OnAICSuperAgentUpdateRepoBranchReceived(payload, bdea);
                        break;
                    
                    case "crudcoordinator.crud.aicsuperagent.deleterepobranch":
                        this.OnAICSuperAgentDeleteRepoBranchReceived(payload, bdea);
                        break;
                    
                    case "crudcoordinator.crud.admin.addrepobranch":
                        this.OnAdminAddRepoBranchReceived(payload, bdea);
                        break;
                    
                    case "crudcoordinator.crud.admin.getrepobranches":
                        this.OnAdminGetRepoBranchesReceived(payload, bdea);
                        break;
                    
                    case "crudcoordinator.crud.admin.updaterepobranch":
                        this.OnAdminUpdateRepoBranchReceived(payload, bdea);
                        break;
                    
                    case "crudcoordinator.crud.admin.deleterepobranch":
                        this.OnAdminDeleteRepoBranchReceived(payload, bdea);
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
                    
                    case "crudcoordinator.crud.aicsuperagent.addaicproject":
                        this.OnAICSuperAgentAddAICProjectReceived(payload, bdea);
                        break;
                    
                    case "crudcoordinator.crud.aicsuperagent.getaicprojects":
                        this.OnAICSuperAgentGetAICProjectsReceived(payload, bdea);
                        break;
                    
                    case "crudcoordinator.crud.aicsuperagent.updateaicproject":
                        this.OnAICSuperAgentUpdateAICProjectReceived(payload, bdea);
                        break;
                    
                    case "crudcoordinator.crud.aicsuperagent.deleteaicproject":
                        this.OnAICSuperAgentDeleteAICProjectReceived(payload, bdea);
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
                    
                    case "crudcoordinator.crud.aicsuperagent.addaicfile":
                        this.OnAICSuperAgentAddAICFileReceived(payload, bdea);
                        break;
                    
                    case "crudcoordinator.crud.aicsuperagent.getaicfiles":
                        this.OnAICSuperAgentGetAICFilesReceived(payload, bdea);
                        break;
                    
                    case "crudcoordinator.crud.aicsuperagent.updateaicfile":
                        this.OnAICSuperAgentUpdateAICFileReceived(payload, bdea);
                        break;
                    
                    case "crudcoordinator.crud.aicsuperagent.deleteaicfile":
                        this.OnAICSuperAgentDeleteAICFileReceived(payload, bdea);
                        break;
                    
                    case "crudcoordinator.crud.admin.addaicfile":
                        this.OnAdminAddAICFileReceived(payload, bdea);
                        break;
                    
                    case "crudcoordinator.crud.admin.getaicfiles":
                        this.OnAdminGetAICFilesReceived(payload, bdea);
                        break;
                    
                    case "crudcoordinator.crud.admin.updateaicfile":
                        this.OnAdminUpdateAICFileReceived(payload, bdea);
                        break;
                    
                    case "crudcoordinator.crud.admin.deleteaicfile":
                        this.OnAdminDeleteAICFileReceived(payload, bdea);
                        break;
                    
                    case "crudcoordinator.crud.aicsuperagent.addaicskillstep":
                        this.OnAICSuperAgentAddAICSkillStepReceived(payload, bdea);
                        break;
                    
                    case "crudcoordinator.crud.aicsuperagent.getaicskillsteps":
                        this.OnAICSuperAgentGetAICSkillStepsReceived(payload, bdea);
                        break;
                    
                    case "crudcoordinator.crud.aicsuperagent.updateaicskillstep":
                        this.OnAICSuperAgentUpdateAICSkillStepReceived(payload, bdea);
                        break;
                    
                    case "crudcoordinator.crud.aicsuperagent.deleteaicskillstep":
                        this.OnAICSuperAgentDeleteAICSkillStepReceived(payload, bdea);
                        break;
                    
                    case "crudcoordinator.crud.admin.addaicskillstep":
                        this.OnAdminAddAICSkillStepReceived(payload, bdea);
                        break;
                    
                    case "crudcoordinator.crud.admin.getaicskillsteps":
                        this.OnAdminGetAICSkillStepsReceived(payload, bdea);
                        break;
                    
                    case "crudcoordinator.crud.admin.updateaicskillstep":
                        this.OnAdminUpdateAICSkillStepReceived(payload, bdea);
                        break;
                    
                    case "crudcoordinator.crud.admin.deleteaicskillstep":
                        this.OnAdminDeleteAICSkillStepReceived(payload, bdea);
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
        /// Responds to: NotifyMeHere from User
        /// </summary>
        public event EventHandler<PayloadEventArgs> UserNotifyMeHereReceived;
        protected virtual void OnUserNotifyMeHereReceived(StandardPayload payload, BasicDeliverEventArgs bdea)
        {
            var plea = new PayloadEventArgs(payload, bdea);
            if (!ReferenceEquals(this.UserNotifyMeHereReceived, null))
            {
                plea.Payload.IsHandled = true;
                this.UserNotifyMeHereReceived(this, plea);
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
        /// Responds to: AddHelpfulPrompt from AICSuperAgent
        /// </summary>
        public event EventHandler<PayloadEventArgs> AICSuperAgentAddHelpfulPromptReceived;
        protected virtual void OnAICSuperAgentAddHelpfulPromptReceived(StandardPayload payload, BasicDeliverEventArgs bdea)
        {
            var plea = new PayloadEventArgs(payload, bdea);
            if (!ReferenceEquals(this.AICSuperAgentAddHelpfulPromptReceived, null))
            {
                plea.Payload.IsHandled = true;
                this.AICSuperAgentAddHelpfulPromptReceived(this, plea);
            }
        }

        /// <summary>
        /// Responds to: GetHelpfulPrompts from AICSuperAgent
        /// </summary>
        public event EventHandler<PayloadEventArgs> AICSuperAgentGetHelpfulPromptsReceived;
        protected virtual void OnAICSuperAgentGetHelpfulPromptsReceived(StandardPayload payload, BasicDeliverEventArgs bdea)
        {
            var plea = new PayloadEventArgs(payload, bdea);
            if (!ReferenceEquals(this.AICSuperAgentGetHelpfulPromptsReceived, null))
            {
                plea.Payload.IsHandled = true;
                this.AICSuperAgentGetHelpfulPromptsReceived(this, plea);
            }
        }

        /// <summary>
        /// Responds to: UpdateHelpfulPrompt from AICSuperAgent
        /// </summary>
        public event EventHandler<PayloadEventArgs> AICSuperAgentUpdateHelpfulPromptReceived;
        protected virtual void OnAICSuperAgentUpdateHelpfulPromptReceived(StandardPayload payload, BasicDeliverEventArgs bdea)
        {
            var plea = new PayloadEventArgs(payload, bdea);
            if (!ReferenceEquals(this.AICSuperAgentUpdateHelpfulPromptReceived, null))
            {
                plea.Payload.IsHandled = true;
                this.AICSuperAgentUpdateHelpfulPromptReceived(this, plea);
            }
        }

        /// <summary>
        /// Responds to: DeleteHelpfulPrompt from AICSuperAgent
        /// </summary>
        public event EventHandler<PayloadEventArgs> AICSuperAgentDeleteHelpfulPromptReceived;
        protected virtual void OnAICSuperAgentDeleteHelpfulPromptReceived(StandardPayload payload, BasicDeliverEventArgs bdea)
        {
            var plea = new PayloadEventArgs(payload, bdea);
            if (!ReferenceEquals(this.AICSuperAgentDeleteHelpfulPromptReceived, null))
            {
                plea.Payload.IsHandled = true;
                this.AICSuperAgentDeleteHelpfulPromptReceived(this, plea);
            }
        }

        /// <summary>
        /// Responds to: AddHelpfulPrompt from Admin
        /// </summary>
        public event EventHandler<PayloadEventArgs> AdminAddHelpfulPromptReceived;
        protected virtual void OnAdminAddHelpfulPromptReceived(StandardPayload payload, BasicDeliverEventArgs bdea)
        {
            var plea = new PayloadEventArgs(payload, bdea);
            if (!ReferenceEquals(this.AdminAddHelpfulPromptReceived, null))
            {
                plea.Payload.IsHandled = true;
                this.AdminAddHelpfulPromptReceived(this, plea);
            }
        }

        /// <summary>
        /// Responds to: GetHelpfulPrompts from Admin
        /// </summary>
        public event EventHandler<PayloadEventArgs> AdminGetHelpfulPromptsReceived;
        protected virtual void OnAdminGetHelpfulPromptsReceived(StandardPayload payload, BasicDeliverEventArgs bdea)
        {
            var plea = new PayloadEventArgs(payload, bdea);
            if (!ReferenceEquals(this.AdminGetHelpfulPromptsReceived, null))
            {
                plea.Payload.IsHandled = true;
                this.AdminGetHelpfulPromptsReceived(this, plea);
            }
        }

        /// <summary>
        /// Responds to: UpdateHelpfulPrompt from Admin
        /// </summary>
        public event EventHandler<PayloadEventArgs> AdminUpdateHelpfulPromptReceived;
        protected virtual void OnAdminUpdateHelpfulPromptReceived(StandardPayload payload, BasicDeliverEventArgs bdea)
        {
            var plea = new PayloadEventArgs(payload, bdea);
            if (!ReferenceEquals(this.AdminUpdateHelpfulPromptReceived, null))
            {
                plea.Payload.IsHandled = true;
                this.AdminUpdateHelpfulPromptReceived(this, plea);
            }
        }

        /// <summary>
        /// Responds to: DeleteHelpfulPrompt from Admin
        /// </summary>
        public event EventHandler<PayloadEventArgs> AdminDeleteHelpfulPromptReceived;
        protected virtual void OnAdminDeleteHelpfulPromptReceived(StandardPayload payload, BasicDeliverEventArgs bdea)
        {
            var plea = new PayloadEventArgs(payload, bdea);
            if (!ReferenceEquals(this.AdminDeleteHelpfulPromptReceived, null))
            {
                plea.Payload.IsHandled = true;
                this.AdminDeleteHelpfulPromptReceived(this, plea);
            }
        }

        /// <summary>
        /// Responds to: AddAIModel from AICSuperAgent
        /// </summary>
        public event EventHandler<PayloadEventArgs> AICSuperAgentAddAIModelReceived;
        protected virtual void OnAICSuperAgentAddAIModelReceived(StandardPayload payload, BasicDeliverEventArgs bdea)
        {
            var plea = new PayloadEventArgs(payload, bdea);
            if (!ReferenceEquals(this.AICSuperAgentAddAIModelReceived, null))
            {
                plea.Payload.IsHandled = true;
                this.AICSuperAgentAddAIModelReceived(this, plea);
            }
        }

        /// <summary>
        /// Responds to: GetAIModels from AICSuperAgent
        /// </summary>
        public event EventHandler<PayloadEventArgs> AICSuperAgentGetAIModelsReceived;
        protected virtual void OnAICSuperAgentGetAIModelsReceived(StandardPayload payload, BasicDeliverEventArgs bdea)
        {
            var plea = new PayloadEventArgs(payload, bdea);
            if (!ReferenceEquals(this.AICSuperAgentGetAIModelsReceived, null))
            {
                plea.Payload.IsHandled = true;
                this.AICSuperAgentGetAIModelsReceived(this, plea);
            }
        }

        /// <summary>
        /// Responds to: UpdateAIModel from AICSuperAgent
        /// </summary>
        public event EventHandler<PayloadEventArgs> AICSuperAgentUpdateAIModelReceived;
        protected virtual void OnAICSuperAgentUpdateAIModelReceived(StandardPayload payload, BasicDeliverEventArgs bdea)
        {
            var plea = new PayloadEventArgs(payload, bdea);
            if (!ReferenceEquals(this.AICSuperAgentUpdateAIModelReceived, null))
            {
                plea.Payload.IsHandled = true;
                this.AICSuperAgentUpdateAIModelReceived(this, plea);
            }
        }

        /// <summary>
        /// Responds to: DeleteAIModel from AICSuperAgent
        /// </summary>
        public event EventHandler<PayloadEventArgs> AICSuperAgentDeleteAIModelReceived;
        protected virtual void OnAICSuperAgentDeleteAIModelReceived(StandardPayload payload, BasicDeliverEventArgs bdea)
        {
            var plea = new PayloadEventArgs(payload, bdea);
            if (!ReferenceEquals(this.AICSuperAgentDeleteAIModelReceived, null))
            {
                plea.Payload.IsHandled = true;
                this.AICSuperAgentDeleteAIModelReceived(this, plea);
            }
        }

        /// <summary>
        /// Responds to: AddAIModel from Admin
        /// </summary>
        public event EventHandler<PayloadEventArgs> AdminAddAIModelReceived;
        protected virtual void OnAdminAddAIModelReceived(StandardPayload payload, BasicDeliverEventArgs bdea)
        {
            var plea = new PayloadEventArgs(payload, bdea);
            if (!ReferenceEquals(this.AdminAddAIModelReceived, null))
            {
                plea.Payload.IsHandled = true;
                this.AdminAddAIModelReceived(this, plea);
            }
        }

        /// <summary>
        /// Responds to: GetAIModels from Admin
        /// </summary>
        public event EventHandler<PayloadEventArgs> AdminGetAIModelsReceived;
        protected virtual void OnAdminGetAIModelsReceived(StandardPayload payload, BasicDeliverEventArgs bdea)
        {
            var plea = new PayloadEventArgs(payload, bdea);
            if (!ReferenceEquals(this.AdminGetAIModelsReceived, null))
            {
                plea.Payload.IsHandled = true;
                this.AdminGetAIModelsReceived(this, plea);
            }
        }

        /// <summary>
        /// Responds to: UpdateAIModel from Admin
        /// </summary>
        public event EventHandler<PayloadEventArgs> AdminUpdateAIModelReceived;
        protected virtual void OnAdminUpdateAIModelReceived(StandardPayload payload, BasicDeliverEventArgs bdea)
        {
            var plea = new PayloadEventArgs(payload, bdea);
            if (!ReferenceEquals(this.AdminUpdateAIModelReceived, null))
            {
                plea.Payload.IsHandled = true;
                this.AdminUpdateAIModelReceived(this, plea);
            }
        }

        /// <summary>
        /// Responds to: DeleteAIModel from Admin
        /// </summary>
        public event EventHandler<PayloadEventArgs> AdminDeleteAIModelReceived;
        protected virtual void OnAdminDeleteAIModelReceived(StandardPayload payload, BasicDeliverEventArgs bdea)
        {
            var plea = new PayloadEventArgs(payload, bdea);
            if (!ReferenceEquals(this.AdminDeleteAIModelReceived, null))
            {
                plea.Payload.IsHandled = true;
                this.AdminDeleteAIModelReceived(this, plea);
            }
        }

        /// <summary>
        /// Responds to: GetSharedDirectories from User
        /// </summary>
        public event EventHandler<PayloadEventArgs> UserGetSharedDirectoriesReceived;
        protected virtual void OnUserGetSharedDirectoriesReceived(StandardPayload payload, BasicDeliverEventArgs bdea)
        {
            var plea = new PayloadEventArgs(payload, bdea);
            if (!ReferenceEquals(this.UserGetSharedDirectoriesReceived, null))
            {
                plea.Payload.IsHandled = true;
                this.UserGetSharedDirectoriesReceived(this, plea);
            }
        }

        /// <summary>
        /// Responds to: AddSharedDirectory from AICSuperAgent
        /// </summary>
        public event EventHandler<PayloadEventArgs> AICSuperAgentAddSharedDirectoryReceived;
        protected virtual void OnAICSuperAgentAddSharedDirectoryReceived(StandardPayload payload, BasicDeliverEventArgs bdea)
        {
            var plea = new PayloadEventArgs(payload, bdea);
            if (!ReferenceEquals(this.AICSuperAgentAddSharedDirectoryReceived, null))
            {
                plea.Payload.IsHandled = true;
                this.AICSuperAgentAddSharedDirectoryReceived(this, plea);
            }
        }

        /// <summary>
        /// Responds to: GetSharedDirectories from AICSuperAgent
        /// </summary>
        public event EventHandler<PayloadEventArgs> AICSuperAgentGetSharedDirectoriesReceived;
        protected virtual void OnAICSuperAgentGetSharedDirectoriesReceived(StandardPayload payload, BasicDeliverEventArgs bdea)
        {
            var plea = new PayloadEventArgs(payload, bdea);
            if (!ReferenceEquals(this.AICSuperAgentGetSharedDirectoriesReceived, null))
            {
                plea.Payload.IsHandled = true;
                this.AICSuperAgentGetSharedDirectoriesReceived(this, plea);
            }
        }

        /// <summary>
        /// Responds to: UpdateSharedDirectory from AICSuperAgent
        /// </summary>
        public event EventHandler<PayloadEventArgs> AICSuperAgentUpdateSharedDirectoryReceived;
        protected virtual void OnAICSuperAgentUpdateSharedDirectoryReceived(StandardPayload payload, BasicDeliverEventArgs bdea)
        {
            var plea = new PayloadEventArgs(payload, bdea);
            if (!ReferenceEquals(this.AICSuperAgentUpdateSharedDirectoryReceived, null))
            {
                plea.Payload.IsHandled = true;
                this.AICSuperAgentUpdateSharedDirectoryReceived(this, plea);
            }
        }

        /// <summary>
        /// Responds to: DeleteSharedDirectory from AICSuperAgent
        /// </summary>
        public event EventHandler<PayloadEventArgs> AICSuperAgentDeleteSharedDirectoryReceived;
        protected virtual void OnAICSuperAgentDeleteSharedDirectoryReceived(StandardPayload payload, BasicDeliverEventArgs bdea)
        {
            var plea = new PayloadEventArgs(payload, bdea);
            if (!ReferenceEquals(this.AICSuperAgentDeleteSharedDirectoryReceived, null))
            {
                plea.Payload.IsHandled = true;
                this.AICSuperAgentDeleteSharedDirectoryReceived(this, plea);
            }
        }

        /// <summary>
        /// Responds to: AddSharedDirectory from Admin
        /// </summary>
        public event EventHandler<PayloadEventArgs> AdminAddSharedDirectoryReceived;
        protected virtual void OnAdminAddSharedDirectoryReceived(StandardPayload payload, BasicDeliverEventArgs bdea)
        {
            var plea = new PayloadEventArgs(payload, bdea);
            if (!ReferenceEquals(this.AdminAddSharedDirectoryReceived, null))
            {
                plea.Payload.IsHandled = true;
                this.AdminAddSharedDirectoryReceived(this, plea);
            }
        }

        /// <summary>
        /// Responds to: GetSharedDirectories from Admin
        /// </summary>
        public event EventHandler<PayloadEventArgs> AdminGetSharedDirectoriesReceived;
        protected virtual void OnAdminGetSharedDirectoriesReceived(StandardPayload payload, BasicDeliverEventArgs bdea)
        {
            var plea = new PayloadEventArgs(payload, bdea);
            if (!ReferenceEquals(this.AdminGetSharedDirectoriesReceived, null))
            {
                plea.Payload.IsHandled = true;
                this.AdminGetSharedDirectoriesReceived(this, plea);
            }
        }

        /// <summary>
        /// Responds to: UpdateSharedDirectory from Admin
        /// </summary>
        public event EventHandler<PayloadEventArgs> AdminUpdateSharedDirectoryReceived;
        protected virtual void OnAdminUpdateSharedDirectoryReceived(StandardPayload payload, BasicDeliverEventArgs bdea)
        {
            var plea = new PayloadEventArgs(payload, bdea);
            if (!ReferenceEquals(this.AdminUpdateSharedDirectoryReceived, null))
            {
                plea.Payload.IsHandled = true;
                this.AdminUpdateSharedDirectoryReceived(this, plea);
            }
        }

        /// <summary>
        /// Responds to: DeleteSharedDirectory from Admin
        /// </summary>
        public event EventHandler<PayloadEventArgs> AdminDeleteSharedDirectoryReceived;
        protected virtual void OnAdminDeleteSharedDirectoryReceived(StandardPayload payload, BasicDeliverEventArgs bdea)
        {
            var plea = new PayloadEventArgs(payload, bdea);
            if (!ReferenceEquals(this.AdminDeleteSharedDirectoryReceived, null))
            {
                plea.Payload.IsHandled = true;
                this.AdminDeleteSharedDirectoryReceived(this, plea);
            }
        }

        /// <summary>
        /// Responds to: AddAICDirectory from AICSuperAgent
        /// </summary>
        public event EventHandler<PayloadEventArgs> AICSuperAgentAddAICDirectoryReceived;
        protected virtual void OnAICSuperAgentAddAICDirectoryReceived(StandardPayload payload, BasicDeliverEventArgs bdea)
        {
            var plea = new PayloadEventArgs(payload, bdea);
            if (!ReferenceEquals(this.AICSuperAgentAddAICDirectoryReceived, null))
            {
                plea.Payload.IsHandled = true;
                this.AICSuperAgentAddAICDirectoryReceived(this, plea);
            }
        }

        /// <summary>
        /// Responds to: GetAICDirectories from AICSuperAgent
        /// </summary>
        public event EventHandler<PayloadEventArgs> AICSuperAgentGetAICDirectoriesReceived;
        protected virtual void OnAICSuperAgentGetAICDirectoriesReceived(StandardPayload payload, BasicDeliverEventArgs bdea)
        {
            var plea = new PayloadEventArgs(payload, bdea);
            if (!ReferenceEquals(this.AICSuperAgentGetAICDirectoriesReceived, null))
            {
                plea.Payload.IsHandled = true;
                this.AICSuperAgentGetAICDirectoriesReceived(this, plea);
            }
        }

        /// <summary>
        /// Responds to: UpdateAICDirectory from AICSuperAgent
        /// </summary>
        public event EventHandler<PayloadEventArgs> AICSuperAgentUpdateAICDirectoryReceived;
        protected virtual void OnAICSuperAgentUpdateAICDirectoryReceived(StandardPayload payload, BasicDeliverEventArgs bdea)
        {
            var plea = new PayloadEventArgs(payload, bdea);
            if (!ReferenceEquals(this.AICSuperAgentUpdateAICDirectoryReceived, null))
            {
                plea.Payload.IsHandled = true;
                this.AICSuperAgentUpdateAICDirectoryReceived(this, plea);
            }
        }

        /// <summary>
        /// Responds to: DeleteAICDirectory from AICSuperAgent
        /// </summary>
        public event EventHandler<PayloadEventArgs> AICSuperAgentDeleteAICDirectoryReceived;
        protected virtual void OnAICSuperAgentDeleteAICDirectoryReceived(StandardPayload payload, BasicDeliverEventArgs bdea)
        {
            var plea = new PayloadEventArgs(payload, bdea);
            if (!ReferenceEquals(this.AICSuperAgentDeleteAICDirectoryReceived, null))
            {
                plea.Payload.IsHandled = true;
                this.AICSuperAgentDeleteAICDirectoryReceived(this, plea);
            }
        }

        /// <summary>
        /// Responds to: AddAICDirectory from Admin
        /// </summary>
        public event EventHandler<PayloadEventArgs> AdminAddAICDirectoryReceived;
        protected virtual void OnAdminAddAICDirectoryReceived(StandardPayload payload, BasicDeliverEventArgs bdea)
        {
            var plea = new PayloadEventArgs(payload, bdea);
            if (!ReferenceEquals(this.AdminAddAICDirectoryReceived, null))
            {
                plea.Payload.IsHandled = true;
                this.AdminAddAICDirectoryReceived(this, plea);
            }
        }

        /// <summary>
        /// Responds to: GetAICDirectories from Admin
        /// </summary>
        public event EventHandler<PayloadEventArgs> AdminGetAICDirectoriesReceived;
        protected virtual void OnAdminGetAICDirectoriesReceived(StandardPayload payload, BasicDeliverEventArgs bdea)
        {
            var plea = new PayloadEventArgs(payload, bdea);
            if (!ReferenceEquals(this.AdminGetAICDirectoriesReceived, null))
            {
                plea.Payload.IsHandled = true;
                this.AdminGetAICDirectoriesReceived(this, plea);
            }
        }

        /// <summary>
        /// Responds to: UpdateAICDirectory from Admin
        /// </summary>
        public event EventHandler<PayloadEventArgs> AdminUpdateAICDirectoryReceived;
        protected virtual void OnAdminUpdateAICDirectoryReceived(StandardPayload payload, BasicDeliverEventArgs bdea)
        {
            var plea = new PayloadEventArgs(payload, bdea);
            if (!ReferenceEquals(this.AdminUpdateAICDirectoryReceived, null))
            {
                plea.Payload.IsHandled = true;
                this.AdminUpdateAICDirectoryReceived(this, plea);
            }
        }

        /// <summary>
        /// Responds to: DeleteAICDirectory from Admin
        /// </summary>
        public event EventHandler<PayloadEventArgs> AdminDeleteAICDirectoryReceived;
        protected virtual void OnAdminDeleteAICDirectoryReceived(StandardPayload payload, BasicDeliverEventArgs bdea)
        {
            var plea = new PayloadEventArgs(payload, bdea);
            if (!ReferenceEquals(this.AdminDeleteAICDirectoryReceived, null))
            {
                plea.Payload.IsHandled = true;
                this.AdminDeleteAICDirectoryReceived(this, plea);
            }
        }

        /// <summary>
        /// Responds to: AddRateLimit from AICSuperAgent
        /// </summary>
        public event EventHandler<PayloadEventArgs> AICSuperAgentAddRateLimitReceived;
        protected virtual void OnAICSuperAgentAddRateLimitReceived(StandardPayload payload, BasicDeliverEventArgs bdea)
        {
            var plea = new PayloadEventArgs(payload, bdea);
            if (!ReferenceEquals(this.AICSuperAgentAddRateLimitReceived, null))
            {
                plea.Payload.IsHandled = true;
                this.AICSuperAgentAddRateLimitReceived(this, plea);
            }
        }

        /// <summary>
        /// Responds to: GetRateLimits from AICSuperAgent
        /// </summary>
        public event EventHandler<PayloadEventArgs> AICSuperAgentGetRateLimitsReceived;
        protected virtual void OnAICSuperAgentGetRateLimitsReceived(StandardPayload payload, BasicDeliverEventArgs bdea)
        {
            var plea = new PayloadEventArgs(payload, bdea);
            if (!ReferenceEquals(this.AICSuperAgentGetRateLimitsReceived, null))
            {
                plea.Payload.IsHandled = true;
                this.AICSuperAgentGetRateLimitsReceived(this, plea);
            }
        }

        /// <summary>
        /// Responds to: UpdateRateLimit from AICSuperAgent
        /// </summary>
        public event EventHandler<PayloadEventArgs> AICSuperAgentUpdateRateLimitReceived;
        protected virtual void OnAICSuperAgentUpdateRateLimitReceived(StandardPayload payload, BasicDeliverEventArgs bdea)
        {
            var plea = new PayloadEventArgs(payload, bdea);
            if (!ReferenceEquals(this.AICSuperAgentUpdateRateLimitReceived, null))
            {
                plea.Payload.IsHandled = true;
                this.AICSuperAgentUpdateRateLimitReceived(this, plea);
            }
        }

        /// <summary>
        /// Responds to: DeleteRateLimit from AICSuperAgent
        /// </summary>
        public event EventHandler<PayloadEventArgs> AICSuperAgentDeleteRateLimitReceived;
        protected virtual void OnAICSuperAgentDeleteRateLimitReceived(StandardPayload payload, BasicDeliverEventArgs bdea)
        {
            var plea = new PayloadEventArgs(payload, bdea);
            if (!ReferenceEquals(this.AICSuperAgentDeleteRateLimitReceived, null))
            {
                plea.Payload.IsHandled = true;
                this.AICSuperAgentDeleteRateLimitReceived(this, plea);
            }
        }

        /// <summary>
        /// Responds to: AddRateLimit from Admin
        /// </summary>
        public event EventHandler<PayloadEventArgs> AdminAddRateLimitReceived;
        protected virtual void OnAdminAddRateLimitReceived(StandardPayload payload, BasicDeliverEventArgs bdea)
        {
            var plea = new PayloadEventArgs(payload, bdea);
            if (!ReferenceEquals(this.AdminAddRateLimitReceived, null))
            {
                plea.Payload.IsHandled = true;
                this.AdminAddRateLimitReceived(this, plea);
            }
        }

        /// <summary>
        /// Responds to: GetRateLimits from Admin
        /// </summary>
        public event EventHandler<PayloadEventArgs> AdminGetRateLimitsReceived;
        protected virtual void OnAdminGetRateLimitsReceived(StandardPayload payload, BasicDeliverEventArgs bdea)
        {
            var plea = new PayloadEventArgs(payload, bdea);
            if (!ReferenceEquals(this.AdminGetRateLimitsReceived, null))
            {
                plea.Payload.IsHandled = true;
                this.AdminGetRateLimitsReceived(this, plea);
            }
        }

        /// <summary>
        /// Responds to: UpdateRateLimit from Admin
        /// </summary>
        public event EventHandler<PayloadEventArgs> AdminUpdateRateLimitReceived;
        protected virtual void OnAdminUpdateRateLimitReceived(StandardPayload payload, BasicDeliverEventArgs bdea)
        {
            var plea = new PayloadEventArgs(payload, bdea);
            if (!ReferenceEquals(this.AdminUpdateRateLimitReceived, null))
            {
                plea.Payload.IsHandled = true;
                this.AdminUpdateRateLimitReceived(this, plea);
            }
        }

        /// <summary>
        /// Responds to: DeleteRateLimit from Admin
        /// </summary>
        public event EventHandler<PayloadEventArgs> AdminDeleteRateLimitReceived;
        protected virtual void OnAdminDeleteRateLimitReceived(StandardPayload payload, BasicDeliverEventArgs bdea)
        {
            var plea = new PayloadEventArgs(payload, bdea);
            if (!ReferenceEquals(this.AdminDeleteRateLimitReceived, null))
            {
                plea.Payload.IsHandled = true;
                this.AdminDeleteRateLimitReceived(this, plea);
            }
        }

        /// <summary>
        /// Responds to: AddAppUser from AICSuperAgent
        /// </summary>
        public event EventHandler<PayloadEventArgs> AICSuperAgentAddAppUserReceived;
        protected virtual void OnAICSuperAgentAddAppUserReceived(StandardPayload payload, BasicDeliverEventArgs bdea)
        {
            var plea = new PayloadEventArgs(payload, bdea);
            if (!ReferenceEquals(this.AICSuperAgentAddAppUserReceived, null))
            {
                plea.Payload.IsHandled = true;
                this.AICSuperAgentAddAppUserReceived(this, plea);
            }
        }

        /// <summary>
        /// Responds to: GetAppUsers from AICSuperAgent
        /// </summary>
        public event EventHandler<PayloadEventArgs> AICSuperAgentGetAppUsersReceived;
        protected virtual void OnAICSuperAgentGetAppUsersReceived(StandardPayload payload, BasicDeliverEventArgs bdea)
        {
            var plea = new PayloadEventArgs(payload, bdea);
            if (!ReferenceEquals(this.AICSuperAgentGetAppUsersReceived, null))
            {
                plea.Payload.IsHandled = true;
                this.AICSuperAgentGetAppUsersReceived(this, plea);
            }
        }

        /// <summary>
        /// Responds to: UpdateAppUser from AICSuperAgent
        /// </summary>
        public event EventHandler<PayloadEventArgs> AICSuperAgentUpdateAppUserReceived;
        protected virtual void OnAICSuperAgentUpdateAppUserReceived(StandardPayload payload, BasicDeliverEventArgs bdea)
        {
            var plea = new PayloadEventArgs(payload, bdea);
            if (!ReferenceEquals(this.AICSuperAgentUpdateAppUserReceived, null))
            {
                plea.Payload.IsHandled = true;
                this.AICSuperAgentUpdateAppUserReceived(this, plea);
            }
        }

        /// <summary>
        /// Responds to: DeleteAppUser from AICSuperAgent
        /// </summary>
        public event EventHandler<PayloadEventArgs> AICSuperAgentDeleteAppUserReceived;
        protected virtual void OnAICSuperAgentDeleteAppUserReceived(StandardPayload payload, BasicDeliverEventArgs bdea)
        {
            var plea = new PayloadEventArgs(payload, bdea);
            if (!ReferenceEquals(this.AICSuperAgentDeleteAppUserReceived, null))
            {
                plea.Payload.IsHandled = true;
                this.AICSuperAgentDeleteAppUserReceived(this, plea);
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
        /// Responds to: GetAICWorkspaces from User
        /// </summary>
        public event EventHandler<PayloadEventArgs> UserGetAICWorkspacesReceived;
        protected virtual void OnUserGetAICWorkspacesReceived(StandardPayload payload, BasicDeliverEventArgs bdea)
        {
            var plea = new PayloadEventArgs(payload, bdea);
            if (!ReferenceEquals(this.UserGetAICWorkspacesReceived, null))
            {
                plea.Payload.IsHandled = true;
                this.UserGetAICWorkspacesReceived(this, plea);
            }
        }

        /// <summary>
        /// Responds to: UpdateAICWorkspace from User
        /// </summary>
        public event EventHandler<PayloadEventArgs> UserUpdateAICWorkspaceReceived;
        protected virtual void OnUserUpdateAICWorkspaceReceived(StandardPayload payload, BasicDeliverEventArgs bdea)
        {
            var plea = new PayloadEventArgs(payload, bdea);
            if (!ReferenceEquals(this.UserUpdateAICWorkspaceReceived, null))
            {
                plea.Payload.IsHandled = true;
                this.UserUpdateAICWorkspaceReceived(this, plea);
            }
        }

        /// <summary>
        /// Responds to: AddAICWorkspace from AICSuperAgent
        /// </summary>
        public event EventHandler<PayloadEventArgs> AICSuperAgentAddAICWorkspaceReceived;
        protected virtual void OnAICSuperAgentAddAICWorkspaceReceived(StandardPayload payload, BasicDeliverEventArgs bdea)
        {
            var plea = new PayloadEventArgs(payload, bdea);
            if (!ReferenceEquals(this.AICSuperAgentAddAICWorkspaceReceived, null))
            {
                plea.Payload.IsHandled = true;
                this.AICSuperAgentAddAICWorkspaceReceived(this, plea);
            }
        }

        /// <summary>
        /// Responds to: GetAICWorkspaces from AICSuperAgent
        /// </summary>
        public event EventHandler<PayloadEventArgs> AICSuperAgentGetAICWorkspacesReceived;
        protected virtual void OnAICSuperAgentGetAICWorkspacesReceived(StandardPayload payload, BasicDeliverEventArgs bdea)
        {
            var plea = new PayloadEventArgs(payload, bdea);
            if (!ReferenceEquals(this.AICSuperAgentGetAICWorkspacesReceived, null))
            {
                plea.Payload.IsHandled = true;
                this.AICSuperAgentGetAICWorkspacesReceived(this, plea);
            }
        }

        /// <summary>
        /// Responds to: UpdateAICWorkspace from AICSuperAgent
        /// </summary>
        public event EventHandler<PayloadEventArgs> AICSuperAgentUpdateAICWorkspaceReceived;
        protected virtual void OnAICSuperAgentUpdateAICWorkspaceReceived(StandardPayload payload, BasicDeliverEventArgs bdea)
        {
            var plea = new PayloadEventArgs(payload, bdea);
            if (!ReferenceEquals(this.AICSuperAgentUpdateAICWorkspaceReceived, null))
            {
                plea.Payload.IsHandled = true;
                this.AICSuperAgentUpdateAICWorkspaceReceived(this, plea);
            }
        }

        /// <summary>
        /// Responds to: DeleteAICWorkspace from AICSuperAgent
        /// </summary>
        public event EventHandler<PayloadEventArgs> AICSuperAgentDeleteAICWorkspaceReceived;
        protected virtual void OnAICSuperAgentDeleteAICWorkspaceReceived(StandardPayload payload, BasicDeliverEventArgs bdea)
        {
            var plea = new PayloadEventArgs(payload, bdea);
            if (!ReferenceEquals(this.AICSuperAgentDeleteAICWorkspaceReceived, null))
            {
                plea.Payload.IsHandled = true;
                this.AICSuperAgentDeleteAICWorkspaceReceived(this, plea);
            }
        }

        /// <summary>
        /// Responds to: AddAICWorkspace from Admin
        /// </summary>
        public event EventHandler<PayloadEventArgs> AdminAddAICWorkspaceReceived;
        protected virtual void OnAdminAddAICWorkspaceReceived(StandardPayload payload, BasicDeliverEventArgs bdea)
        {
            var plea = new PayloadEventArgs(payload, bdea);
            if (!ReferenceEquals(this.AdminAddAICWorkspaceReceived, null))
            {
                plea.Payload.IsHandled = true;
                this.AdminAddAICWorkspaceReceived(this, plea);
            }
        }

        /// <summary>
        /// Responds to: GetAICWorkspaces from Admin
        /// </summary>
        public event EventHandler<PayloadEventArgs> AdminGetAICWorkspacesReceived;
        protected virtual void OnAdminGetAICWorkspacesReceived(StandardPayload payload, BasicDeliverEventArgs bdea)
        {
            var plea = new PayloadEventArgs(payload, bdea);
            if (!ReferenceEquals(this.AdminGetAICWorkspacesReceived, null))
            {
                plea.Payload.IsHandled = true;
                this.AdminGetAICWorkspacesReceived(this, plea);
            }
        }

        /// <summary>
        /// Responds to: UpdateAICWorkspace from Admin
        /// </summary>
        public event EventHandler<PayloadEventArgs> AdminUpdateAICWorkspaceReceived;
        protected virtual void OnAdminUpdateAICWorkspaceReceived(StandardPayload payload, BasicDeliverEventArgs bdea)
        {
            var plea = new PayloadEventArgs(payload, bdea);
            if (!ReferenceEquals(this.AdminUpdateAICWorkspaceReceived, null))
            {
                plea.Payload.IsHandled = true;
                this.AdminUpdateAICWorkspaceReceived(this, plea);
            }
        }

        /// <summary>
        /// Responds to: DeleteAICWorkspace from Admin
        /// </summary>
        public event EventHandler<PayloadEventArgs> AdminDeleteAICWorkspaceReceived;
        protected virtual void OnAdminDeleteAICWorkspaceReceived(StandardPayload payload, BasicDeliverEventArgs bdea)
        {
            var plea = new PayloadEventArgs(payload, bdea);
            if (!ReferenceEquals(this.AdminDeleteAICWorkspaceReceived, null))
            {
                plea.Payload.IsHandled = true;
                this.AdminDeleteAICWorkspaceReceived(this, plea);
            }
        }

        /// <summary>
        /// Responds to: AddRepo from AICSuperAgent
        /// </summary>
        public event EventHandler<PayloadEventArgs> AICSuperAgentAddRepoReceived;
        protected virtual void OnAICSuperAgentAddRepoReceived(StandardPayload payload, BasicDeliverEventArgs bdea)
        {
            var plea = new PayloadEventArgs(payload, bdea);
            if (!ReferenceEquals(this.AICSuperAgentAddRepoReceived, null))
            {
                plea.Payload.IsHandled = true;
                this.AICSuperAgentAddRepoReceived(this, plea);
            }
        }

        /// <summary>
        /// Responds to: GetRepos from AICSuperAgent
        /// </summary>
        public event EventHandler<PayloadEventArgs> AICSuperAgentGetReposReceived;
        protected virtual void OnAICSuperAgentGetReposReceived(StandardPayload payload, BasicDeliverEventArgs bdea)
        {
            var plea = new PayloadEventArgs(payload, bdea);
            if (!ReferenceEquals(this.AICSuperAgentGetReposReceived, null))
            {
                plea.Payload.IsHandled = true;
                this.AICSuperAgentGetReposReceived(this, plea);
            }
        }

        /// <summary>
        /// Responds to: UpdateRepo from AICSuperAgent
        /// </summary>
        public event EventHandler<PayloadEventArgs> AICSuperAgentUpdateRepoReceived;
        protected virtual void OnAICSuperAgentUpdateRepoReceived(StandardPayload payload, BasicDeliverEventArgs bdea)
        {
            var plea = new PayloadEventArgs(payload, bdea);
            if (!ReferenceEquals(this.AICSuperAgentUpdateRepoReceived, null))
            {
                plea.Payload.IsHandled = true;
                this.AICSuperAgentUpdateRepoReceived(this, plea);
            }
        }

        /// <summary>
        /// Responds to: DeleteRepo from AICSuperAgent
        /// </summary>
        public event EventHandler<PayloadEventArgs> AICSuperAgentDeleteRepoReceived;
        protected virtual void OnAICSuperAgentDeleteRepoReceived(StandardPayload payload, BasicDeliverEventArgs bdea)
        {
            var plea = new PayloadEventArgs(payload, bdea);
            if (!ReferenceEquals(this.AICSuperAgentDeleteRepoReceived, null))
            {
                plea.Payload.IsHandled = true;
                this.AICSuperAgentDeleteRepoReceived(this, plea);
            }
        }

        /// <summary>
        /// Responds to: AddRepo from Admin
        /// </summary>
        public event EventHandler<PayloadEventArgs> AdminAddRepoReceived;
        protected virtual void OnAdminAddRepoReceived(StandardPayload payload, BasicDeliverEventArgs bdea)
        {
            var plea = new PayloadEventArgs(payload, bdea);
            if (!ReferenceEquals(this.AdminAddRepoReceived, null))
            {
                plea.Payload.IsHandled = true;
                this.AdminAddRepoReceived(this, plea);
            }
        }

        /// <summary>
        /// Responds to: GetRepos from Admin
        /// </summary>
        public event EventHandler<PayloadEventArgs> AdminGetReposReceived;
        protected virtual void OnAdminGetReposReceived(StandardPayload payload, BasicDeliverEventArgs bdea)
        {
            var plea = new PayloadEventArgs(payload, bdea);
            if (!ReferenceEquals(this.AdminGetReposReceived, null))
            {
                plea.Payload.IsHandled = true;
                this.AdminGetReposReceived(this, plea);
            }
        }

        /// <summary>
        /// Responds to: UpdateRepo from Admin
        /// </summary>
        public event EventHandler<PayloadEventArgs> AdminUpdateRepoReceived;
        protected virtual void OnAdminUpdateRepoReceived(StandardPayload payload, BasicDeliverEventArgs bdea)
        {
            var plea = new PayloadEventArgs(payload, bdea);
            if (!ReferenceEquals(this.AdminUpdateRepoReceived, null))
            {
                plea.Payload.IsHandled = true;
                this.AdminUpdateRepoReceived(this, plea);
            }
        }

        /// <summary>
        /// Responds to: DeleteRepo from Admin
        /// </summary>
        public event EventHandler<PayloadEventArgs> AdminDeleteRepoReceived;
        protected virtual void OnAdminDeleteRepoReceived(StandardPayload payload, BasicDeliverEventArgs bdea)
        {
            var plea = new PayloadEventArgs(payload, bdea);
            if (!ReferenceEquals(this.AdminDeleteRepoReceived, null))
            {
                plea.Payload.IsHandled = true;
                this.AdminDeleteRepoReceived(this, plea);
            }
        }

        /// <summary>
        /// Responds to: AddAICContextCategory from AICSuperAgent
        /// </summary>
        public event EventHandler<PayloadEventArgs> AICSuperAgentAddAICContextCategoryReceived;
        protected virtual void OnAICSuperAgentAddAICContextCategoryReceived(StandardPayload payload, BasicDeliverEventArgs bdea)
        {
            var plea = new PayloadEventArgs(payload, bdea);
            if (!ReferenceEquals(this.AICSuperAgentAddAICContextCategoryReceived, null))
            {
                plea.Payload.IsHandled = true;
                this.AICSuperAgentAddAICContextCategoryReceived(this, plea);
            }
        }

        /// <summary>
        /// Responds to: GetAICContextCategories from AICSuperAgent
        /// </summary>
        public event EventHandler<PayloadEventArgs> AICSuperAgentGetAICContextCategoriesReceived;
        protected virtual void OnAICSuperAgentGetAICContextCategoriesReceived(StandardPayload payload, BasicDeliverEventArgs bdea)
        {
            var plea = new PayloadEventArgs(payload, bdea);
            if (!ReferenceEquals(this.AICSuperAgentGetAICContextCategoriesReceived, null))
            {
                plea.Payload.IsHandled = true;
                this.AICSuperAgentGetAICContextCategoriesReceived(this, plea);
            }
        }

        /// <summary>
        /// Responds to: UpdateAICContextCategory from AICSuperAgent
        /// </summary>
        public event EventHandler<PayloadEventArgs> AICSuperAgentUpdateAICContextCategoryReceived;
        protected virtual void OnAICSuperAgentUpdateAICContextCategoryReceived(StandardPayload payload, BasicDeliverEventArgs bdea)
        {
            var plea = new PayloadEventArgs(payload, bdea);
            if (!ReferenceEquals(this.AICSuperAgentUpdateAICContextCategoryReceived, null))
            {
                plea.Payload.IsHandled = true;
                this.AICSuperAgentUpdateAICContextCategoryReceived(this, plea);
            }
        }

        /// <summary>
        /// Responds to: DeleteAICContextCategory from AICSuperAgent
        /// </summary>
        public event EventHandler<PayloadEventArgs> AICSuperAgentDeleteAICContextCategoryReceived;
        protected virtual void OnAICSuperAgentDeleteAICContextCategoryReceived(StandardPayload payload, BasicDeliverEventArgs bdea)
        {
            var plea = new PayloadEventArgs(payload, bdea);
            if (!ReferenceEquals(this.AICSuperAgentDeleteAICContextCategoryReceived, null))
            {
                plea.Payload.IsHandled = true;
                this.AICSuperAgentDeleteAICContextCategoryReceived(this, plea);
            }
        }

        /// <summary>
        /// Responds to: AddAICContextCategory from Admin
        /// </summary>
        public event EventHandler<PayloadEventArgs> AdminAddAICContextCategoryReceived;
        protected virtual void OnAdminAddAICContextCategoryReceived(StandardPayload payload, BasicDeliverEventArgs bdea)
        {
            var plea = new PayloadEventArgs(payload, bdea);
            if (!ReferenceEquals(this.AdminAddAICContextCategoryReceived, null))
            {
                plea.Payload.IsHandled = true;
                this.AdminAddAICContextCategoryReceived(this, plea);
            }
        }

        /// <summary>
        /// Responds to: GetAICContextCategories from Admin
        /// </summary>
        public event EventHandler<PayloadEventArgs> AdminGetAICContextCategoriesReceived;
        protected virtual void OnAdminGetAICContextCategoriesReceived(StandardPayload payload, BasicDeliverEventArgs bdea)
        {
            var plea = new PayloadEventArgs(payload, bdea);
            if (!ReferenceEquals(this.AdminGetAICContextCategoriesReceived, null))
            {
                plea.Payload.IsHandled = true;
                this.AdminGetAICContextCategoriesReceived(this, plea);
            }
        }

        /// <summary>
        /// Responds to: UpdateAICContextCategory from Admin
        /// </summary>
        public event EventHandler<PayloadEventArgs> AdminUpdateAICContextCategoryReceived;
        protected virtual void OnAdminUpdateAICContextCategoryReceived(StandardPayload payload, BasicDeliverEventArgs bdea)
        {
            var plea = new PayloadEventArgs(payload, bdea);
            if (!ReferenceEquals(this.AdminUpdateAICContextCategoryReceived, null))
            {
                plea.Payload.IsHandled = true;
                this.AdminUpdateAICContextCategoryReceived(this, plea);
            }
        }

        /// <summary>
        /// Responds to: DeleteAICContextCategory from Admin
        /// </summary>
        public event EventHandler<PayloadEventArgs> AdminDeleteAICContextCategoryReceived;
        protected virtual void OnAdminDeleteAICContextCategoryReceived(StandardPayload payload, BasicDeliverEventArgs bdea)
        {
            var plea = new PayloadEventArgs(payload, bdea);
            if (!ReferenceEquals(this.AdminDeleteAICContextCategoryReceived, null))
            {
                plea.Payload.IsHandled = true;
                this.AdminDeleteAICContextCategoryReceived(this, plea);
            }
        }

        /// <summary>
        /// Responds to: AddAICPlan from AICSuperAgent
        /// </summary>
        public event EventHandler<PayloadEventArgs> AICSuperAgentAddAICPlanReceived;
        protected virtual void OnAICSuperAgentAddAICPlanReceived(StandardPayload payload, BasicDeliverEventArgs bdea)
        {
            var plea = new PayloadEventArgs(payload, bdea);
            if (!ReferenceEquals(this.AICSuperAgentAddAICPlanReceived, null))
            {
                plea.Payload.IsHandled = true;
                this.AICSuperAgentAddAICPlanReceived(this, plea);
            }
        }

        /// <summary>
        /// Responds to: GetAICPlans from AICSuperAgent
        /// </summary>
        public event EventHandler<PayloadEventArgs> AICSuperAgentGetAICPlansReceived;
        protected virtual void OnAICSuperAgentGetAICPlansReceived(StandardPayload payload, BasicDeliverEventArgs bdea)
        {
            var plea = new PayloadEventArgs(payload, bdea);
            if (!ReferenceEquals(this.AICSuperAgentGetAICPlansReceived, null))
            {
                plea.Payload.IsHandled = true;
                this.AICSuperAgentGetAICPlansReceived(this, plea);
            }
        }

        /// <summary>
        /// Responds to: UpdateAICPlan from AICSuperAgent
        /// </summary>
        public event EventHandler<PayloadEventArgs> AICSuperAgentUpdateAICPlanReceived;
        protected virtual void OnAICSuperAgentUpdateAICPlanReceived(StandardPayload payload, BasicDeliverEventArgs bdea)
        {
            var plea = new PayloadEventArgs(payload, bdea);
            if (!ReferenceEquals(this.AICSuperAgentUpdateAICPlanReceived, null))
            {
                plea.Payload.IsHandled = true;
                this.AICSuperAgentUpdateAICPlanReceived(this, plea);
            }
        }

        /// <summary>
        /// Responds to: DeleteAICPlan from AICSuperAgent
        /// </summary>
        public event EventHandler<PayloadEventArgs> AICSuperAgentDeleteAICPlanReceived;
        protected virtual void OnAICSuperAgentDeleteAICPlanReceived(StandardPayload payload, BasicDeliverEventArgs bdea)
        {
            var plea = new PayloadEventArgs(payload, bdea);
            if (!ReferenceEquals(this.AICSuperAgentDeleteAICPlanReceived, null))
            {
                plea.Payload.IsHandled = true;
                this.AICSuperAgentDeleteAICPlanReceived(this, plea);
            }
        }

        /// <summary>
        /// Responds to: AddAICPlan from Admin
        /// </summary>
        public event EventHandler<PayloadEventArgs> AdminAddAICPlanReceived;
        protected virtual void OnAdminAddAICPlanReceived(StandardPayload payload, BasicDeliverEventArgs bdea)
        {
            var plea = new PayloadEventArgs(payload, bdea);
            if (!ReferenceEquals(this.AdminAddAICPlanReceived, null))
            {
                plea.Payload.IsHandled = true;
                this.AdminAddAICPlanReceived(this, plea);
            }
        }

        /// <summary>
        /// Responds to: GetAICPlans from Admin
        /// </summary>
        public event EventHandler<PayloadEventArgs> AdminGetAICPlansReceived;
        protected virtual void OnAdminGetAICPlansReceived(StandardPayload payload, BasicDeliverEventArgs bdea)
        {
            var plea = new PayloadEventArgs(payload, bdea);
            if (!ReferenceEquals(this.AdminGetAICPlansReceived, null))
            {
                plea.Payload.IsHandled = true;
                this.AdminGetAICPlansReceived(this, plea);
            }
        }

        /// <summary>
        /// Responds to: UpdateAICPlan from Admin
        /// </summary>
        public event EventHandler<PayloadEventArgs> AdminUpdateAICPlanReceived;
        protected virtual void OnAdminUpdateAICPlanReceived(StandardPayload payload, BasicDeliverEventArgs bdea)
        {
            var plea = new PayloadEventArgs(payload, bdea);
            if (!ReferenceEquals(this.AdminUpdateAICPlanReceived, null))
            {
                plea.Payload.IsHandled = true;
                this.AdminUpdateAICPlanReceived(this, plea);
            }
        }

        /// <summary>
        /// Responds to: DeleteAICPlan from Admin
        /// </summary>
        public event EventHandler<PayloadEventArgs> AdminDeleteAICPlanReceived;
        protected virtual void OnAdminDeleteAICPlanReceived(StandardPayload payload, BasicDeliverEventArgs bdea)
        {
            var plea = new PayloadEventArgs(payload, bdea);
            if (!ReferenceEquals(this.AdminDeleteAICPlanReceived, null))
            {
                plea.Payload.IsHandled = true;
                this.AdminDeleteAICPlanReceived(this, plea);
            }
        }

        /// <summary>
        /// Responds to: AddAICModelPricing from AICSuperAgent
        /// </summary>
        public event EventHandler<PayloadEventArgs> AICSuperAgentAddAICModelPricingReceived;
        protected virtual void OnAICSuperAgentAddAICModelPricingReceived(StandardPayload payload, BasicDeliverEventArgs bdea)
        {
            var plea = new PayloadEventArgs(payload, bdea);
            if (!ReferenceEquals(this.AICSuperAgentAddAICModelPricingReceived, null))
            {
                plea.Payload.IsHandled = true;
                this.AICSuperAgentAddAICModelPricingReceived(this, plea);
            }
        }

        /// <summary>
        /// Responds to: GetAICModelPricings from AICSuperAgent
        /// </summary>
        public event EventHandler<PayloadEventArgs> AICSuperAgentGetAICModelPricingsReceived;
        protected virtual void OnAICSuperAgentGetAICModelPricingsReceived(StandardPayload payload, BasicDeliverEventArgs bdea)
        {
            var plea = new PayloadEventArgs(payload, bdea);
            if (!ReferenceEquals(this.AICSuperAgentGetAICModelPricingsReceived, null))
            {
                plea.Payload.IsHandled = true;
                this.AICSuperAgentGetAICModelPricingsReceived(this, plea);
            }
        }

        /// <summary>
        /// Responds to: UpdateAICModelPricing from AICSuperAgent
        /// </summary>
        public event EventHandler<PayloadEventArgs> AICSuperAgentUpdateAICModelPricingReceived;
        protected virtual void OnAICSuperAgentUpdateAICModelPricingReceived(StandardPayload payload, BasicDeliverEventArgs bdea)
        {
            var plea = new PayloadEventArgs(payload, bdea);
            if (!ReferenceEquals(this.AICSuperAgentUpdateAICModelPricingReceived, null))
            {
                plea.Payload.IsHandled = true;
                this.AICSuperAgentUpdateAICModelPricingReceived(this, plea);
            }
        }

        /// <summary>
        /// Responds to: DeleteAICModelPricing from AICSuperAgent
        /// </summary>
        public event EventHandler<PayloadEventArgs> AICSuperAgentDeleteAICModelPricingReceived;
        protected virtual void OnAICSuperAgentDeleteAICModelPricingReceived(StandardPayload payload, BasicDeliverEventArgs bdea)
        {
            var plea = new PayloadEventArgs(payload, bdea);
            if (!ReferenceEquals(this.AICSuperAgentDeleteAICModelPricingReceived, null))
            {
                plea.Payload.IsHandled = true;
                this.AICSuperAgentDeleteAICModelPricingReceived(this, plea);
            }
        }

        /// <summary>
        /// Responds to: AddAICModelPricing from Admin
        /// </summary>
        public event EventHandler<PayloadEventArgs> AdminAddAICModelPricingReceived;
        protected virtual void OnAdminAddAICModelPricingReceived(StandardPayload payload, BasicDeliverEventArgs bdea)
        {
            var plea = new PayloadEventArgs(payload, bdea);
            if (!ReferenceEquals(this.AdminAddAICModelPricingReceived, null))
            {
                plea.Payload.IsHandled = true;
                this.AdminAddAICModelPricingReceived(this, plea);
            }
        }

        /// <summary>
        /// Responds to: GetAICModelPricings from Admin
        /// </summary>
        public event EventHandler<PayloadEventArgs> AdminGetAICModelPricingsReceived;
        protected virtual void OnAdminGetAICModelPricingsReceived(StandardPayload payload, BasicDeliverEventArgs bdea)
        {
            var plea = new PayloadEventArgs(payload, bdea);
            if (!ReferenceEquals(this.AdminGetAICModelPricingsReceived, null))
            {
                plea.Payload.IsHandled = true;
                this.AdminGetAICModelPricingsReceived(this, plea);
            }
        }

        /// <summary>
        /// Responds to: UpdateAICModelPricing from Admin
        /// </summary>
        public event EventHandler<PayloadEventArgs> AdminUpdateAICModelPricingReceived;
        protected virtual void OnAdminUpdateAICModelPricingReceived(StandardPayload payload, BasicDeliverEventArgs bdea)
        {
            var plea = new PayloadEventArgs(payload, bdea);
            if (!ReferenceEquals(this.AdminUpdateAICModelPricingReceived, null))
            {
                plea.Payload.IsHandled = true;
                this.AdminUpdateAICModelPricingReceived(this, plea);
            }
        }

        /// <summary>
        /// Responds to: DeleteAICModelPricing from Admin
        /// </summary>
        public event EventHandler<PayloadEventArgs> AdminDeleteAICModelPricingReceived;
        protected virtual void OnAdminDeleteAICModelPricingReceived(StandardPayload payload, BasicDeliverEventArgs bdea)
        {
            var plea = new PayloadEventArgs(payload, bdea);
            if (!ReferenceEquals(this.AdminDeleteAICModelPricingReceived, null))
            {
                plea.Payload.IsHandled = true;
                this.AdminDeleteAICModelPricingReceived(this, plea);
            }
        }

        /// <summary>
        /// Responds to: AddLanguage from AICSuperAgent
        /// </summary>
        public event EventHandler<PayloadEventArgs> AICSuperAgentAddLanguageReceived;
        protected virtual void OnAICSuperAgentAddLanguageReceived(StandardPayload payload, BasicDeliverEventArgs bdea)
        {
            var plea = new PayloadEventArgs(payload, bdea);
            if (!ReferenceEquals(this.AICSuperAgentAddLanguageReceived, null))
            {
                plea.Payload.IsHandled = true;
                this.AICSuperAgentAddLanguageReceived(this, plea);
            }
        }

        /// <summary>
        /// Responds to: GetLanguages from AICSuperAgent
        /// </summary>
        public event EventHandler<PayloadEventArgs> AICSuperAgentGetLanguagesReceived;
        protected virtual void OnAICSuperAgentGetLanguagesReceived(StandardPayload payload, BasicDeliverEventArgs bdea)
        {
            var plea = new PayloadEventArgs(payload, bdea);
            if (!ReferenceEquals(this.AICSuperAgentGetLanguagesReceived, null))
            {
                plea.Payload.IsHandled = true;
                this.AICSuperAgentGetLanguagesReceived(this, plea);
            }
        }

        /// <summary>
        /// Responds to: UpdateLanguage from AICSuperAgent
        /// </summary>
        public event EventHandler<PayloadEventArgs> AICSuperAgentUpdateLanguageReceived;
        protected virtual void OnAICSuperAgentUpdateLanguageReceived(StandardPayload payload, BasicDeliverEventArgs bdea)
        {
            var plea = new PayloadEventArgs(payload, bdea);
            if (!ReferenceEquals(this.AICSuperAgentUpdateLanguageReceived, null))
            {
                plea.Payload.IsHandled = true;
                this.AICSuperAgentUpdateLanguageReceived(this, plea);
            }
        }

        /// <summary>
        /// Responds to: DeleteLanguage from AICSuperAgent
        /// </summary>
        public event EventHandler<PayloadEventArgs> AICSuperAgentDeleteLanguageReceived;
        protected virtual void OnAICSuperAgentDeleteLanguageReceived(StandardPayload payload, BasicDeliverEventArgs bdea)
        {
            var plea = new PayloadEventArgs(payload, bdea);
            if (!ReferenceEquals(this.AICSuperAgentDeleteLanguageReceived, null))
            {
                plea.Payload.IsHandled = true;
                this.AICSuperAgentDeleteLanguageReceived(this, plea);
            }
        }

        /// <summary>
        /// Responds to: AddLanguage from Admin
        /// </summary>
        public event EventHandler<PayloadEventArgs> AdminAddLanguageReceived;
        protected virtual void OnAdminAddLanguageReceived(StandardPayload payload, BasicDeliverEventArgs bdea)
        {
            var plea = new PayloadEventArgs(payload, bdea);
            if (!ReferenceEquals(this.AdminAddLanguageReceived, null))
            {
                plea.Payload.IsHandled = true;
                this.AdminAddLanguageReceived(this, plea);
            }
        }

        /// <summary>
        /// Responds to: GetLanguages from Admin
        /// </summary>
        public event EventHandler<PayloadEventArgs> AdminGetLanguagesReceived;
        protected virtual void OnAdminGetLanguagesReceived(StandardPayload payload, BasicDeliverEventArgs bdea)
        {
            var plea = new PayloadEventArgs(payload, bdea);
            if (!ReferenceEquals(this.AdminGetLanguagesReceived, null))
            {
                plea.Payload.IsHandled = true;
                this.AdminGetLanguagesReceived(this, plea);
            }
        }

        /// <summary>
        /// Responds to: UpdateLanguage from Admin
        /// </summary>
        public event EventHandler<PayloadEventArgs> AdminUpdateLanguageReceived;
        protected virtual void OnAdminUpdateLanguageReceived(StandardPayload payload, BasicDeliverEventArgs bdea)
        {
            var plea = new PayloadEventArgs(payload, bdea);
            if (!ReferenceEquals(this.AdminUpdateLanguageReceived, null))
            {
                plea.Payload.IsHandled = true;
                this.AdminUpdateLanguageReceived(this, plea);
            }
        }

        /// <summary>
        /// Responds to: DeleteLanguage from Admin
        /// </summary>
        public event EventHandler<PayloadEventArgs> AdminDeleteLanguageReceived;
        protected virtual void OnAdminDeleteLanguageReceived(StandardPayload payload, BasicDeliverEventArgs bdea)
        {
            var plea = new PayloadEventArgs(payload, bdea);
            if (!ReferenceEquals(this.AdminDeleteLanguageReceived, null))
            {
                plea.Payload.IsHandled = true;
                this.AdminDeleteLanguageReceived(this, plea);
            }
        }

        /// <summary>
        /// Responds to: AddAppUserUsage from AICSuperAgent
        /// </summary>
        public event EventHandler<PayloadEventArgs> AICSuperAgentAddAppUserUsageReceived;
        protected virtual void OnAICSuperAgentAddAppUserUsageReceived(StandardPayload payload, BasicDeliverEventArgs bdea)
        {
            var plea = new PayloadEventArgs(payload, bdea);
            if (!ReferenceEquals(this.AICSuperAgentAddAppUserUsageReceived, null))
            {
                plea.Payload.IsHandled = true;
                this.AICSuperAgentAddAppUserUsageReceived(this, plea);
            }
        }

        /// <summary>
        /// Responds to: GetAppUserUsages from AICSuperAgent
        /// </summary>
        public event EventHandler<PayloadEventArgs> AICSuperAgentGetAppUserUsagesReceived;
        protected virtual void OnAICSuperAgentGetAppUserUsagesReceived(StandardPayload payload, BasicDeliverEventArgs bdea)
        {
            var plea = new PayloadEventArgs(payload, bdea);
            if (!ReferenceEquals(this.AICSuperAgentGetAppUserUsagesReceived, null))
            {
                plea.Payload.IsHandled = true;
                this.AICSuperAgentGetAppUserUsagesReceived(this, plea);
            }
        }

        /// <summary>
        /// Responds to: UpdateAppUserUsage from AICSuperAgent
        /// </summary>
        public event EventHandler<PayloadEventArgs> AICSuperAgentUpdateAppUserUsageReceived;
        protected virtual void OnAICSuperAgentUpdateAppUserUsageReceived(StandardPayload payload, BasicDeliverEventArgs bdea)
        {
            var plea = new PayloadEventArgs(payload, bdea);
            if (!ReferenceEquals(this.AICSuperAgentUpdateAppUserUsageReceived, null))
            {
                plea.Payload.IsHandled = true;
                this.AICSuperAgentUpdateAppUserUsageReceived(this, plea);
            }
        }

        /// <summary>
        /// Responds to: DeleteAppUserUsage from AICSuperAgent
        /// </summary>
        public event EventHandler<PayloadEventArgs> AICSuperAgentDeleteAppUserUsageReceived;
        protected virtual void OnAICSuperAgentDeleteAppUserUsageReceived(StandardPayload payload, BasicDeliverEventArgs bdea)
        {
            var plea = new PayloadEventArgs(payload, bdea);
            if (!ReferenceEquals(this.AICSuperAgentDeleteAppUserUsageReceived, null))
            {
                plea.Payload.IsHandled = true;
                this.AICSuperAgentDeleteAppUserUsageReceived(this, plea);
            }
        }

        /// <summary>
        /// Responds to: AddAppUserUsage from Admin
        /// </summary>
        public event EventHandler<PayloadEventArgs> AdminAddAppUserUsageReceived;
        protected virtual void OnAdminAddAppUserUsageReceived(StandardPayload payload, BasicDeliverEventArgs bdea)
        {
            var plea = new PayloadEventArgs(payload, bdea);
            if (!ReferenceEquals(this.AdminAddAppUserUsageReceived, null))
            {
                plea.Payload.IsHandled = true;
                this.AdminAddAppUserUsageReceived(this, plea);
            }
        }

        /// <summary>
        /// Responds to: GetAppUserUsages from Admin
        /// </summary>
        public event EventHandler<PayloadEventArgs> AdminGetAppUserUsagesReceived;
        protected virtual void OnAdminGetAppUserUsagesReceived(StandardPayload payload, BasicDeliverEventArgs bdea)
        {
            var plea = new PayloadEventArgs(payload, bdea);
            if (!ReferenceEquals(this.AdminGetAppUserUsagesReceived, null))
            {
                plea.Payload.IsHandled = true;
                this.AdminGetAppUserUsagesReceived(this, plea);
            }
        }

        /// <summary>
        /// Responds to: UpdateAppUserUsage from Admin
        /// </summary>
        public event EventHandler<PayloadEventArgs> AdminUpdateAppUserUsageReceived;
        protected virtual void OnAdminUpdateAppUserUsageReceived(StandardPayload payload, BasicDeliverEventArgs bdea)
        {
            var plea = new PayloadEventArgs(payload, bdea);
            if (!ReferenceEquals(this.AdminUpdateAppUserUsageReceived, null))
            {
                plea.Payload.IsHandled = true;
                this.AdminUpdateAppUserUsageReceived(this, plea);
            }
        }

        /// <summary>
        /// Responds to: DeleteAppUserUsage from Admin
        /// </summary>
        public event EventHandler<PayloadEventArgs> AdminDeleteAppUserUsageReceived;
        protected virtual void OnAdminDeleteAppUserUsageReceived(StandardPayload payload, BasicDeliverEventArgs bdea)
        {
            var plea = new PayloadEventArgs(payload, bdea);
            if (!ReferenceEquals(this.AdminDeleteAppUserUsageReceived, null))
            {
                plea.Payload.IsHandled = true;
                this.AdminDeleteAppUserUsageReceived(this, plea);
            }
        }

        /// <summary>
        /// Responds to: AddAIProvider from AICSuperAgent
        /// </summary>
        public event EventHandler<PayloadEventArgs> AICSuperAgentAddAIProviderReceived;
        protected virtual void OnAICSuperAgentAddAIProviderReceived(StandardPayload payload, BasicDeliverEventArgs bdea)
        {
            var plea = new PayloadEventArgs(payload, bdea);
            if (!ReferenceEquals(this.AICSuperAgentAddAIProviderReceived, null))
            {
                plea.Payload.IsHandled = true;
                this.AICSuperAgentAddAIProviderReceived(this, plea);
            }
        }

        /// <summary>
        /// Responds to: GetAIProviders from AICSuperAgent
        /// </summary>
        public event EventHandler<PayloadEventArgs> AICSuperAgentGetAIProvidersReceived;
        protected virtual void OnAICSuperAgentGetAIProvidersReceived(StandardPayload payload, BasicDeliverEventArgs bdea)
        {
            var plea = new PayloadEventArgs(payload, bdea);
            if (!ReferenceEquals(this.AICSuperAgentGetAIProvidersReceived, null))
            {
                plea.Payload.IsHandled = true;
                this.AICSuperAgentGetAIProvidersReceived(this, plea);
            }
        }

        /// <summary>
        /// Responds to: UpdateAIProvider from AICSuperAgent
        /// </summary>
        public event EventHandler<PayloadEventArgs> AICSuperAgentUpdateAIProviderReceived;
        protected virtual void OnAICSuperAgentUpdateAIProviderReceived(StandardPayload payload, BasicDeliverEventArgs bdea)
        {
            var plea = new PayloadEventArgs(payload, bdea);
            if (!ReferenceEquals(this.AICSuperAgentUpdateAIProviderReceived, null))
            {
                plea.Payload.IsHandled = true;
                this.AICSuperAgentUpdateAIProviderReceived(this, plea);
            }
        }

        /// <summary>
        /// Responds to: DeleteAIProvider from AICSuperAgent
        /// </summary>
        public event EventHandler<PayloadEventArgs> AICSuperAgentDeleteAIProviderReceived;
        protected virtual void OnAICSuperAgentDeleteAIProviderReceived(StandardPayload payload, BasicDeliverEventArgs bdea)
        {
            var plea = new PayloadEventArgs(payload, bdea);
            if (!ReferenceEquals(this.AICSuperAgentDeleteAIProviderReceived, null))
            {
                plea.Payload.IsHandled = true;
                this.AICSuperAgentDeleteAIProviderReceived(this, plea);
            }
        }

        /// <summary>
        /// Responds to: AddAIProvider from Admin
        /// </summary>
        public event EventHandler<PayloadEventArgs> AdminAddAIProviderReceived;
        protected virtual void OnAdminAddAIProviderReceived(StandardPayload payload, BasicDeliverEventArgs bdea)
        {
            var plea = new PayloadEventArgs(payload, bdea);
            if (!ReferenceEquals(this.AdminAddAIProviderReceived, null))
            {
                plea.Payload.IsHandled = true;
                this.AdminAddAIProviderReceived(this, plea);
            }
        }

        /// <summary>
        /// Responds to: GetAIProviders from Admin
        /// </summary>
        public event EventHandler<PayloadEventArgs> AdminGetAIProvidersReceived;
        protected virtual void OnAdminGetAIProvidersReceived(StandardPayload payload, BasicDeliverEventArgs bdea)
        {
            var plea = new PayloadEventArgs(payload, bdea);
            if (!ReferenceEquals(this.AdminGetAIProvidersReceived, null))
            {
                plea.Payload.IsHandled = true;
                this.AdminGetAIProvidersReceived(this, plea);
            }
        }

        /// <summary>
        /// Responds to: UpdateAIProvider from Admin
        /// </summary>
        public event EventHandler<PayloadEventArgs> AdminUpdateAIProviderReceived;
        protected virtual void OnAdminUpdateAIProviderReceived(StandardPayload payload, BasicDeliverEventArgs bdea)
        {
            var plea = new PayloadEventArgs(payload, bdea);
            if (!ReferenceEquals(this.AdminUpdateAIProviderReceived, null))
            {
                plea.Payload.IsHandled = true;
                this.AdminUpdateAIProviderReceived(this, plea);
            }
        }

        /// <summary>
        /// Responds to: DeleteAIProvider from Admin
        /// </summary>
        public event EventHandler<PayloadEventArgs> AdminDeleteAIProviderReceived;
        protected virtual void OnAdminDeleteAIProviderReceived(StandardPayload payload, BasicDeliverEventArgs bdea)
        {
            var plea = new PayloadEventArgs(payload, bdea);
            if (!ReferenceEquals(this.AdminDeleteAIProviderReceived, null))
            {
                plea.Payload.IsHandled = true;
                this.AdminDeleteAIProviderReceived(this, plea);
            }
        }

        /// <summary>
        /// Responds to: AddAICSkill from AICSuperAgent
        /// </summary>
        public event EventHandler<PayloadEventArgs> AICSuperAgentAddAICSkillReceived;
        protected virtual void OnAICSuperAgentAddAICSkillReceived(StandardPayload payload, BasicDeliverEventArgs bdea)
        {
            var plea = new PayloadEventArgs(payload, bdea);
            if (!ReferenceEquals(this.AICSuperAgentAddAICSkillReceived, null))
            {
                plea.Payload.IsHandled = true;
                this.AICSuperAgentAddAICSkillReceived(this, plea);
            }
        }

        /// <summary>
        /// Responds to: GetAICSkills from AICSuperAgent
        /// </summary>
        public event EventHandler<PayloadEventArgs> AICSuperAgentGetAICSkillsReceived;
        protected virtual void OnAICSuperAgentGetAICSkillsReceived(StandardPayload payload, BasicDeliverEventArgs bdea)
        {
            var plea = new PayloadEventArgs(payload, bdea);
            if (!ReferenceEquals(this.AICSuperAgentGetAICSkillsReceived, null))
            {
                plea.Payload.IsHandled = true;
                this.AICSuperAgentGetAICSkillsReceived(this, plea);
            }
        }

        /// <summary>
        /// Responds to: UpdateAICSkill from AICSuperAgent
        /// </summary>
        public event EventHandler<PayloadEventArgs> AICSuperAgentUpdateAICSkillReceived;
        protected virtual void OnAICSuperAgentUpdateAICSkillReceived(StandardPayload payload, BasicDeliverEventArgs bdea)
        {
            var plea = new PayloadEventArgs(payload, bdea);
            if (!ReferenceEquals(this.AICSuperAgentUpdateAICSkillReceived, null))
            {
                plea.Payload.IsHandled = true;
                this.AICSuperAgentUpdateAICSkillReceived(this, plea);
            }
        }

        /// <summary>
        /// Responds to: DeleteAICSkill from AICSuperAgent
        /// </summary>
        public event EventHandler<PayloadEventArgs> AICSuperAgentDeleteAICSkillReceived;
        protected virtual void OnAICSuperAgentDeleteAICSkillReceived(StandardPayload payload, BasicDeliverEventArgs bdea)
        {
            var plea = new PayloadEventArgs(payload, bdea);
            if (!ReferenceEquals(this.AICSuperAgentDeleteAICSkillReceived, null))
            {
                plea.Payload.IsHandled = true;
                this.AICSuperAgentDeleteAICSkillReceived(this, plea);
            }
        }

        /// <summary>
        /// Responds to: AddAICSkill from Admin
        /// </summary>
        public event EventHandler<PayloadEventArgs> AdminAddAICSkillReceived;
        protected virtual void OnAdminAddAICSkillReceived(StandardPayload payload, BasicDeliverEventArgs bdea)
        {
            var plea = new PayloadEventArgs(payload, bdea);
            if (!ReferenceEquals(this.AdminAddAICSkillReceived, null))
            {
                plea.Payload.IsHandled = true;
                this.AdminAddAICSkillReceived(this, plea);
            }
        }

        /// <summary>
        /// Responds to: GetAICSkills from Admin
        /// </summary>
        public event EventHandler<PayloadEventArgs> AdminGetAICSkillsReceived;
        protected virtual void OnAdminGetAICSkillsReceived(StandardPayload payload, BasicDeliverEventArgs bdea)
        {
            var plea = new PayloadEventArgs(payload, bdea);
            if (!ReferenceEquals(this.AdminGetAICSkillsReceived, null))
            {
                plea.Payload.IsHandled = true;
                this.AdminGetAICSkillsReceived(this, plea);
            }
        }

        /// <summary>
        /// Responds to: UpdateAICSkill from Admin
        /// </summary>
        public event EventHandler<PayloadEventArgs> AdminUpdateAICSkillReceived;
        protected virtual void OnAdminUpdateAICSkillReceived(StandardPayload payload, BasicDeliverEventArgs bdea)
        {
            var plea = new PayloadEventArgs(payload, bdea);
            if (!ReferenceEquals(this.AdminUpdateAICSkillReceived, null))
            {
                plea.Payload.IsHandled = true;
                this.AdminUpdateAICSkillReceived(this, plea);
            }
        }

        /// <summary>
        /// Responds to: DeleteAICSkill from Admin
        /// </summary>
        public event EventHandler<PayloadEventArgs> AdminDeleteAICSkillReceived;
        protected virtual void OnAdminDeleteAICSkillReceived(StandardPayload payload, BasicDeliverEventArgs bdea)
        {
            var plea = new PayloadEventArgs(payload, bdea);
            if (!ReferenceEquals(this.AdminDeleteAICSkillReceived, null))
            {
                plea.Payload.IsHandled = true;
                this.AdminDeleteAICSkillReceived(this, plea);
            }
        }

        /// <summary>
        /// Responds to: AddAICContext from AICSuperAgent
        /// </summary>
        public event EventHandler<PayloadEventArgs> AICSuperAgentAddAICContextReceived;
        protected virtual void OnAICSuperAgentAddAICContextReceived(StandardPayload payload, BasicDeliverEventArgs bdea)
        {
            var plea = new PayloadEventArgs(payload, bdea);
            if (!ReferenceEquals(this.AICSuperAgentAddAICContextReceived, null))
            {
                plea.Payload.IsHandled = true;
                this.AICSuperAgentAddAICContextReceived(this, plea);
            }
        }

        /// <summary>
        /// Responds to: GetAICContexts from AICSuperAgent
        /// </summary>
        public event EventHandler<PayloadEventArgs> AICSuperAgentGetAICContextsReceived;
        protected virtual void OnAICSuperAgentGetAICContextsReceived(StandardPayload payload, BasicDeliverEventArgs bdea)
        {
            var plea = new PayloadEventArgs(payload, bdea);
            if (!ReferenceEquals(this.AICSuperAgentGetAICContextsReceived, null))
            {
                plea.Payload.IsHandled = true;
                this.AICSuperAgentGetAICContextsReceived(this, plea);
            }
        }

        /// <summary>
        /// Responds to: UpdateAICContext from AICSuperAgent
        /// </summary>
        public event EventHandler<PayloadEventArgs> AICSuperAgentUpdateAICContextReceived;
        protected virtual void OnAICSuperAgentUpdateAICContextReceived(StandardPayload payload, BasicDeliverEventArgs bdea)
        {
            var plea = new PayloadEventArgs(payload, bdea);
            if (!ReferenceEquals(this.AICSuperAgentUpdateAICContextReceived, null))
            {
                plea.Payload.IsHandled = true;
                this.AICSuperAgentUpdateAICContextReceived(this, plea);
            }
        }

        /// <summary>
        /// Responds to: DeleteAICContext from AICSuperAgent
        /// </summary>
        public event EventHandler<PayloadEventArgs> AICSuperAgentDeleteAICContextReceived;
        protected virtual void OnAICSuperAgentDeleteAICContextReceived(StandardPayload payload, BasicDeliverEventArgs bdea)
        {
            var plea = new PayloadEventArgs(payload, bdea);
            if (!ReferenceEquals(this.AICSuperAgentDeleteAICContextReceived, null))
            {
                plea.Payload.IsHandled = true;
                this.AICSuperAgentDeleteAICContextReceived(this, plea);
            }
        }

        /// <summary>
        /// Responds to: AddAICContext from Admin
        /// </summary>
        public event EventHandler<PayloadEventArgs> AdminAddAICContextReceived;
        protected virtual void OnAdminAddAICContextReceived(StandardPayload payload, BasicDeliverEventArgs bdea)
        {
            var plea = new PayloadEventArgs(payload, bdea);
            if (!ReferenceEquals(this.AdminAddAICContextReceived, null))
            {
                plea.Payload.IsHandled = true;
                this.AdminAddAICContextReceived(this, plea);
            }
        }

        /// <summary>
        /// Responds to: GetAICContexts from Admin
        /// </summary>
        public event EventHandler<PayloadEventArgs> AdminGetAICContextsReceived;
        protected virtual void OnAdminGetAICContextsReceived(StandardPayload payload, BasicDeliverEventArgs bdea)
        {
            var plea = new PayloadEventArgs(payload, bdea);
            if (!ReferenceEquals(this.AdminGetAICContextsReceived, null))
            {
                plea.Payload.IsHandled = true;
                this.AdminGetAICContextsReceived(this, plea);
            }
        }

        /// <summary>
        /// Responds to: UpdateAICContext from Admin
        /// </summary>
        public event EventHandler<PayloadEventArgs> AdminUpdateAICContextReceived;
        protected virtual void OnAdminUpdateAICContextReceived(StandardPayload payload, BasicDeliverEventArgs bdea)
        {
            var plea = new PayloadEventArgs(payload, bdea);
            if (!ReferenceEquals(this.AdminUpdateAICContextReceived, null))
            {
                plea.Payload.IsHandled = true;
                this.AdminUpdateAICContextReceived(this, plea);
            }
        }

        /// <summary>
        /// Responds to: DeleteAICContext from Admin
        /// </summary>
        public event EventHandler<PayloadEventArgs> AdminDeleteAICContextReceived;
        protected virtual void OnAdminDeleteAICContextReceived(StandardPayload payload, BasicDeliverEventArgs bdea)
        {
            var plea = new PayloadEventArgs(payload, bdea);
            if (!ReferenceEquals(this.AdminDeleteAICContextReceived, null))
            {
                plea.Payload.IsHandled = true;
                this.AdminDeleteAICContextReceived(this, plea);
            }
        }

        /// <summary>
        /// Responds to: AddFriendRequest from AICSuperAgent
        /// </summary>
        public event EventHandler<PayloadEventArgs> AICSuperAgentAddFriendRequestReceived;
        protected virtual void OnAICSuperAgentAddFriendRequestReceived(StandardPayload payload, BasicDeliverEventArgs bdea)
        {
            var plea = new PayloadEventArgs(payload, bdea);
            if (!ReferenceEquals(this.AICSuperAgentAddFriendRequestReceived, null))
            {
                plea.Payload.IsHandled = true;
                this.AICSuperAgentAddFriendRequestReceived(this, plea);
            }
        }

        /// <summary>
        /// Responds to: GetFriendRequests from AICSuperAgent
        /// </summary>
        public event EventHandler<PayloadEventArgs> AICSuperAgentGetFriendRequestsReceived;
        protected virtual void OnAICSuperAgentGetFriendRequestsReceived(StandardPayload payload, BasicDeliverEventArgs bdea)
        {
            var plea = new PayloadEventArgs(payload, bdea);
            if (!ReferenceEquals(this.AICSuperAgentGetFriendRequestsReceived, null))
            {
                plea.Payload.IsHandled = true;
                this.AICSuperAgentGetFriendRequestsReceived(this, plea);
            }
        }

        /// <summary>
        /// Responds to: UpdateFriendRequest from AICSuperAgent
        /// </summary>
        public event EventHandler<PayloadEventArgs> AICSuperAgentUpdateFriendRequestReceived;
        protected virtual void OnAICSuperAgentUpdateFriendRequestReceived(StandardPayload payload, BasicDeliverEventArgs bdea)
        {
            var plea = new PayloadEventArgs(payload, bdea);
            if (!ReferenceEquals(this.AICSuperAgentUpdateFriendRequestReceived, null))
            {
                plea.Payload.IsHandled = true;
                this.AICSuperAgentUpdateFriendRequestReceived(this, plea);
            }
        }

        /// <summary>
        /// Responds to: DeleteFriendRequest from AICSuperAgent
        /// </summary>
        public event EventHandler<PayloadEventArgs> AICSuperAgentDeleteFriendRequestReceived;
        protected virtual void OnAICSuperAgentDeleteFriendRequestReceived(StandardPayload payload, BasicDeliverEventArgs bdea)
        {
            var plea = new PayloadEventArgs(payload, bdea);
            if (!ReferenceEquals(this.AICSuperAgentDeleteFriendRequestReceived, null))
            {
                plea.Payload.IsHandled = true;
                this.AICSuperAgentDeleteFriendRequestReceived(this, plea);
            }
        }

        /// <summary>
        /// Responds to: AddFriendRequest from Admin
        /// </summary>
        public event EventHandler<PayloadEventArgs> AdminAddFriendRequestReceived;
        protected virtual void OnAdminAddFriendRequestReceived(StandardPayload payload, BasicDeliverEventArgs bdea)
        {
            var plea = new PayloadEventArgs(payload, bdea);
            if (!ReferenceEquals(this.AdminAddFriendRequestReceived, null))
            {
                plea.Payload.IsHandled = true;
                this.AdminAddFriendRequestReceived(this, plea);
            }
        }

        /// <summary>
        /// Responds to: GetFriendRequests from Admin
        /// </summary>
        public event EventHandler<PayloadEventArgs> AdminGetFriendRequestsReceived;
        protected virtual void OnAdminGetFriendRequestsReceived(StandardPayload payload, BasicDeliverEventArgs bdea)
        {
            var plea = new PayloadEventArgs(payload, bdea);
            if (!ReferenceEquals(this.AdminGetFriendRequestsReceived, null))
            {
                plea.Payload.IsHandled = true;
                this.AdminGetFriendRequestsReceived(this, plea);
            }
        }

        /// <summary>
        /// Responds to: UpdateFriendRequest from Admin
        /// </summary>
        public event EventHandler<PayloadEventArgs> AdminUpdateFriendRequestReceived;
        protected virtual void OnAdminUpdateFriendRequestReceived(StandardPayload payload, BasicDeliverEventArgs bdea)
        {
            var plea = new PayloadEventArgs(payload, bdea);
            if (!ReferenceEquals(this.AdminUpdateFriendRequestReceived, null))
            {
                plea.Payload.IsHandled = true;
                this.AdminUpdateFriendRequestReceived(this, plea);
            }
        }

        /// <summary>
        /// Responds to: DeleteFriendRequest from Admin
        /// </summary>
        public event EventHandler<PayloadEventArgs> AdminDeleteFriendRequestReceived;
        protected virtual void OnAdminDeleteFriendRequestReceived(StandardPayload payload, BasicDeliverEventArgs bdea)
        {
            var plea = new PayloadEventArgs(payload, bdea);
            if (!ReferenceEquals(this.AdminDeleteFriendRequestReceived, null))
            {
                plea.Payload.IsHandled = true;
                this.AdminDeleteFriendRequestReceived(this, plea);
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
        /// Responds to: AddAICConversation from AICSuperAgent
        /// </summary>
        public event EventHandler<PayloadEventArgs> AICSuperAgentAddAICConversationReceived;
        protected virtual void OnAICSuperAgentAddAICConversationReceived(StandardPayload payload, BasicDeliverEventArgs bdea)
        {
            var plea = new PayloadEventArgs(payload, bdea);
            if (!ReferenceEquals(this.AICSuperAgentAddAICConversationReceived, null))
            {
                plea.Payload.IsHandled = true;
                this.AICSuperAgentAddAICConversationReceived(this, plea);
            }
        }

        /// <summary>
        /// Responds to: GetAICConversations from AICSuperAgent
        /// </summary>
        public event EventHandler<PayloadEventArgs> AICSuperAgentGetAICConversationsReceived;
        protected virtual void OnAICSuperAgentGetAICConversationsReceived(StandardPayload payload, BasicDeliverEventArgs bdea)
        {
            var plea = new PayloadEventArgs(payload, bdea);
            if (!ReferenceEquals(this.AICSuperAgentGetAICConversationsReceived, null))
            {
                plea.Payload.IsHandled = true;
                this.AICSuperAgentGetAICConversationsReceived(this, plea);
            }
        }

        /// <summary>
        /// Responds to: UpdateAICConversation from AICSuperAgent
        /// </summary>
        public event EventHandler<PayloadEventArgs> AICSuperAgentUpdateAICConversationReceived;
        protected virtual void OnAICSuperAgentUpdateAICConversationReceived(StandardPayload payload, BasicDeliverEventArgs bdea)
        {
            var plea = new PayloadEventArgs(payload, bdea);
            if (!ReferenceEquals(this.AICSuperAgentUpdateAICConversationReceived, null))
            {
                plea.Payload.IsHandled = true;
                this.AICSuperAgentUpdateAICConversationReceived(this, plea);
            }
        }

        /// <summary>
        /// Responds to: DeleteAICConversation from AICSuperAgent
        /// </summary>
        public event EventHandler<PayloadEventArgs> AICSuperAgentDeleteAICConversationReceived;
        protected virtual void OnAICSuperAgentDeleteAICConversationReceived(StandardPayload payload, BasicDeliverEventArgs bdea)
        {
            var plea = new PayloadEventArgs(payload, bdea);
            if (!ReferenceEquals(this.AICSuperAgentDeleteAICConversationReceived, null))
            {
                plea.Payload.IsHandled = true;
                this.AICSuperAgentDeleteAICConversationReceived(this, plea);
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
        /// Responds to: AddEntity from AICSuperAgent
        /// </summary>
        public event EventHandler<PayloadEventArgs> AICSuperAgentAddEntityReceived;
        protected virtual void OnAICSuperAgentAddEntityReceived(StandardPayload payload, BasicDeliverEventArgs bdea)
        {
            var plea = new PayloadEventArgs(payload, bdea);
            if (!ReferenceEquals(this.AICSuperAgentAddEntityReceived, null))
            {
                plea.Payload.IsHandled = true;
                this.AICSuperAgentAddEntityReceived(this, plea);
            }
        }

        /// <summary>
        /// Responds to: GetEntities from AICSuperAgent
        /// </summary>
        public event EventHandler<PayloadEventArgs> AICSuperAgentGetEntitiesReceived;
        protected virtual void OnAICSuperAgentGetEntitiesReceived(StandardPayload payload, BasicDeliverEventArgs bdea)
        {
            var plea = new PayloadEventArgs(payload, bdea);
            if (!ReferenceEquals(this.AICSuperAgentGetEntitiesReceived, null))
            {
                plea.Payload.IsHandled = true;
                this.AICSuperAgentGetEntitiesReceived(this, plea);
            }
        }

        /// <summary>
        /// Responds to: UpdateEntity from AICSuperAgent
        /// </summary>
        public event EventHandler<PayloadEventArgs> AICSuperAgentUpdateEntityReceived;
        protected virtual void OnAICSuperAgentUpdateEntityReceived(StandardPayload payload, BasicDeliverEventArgs bdea)
        {
            var plea = new PayloadEventArgs(payload, bdea);
            if (!ReferenceEquals(this.AICSuperAgentUpdateEntityReceived, null))
            {
                plea.Payload.IsHandled = true;
                this.AICSuperAgentUpdateEntityReceived(this, plea);
            }
        }

        /// <summary>
        /// Responds to: DeleteEntity from AICSuperAgent
        /// </summary>
        public event EventHandler<PayloadEventArgs> AICSuperAgentDeleteEntityReceived;
        protected virtual void OnAICSuperAgentDeleteEntityReceived(StandardPayload payload, BasicDeliverEventArgs bdea)
        {
            var plea = new PayloadEventArgs(payload, bdea);
            if (!ReferenceEquals(this.AICSuperAgentDeleteEntityReceived, null))
            {
                plea.Payload.IsHandled = true;
                this.AICSuperAgentDeleteEntityReceived(this, plea);
            }
        }

        /// <summary>
        /// Responds to: AddEntity from Admin
        /// </summary>
        public event EventHandler<PayloadEventArgs> AdminAddEntityReceived;
        protected virtual void OnAdminAddEntityReceived(StandardPayload payload, BasicDeliverEventArgs bdea)
        {
            var plea = new PayloadEventArgs(payload, bdea);
            if (!ReferenceEquals(this.AdminAddEntityReceived, null))
            {
                plea.Payload.IsHandled = true;
                this.AdminAddEntityReceived(this, plea);
            }
        }

        /// <summary>
        /// Responds to: GetEntities from Admin
        /// </summary>
        public event EventHandler<PayloadEventArgs> AdminGetEntitiesReceived;
        protected virtual void OnAdminGetEntitiesReceived(StandardPayload payload, BasicDeliverEventArgs bdea)
        {
            var plea = new PayloadEventArgs(payload, bdea);
            if (!ReferenceEquals(this.AdminGetEntitiesReceived, null))
            {
                plea.Payload.IsHandled = true;
                this.AdminGetEntitiesReceived(this, plea);
            }
        }

        /// <summary>
        /// Responds to: UpdateEntity from Admin
        /// </summary>
        public event EventHandler<PayloadEventArgs> AdminUpdateEntityReceived;
        protected virtual void OnAdminUpdateEntityReceived(StandardPayload payload, BasicDeliverEventArgs bdea)
        {
            var plea = new PayloadEventArgs(payload, bdea);
            if (!ReferenceEquals(this.AdminUpdateEntityReceived, null))
            {
                plea.Payload.IsHandled = true;
                this.AdminUpdateEntityReceived(this, plea);
            }
        }

        /// <summary>
        /// Responds to: DeleteEntity from Admin
        /// </summary>
        public event EventHandler<PayloadEventArgs> AdminDeleteEntityReceived;
        protected virtual void OnAdminDeleteEntityReceived(StandardPayload payload, BasicDeliverEventArgs bdea)
        {
            var plea = new PayloadEventArgs(payload, bdea);
            if (!ReferenceEquals(this.AdminDeleteEntityReceived, null))
            {
                plea.Payload.IsHandled = true;
                this.AdminDeleteEntityReceived(this, plea);
            }
        }

        /// <summary>
        /// Responds to: AddAICSkillVersion from AICSuperAgent
        /// </summary>
        public event EventHandler<PayloadEventArgs> AICSuperAgentAddAICSkillVersionReceived;
        protected virtual void OnAICSuperAgentAddAICSkillVersionReceived(StandardPayload payload, BasicDeliverEventArgs bdea)
        {
            var plea = new PayloadEventArgs(payload, bdea);
            if (!ReferenceEquals(this.AICSuperAgentAddAICSkillVersionReceived, null))
            {
                plea.Payload.IsHandled = true;
                this.AICSuperAgentAddAICSkillVersionReceived(this, plea);
            }
        }

        /// <summary>
        /// Responds to: GetAICSkillVersions from AICSuperAgent
        /// </summary>
        public event EventHandler<PayloadEventArgs> AICSuperAgentGetAICSkillVersionsReceived;
        protected virtual void OnAICSuperAgentGetAICSkillVersionsReceived(StandardPayload payload, BasicDeliverEventArgs bdea)
        {
            var plea = new PayloadEventArgs(payload, bdea);
            if (!ReferenceEquals(this.AICSuperAgentGetAICSkillVersionsReceived, null))
            {
                plea.Payload.IsHandled = true;
                this.AICSuperAgentGetAICSkillVersionsReceived(this, plea);
            }
        }

        /// <summary>
        /// Responds to: UpdateAICSkillVersion from AICSuperAgent
        /// </summary>
        public event EventHandler<PayloadEventArgs> AICSuperAgentUpdateAICSkillVersionReceived;
        protected virtual void OnAICSuperAgentUpdateAICSkillVersionReceived(StandardPayload payload, BasicDeliverEventArgs bdea)
        {
            var plea = new PayloadEventArgs(payload, bdea);
            if (!ReferenceEquals(this.AICSuperAgentUpdateAICSkillVersionReceived, null))
            {
                plea.Payload.IsHandled = true;
                this.AICSuperAgentUpdateAICSkillVersionReceived(this, plea);
            }
        }

        /// <summary>
        /// Responds to: DeleteAICSkillVersion from AICSuperAgent
        /// </summary>
        public event EventHandler<PayloadEventArgs> AICSuperAgentDeleteAICSkillVersionReceived;
        protected virtual void OnAICSuperAgentDeleteAICSkillVersionReceived(StandardPayload payload, BasicDeliverEventArgs bdea)
        {
            var plea = new PayloadEventArgs(payload, bdea);
            if (!ReferenceEquals(this.AICSuperAgentDeleteAICSkillVersionReceived, null))
            {
                plea.Payload.IsHandled = true;
                this.AICSuperAgentDeleteAICSkillVersionReceived(this, plea);
            }
        }

        /// <summary>
        /// Responds to: AddAICSkillVersion from Admin
        /// </summary>
        public event EventHandler<PayloadEventArgs> AdminAddAICSkillVersionReceived;
        protected virtual void OnAdminAddAICSkillVersionReceived(StandardPayload payload, BasicDeliverEventArgs bdea)
        {
            var plea = new PayloadEventArgs(payload, bdea);
            if (!ReferenceEquals(this.AdminAddAICSkillVersionReceived, null))
            {
                plea.Payload.IsHandled = true;
                this.AdminAddAICSkillVersionReceived(this, plea);
            }
        }

        /// <summary>
        /// Responds to: GetAICSkillVersions from Admin
        /// </summary>
        public event EventHandler<PayloadEventArgs> AdminGetAICSkillVersionsReceived;
        protected virtual void OnAdminGetAICSkillVersionsReceived(StandardPayload payload, BasicDeliverEventArgs bdea)
        {
            var plea = new PayloadEventArgs(payload, bdea);
            if (!ReferenceEquals(this.AdminGetAICSkillVersionsReceived, null))
            {
                plea.Payload.IsHandled = true;
                this.AdminGetAICSkillVersionsReceived(this, plea);
            }
        }

        /// <summary>
        /// Responds to: UpdateAICSkillVersion from Admin
        /// </summary>
        public event EventHandler<PayloadEventArgs> AdminUpdateAICSkillVersionReceived;
        protected virtual void OnAdminUpdateAICSkillVersionReceived(StandardPayload payload, BasicDeliverEventArgs bdea)
        {
            var plea = new PayloadEventArgs(payload, bdea);
            if (!ReferenceEquals(this.AdminUpdateAICSkillVersionReceived, null))
            {
                plea.Payload.IsHandled = true;
                this.AdminUpdateAICSkillVersionReceived(this, plea);
            }
        }

        /// <summary>
        /// Responds to: DeleteAICSkillVersion from Admin
        /// </summary>
        public event EventHandler<PayloadEventArgs> AdminDeleteAICSkillVersionReceived;
        protected virtual void OnAdminDeleteAICSkillVersionReceived(StandardPayload payload, BasicDeliverEventArgs bdea)
        {
            var plea = new PayloadEventArgs(payload, bdea);
            if (!ReferenceEquals(this.AdminDeleteAICSkillVersionReceived, null))
            {
                plea.Payload.IsHandled = true;
                this.AdminDeleteAICSkillVersionReceived(this, plea);
            }
        }

        /// <summary>
        /// Responds to: AddAICMessage from AICSuperAgent
        /// </summary>
        public event EventHandler<PayloadEventArgs> AICSuperAgentAddAICMessageReceived;
        protected virtual void OnAICSuperAgentAddAICMessageReceived(StandardPayload payload, BasicDeliverEventArgs bdea)
        {
            var plea = new PayloadEventArgs(payload, bdea);
            if (!ReferenceEquals(this.AICSuperAgentAddAICMessageReceived, null))
            {
                plea.Payload.IsHandled = true;
                this.AICSuperAgentAddAICMessageReceived(this, plea);
            }
        }

        /// <summary>
        /// Responds to: GetAICMessages from AICSuperAgent
        /// </summary>
        public event EventHandler<PayloadEventArgs> AICSuperAgentGetAICMessagesReceived;
        protected virtual void OnAICSuperAgentGetAICMessagesReceived(StandardPayload payload, BasicDeliverEventArgs bdea)
        {
            var plea = new PayloadEventArgs(payload, bdea);
            if (!ReferenceEquals(this.AICSuperAgentGetAICMessagesReceived, null))
            {
                plea.Payload.IsHandled = true;
                this.AICSuperAgentGetAICMessagesReceived(this, plea);
            }
        }

        /// <summary>
        /// Responds to: UpdateAICMessage from AICSuperAgent
        /// </summary>
        public event EventHandler<PayloadEventArgs> AICSuperAgentUpdateAICMessageReceived;
        protected virtual void OnAICSuperAgentUpdateAICMessageReceived(StandardPayload payload, BasicDeliverEventArgs bdea)
        {
            var plea = new PayloadEventArgs(payload, bdea);
            if (!ReferenceEquals(this.AICSuperAgentUpdateAICMessageReceived, null))
            {
                plea.Payload.IsHandled = true;
                this.AICSuperAgentUpdateAICMessageReceived(this, plea);
            }
        }

        /// <summary>
        /// Responds to: DeleteAICMessage from AICSuperAgent
        /// </summary>
        public event EventHandler<PayloadEventArgs> AICSuperAgentDeleteAICMessageReceived;
        protected virtual void OnAICSuperAgentDeleteAICMessageReceived(StandardPayload payload, BasicDeliverEventArgs bdea)
        {
            var plea = new PayloadEventArgs(payload, bdea);
            if (!ReferenceEquals(this.AICSuperAgentDeleteAICMessageReceived, null))
            {
                plea.Payload.IsHandled = true;
                this.AICSuperAgentDeleteAICMessageReceived(this, plea);
            }
        }

        /// <summary>
        /// Responds to: AddAICMessage from Admin
        /// </summary>
        public event EventHandler<PayloadEventArgs> AdminAddAICMessageReceived;
        protected virtual void OnAdminAddAICMessageReceived(StandardPayload payload, BasicDeliverEventArgs bdea)
        {
            var plea = new PayloadEventArgs(payload, bdea);
            if (!ReferenceEquals(this.AdminAddAICMessageReceived, null))
            {
                plea.Payload.IsHandled = true;
                this.AdminAddAICMessageReceived(this, plea);
            }
        }

        /// <summary>
        /// Responds to: GetAICMessages from Admin
        /// </summary>
        public event EventHandler<PayloadEventArgs> AdminGetAICMessagesReceived;
        protected virtual void OnAdminGetAICMessagesReceived(StandardPayload payload, BasicDeliverEventArgs bdea)
        {
            var plea = new PayloadEventArgs(payload, bdea);
            if (!ReferenceEquals(this.AdminGetAICMessagesReceived, null))
            {
                plea.Payload.IsHandled = true;
                this.AdminGetAICMessagesReceived(this, plea);
            }
        }

        /// <summary>
        /// Responds to: UpdateAICMessage from Admin
        /// </summary>
        public event EventHandler<PayloadEventArgs> AdminUpdateAICMessageReceived;
        protected virtual void OnAdminUpdateAICMessageReceived(StandardPayload payload, BasicDeliverEventArgs bdea)
        {
            var plea = new PayloadEventArgs(payload, bdea);
            if (!ReferenceEquals(this.AdminUpdateAICMessageReceived, null))
            {
                plea.Payload.IsHandled = true;
                this.AdminUpdateAICMessageReceived(this, plea);
            }
        }

        /// <summary>
        /// Responds to: DeleteAICMessage from Admin
        /// </summary>
        public event EventHandler<PayloadEventArgs> AdminDeleteAICMessageReceived;
        protected virtual void OnAdminDeleteAICMessageReceived(StandardPayload payload, BasicDeliverEventArgs bdea)
        {
            var plea = new PayloadEventArgs(payload, bdea);
            if (!ReferenceEquals(this.AdminDeleteAICMessageReceived, null))
            {
                plea.Payload.IsHandled = true;
                this.AdminDeleteAICMessageReceived(this, plea);
            }
        }

        /// <summary>
        /// Responds to: AddRepoBranch from AICSuperAgent
        /// </summary>
        public event EventHandler<PayloadEventArgs> AICSuperAgentAddRepoBranchReceived;
        protected virtual void OnAICSuperAgentAddRepoBranchReceived(StandardPayload payload, BasicDeliverEventArgs bdea)
        {
            var plea = new PayloadEventArgs(payload, bdea);
            if (!ReferenceEquals(this.AICSuperAgentAddRepoBranchReceived, null))
            {
                plea.Payload.IsHandled = true;
                this.AICSuperAgentAddRepoBranchReceived(this, plea);
            }
        }

        /// <summary>
        /// Responds to: GetRepoBranches from AICSuperAgent
        /// </summary>
        public event EventHandler<PayloadEventArgs> AICSuperAgentGetRepoBranchesReceived;
        protected virtual void OnAICSuperAgentGetRepoBranchesReceived(StandardPayload payload, BasicDeliverEventArgs bdea)
        {
            var plea = new PayloadEventArgs(payload, bdea);
            if (!ReferenceEquals(this.AICSuperAgentGetRepoBranchesReceived, null))
            {
                plea.Payload.IsHandled = true;
                this.AICSuperAgentGetRepoBranchesReceived(this, plea);
            }
        }

        /// <summary>
        /// Responds to: UpdateRepoBranch from AICSuperAgent
        /// </summary>
        public event EventHandler<PayloadEventArgs> AICSuperAgentUpdateRepoBranchReceived;
        protected virtual void OnAICSuperAgentUpdateRepoBranchReceived(StandardPayload payload, BasicDeliverEventArgs bdea)
        {
            var plea = new PayloadEventArgs(payload, bdea);
            if (!ReferenceEquals(this.AICSuperAgentUpdateRepoBranchReceived, null))
            {
                plea.Payload.IsHandled = true;
                this.AICSuperAgentUpdateRepoBranchReceived(this, plea);
            }
        }

        /// <summary>
        /// Responds to: DeleteRepoBranch from AICSuperAgent
        /// </summary>
        public event EventHandler<PayloadEventArgs> AICSuperAgentDeleteRepoBranchReceived;
        protected virtual void OnAICSuperAgentDeleteRepoBranchReceived(StandardPayload payload, BasicDeliverEventArgs bdea)
        {
            var plea = new PayloadEventArgs(payload, bdea);
            if (!ReferenceEquals(this.AICSuperAgentDeleteRepoBranchReceived, null))
            {
                plea.Payload.IsHandled = true;
                this.AICSuperAgentDeleteRepoBranchReceived(this, plea);
            }
        }

        /// <summary>
        /// Responds to: AddRepoBranch from Admin
        /// </summary>
        public event EventHandler<PayloadEventArgs> AdminAddRepoBranchReceived;
        protected virtual void OnAdminAddRepoBranchReceived(StandardPayload payload, BasicDeliverEventArgs bdea)
        {
            var plea = new PayloadEventArgs(payload, bdea);
            if (!ReferenceEquals(this.AdminAddRepoBranchReceived, null))
            {
                plea.Payload.IsHandled = true;
                this.AdminAddRepoBranchReceived(this, plea);
            }
        }

        /// <summary>
        /// Responds to: GetRepoBranches from Admin
        /// </summary>
        public event EventHandler<PayloadEventArgs> AdminGetRepoBranchesReceived;
        protected virtual void OnAdminGetRepoBranchesReceived(StandardPayload payload, BasicDeliverEventArgs bdea)
        {
            var plea = new PayloadEventArgs(payload, bdea);
            if (!ReferenceEquals(this.AdminGetRepoBranchesReceived, null))
            {
                plea.Payload.IsHandled = true;
                this.AdminGetRepoBranchesReceived(this, plea);
            }
        }

        /// <summary>
        /// Responds to: UpdateRepoBranch from Admin
        /// </summary>
        public event EventHandler<PayloadEventArgs> AdminUpdateRepoBranchReceived;
        protected virtual void OnAdminUpdateRepoBranchReceived(StandardPayload payload, BasicDeliverEventArgs bdea)
        {
            var plea = new PayloadEventArgs(payload, bdea);
            if (!ReferenceEquals(this.AdminUpdateRepoBranchReceived, null))
            {
                plea.Payload.IsHandled = true;
                this.AdminUpdateRepoBranchReceived(this, plea);
            }
        }

        /// <summary>
        /// Responds to: DeleteRepoBranch from Admin
        /// </summary>
        public event EventHandler<PayloadEventArgs> AdminDeleteRepoBranchReceived;
        protected virtual void OnAdminDeleteRepoBranchReceived(StandardPayload payload, BasicDeliverEventArgs bdea)
        {
            var plea = new PayloadEventArgs(payload, bdea);
            if (!ReferenceEquals(this.AdminDeleteRepoBranchReceived, null))
            {
                plea.Payload.IsHandled = true;
                this.AdminDeleteRepoBranchReceived(this, plea);
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
        /// Responds to: AddAICProject from AICSuperAgent
        /// </summary>
        public event EventHandler<PayloadEventArgs> AICSuperAgentAddAICProjectReceived;
        protected virtual void OnAICSuperAgentAddAICProjectReceived(StandardPayload payload, BasicDeliverEventArgs bdea)
        {
            var plea = new PayloadEventArgs(payload, bdea);
            if (!ReferenceEquals(this.AICSuperAgentAddAICProjectReceived, null))
            {
                plea.Payload.IsHandled = true;
                this.AICSuperAgentAddAICProjectReceived(this, plea);
            }
        }

        /// <summary>
        /// Responds to: GetAICProjects from AICSuperAgent
        /// </summary>
        public event EventHandler<PayloadEventArgs> AICSuperAgentGetAICProjectsReceived;
        protected virtual void OnAICSuperAgentGetAICProjectsReceived(StandardPayload payload, BasicDeliverEventArgs bdea)
        {
            var plea = new PayloadEventArgs(payload, bdea);
            if (!ReferenceEquals(this.AICSuperAgentGetAICProjectsReceived, null))
            {
                plea.Payload.IsHandled = true;
                this.AICSuperAgentGetAICProjectsReceived(this, plea);
            }
        }

        /// <summary>
        /// Responds to: UpdateAICProject from AICSuperAgent
        /// </summary>
        public event EventHandler<PayloadEventArgs> AICSuperAgentUpdateAICProjectReceived;
        protected virtual void OnAICSuperAgentUpdateAICProjectReceived(StandardPayload payload, BasicDeliverEventArgs bdea)
        {
            var plea = new PayloadEventArgs(payload, bdea);
            if (!ReferenceEquals(this.AICSuperAgentUpdateAICProjectReceived, null))
            {
                plea.Payload.IsHandled = true;
                this.AICSuperAgentUpdateAICProjectReceived(this, plea);
            }
        }

        /// <summary>
        /// Responds to: DeleteAICProject from AICSuperAgent
        /// </summary>
        public event EventHandler<PayloadEventArgs> AICSuperAgentDeleteAICProjectReceived;
        protected virtual void OnAICSuperAgentDeleteAICProjectReceived(StandardPayload payload, BasicDeliverEventArgs bdea)
        {
            var plea = new PayloadEventArgs(payload, bdea);
            if (!ReferenceEquals(this.AICSuperAgentDeleteAICProjectReceived, null))
            {
                plea.Payload.IsHandled = true;
                this.AICSuperAgentDeleteAICProjectReceived(this, plea);
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
        /// Responds to: AddAICFile from AICSuperAgent
        /// </summary>
        public event EventHandler<PayloadEventArgs> AICSuperAgentAddAICFileReceived;
        protected virtual void OnAICSuperAgentAddAICFileReceived(StandardPayload payload, BasicDeliverEventArgs bdea)
        {
            var plea = new PayloadEventArgs(payload, bdea);
            if (!ReferenceEquals(this.AICSuperAgentAddAICFileReceived, null))
            {
                plea.Payload.IsHandled = true;
                this.AICSuperAgentAddAICFileReceived(this, plea);
            }
        }

        /// <summary>
        /// Responds to: GetAICFiles from AICSuperAgent
        /// </summary>
        public event EventHandler<PayloadEventArgs> AICSuperAgentGetAICFilesReceived;
        protected virtual void OnAICSuperAgentGetAICFilesReceived(StandardPayload payload, BasicDeliverEventArgs bdea)
        {
            var plea = new PayloadEventArgs(payload, bdea);
            if (!ReferenceEquals(this.AICSuperAgentGetAICFilesReceived, null))
            {
                plea.Payload.IsHandled = true;
                this.AICSuperAgentGetAICFilesReceived(this, plea);
            }
        }

        /// <summary>
        /// Responds to: UpdateAICFile from AICSuperAgent
        /// </summary>
        public event EventHandler<PayloadEventArgs> AICSuperAgentUpdateAICFileReceived;
        protected virtual void OnAICSuperAgentUpdateAICFileReceived(StandardPayload payload, BasicDeliverEventArgs bdea)
        {
            var plea = new PayloadEventArgs(payload, bdea);
            if (!ReferenceEquals(this.AICSuperAgentUpdateAICFileReceived, null))
            {
                plea.Payload.IsHandled = true;
                this.AICSuperAgentUpdateAICFileReceived(this, plea);
            }
        }

        /// <summary>
        /// Responds to: DeleteAICFile from AICSuperAgent
        /// </summary>
        public event EventHandler<PayloadEventArgs> AICSuperAgentDeleteAICFileReceived;
        protected virtual void OnAICSuperAgentDeleteAICFileReceived(StandardPayload payload, BasicDeliverEventArgs bdea)
        {
            var plea = new PayloadEventArgs(payload, bdea);
            if (!ReferenceEquals(this.AICSuperAgentDeleteAICFileReceived, null))
            {
                plea.Payload.IsHandled = true;
                this.AICSuperAgentDeleteAICFileReceived(this, plea);
            }
        }

        /// <summary>
        /// Responds to: AddAICFile from Admin
        /// </summary>
        public event EventHandler<PayloadEventArgs> AdminAddAICFileReceived;
        protected virtual void OnAdminAddAICFileReceived(StandardPayload payload, BasicDeliverEventArgs bdea)
        {
            var plea = new PayloadEventArgs(payload, bdea);
            if (!ReferenceEquals(this.AdminAddAICFileReceived, null))
            {
                plea.Payload.IsHandled = true;
                this.AdminAddAICFileReceived(this, plea);
            }
        }

        /// <summary>
        /// Responds to: GetAICFiles from Admin
        /// </summary>
        public event EventHandler<PayloadEventArgs> AdminGetAICFilesReceived;
        protected virtual void OnAdminGetAICFilesReceived(StandardPayload payload, BasicDeliverEventArgs bdea)
        {
            var plea = new PayloadEventArgs(payload, bdea);
            if (!ReferenceEquals(this.AdminGetAICFilesReceived, null))
            {
                plea.Payload.IsHandled = true;
                this.AdminGetAICFilesReceived(this, plea);
            }
        }

        /// <summary>
        /// Responds to: UpdateAICFile from Admin
        /// </summary>
        public event EventHandler<PayloadEventArgs> AdminUpdateAICFileReceived;
        protected virtual void OnAdminUpdateAICFileReceived(StandardPayload payload, BasicDeliverEventArgs bdea)
        {
            var plea = new PayloadEventArgs(payload, bdea);
            if (!ReferenceEquals(this.AdminUpdateAICFileReceived, null))
            {
                plea.Payload.IsHandled = true;
                this.AdminUpdateAICFileReceived(this, plea);
            }
        }

        /// <summary>
        /// Responds to: DeleteAICFile from Admin
        /// </summary>
        public event EventHandler<PayloadEventArgs> AdminDeleteAICFileReceived;
        protected virtual void OnAdminDeleteAICFileReceived(StandardPayload payload, BasicDeliverEventArgs bdea)
        {
            var plea = new PayloadEventArgs(payload, bdea);
            if (!ReferenceEquals(this.AdminDeleteAICFileReceived, null))
            {
                plea.Payload.IsHandled = true;
                this.AdminDeleteAICFileReceived(this, plea);
            }
        }

        /// <summary>
        /// Responds to: AddAICSkillStep from AICSuperAgent
        /// </summary>
        public event EventHandler<PayloadEventArgs> AICSuperAgentAddAICSkillStepReceived;
        protected virtual void OnAICSuperAgentAddAICSkillStepReceived(StandardPayload payload, BasicDeliverEventArgs bdea)
        {
            var plea = new PayloadEventArgs(payload, bdea);
            if (!ReferenceEquals(this.AICSuperAgentAddAICSkillStepReceived, null))
            {
                plea.Payload.IsHandled = true;
                this.AICSuperAgentAddAICSkillStepReceived(this, plea);
            }
        }

        /// <summary>
        /// Responds to: GetAICSkillSteps from AICSuperAgent
        /// </summary>
        public event EventHandler<PayloadEventArgs> AICSuperAgentGetAICSkillStepsReceived;
        protected virtual void OnAICSuperAgentGetAICSkillStepsReceived(StandardPayload payload, BasicDeliverEventArgs bdea)
        {
            var plea = new PayloadEventArgs(payload, bdea);
            if (!ReferenceEquals(this.AICSuperAgentGetAICSkillStepsReceived, null))
            {
                plea.Payload.IsHandled = true;
                this.AICSuperAgentGetAICSkillStepsReceived(this, plea);
            }
        }

        /// <summary>
        /// Responds to: UpdateAICSkillStep from AICSuperAgent
        /// </summary>
        public event EventHandler<PayloadEventArgs> AICSuperAgentUpdateAICSkillStepReceived;
        protected virtual void OnAICSuperAgentUpdateAICSkillStepReceived(StandardPayload payload, BasicDeliverEventArgs bdea)
        {
            var plea = new PayloadEventArgs(payload, bdea);
            if (!ReferenceEquals(this.AICSuperAgentUpdateAICSkillStepReceived, null))
            {
                plea.Payload.IsHandled = true;
                this.AICSuperAgentUpdateAICSkillStepReceived(this, plea);
            }
        }

        /// <summary>
        /// Responds to: DeleteAICSkillStep from AICSuperAgent
        /// </summary>
        public event EventHandler<PayloadEventArgs> AICSuperAgentDeleteAICSkillStepReceived;
        protected virtual void OnAICSuperAgentDeleteAICSkillStepReceived(StandardPayload payload, BasicDeliverEventArgs bdea)
        {
            var plea = new PayloadEventArgs(payload, bdea);
            if (!ReferenceEquals(this.AICSuperAgentDeleteAICSkillStepReceived, null))
            {
                plea.Payload.IsHandled = true;
                this.AICSuperAgentDeleteAICSkillStepReceived(this, plea);
            }
        }

        /// <summary>
        /// Responds to: AddAICSkillStep from Admin
        /// </summary>
        public event EventHandler<PayloadEventArgs> AdminAddAICSkillStepReceived;
        protected virtual void OnAdminAddAICSkillStepReceived(StandardPayload payload, BasicDeliverEventArgs bdea)
        {
            var plea = new PayloadEventArgs(payload, bdea);
            if (!ReferenceEquals(this.AdminAddAICSkillStepReceived, null))
            {
                plea.Payload.IsHandled = true;
                this.AdminAddAICSkillStepReceived(this, plea);
            }
        }

        /// <summary>
        /// Responds to: GetAICSkillSteps from Admin
        /// </summary>
        public event EventHandler<PayloadEventArgs> AdminGetAICSkillStepsReceived;
        protected virtual void OnAdminGetAICSkillStepsReceived(StandardPayload payload, BasicDeliverEventArgs bdea)
        {
            var plea = new PayloadEventArgs(payload, bdea);
            if (!ReferenceEquals(this.AdminGetAICSkillStepsReceived, null))
            {
                plea.Payload.IsHandled = true;
                this.AdminGetAICSkillStepsReceived(this, plea);
            }
        }

        /// <summary>
        /// Responds to: UpdateAICSkillStep from Admin
        /// </summary>
        public event EventHandler<PayloadEventArgs> AdminUpdateAICSkillStepReceived;
        protected virtual void OnAdminUpdateAICSkillStepReceived(StandardPayload payload, BasicDeliverEventArgs bdea)
        {
            var plea = new PayloadEventArgs(payload, bdea);
            if (!ReferenceEquals(this.AdminUpdateAICSkillStepReceived, null))
            {
                plea.Payload.IsHandled = true;
                this.AdminUpdateAICSkillStepReceived(this, plea);
            }
        }

        /// <summary>
        /// Responds to: DeleteAICSkillStep from Admin
        /// </summary>
        public event EventHandler<PayloadEventArgs> AdminDeleteAICSkillStepReceived;
        protected virtual void OnAdminDeleteAICSkillStepReceived(StandardPayload payload, BasicDeliverEventArgs bdea)
        {
            var plea = new PayloadEventArgs(payload, bdea);
            if (!ReferenceEquals(this.AdminDeleteAICSkillStepReceived, null))
            {
                plea.Payload.IsHandled = true;
                this.AdminDeleteAICSkillStepReceived(this, plea);
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

                    
