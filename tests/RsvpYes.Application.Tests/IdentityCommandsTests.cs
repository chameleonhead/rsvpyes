using Microsoft.VisualStudio.TestTools.UnitTesting;
using RsvpYes.Application.Tests.Utils;
using RsvpYes.Application.Users;
using RsvpYes.Domain.Users;
using System.Threading.Tasks;

namespace RsvpYes.Application.Tests
{
    [TestClass]
    public class IdentityCommandsTests
    {
        [TestMethod]
        public async Task アプリケーションユーザーの登録コマンドのテスト()
        {
            var mail = new MailAddress("user1@example.com");
            var command = new IdentityRegisterWithoutUserCommand(mail.ToString(), "PasswordHash",  "User Name", "Organization Name", mail);
            var repo = new InMemoryIdentityRepository();
            var organizationRepository = new InMemoryOrganizationRepository();
            var userRepository = new InMemoryUserRepository();
            var sut = new IdentityRegisterWithoutUserCommandHandler(repo, organizationRepository, userRepository);
            await sut.Handle(command).ConfigureAwait(false);

            var identity = await repo.FindByAccountNameAndPasswordAsync(mail.ToString(), "PasswordHash").ConfigureAwait(false);
            var user = await userRepository.FindByIdAsync(identity.UserId).ConfigureAwait(false);
            var organization = await organizationRepository.FindByIdAsync(user.OrganizationId);

            Assert.AreEqual(command.AccountName, identity.AccountName);
            Assert.AreEqual(command.PasswordHash, identity.PasswordHash);

            Assert.AreEqual(command.UserName, user.Name);
            Assert.AreEqual(command.UserMailAddress, user.DefaultMailAddress);

            Assert.AreEqual(command.OrganizationName, organization.Name);
        }
    }
}
