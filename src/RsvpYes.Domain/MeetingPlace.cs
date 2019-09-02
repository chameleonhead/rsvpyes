using System;
using System.Collections.Generic;

namespace RsvpYes.Domain
{
    public class MeetingPlace
    {
        public MeetingPlace(PlaceId placeId, string detailInfo)
        {
            if (placeId == null && detailInfo == null)
            {
                throw new ArgumentNullException(nameof(placeId));
            }
            PlaceId = placeId;
            DetailInfo = detailInfo;
        }

        public PlaceId PlaceId { get; }
        public string DetailInfo { get; }

        public override bool Equals(object obj)
        {
            return obj is MeetingPlace place &&
                   EqualityComparer<PlaceId>.Default.Equals(PlaceId, place.PlaceId) &&
                   DetailInfo == place.DetailInfo;
        }

        public override int GetHashCode()
        {
            var hashCode = 2067968577;
            hashCode = hashCode * -1521134295 + EqualityComparer<PlaceId>.Default.GetHashCode(PlaceId);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(DetailInfo);
            return hashCode;
        }
    }
}