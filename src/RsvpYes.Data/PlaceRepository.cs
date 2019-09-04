using RsvpYes.Data.Places;
using RsvpYes.Domain.Places;
using System.Threading.Tasks;

namespace RsvpYes.Data
{
    public class PlaceRepository : IPlaceRepository
    {
        private readonly RsvpYesDbContext _context;

        public PlaceRepository(RsvpYesDbContext context)
        {
            _context = context;
        }

        public async Task SaveAsync(Place place)
        {
            var placeId = place.Id.Value;
            var placeEntity = await _context.Places.FindAsync(placeId).ConfigureAwait(false);

            if (placeEntity != null)
            {
                _context.Places.Remove(placeEntity);
            }

            _context.Places.Add(new PlaceEntity()
            {
                Id = placeId,
                Name = place.Name,
                Url = place.Url.Value,
            });
        }

        public async Task<Place> FindByIdAsync(PlaceId placeId)
        {
            var placeEntity = await _context.Places.FindAsync(placeId.Value).ConfigureAwait(false);

            if (placeEntity == null)
            {
                return null;
            }

            return new Place(
                placeId,
                placeEntity.Name,
                new Url(placeEntity.Url));
        }
    }
}
