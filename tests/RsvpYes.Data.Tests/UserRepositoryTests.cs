using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RsvpYes.Domain.Users;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace RsvpYes.Data.Tests
{
    [TestClass]
    public class UserRepositoryTests
    {
        private RsvpYesDbContext context;
        private UserRepository sut;

        [TestInitialize]
        public async Task Initialize()
        {
            context = new RsvpYesDbContext(
                new DbContextOptionsBuilder<RsvpYesDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options);
            await context.Database.EnsureCreatedAsync().ConfigureAwait(false);
            sut = new UserRepository(context);
        }

        [TestCleanup]
        public void Cleanup()
        {
            context.Dispose();
        }

        [TestMethod]
        public async Task Userリポジトリにデータを登録して取得する()
        {
            var data = new User("User1", new MailAddress("mail1@example.com"), new OrganizationId());
            data.AddMailAddress(new MailAddress("mail2@example.com"));
            data.AddMailAddress(new MailAddress("mail3@example.com"));
            data.AddMailAddress(new MailAddress("mail4@example.com"));
            data.AddMailAddress(new MailAddress("mail5@example.com"));
            data.AddPhoneNumber(new PhoneNumber("090-1000-0001"));
            data.AddPhoneNumber(new PhoneNumber("090-1000-0002"));
            data.AddPhoneNumber(new PhoneNumber("090-1000-0003"));
            data.AddPhoneNumber(new PhoneNumber("090-1000-0004"));
            data.AddPhoneNumber(new PhoneNumber("090-1000-0005"));
            data.UpdateMessageSignature("========\ntest");
            await sut.SaveAsync(data).ConfigureAwait(false);
            var persistedData = await sut.FindByIdAsync(data.Id);
            AssertData(data, persistedData);
            await sut.SaveAsync(data).ConfigureAwait(false);
            persistedData = await sut.FindByIdAsync(data.Id);
            AssertData(data, persistedData);
        }

        private void AssertData(User data, User persistedData)
        {
            Assert.AreEqual(data.Id, persistedData.Id);
            Assert.AreEqual(data.Name, persistedData.Name);
            Assert.AreEqual(data.OrganizationId, persistedData.OrganizationId);
            Assert.AreEqual(data.DefaultMailAddress, persistedData.DefaultMailAddress);
            Assert.AreEqual(data.MessageSignature, persistedData.MessageSignature);

            CollectionAssert.AreEquivalent(
                data.MailAddresses.ToList(),
                persistedData.MailAddresses.ToList());

            CollectionAssert.AreEquivalent(
                data.PhoneNumbers.ToList(),
                persistedData.PhoneNumbers.ToList());
        }
    }
}
