using MediatR;
using RsvpYes.Domain.Places;
using System.Threading;
using System.Threading.Tasks;

namespace RsvpYes.Application.Places
{
    public class PlaceCreateCommandHandler : IRequestHandler<PlaceCreateCommand, PlaceId>
    {
        private readonly IPlaceRepository _repository;

        public PlaceCreateCommandHandler(IPlaceRepository repository)
        {
            _repository = repository;
        }

        public async Task<PlaceId> Handle(PlaceCreateCommand command, CancellationToken cancellationToken = default)
        {
            var place = new Place(command.PlaceName, command.PlaceUrl);
            await _repository.SaveAsync(place).ConfigureAwait(false);
            return place.Id;
        }
    }
}
