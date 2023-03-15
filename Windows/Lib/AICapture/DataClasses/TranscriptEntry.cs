using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace AIC.Lib.DataClasses
{
    public class TranscriptEntry
    {
        public string Time { get; set; }
        public string Type { get; set; }   
        public string Text { get; set; }
        public string ParentMessageId { get; set; }
        public string ConversationId { get; set; }
        public string IsHidden { get; set; }
    }
}
