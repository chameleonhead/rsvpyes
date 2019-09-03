using System.Threading.Tasks;

namespace RsvpYes.Domain.Places
{
    public interface IPlaceRepository
    {
        Task<Place> FindByIdAsync(PlaceId placeId);
        Task SaveAsync(Place place);
    }
}
