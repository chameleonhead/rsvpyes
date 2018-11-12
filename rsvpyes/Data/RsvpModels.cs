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
        public DateTime StartTime { get; set; }
        public string PlaceName { get; set; }
        public string PlaceUri { get; set; }
        [DataType(DataType.Currency)]
        public decimal? Fee { get; set; }
    }

    public class RsvpRequest
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid MeetingId { get; set; }
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
