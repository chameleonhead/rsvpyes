using System;
using System.ComponentModel.DataAnnotations;

namespace rsvpyes.Models.Messages
{
    public class MessageViewModel
    {
        public string SenderName { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd HH:mm:ss}")]
        public DateTime SendTimestamp { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
    }
}
