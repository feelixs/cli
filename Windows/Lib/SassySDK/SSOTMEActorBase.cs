using SassyMQ.Lib.RabbitMQ.Payload;
using SassyMQ.SSOTME.Lib.RMQActors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSoTme.OST.Lib.SassySDK
{
    public abstract class SSOTMEActorBase : SMQActorBase<SSOTMEPayload>
    {

        public string EmailAddress { get; set; }
        public string Secret { get; set; }

        public SSOTMEActorBase(string allExchange, bool isAutoConnect)
            : base(allExchange, isAutoConnect)
        {
        }

        public virtual IHandlerFactory HandlerFactory { get; set; }
    }
}

