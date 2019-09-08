using Microsoft.VisualStudio.TestTools.UnitTesting;
using RsvpYes.Application.Places;
using RsvpYes.Application.Tests.Utils;
using RsvpYes.Domain.Places;
using System.Threading.Tasks;

namespace RsvpYes.Application.Tests
{
    [TestClass]
    public class PlaceCommandsTests
    {
        private IPlaceRepository repo;
        private PlaceCreateCommand createCommand;
        private PlaceId placeId;

        [TestInitialize]
        public async Task Initialize()
        {
            repo = new InMemoryPlaceRepository();
            var sut = new PlaceCreateCommandHandler(repo);
            createCommand = new PlaceCreateCommand("Place1", new Url("http://place.com"));
            placeId = await sut.Handle(createCommand).ConfigureAwait(false);
        }

        [TestMethod]
        public async Task 場所の作成コマンドのテスト()
        {
            var place = await repo.FindByIdAsync(placeId).ConfigureAwait(false);
            Assert.AreEqual(createCommand.PlaceName, place.Name);
            Assert.AreEqual(createCommand.PlaceUrl, place.Url);
        }

        [TestMethod]
        public async Task 場所の更新コマンドのテスト()
        {
            var command = new PlaceUpdateCommand(placeId, "Place2", new Url("http://place2.com"));
            var sut = new PlaceUpdateCommandHandler(repo);
            await sut.Handle(command).ConfigureAwait(false);

            var place = await repo.FindByIdAsync(placeId).ConfigureAwait(false);
            Assert.AreEqual(command.PlaceName, place.Name);
            Assert.AreEqual(command.PlaceUrl, place.Url);
        }
    }
}
