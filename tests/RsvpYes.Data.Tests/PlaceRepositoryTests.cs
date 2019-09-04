using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RsvpYes.Domain.Places;
using System;
using System.Threading.Tasks;

namespace RsvpYes.Data.Tests
{
    [TestClass]
    public class PlaceRepositoryTests
    {
        private RsvpYesDbContext context;
        private PlaceRepository sut;

        [TestInitialize]
        public async Task Initialize()
        {
            context = new RsvpYesDbContext(
                new DbContextOptionsBuilder<RsvpYesDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options);
            await context.Database.EnsureCreatedAsync().ConfigureAwait(false);
            sut = new PlaceRepository(context);
        }

        [TestCleanup]
        public void Cleanup()
        {
            context.Dispose();
        }

        [TestMethod]
        public async Task Placeリポジトリにデータを登録して取得する()
        {
            var data = new Place("Place1", new Url("http://place1.com"));
            await sut.SaveAsync(data).ConfigureAwait(false);
            var persistedData = await sut.FindByIdAsync(data.Id);
            AssertData(data, persistedData);
            await sut.SaveAsync(data).ConfigureAwait(false);
            persistedData = await sut.FindByIdAsync(data.Id);
            AssertData(data, persistedData);
        }

        private void AssertData(Place data, Place persistedData)
        {
            Assert.AreEqual(data.Id, persistedData.Id);
            Assert.AreEqual(data.Name, persistedData.Name);
            Assert.AreEqual(data.Url, persistedData.Url);
        }
    }
}
