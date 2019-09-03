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
            var meetingPlan = new MeetingPlan(new UserId(), "新入社員歓迎会", DateTime.UtcNow);
            meetingPlan.AddMainGuest(new UserId());
            meetingPlan.AddGuest(new UserId());
            meetingPlan.AddCandidatePlace(new MeetingPlace(new PlaceId(), "テスト"));
            meetingPlan.AddCandidateSchedule(new MeetingSchedule(new DateTime(2019, 9, 18, 18, 30, 00, DateTimeKind.Local), TimeSpan.FromHours(2)));
            await sut.SaveAsync(meetingPlan).ConfigureAwait(false);
            var persistedMeetingPlan = await sut.FindByIdAsync(meetingPlan.Id);
            Assert.AreEqual(meetingPlan.Id, persistedMeetingPlan.Id);
            Assert.AreEqual(meetingPlan.HostedBy, persistedMeetingPlan.HostedBy);
            Assert.AreEqual(meetingPlan.Name, persistedMeetingPlan.Name);
            Assert.AreEqual(meetingPlan.CreatedAt, persistedMeetingPlan.CreatedAt);
            Assert.AreEqual(meetingPlan.Schedule, persistedMeetingPlan.Schedule);
            Assert.AreEqual(meetingPlan.Place, persistedMeetingPlan.Place);
            Assert.AreEqual(meetingPlan.IsFixed, persistedMeetingPlan.IsFixed);

            CollectionAssert.AreEquivalent(
                meetingPlan.Participants.ToList(),
                persistedMeetingPlan.Participants.ToList());

            var persistedPlaceCandidate = persistedMeetingPlan.PlaceCandidates.First();
            Assert.AreEqual(meetingPlan.Place, persistedPlaceCandidate.Place);

            var persistedScheduleCandidate = persistedMeetingPlan.ScheduleCandidates.First();
            Assert.AreEqual(meetingPlan.Schedule, persistedScheduleCandidate.Schedule);

            await sut.SaveAsync(meetingPlan).ConfigureAwait(false);
            persistedMeetingPlan = await sut.FindByIdAsync(meetingPlan.Id);
            Assert.AreEqual(meetingPlan.Id, persistedMeetingPlan.Id);
            Assert.AreEqual(meetingPlan.HostedBy, persistedMeetingPlan.HostedBy);
            Assert.AreEqual(meetingPlan.Name, persistedMeetingPlan.Name);
            Assert.AreEqual(meetingPlan.CreatedAt, persistedMeetingPlan.CreatedAt);
            Assert.AreEqual(meetingPlan.Schedule, persistedMeetingPlan.Schedule);
            Assert.AreEqual(meetingPlan.Place, persistedMeetingPlan.Place);
            Assert.AreEqual(meetingPlan.IsFixed, persistedMeetingPlan.IsFixed);

            CollectionAssert.AreEquivalent(
                meetingPlan.Participants.ToList(),
                persistedMeetingPlan.Participants.ToList());

            persistedPlaceCandidate = persistedMeetingPlan.PlaceCandidates.First();
            Assert.AreEqual(meetingPlan.Place, persistedPlaceCandidate.Place);

            persistedScheduleCandidate = persistedMeetingPlan.ScheduleCandidates.First();
            Assert.AreEqual(meetingPlan.Schedule, persistedScheduleCandidate.Schedule);

        }
    }
}
