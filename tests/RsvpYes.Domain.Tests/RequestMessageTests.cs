using Microsoft.VisualStudio.TestTools.UnitTesting;
using RsvpYes.Domain.Messaging;
using RsvpYes.Domain.Users;
using System;

namespace RsvpYes.Domain.Tests
{
    [TestClass]
    public class RequestMessageTests
    {
        [TestMethod]
        public void 署名なしで要求メッセージを作成する()
        {
            var from = new User("Test User", new MailAddress("test@test.com"), new OrganizationId());
            var to = new User("Test User", new MailAddress("test@test.com"), new OrganizationId());
            var message = new RequestMessage(from, to, "test message", "http://response");
            Assert.AreEqual(string.Concat(
                "Test User 様", Environment.NewLine,
                Environment.NewLine,
                "test message", Environment.NewLine,
                Environment.NewLine,
                "回答は以下のURLよりお願いいたします。", Environment.NewLine,
                "http://response", Environment.NewLine,
                Environment.NewLine,
                "以上", Environment.NewLine), message.MessageBody);
        }

        [TestMethod]
        public void 署名ありで要求メッセージを作成する()
        {
            var from = new User("Test User", new MailAddress("test@test.com"), new OrganizationId());
            from.UpdateMessageSignature("==================\nTest User");
            var to = new User("Test User", new MailAddress("test@test.com"), new OrganizationId());
            var message = new RequestMessage(from, to, "test message", "http://response");
            Assert.AreEqual(string.Concat(
                "Test User 様", Environment.NewLine,
                Environment.NewLine,
                "test message", Environment.NewLine,
                Environment.NewLine,
                "回答は以下のURLよりお願いいたします。", Environment.NewLine,
                "http://response", Environment.NewLine,
                Environment.NewLine,
                "以上", Environment.NewLine,
                Environment.NewLine,
                "==================", Environment.NewLine,
                "Test User", Environment.NewLine), message.MessageBody);
        }
    }
}
