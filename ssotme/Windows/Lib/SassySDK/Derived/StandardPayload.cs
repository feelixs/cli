using Newtonsoft.Json;
using RabbitMQ.Client.Events;
using SassyMQ.SSOTME.Lib.RabbitMQ;
using System;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace SassyMQ.Lib.RabbitMQ.Payload
{
    public class StandardPayload<T>
        where T : StandardPayload<T>, new()
    {
        public StandardPayload() : this(String.Empty)
        {

        }

        public StandardPayload(string content, string senderId = default(String))
        {
            this.PayloadId = Guid.NewGuid().ToString();
            this.Content = content;
            this.SenderId = senderId;
        }

        public LexiconTerm LexiconTerm
        {
            get { return Lexicon.Terms.FromRoutingKey(this.RoutingKey); }
        }

        public string PayloadId { get; set; }
        public string SenderId { get; set; }
        public string SenderName { get; set; }
        public string Content { get; set; }
        public string DeliveryTag { get; set; }
        private string _routingKey;
        public string RoutingKey
        {
            get { return _routingKey; }
            set
            {
                _routingKey = value;
            }
        }
        public String Exchange { get; set; }
        public String ReplyTo { get; set; }
        public bool IsRejected { get; set; }
        public string RejectionMsg { get; private set; }
        public string ResolutionMsg { get; private set; }
        public Exception Exception { get; set; }
        public string ErrorMessage { get; set; }
        public bool IsHandled { get; set; }
        public DateTime CreatedAt { get; private set; }
        public bool ReplyRecieved { get; private set; }
        public bool TimedOutWaiting { get; private set; }
        public T ReplyPayload { get; private set; }
        public string DirectMessageQueue { get; set; }
        public string CorrelationId { get; set; }

        public void Reject(string msg)
        {
            this.IsRejected = true;
            this.RejectionMsg = msg;
        }

        public void Resolve(string msg)
        {
            this.IsRejected = false;
            this.ResolutionMsg = msg;
        }

        public string ToJSonString()
        {
            return JsonConvert.SerializeObject(this);
        }

        public static T FromJSonString(String json)
        {
            return JsonConvert.DeserializeObject<T>(json, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });
        }

        public void HandleReplyTo(object sender, PayloadEventArgs<T> e)
        {
            if (e.Payload.PayloadId == this.PayloadId)
            {
                this.ReplyPayload = e.Payload;
                this.ReplyRecieved = true;
                SMQActorBase<T> actor = sender as SMQActorBase<T>;
                actor.ReplyTo -= this.HandleReplyTo;
            }
        }

        public bool WaitForResponse(int waitTimeout)
        {
            this.TimedOutWaiting = false;
            var waitTask = System.Threading.Tasks.Task.Factory.StartNew(() =>
            {
                while (!this.ReplyRecieved && !this.TimedOutWaiting)
                {
                    Thread.Sleep(100);
                    if (this.ReplyRecieved) break;
                }
            });
            waitTask.Wait(waitTimeout);
            if (!this.ReplyRecieved) this.TimedOutWaiting = true;

            var errorMessageReceived = !ReferenceEquals(this.ReplyPayload, null) && !String.IsNullOrEmpty(this.ReplyPayload.ErrorMessage);

            return this.ReplyRecieved && !errorMessageReceived;
        }
    }
}