using Microsoft.VisualStudio.TestTools.UnitTesting;
using RsvpYes.Domain.Users;

namespace RsvpYes.Domain.Tests
{
    [TestClass]
    public class IdentityTests
    {
        private UserId userId1;
        private Identity sut;

        [TestInitialize]
        public void Initizalize()
        {
            userId1 = new UserId();
            sut = new Identity("test", "passwordHash", userId1);
        }

        [TestMethod]
        public void アプリケーションユーザーを作成する()
        {
            Assert.AreEqual("test", sut.AccountName);
            Assert.AreEqual("passwordHash", sut.PasswordHash);
            Assert.AreEqual(userId1, sut.UserId);
        }

        [TestMethod]
        public void アプリケーションユーザーのアカウント名を変更する()
        {
            sut.ChangeAccountName("test2");
            Assert.AreEqual("test2", sut.AccountName);
        }

        [TestMethod]
        public void アプリケーションユーザーのパスワードを変更する()
        {
            sut.ChangePassword("passwordHash2");
            Assert.AreEqual("passwordHash2", sut.PasswordHash);
        }

        [TestMethod]
        public void アプリケーションユーザーのユーザーIDを変更する()
        {
            var userId2 = new UserId();
            sut.ChangeUserId(userId2);
            Assert.AreEqual(userId2, sut.UserId);
        }
    }
}
