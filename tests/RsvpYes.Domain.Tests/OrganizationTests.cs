using Microsoft.VisualStudio.TestTools.UnitTesting;
using RsvpYes.Domain.Users;

namespace RsvpYes.Domain.Tests
{
    [TestClass]
    public class OrganizationTests
    {
        private Organization sut;

        [TestInitialize]
        public void Initialize()
        {
            sut = new Organization("Organization1");
        }

        [TestMethod]
        public void 所属を作成する()
        {
            Assert.AreEqual("Organization1", sut.Name);
        }

        [TestMethod]
        public void 所属の名前を更新する()
        {
            sut.UpdateName("Organization2");
            Assert.AreEqual("Organization2", sut.Name);
        }
    }
}
