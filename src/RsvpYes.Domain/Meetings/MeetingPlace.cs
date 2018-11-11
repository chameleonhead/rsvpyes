using RsvpYes.Domain.SeedWork;
using System.Collections.Generic;

namespace RsvpYes.Domain.Meetings
{
    public class MeetingPlace : ValueObject
    {
        public string Name { get; }
        public string Address { get; }
        public string Url { get; }

        public MeetingPlace(string name) : this(name, null, null)
        {
        }

        public MeetingPlace(string name, string address, string url)
        {
            Name = name;
            Address = address;
            Url = url;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Name;
            yield return Address;
            yield return Url;
        }
    }
}
