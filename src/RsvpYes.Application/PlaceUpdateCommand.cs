using RsvpYes.Domain.Places;

namespace RsvpYes.Application
{
    public class PlaceUpdateCommand
    {
        public PlaceUpdateCommand(PlaceId placeId, string placeName, Url placeUrl)
        {
            PlaceId = placeId;
            PlaceName = placeName;
            PlaceUrl = placeUrl;
        }

        public PlaceId PlaceId { get; }
        public string PlaceName { get; }
        public Url PlaceUrl { get; }
    }
}