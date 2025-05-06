using SassyMQ.Lib.RabbitMQ.Payload;
using System;

namespace SassyMQ.Lib.RabbitMQ
{
    public class InvokeEventArgs<T> : EventArgs
        where T : StandardPayload<T>, new()
    {
        public EventHandler<PayloadEventArgs<T>> MethodDelegate { get; set; }
        public PayloadEventArgs<T> PayloadEventArgs { get; set; }
    }
}