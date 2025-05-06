using System;

using System.Linq;
using System.Collections.Generic;
using System.Collections;
using SassyMQ.Lib.RabbitMQ;

namespace SassyMQ.SSOTME.Lib.RabbitMQ
{
    public class LexiconTerm : LexiconTerm<LexiconTermEnum> { }

    public partial class Lexicon  : IEnumerable<LexiconTerm>
    {
        public static Lexicon Terms = new Lexicon();
        protected static new Dictionary<LexiconTermEnum, LexiconTerm> TermsByKey { get; set; }

        public LexiconTerm this[LexiconTermEnum termKey]
        {
            get { return TermsByKey[termKey]; }
        }

        public LexiconTerm FromRoutingKey(string routingKey)
        {
            return Lexicon.TermsByKey.Values.FirstOrDefault(first => first.RoutingKey == routingKey);
        }


        public IEnumerator<LexiconTerm> GetEnumerator()
        {
            return Lexicon.TermsByKey.Values.GetEnumerator();
        }

        
        IEnumerator IEnumerable.GetEnumerator()
        {
            return Lexicon.TermsByKey.Values.GetEnumerator();
        }

        static Lexicon()
        {
            Lexicon.TermsByKey = new Dictionary<LexiconTermEnum, LexiconTerm>();
            
            Lexicon.TermsByKey[LexiconTermEnum.publicuser_ping_ssotmecoordinator] = new LexiconTerm() {
                Term = LexiconTermEnum.publicuser_ping_ssotmecoordinator,
                Sender = "publicuser",
                Verb = "ping",
                Receiver = "ssotmecoordinator",
                Category = "general"
            };
            
            Lexicon.TermsByKey[LexiconTermEnum.publicuser_register_ssotmecoordinator] = new LexiconTerm() {
                Term = LexiconTermEnum.publicuser_register_ssotmecoordinator,
                Sender = "publicuser",
                Verb = "register",
                Receiver = "ssotmecoordinator",
                Category = "general"
            };
            
            Lexicon.TermsByKey[LexiconTermEnum.publicuser_authenticate_ssotmecoordinator] = new LexiconTerm() {
                Term = LexiconTermEnum.publicuser_authenticate_ssotmecoordinator,
                Sender = "publicuser",
                Verb = "authenticate",
                Receiver = "ssotmecoordinator",
                Category = "general"
            };
            
            Lexicon.TermsByKey[LexiconTermEnum.publicuser_validateauthtoken_ssotmecoordinator] = new LexiconTerm() {
                Term = LexiconTermEnum.publicuser_validateauthtoken_ssotmecoordinator,
                Sender = "publicuser",
                Verb = "validateauthtoken",
                Receiver = "ssotmecoordinator",
                Category = "general"
            };
            
            Lexicon.TermsByKey[LexiconTermEnum.publicuser_recover_ssotmecoordinator] = new LexiconTerm() {
                Term = LexiconTermEnum.publicuser_recover_ssotmecoordinator,
                Sender = "publicuser",
                Verb = "recover",
                Receiver = "ssotmecoordinator",
                Category = "general"
            };
            
            Lexicon.TermsByKey[LexiconTermEnum.publicuser_getalltranspilers_ssotmecoordinator] = new LexiconTerm() {
                Term = LexiconTermEnum.publicuser_getalltranspilers_ssotmecoordinator,
                Sender = "publicuser",
                Verb = "getalltranspilers",
                Receiver = "ssotmecoordinator",
                Category = "general"
            };
            
            Lexicon.TermsByKey[LexiconTermEnum.publicuser_getallplatformdata_ssotmecoordinator] = new LexiconTerm() {
                Term = LexiconTermEnum.publicuser_getallplatformdata_ssotmecoordinator,
                Sender = "publicuser",
                Verb = "getallplatformdata",
                Receiver = "ssotmecoordinator",
                Category = "general"
            };
            
            Lexicon.TermsByKey[LexiconTermEnum.publicuser_getallfiletypes_ssotmecoordinator] = new LexiconTerm() {
                Term = LexiconTermEnum.publicuser_getallfiletypes_ssotmecoordinator,
                Sender = "publicuser",
                Verb = "getallfiletypes",
                Receiver = "ssotmecoordinator",
                Category = "general"
            };
            
            Lexicon.TermsByKey[LexiconTermEnum.accountholder_ping_ssotmecoordinator] = new LexiconTerm() {
                Term = LexiconTermEnum.accountholder_ping_ssotmecoordinator,
                Sender = "accountholder",
                Verb = "ping",
                Receiver = "ssotmecoordinator",
                Category = "general"
            };
            
            Lexicon.TermsByKey[LexiconTermEnum.accountholder_login_ssotmecoordinator] = new LexiconTerm() {
                Term = LexiconTermEnum.accountholder_login_ssotmecoordinator,
                Sender = "accountholder",
                Verb = "login",
                Receiver = "ssotmecoordinator",
                Category = "general"
            };
            
            Lexicon.TermsByKey[LexiconTermEnum.accountholder_logout_ssotmecoordinator] = new LexiconTerm() {
                Term = LexiconTermEnum.accountholder_logout_ssotmecoordinator,
                Sender = "accountholder",
                Verb = "logout",
                Receiver = "ssotmecoordinator",
                Category = "general"
            };
            
            Lexicon.TermsByKey[LexiconTermEnum.accountholder_addtranspiler_ssotmecoordinator] = new LexiconTerm() {
                Term = LexiconTermEnum.accountholder_addtranspiler_ssotmecoordinator,
                Sender = "accountholder",
                Verb = "addtranspiler",
                Receiver = "ssotmecoordinator",
                Category = "general"
            };
            
            Lexicon.TermsByKey[LexiconTermEnum.accountholder_deletetranspiler_ssotmecoordinator] = new LexiconTerm() {
                Term = LexiconTermEnum.accountholder_deletetranspiler_ssotmecoordinator,
                Sender = "accountholder",
                Verb = "deletetranspiler",
                Receiver = "ssotmecoordinator",
                Category = "general"
            };
            
            Lexicon.TermsByKey[LexiconTermEnum.accountholder_updatetranspiler_ssotmecoordinator] = new LexiconTerm() {
                Term = LexiconTermEnum.accountholder_updatetranspiler_ssotmecoordinator,
                Sender = "accountholder",
                Verb = "updatetranspiler",
                Receiver = "ssotmecoordinator",
                Category = "general"
            };
            
            Lexicon.TermsByKey[LexiconTermEnum.accountholder_gettranspiler_ssotmecoordinator] = new LexiconTerm() {
                Term = LexiconTermEnum.accountholder_gettranspiler_ssotmecoordinator,
                Sender = "accountholder",
                Verb = "gettranspiler",
                Receiver = "ssotmecoordinator",
                Category = "general"
            };
            
            Lexicon.TermsByKey[LexiconTermEnum.accountholder_addtranspilerversion_ssotmecoordinator] = new LexiconTerm() {
                Term = LexiconTermEnum.accountholder_addtranspilerversion_ssotmecoordinator,
                Sender = "accountholder",
                Verb = "addtranspilerversion",
                Receiver = "ssotmecoordinator",
                Category = "general"
            };
            
            Lexicon.TermsByKey[LexiconTermEnum.accountholder_deletetranspilerversion_ssotmecoordinator] = new LexiconTerm() {
                Term = LexiconTermEnum.accountholder_deletetranspilerversion_ssotmecoordinator,
                Sender = "accountholder",
                Verb = "deletetranspilerversion",
                Receiver = "ssotmecoordinator",
                Category = "general"
            };
            
            Lexicon.TermsByKey[LexiconTermEnum.accountholder_updatetranspilerversion_ssotmecoordinator] = new LexiconTerm() {
                Term = LexiconTermEnum.accountholder_updatetranspilerversion_ssotmecoordinator,
                Sender = "accountholder",
                Verb = "updatetranspilerversion",
                Receiver = "ssotmecoordinator",
                Category = "general"
            };
            
            Lexicon.TermsByKey[LexiconTermEnum.accountholder_gettranspilerlist_ssotmecoordinator] = new LexiconTerm() {
                Term = LexiconTermEnum.accountholder_gettranspilerlist_ssotmecoordinator,
                Sender = "accountholder",
                Verb = "gettranspilerlist",
                Receiver = "ssotmecoordinator",
                Category = "general"
            };
            
            Lexicon.TermsByKey[LexiconTermEnum.accountholder_createproject_ssotmecoordinator] = new LexiconTerm() {
                Term = LexiconTermEnum.accountholder_createproject_ssotmecoordinator,
                Sender = "accountholder",
                Verb = "createproject",
                Receiver = "ssotmecoordinator",
                Category = "general"
            };
            
            Lexicon.TermsByKey[LexiconTermEnum.accountholder_requesttranspile_ssotmecoordinator] = new LexiconTerm() {
                Term = LexiconTermEnum.accountholder_requesttranspile_ssotmecoordinator,
                Sender = "accountholder",
                Verb = "requesttranspile",
                Receiver = "ssotmecoordinator",
                Category = "general"
            };
            
            Lexicon.TermsByKey[LexiconTermEnum.accountholder_requesttranspilerhost_ssotmecoordinator] = new LexiconTerm() {
                Term = LexiconTermEnum.accountholder_requesttranspilerhost_ssotmecoordinator,
                Sender = "accountholder",
                Verb = "requesttranspilerhost",
                Receiver = "ssotmecoordinator",
                Category = "general"
            };
            
            Lexicon.TermsByKey[LexiconTermEnum.accountholder_requeststopinstance_ssotmecoordinator] = new LexiconTerm() {
                Term = LexiconTermEnum.accountholder_requeststopinstance_ssotmecoordinator,
                Sender = "accountholder",
                Verb = "requeststopinstance",
                Receiver = "ssotmecoordinator",
                Category = "general"
            };
            
            Lexicon.TermsByKey[LexiconTermEnum.accountholder_requeststophost_ssotmecoordinator] = new LexiconTerm() {
                Term = LexiconTermEnum.accountholder_requeststophost_ssotmecoordinator,
                Sender = "accountholder",
                Verb = "requeststophost",
                Receiver = "ssotmecoordinator",
                Category = "general"
            };
            
            Lexicon.TermsByKey[LexiconTermEnum.accountholder_commandlinetranspile_ssotmecoordinator] = new LexiconTerm() {
                Term = LexiconTermEnum.accountholder_commandlinetranspile_ssotmecoordinator,
                Sender = "accountholder",
                Verb = "commandlinetranspile",
                Receiver = "ssotmecoordinator",
                Category = "general"
            };
            
            Lexicon.TermsByKey[LexiconTermEnum.transpilerhost_ping_ssotmecoordinator] = new LexiconTerm() {
                Term = LexiconTermEnum.transpilerhost_ping_ssotmecoordinator,
                Sender = "transpilerhost",
                Verb = "ping",
                Receiver = "ssotmecoordinator",
                Category = "general"
            };
            
            Lexicon.TermsByKey[LexiconTermEnum.transpilerhost_offline_ssotmecoordinator] = new LexiconTerm() {
                Term = LexiconTermEnum.transpilerhost_offline_ssotmecoordinator,
                Sender = "transpilerhost",
                Verb = "offline",
                Receiver = "ssotmecoordinator",
                Category = "general"
            };
            
            Lexicon.TermsByKey[LexiconTermEnum.transpilerhost_instancestarted_ssotmecoordinator] = new LexiconTerm() {
                Term = LexiconTermEnum.transpilerhost_instancestarted_ssotmecoordinator,
                Sender = "transpilerhost",
                Verb = "instancestarted",
                Receiver = "ssotmecoordinator",
                Category = "general"
            };
            
            Lexicon.TermsByKey[LexiconTermEnum.transpilerhost_instancestopped_ssotmecoordinator] = new LexiconTerm() {
                Term = LexiconTermEnum.transpilerhost_instancestopped_ssotmecoordinator,
                Sender = "transpilerhost",
                Verb = "instancestopped",
                Receiver = "ssotmecoordinator",
                Category = "general"
            };
            
            Lexicon.TermsByKey[LexiconTermEnum.ssotmeadmin_ping_ssotmecoordinator] = new LexiconTerm() {
                Term = LexiconTermEnum.ssotmeadmin_ping_ssotmecoordinator,
                Sender = "ssotmeadmin",
                Verb = "ping",
                Receiver = "ssotmecoordinator",
                Category = "general"
            };
            
            Lexicon.TermsByKey[LexiconTermEnum.ssotmeadmin_addplatformcategory_ssotmecoordinator] = new LexiconTerm() {
                Term = LexiconTermEnum.ssotmeadmin_addplatformcategory_ssotmecoordinator,
                Sender = "ssotmeadmin",
                Verb = "addplatformcategory",
                Receiver = "ssotmecoordinator",
                Category = "general"
            };
            
            Lexicon.TermsByKey[LexiconTermEnum.ssotmeadmin_updateplatformcategory_ssotmecoordinator] = new LexiconTerm() {
                Term = LexiconTermEnum.ssotmeadmin_updateplatformcategory_ssotmecoordinator,
                Sender = "ssotmeadmin",
                Verb = "updateplatformcategory",
                Receiver = "ssotmecoordinator",
                Category = "general"
            };
            
            Lexicon.TermsByKey[LexiconTermEnum.ssotmeadmin_addtranspilerplatform_ssotmecoordinator] = new LexiconTerm() {
                Term = LexiconTermEnum.ssotmeadmin_addtranspilerplatform_ssotmecoordinator,
                Sender = "ssotmeadmin",
                Verb = "addtranspilerplatform",
                Receiver = "ssotmecoordinator",
                Category = "general"
            };
            
            Lexicon.TermsByKey[LexiconTermEnum.ssotmeadmin_updatetranspilerplatform_ssotmecoordinator] = new LexiconTerm() {
                Term = LexiconTermEnum.ssotmeadmin_updatetranspilerplatform_ssotmecoordinator,
                Sender = "ssotmeadmin",
                Verb = "updatetranspilerplatform",
                Receiver = "ssotmecoordinator",
                Category = "general"
            };
            
            Lexicon.TermsByKey[LexiconTermEnum.ssotmecoordinator_getinstances_transpilerhost] = new LexiconTerm() {
                Term = LexiconTermEnum.ssotmecoordinator_getinstances_transpilerhost,
                Sender = "ssotmecoordinator",
                Verb = "getinstances",
                Receiver = "transpilerhost",
                Category = "general"
            };
            
            Lexicon.TermsByKey[LexiconTermEnum.ssotmecoordinator_transpilerequested_transpilerhost] = new LexiconTerm() {
                Term = LexiconTermEnum.ssotmecoordinator_transpilerequested_transpilerhost,
                Sender = "ssotmecoordinator",
                Verb = "transpilerequested",
                Receiver = "transpilerhost",
                Category = "general"
            };
            
            Lexicon.TermsByKey[LexiconTermEnum.ssotmecoordinator_transpileronline_publicuser] = new LexiconTerm() {
                Term = LexiconTermEnum.ssotmecoordinator_transpileronline_publicuser,
                Sender = "ssotmecoordinator",
                Verb = "transpileronline",
                Receiver = "publicuser",
                Category = "general"
            };
            
            Lexicon.TermsByKey[LexiconTermEnum.ssotmecoordinator_transpileroffline_publicuser] = new LexiconTerm() {
                Term = LexiconTermEnum.ssotmecoordinator_transpileroffline_publicuser,
                Sender = "ssotmecoordinator",
                Verb = "transpileroffline",
                Receiver = "publicuser",
                Category = "general"
            };
            
            Lexicon.TermsByKey[LexiconTermEnum.ssotmecoordinator_stopinstance_transpilerhost] = new LexiconTerm() {
                Term = LexiconTermEnum.ssotmecoordinator_stopinstance_transpilerhost,
                Sender = "ssotmecoordinator",
                Verb = "stopinstance",
                Receiver = "transpilerhost",
                Category = "general"
            };
            
            Lexicon.TermsByKey[LexiconTermEnum.ssotmecoordinator_stophost_transpilerhost] = new LexiconTerm() {
                Term = LexiconTermEnum.ssotmecoordinator_stophost_transpilerhost,
                Sender = "ssotmecoordinator",
                Verb = "stophost",
                Receiver = "transpilerhost",
                Category = "general"
            };
            
        }
    }
}