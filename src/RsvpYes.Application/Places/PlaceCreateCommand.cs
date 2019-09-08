using MediatR;
using RsvpYes.Domain.Places;

namespace RsvpYes.Application.Places
{
    public class PlaceCreateCommand : IRequest<PlaceId>
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