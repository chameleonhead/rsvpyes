using Microsoft.VisualStudio.TestTools.UnitTesting;
using RsvpYes.Application.Tests.Utils;
using RsvpYes.Application.Users;
using RsvpYes.Domain.Users;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace RsvpYes.Application.Tests
{
    [TestClass]
    public class UserServiceTests
    {
        private OrganizationId org1;
        private OrganizationId org2;
        private MailAddress mail1;
        private MailAddress mail2;
        private MailAddress mail3;
        private MailAddress mail4;
        private MailAddress mail5;
        private PhoneNumber tel1;
        private PhoneNumber tel2;
        private PhoneNumber tel3;
        private PhoneNumber tel4;
        private PhoneNumber tel5;
        private IUserRepository repo;
        private UserCreateCommand createCommand;
        private UserId userId;

        [TestInitialize]
        public async Task Initialize()
        {
            repo = new InMemoryUserRepository();
            org1 = new OrganizationId();
            org2 = new OrganizationId();
            mail1 = new MailAddress("mail1@example.com");
            mail2 = new MailAddress("mail2@example.com");
            mail3 = new MailAddress("mail3@example.com");
            mail4 = new MailAddress("mail4@example.com");
            mail5 = new MailAddress("mail5@example.com");
            tel1 = new PhoneNumber("090-1000-0001");
            tel2 = new PhoneNumber("090-1000-0002");
            tel3 = new PhoneNumber("090-1000-0003");
            tel4 = new PhoneNumber("090-1000-0004");
            tel5 = new PhoneNumber("090-1000-0005");
            createCommand = new UserCreateCommand("User1", mail1, org1);
            var sut = new UserCreateCommandHandler(repo);
            userId = await sut.Handle(createCommand).ConfigureAwait(false);
        }

        [TestMethod]
        public async Task ユーザーの作成コマンドのテスト()
        {
            var user = await repo.FindByIdAsync(userId).ConfigureAwait(false);
            Assert.AreEqual(createCommand.UserName, user.Name);
            Assert.AreEqual(createCommand.UserMailAddress, user.DefaultMailAddress);
            Assert.AreEqual(createCommand.UserOrganizationId, user.OrganizationId);
        }

        [TestMethod]
        public async Task ユーザーの更新コマンドのテスト()
        {
            var command = new UserUpdateCommand(userId, "User2", mail5, org2, "test signature");
            command.UserMailAddressesToAdd.AddRange(new[] { mail2, mail3, mail4, mail5 });
            command.UserPhoneNumberToAdd.AddRange(new[] { tel1, tel2, tel3, tel4, tel5 });

            var sut = new UserUpdateCommandHandler(repo);
            await sut.Handle(command).ConfigureAwait(false);

            var user = await repo.FindByIdAsync(userId).ConfigureAwait(false);
            Assert.AreEqual(command.UserName, user.Name);
            Assert.AreEqual(command.UserOrganizationId, user.OrganizationId);
            Assert.AreEqual(command.UserDefaultMailAddress, user.DefaultMailAddress);
            Assert.AreEqual(command.UserMessageSignature, user.MessageSignature);

            CollectionAssert.AreEquivalent(
                new[] { mail1, mail2, mail3, mail4, mail5 },
                user.MailAddresses.ToList());

            CollectionAssert.AreEquivalent(
                new[] { tel1, tel2, tel3, tel4, tel5 },
                user.PhoneNumbers.ToList());

            command = new UserUpdateCommand(userId, "User2", mail1, org2, "test signature");
            command.UserMailAddressesToRemove.AddRange(new[] { mail2, mail3, mail4, mail5 });
            command.UserPhoneNumberToRemove.AddRange(new[] { tel1, tel2, tel3, tel4, tel5 });

            await sut.Handle(command).ConfigureAwait(false);

            user = await repo.FindByIdAsync(userId).ConfigureAwait(false);
            Assert.AreEqual(command.UserName, user.Name);
            Assert.AreEqual(command.UserOrganizationId, user.OrganizationId);
            Assert.AreEqual(command.UserDefaultMailAddress, user.DefaultMailAddress);
            Assert.AreEqual(command.UserMessageSignature, user.MessageSignature);

            CollectionAssert.AreEquivalent(
                new[] { mail1 },
                user.MailAddresses.ToList());

            CollectionAssert.AreEquivalent(
                Array.Empty<PhoneNumber>(),
                user.PhoneNumbers.ToList());
        }
    }
}
