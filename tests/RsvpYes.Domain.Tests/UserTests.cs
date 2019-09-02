using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RsvpYes.Domain.Tests
{
    [TestClass]
    public class UserTests
    {
        private MailAddress mail;
        private User sut;

        [TestInitialize]
        public void Initialize()
        {
            mail = new MailAddress("mail@example.com");
            sut = new User("User1", mail);
        }

        [TestMethod]
        public void ユーザーを作成する()
        {
            Assert.AreEqual("User1", sut.Name);
            Assert.AreEqual(mail, sut.MailAddress);
        }

        [TestMethod]
        public void ユーザーの名前を更新する()
        {
            sut.UpdateName("User2");
            Assert.AreEqual("User2", sut.Name);
        }

        [TestMethod]
        public void ユーザーのメールアドレスを更新する()
        {
            var mail = new MailAddress("mail2@example.com");
            sut.UpdateMailAddress(mail);
            Assert.AreEqual(mail, sut.MailAddress);
        }
    }
}
