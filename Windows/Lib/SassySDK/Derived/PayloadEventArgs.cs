using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SassyMQ.Lib.RabbitMQ.Payload;

namespace SassyMQ.Lib.RabbitMQ
{
    public class PayloadEventArgs<T> : EventArgs
        where T : StandardPayload<T>, new()
    {
        public PayloadEventArgs()
            : this(default(T))
        {

        }
        public PayloadEventArgs(T payload)
        {
            this.Payload = payload;
        }

        public T Payload { get; private set; }
    }
}
