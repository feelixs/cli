using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SassyMQ.Lib.RabbitMQ
{
    public class LexiconTerm<T_ENUM>
    {
        public LexiconTerm(T_ENUM term)
        {
            this.Term = term;
            var parts = term.ToString().Split('_');
            this.Sender = parts[0];
            this.Category = parts[1];
            this.Receiver = parts[2];
            this.Verb = parts[4];

        }

        public LexiconTerm()
        {
        }

        public T_ENUM Term { get; internal set; }
        public string Category { get; internal set; }
        public string Receiver { get; internal set; }
        public string Sender { get; internal set; }
        public string Verb { get; internal set; }
        public string RoutingKey { get { return this.ToString(); } }

        public override string ToString()
        {
            return String.Format("{0}.{1}.{2}.{3}", this.Receiver, this.Category, this.Sender, this.Verb).ToLower();
        }
    }
}
