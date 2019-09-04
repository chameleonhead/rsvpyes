using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RsvpYes.Domain.Users;
using System;
using System.Threading.Tasks;

namespace RsvpYes.Data.Tests
{
    [TestClass]
    public class OrganizationRepositoryTests
    {
        private RsvpYesDbContext context;
        private OrganizationRepository sut;

        [TestInitialize]
        public async Task Initialize()
        {
            context = new RsvpYesDbContext(
                new DbContextOptionsBuilder<RsvpYesDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options);
            await context.Database.EnsureCreatedAsync().ConfigureAwait(false);
            sut = new OrganizationRepository(context);
        }

        [TestCleanup]
        public void Cleanup()
        {
            context.Dispose();
        }

        [TestMethod]
        public async Task Organizationリポジトリにデータを登録して取得する()
        {
            var data = new Organization("Organization1");
            await sut.SaveAsync(data).ConfigureAwait(false);
            var persistedData = await sut.FindByIdAsync(data.Id);
            AssertData(data, persistedData);
            await sut.SaveAsync(data).ConfigureAwait(false);
            persistedData = await sut.FindByIdAsync(data.Id);
            AssertData(data, persistedData);
        }

        private void AssertData(Organization data, Organization persistedData)
        {
            Assert.AreEqual(data.Id, persistedData.Id);
            Assert.AreEqual(data.Name, persistedData.Name);
        }
    }
}
