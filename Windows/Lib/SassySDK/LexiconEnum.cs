using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections;

namespace SassyMQ.SSOTME.Lib.RabbitMQ
{
    public enum LexiconTermEnum
    {
        accountholder_addtranspiler_ssotmecoordinator, 
        accountholder_addtranspilerversion_ssotmecoordinator, 
        accountholder_commandlinetranspile_ssotmecoordinator, 
        accountholder_createproject_ssotmecoordinator, 
        accountholder_deletetranspiler_ssotmecoordinator, 
        accountholder_deletetranspilerversion_ssotmecoordinator, 
        accountholder_gettranspiler_ssotmecoordinator, 
        accountholder_gettranspilerlist_ssotmecoordinator, 
        accountholder_login_ssotmecoordinator, 
        accountholder_logout_ssotmecoordinator, 
        accountholder_ping_ssotmecoordinator, 
        accountholder_requeststophost_ssotmecoordinator, 
        accountholder_requeststopinstance_ssotmecoordinator, 
        accountholder_requesttranspile_ssotmecoordinator, 
        accountholder_requesttranspilerhost_ssotmecoordinator, 
        accountholder_updatetranspiler_ssotmecoordinator, 
        accountholder_updatetranspilerversion_ssotmecoordinator, 
        publicuser_getallfiletypes_ssotmecoordinator, 
        publicuser_getallplatformdata_ssotmecoordinator, 
        publicuser_getalltranspilers_ssotmecoordinator, 
        publicuser_ping_ssotmecoordinator, 
        publicuser_recover_ssotmecoordinator, 
        publicuser_register_ssotmecoordinator, 
        ssotmeadmin_addplatformcategory_ssotmecoordinator, 
        ssotmeadmin_addtranspilerplatform_ssotmecoordinator, 
        ssotmeadmin_ping_ssotmecoordinator, 
        ssotmeadmin_updateplatformcategory_ssotmecoordinator, 
        ssotmeadmin_updatetranspilerplatform_ssotmecoordinator, 
        ssotmecoordinator_getinstances_transpilerhost, 
        ssotmecoordinator_stophost_transpilerhost, 
        ssotmecoordinator_stopinstance_transpilerhost, 
        ssotmecoordinator_transpilerequested_transpilerhost, 
        ssotmecoordinator_transpileroffline_publicuser, 
        ssotmecoordinator_transpileronline_publicuser, 
        transpilerhost_instancestarted_ssotmecoordinator, 
        transpilerhost_instancestopped_ssotmecoordinator, 
        transpilerhost_offline_ssotmecoordinator, 
        transpilerhost_ping_ssotmecoordinator
    }
}