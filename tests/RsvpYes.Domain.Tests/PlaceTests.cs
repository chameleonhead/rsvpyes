using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RsvpYes.Domain.Tests
{
    [TestClass]
    public class PlaceTests
    {
        private Url url;
        private Place sut;

        [TestInitialize]
        public void Initialize()
        {
            url = new Url("http://example.com");
            sut = new Place("Place1", url);
        }

        [TestMethod]
        public void 場所を作成する()
        {
            Assert.AreEqual("Place1", sut.Name);
            Assert.AreEqual(url, sut.Url);
        }

        [TestMethod]
        public void 場所の名前を更新する()
        {
            sut.UpdateName("Place2");
            Assert.AreEqual("Place2", sut.Name);
        }

        [TestMethod]
        public void 場所のメールアドレスを更新する()
        {
            var url = new Url("http://example2.com");
            sut.UpdateUrl(url);
            Assert.AreEqual(url, sut.Url);
        }
    }
}
