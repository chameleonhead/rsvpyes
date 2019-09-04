using System;

namespace RsvpYes.Data.Messaging
{
    class SentMessageEntity
    {
        public Guid Id { get; set;  }
        public Guid From { get; set; }
        public string FromAddress { get; set; }
        public Guid To { get; set; }
        public string ToAddress { get; set; }
        public string Body { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
