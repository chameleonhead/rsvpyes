using MediatR;
using RsvpYes.Domain.Places;
using System.Threading;
using System.Threading.Tasks;

namespace RsvpYes.Application.Places
{
    public class PlaceUpdateCommandHandler : IRequestHandler<PlaceUpdateCommand>
    {
        private readonly IPlaceRepository _repository;

        public PlaceUpdateCommandHandler(IPlaceRepository repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(PlaceUpdateCommand command, CancellationToken cancellationToken = default)
        {
            var place = await _repository.FindByIdAsync(command.PlaceId).ConfigureAwait(false);
            place.UpdateName(command.PlaceName);
            place.UpdateUrl(command.PlaceUrl);
            await _repository.SaveAsync(place).ConfigureAwait(false);
            return Unit.Value;
        }
    }
}
