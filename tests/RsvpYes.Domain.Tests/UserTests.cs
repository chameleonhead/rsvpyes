using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace RsvpYes.Domain.Tests
{
    [TestClass]
    public class UserTests
    {
        private User sut;
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

        [TestInitialize]
        public void Initialize()
        {
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
            sut = new User("User1", mail1, org1);
        }

        [TestMethod]
        public void ユーザーを作成する()
        {
            Assert.AreEqual("User1", sut.Name);
            Assert.AreEqual(org1, sut.OrganizationId);
            Assert.AreEqual(mail1, sut.DefaultMailAddress);
            CollectionAssert.AreEquivalent(
                new[] { mail1 },
                sut.MailAddresses.ToList());
        }

        [TestMethod]
        public void ユーザーの名前を更新する()
        {
            sut.UpdateName("User2");
            Assert.AreEqual("User2", sut.Name);
        }

        [TestMethod]
        public void ユーザーの所属を更新する()
        {
            sut.UpdateOrganizationId(org2);
            Assert.AreEqual(org2, sut.OrganizationId);
        }

        [TestMethod]
        public void ユーザーのメールアドレスを最大5件追加する()
        {
            sut.AddMailAddress(mail2);
            sut.AddMailAddress(mail3);
            sut.AddMailAddress(mail4);
            sut.AddMailAddress(mail5);
            CollectionAssert.AreEquivalent(
                new[] { mail1, mail2, mail3, mail4, mail5 },
                sut.MailAddresses.ToList());
        }

        [TestMethod]
        public void ユーザーのメールアドレスを削除する()
        {
            sut.AddMailAddress(mail2);
            sut.AddMailAddress(mail3);
            sut.AddMailAddress(mail4);
            sut.AddMailAddress(mail5);
            sut.RemoveMailAddress(mail1);
            Assert.AreEqual(mail2, sut.DefaultMailAddress);
            sut.RemoveMailAddress(mail2);
            sut.RemoveMailAddress(mail3);
            sut.RemoveMailAddress(mail4);
            sut.RemoveMailAddress(mail5);
            Assert.IsNull(sut.DefaultMailAddress);
            Assert.IsFalse(sut.MailAddresses.Any());
        }

        [TestMethod]
        public void ユーザーの電話番号を最大5件追加する()
        {
            sut.AddPhoneNumber(tel1);
            sut.AddPhoneNumber(tel2);
            sut.AddPhoneNumber(tel3);
            sut.AddPhoneNumber(tel4);
            sut.AddPhoneNumber(tel5);
            CollectionAssert.AreEquivalent(
                new[] { tel1, tel2, tel3, tel4, tel5 },
                sut.PhoneNumbers.ToList());
        }

        [TestMethod]
        public void ユーザーの電話番号を削除する()
        {
            sut.AddPhoneNumber(tel1);
            sut.AddPhoneNumber(tel2);
            sut.AddPhoneNumber(tel3);
            sut.AddPhoneNumber(tel4);
            sut.AddPhoneNumber(tel5);
            sut.RemovePhoneNumber(tel1);
            sut.RemovePhoneNumber(tel2);
            sut.RemovePhoneNumber(tel3);
            sut.RemovePhoneNumber(tel4);
            sut.RemovePhoneNumber(tel5);
            Assert.IsFalse(sut.PhoneNumbers.Any());
        }
    }
}
