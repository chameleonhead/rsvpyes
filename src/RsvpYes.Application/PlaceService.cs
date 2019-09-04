using RsvpYes.Domain.Places;
using System.Threading.Tasks;

namespace RsvpYes.Application
{
    public class PlaceService
    {
        private readonly IPlaceRepository _repository;

        public PlaceService(IPlaceRepository repository)
        {
            _repository = repository;
        }

        public async Task<PlaceId> CreateAsync(PlaceCreateCommand command)
        {
            var place = new Place(command.PlaceName, command.PlaceUrl);
            await _repository.SaveAsync(place).ConfigureAwait(false);
            return place.Id;
        }

        public async Task UpdateAsync(PlaceUpdateCommand command)
        {
            var place = await _repository.FindByIdAsync(command.PlaceId).ConfigureAwait(false);
            place.UpdateName(command.PlaceName);
            place.UpdateUrl(command.PlaceUrl);
            await _repository.SaveAsync(place).ConfigureAwait(false);
        }
    }
}
