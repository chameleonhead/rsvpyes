using System.Threading.Tasks;

namespace RsvpYes.Domain
{
    public interface IPlaceRepository
    {
        Task<Place> FindByIdAsync(PlaceId placeId);
        Task SaveAsync(Place place);
    }
}
