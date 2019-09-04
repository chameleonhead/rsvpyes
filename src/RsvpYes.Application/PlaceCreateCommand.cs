using RsvpYes.Domain.Places;

namespace RsvpYes.Application
{
    public class PlaceCreateCommand
    {
        public PlaceCreateCommand(string placeName, Url placeUrl)
        {
            PlaceName = placeName;
            PlaceUrl = placeUrl;
        }

        public string PlaceName { get; }
        public Url PlaceUrl { get; }
    }
}