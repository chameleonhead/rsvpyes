using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RsvpYes.Domain.Meetings;
using RsvpYes.Domain.Places;
using RsvpYes.Domain.Users;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace RsvpYes.Data.Tests
{
    [TestClass]
    public class MeetingPlanRepositoryTests
    {
        private RsvpYesDbContext context;
        private MeetingPlanRepository sut;

        [TestInitialize]
        public async Task Initialize()
        {
            context = new RsvpYesDbContext(
                new DbContextOptionsBuilder<RsvpYesDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options);
            await context.Database.EnsureCreatedAsync().ConfigureAwait(false);
            sut = new MeetingPlanRepository(context);
        }

        [TestCleanup]
        public void Cleanup()
        {
            context.Dispose();
        }

        [TestMethod]
        public async Task MeetingPlanリポジトリにデータを登録して取得する()
        {
            var data = new MeetingPlan(new UserId(), "新入社員歓迎会", DateTime.UtcNow);
            data.AddMainGuest(new UserId());
            data.AddGuest(new UserId());
            data.AddCandidatePlace(new MeetingPlace(new PlaceId(), "テスト"));
            data.AddCandidateSchedule(new MeetingSchedule(new DateTime(2019, 9, 18, 18, 30, 00, DateTimeKind.Local), TimeSpan.FromHours(2)));
            await sut.SaveAsync(data).ConfigureAwait(false);
            var persistedData = await sut.FindByIdAsync(data.Id);
            AssertData(data, persistedData);
            await sut.SaveAsync(data).ConfigureAwait(false);
            persistedData = await sut.FindByIdAsync(data.Id);
            AssertData(data, persistedData);
        }

        private void AssertData(MeetingPlan data, MeetingPlan persistedData)
        {
            Assert.AreEqual(data.Id, persistedData.Id);
            Assert.AreEqual(data.HostedBy, persistedData.HostedBy);
            Assert.AreEqual(data.Name, persistedData.Name);
            Assert.AreEqual(data.CreatedAt, persistedData.CreatedAt);
            Assert.AreEqual(data.Schedule, persistedData.Schedule);
            Assert.AreEqual(data.Place, persistedData.Place);
            Assert.AreEqual(data.IsFixed, persistedData.IsFixed);

            CollectionAssert.AreEquivalent(
                data.Participants.ToList(),
                persistedData.Participants.ToList());

            var persistedPlaceCandidate = persistedData.PlaceCandidates.First();
            Assert.AreEqual(data.Place, persistedPlaceCandidate.Place);

            var persistedScheduleCandidate = persistedData.ScheduleCandidates.First();
            Assert.AreEqual(data.Schedule, persistedScheduleCandidate.Schedule);
        }
    }
}
