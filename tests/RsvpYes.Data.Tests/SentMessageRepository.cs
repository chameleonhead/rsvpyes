using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RsvpYes.Domain.Messaging;
using RsvpYes.Domain.Users;
using System;
using System.Threading.Tasks;

namespace RsvpYes.Data.Tests
{
    [TestClass]
    public class SentMessageRepositoryTests
    {
        private RsvpYesDbContext context;
        private SentMessageRepository sut;

        [TestInitialize]
        public async Task Initialize()
        {
            context = new RsvpYesDbContext(
                new DbContextOptionsBuilder<RsvpYesDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options);
            await context.Database.EnsureCreatedAsync().ConfigureAwait(false);
            sut = new SentMessageRepository(context);
        }

        [TestCleanup]
        public void Cleanup()
        {
            context.Dispose();
        }

        [TestMethod]
        public async Task SentMessageリポジトリにデータを登録して取得する()
        {
            var data = new SentMessage(new MessageId(), new UserId(), new MailAddress("from@example.com"), new UserId(), new MailAddress("to@example.com"), "testMessage");
            await sut.SaveAsync(data).ConfigureAwait(false);
            var persistedData = await sut.FindByIdAsync(data.Id);
            AssertData(data, persistedData);
            await sut.SaveAsync(data).ConfigureAwait(false);
            persistedData = await sut.FindByIdAsync(data.Id);
            AssertData(data, persistedData);
        }

        private void AssertData(SentMessage data, SentMessage persistedData)
        {
            Assert.AreEqual(data.Id, persistedData.Id);
            Assert.AreEqual(data.From, persistedData.From);
            Assert.AreEqual(data.FromAddress, persistedData.FromAddress);
            Assert.AreEqual(data.To, persistedData.To);
            Assert.AreEqual(data.ToAddress, persistedData.ToAddress);
            Assert.AreEqual(data.Body, persistedData.Body);
            Assert.AreEqual(data.Timestamp, persistedData.Timestamp);
        }
    }
}
