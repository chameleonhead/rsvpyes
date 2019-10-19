using System;
using System.ComponentModel.DataAnnotations;

namespace rsvpyes.Data
{
    public class User
    {
        public Guid Id { get; set; }
        public string Organization { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }

    public class Meeting
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-ddTHH:mm}")]
        public DateTime StartTime { get; set; }
        public string PlaceName { get; set; }
        public string PlaceUri { get; set; }
        [DataType(DataType.Currency)]
        public decimal? Fee { get; set; }
    }

    public class Message
    {
        public Guid Id { get; set; }
        public DateTime SendTimestamp { get; set; }
        public Guid SenderUserId { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
    }

    public class RsvpRequest
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid MeetingId { get; set; }
        public Guid? MessageId { get; set; }
    }

    public enum Rsvp
    {
        NotRespond,
        Yes,
        No,
    }

    public class RsvpResponse
    {
        public Guid Id { get; set; }
        public DateTime Timestamp { get; set; }
        public Guid RsvpRequestId { get; set; }
        public Rsvp Rsvp { get; set; }
        public string Reason { get; set; }
    }
}
