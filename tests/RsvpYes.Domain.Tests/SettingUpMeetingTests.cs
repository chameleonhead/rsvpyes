using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace RsvpYes.Domain.Tests
{
    [TestClass]
    public class SettingUpMeetingTests
    {
        private MeetingId meetingId;
        private MeetingPlan sut;
        private UserId host;
        private DateTime createdAt;
        private UserId user1;
        private UserId user2;
        private MeetingSchedule schedule1;
        private MeetingSchedule schedule2;
        private MeetingPlace place1;
        private MeetingPlace place2;

        [TestInitialize]
        public void Initialize()
        {
            meetingId = new MeetingId();
            host = new UserId();
            createdAt = DateTime.UtcNow;
            sut = new MeetingPlan(meetingId, host, "新入社員歓迎会", createdAt);
            user1 = new UserId();
            user2 = new UserId();
            schedule1 = new MeetingSchedule(new DateTime(2019, 09, 20, 18, 30, 00, DateTimeKind.Local), TimeSpan.FromHours(2));
            schedule2 = new MeetingSchedule(new DateTime(2019, 09, 21, 18, 30, 00, DateTimeKind.Local), TimeSpan.FromHours(2));
            place1 = new MeetingPlace(new PlaceId(), "会議室１");
            place2 = new MeetingPlace(new PlaceId(), "会議室２");
        }

        [TestMethod]
        public void 会を作成する()
        {
            Assert.AreEqual("新入社員歓迎会", sut.Name);
            Assert.AreEqual(host, sut.HostedBy);
            Assert.AreEqual(createdAt, sut.CreatedAt);
            Assert.AreEqual(false, sut.IsScheduleFixed);
        }

        [TestMethod]
        public void メインゲストを設定する()
        {
            sut.AddMainGuest(user1);
            CollectionAssert.AreEquivalent(
                new[] { new Participant(ParticipantRole.Host, host), new Participant(ParticipantRole.MainGuest, user1) },
                sut.Participants.ToList());
        }

        [TestMethod]
        public void 招待者を追加する()
        {
            sut.AddMainGuest(user1);
            sut.AddGuest(user2);
            CollectionAssert.AreEquivalent(
                new[] {
                    new Participant(ParticipantRole.Host, host),
                    new Participant(ParticipantRole.MainGuest, user1),
                    new Participant(ParticipantRole.Guest, user2)
                },
                sut.Participants.ToList());
        }

        [TestMethod]
        public void 参加者を削除する()
        {
            sut.AddMainGuest(user1);
            sut.RemoveParticipant(user1);
            CollectionAssert.AreEquivalent(
                new[] { new Participant(ParticipantRole.Host, host) },
                sut.Participants.ToList());
        }

        [TestMethod]
        public void 会に時間の候補を設定する()
        {
            sut.AddMainGuest(user1);
            sut.AddCandidateSchedule(schedule1);
            Assert.AreEqual(true, sut.IsScheduleFixed);
            Assert.AreEqual(schedule1, sut.Schedule);
            CollectionAssert.AreEquivalent(
                new[] { schedule1 },
                sut.ScheduleCandidates.Select(c => c.Schedule).ToList());
        }

        [TestMethod]
        public void 会に時間の候補を複数設定する()
        {
            sut.AddMainGuest(user1);
            sut.AddCandidateSchedule(schedule1);
            sut.AddCandidateSchedule(schedule2);
            Assert.AreEqual(false, sut.IsScheduleFixed);
            Assert.IsNull(sut.Schedule);
            CollectionAssert.AreEquivalent(
                new[] { schedule1, schedule2 },
                sut.ScheduleCandidates.Select(c => c.Schedule).ToList());
        }

        [TestMethod]
        public void 会に時間の候補を複数設定後1件になるように削除する()
        {
            sut.AddMainGuest(user1);
            sut.AddCandidateSchedule(schedule1);
            sut.AddCandidateSchedule(schedule2);
            sut.RemoveCandidateSchedule(new MeetingSchedule(schedule2.BeginAt, schedule2.Duration));
            Assert.AreEqual(true, sut.IsScheduleFixed);
            Assert.AreEqual(schedule1, sut.Schedule);
            CollectionAssert.AreEquivalent(
                new[] { schedule1 },
                sut.ScheduleCandidates.Select(c => c.Schedule).ToList());
        }

        [TestMethod]
        public void 会に時間の候補を複数設定後1つを選択する()
        {
            sut.AddMainGuest(user1);
            sut.AddCandidateSchedule(schedule1);
            sut.AddCandidateSchedule(schedule2);
            sut.SelectCandidateSchedule(schedule1);
            Assert.AreEqual(true, sut.IsScheduleFixed);
            Assert.AreEqual(schedule1, sut.Schedule);
            CollectionAssert.AreEquivalent(
                new[] { schedule1, schedule2 },
                sut.ScheduleCandidates.Select(c => c.Schedule).ToList());
        }

        [TestMethod]
        public void 会に場所の候補を設定する()
        {
            sut.AddMainGuest(user1);
            sut.AddCandidatePlace(place1);
            Assert.AreEqual(true, sut.IsPlaceFixed);
            Assert.AreEqual(place1, sut.Place);
            CollectionAssert.AreEquivalent(
                new[] { place1 },
                sut.PlaceCandidates.Select(c => c.Place).ToList());
        }

        [TestMethod]
        public void 会に場所の候補を複数設定する()
        {
            sut.AddMainGuest(user1);
            sut.AddCandidatePlace(place1);
            sut.AddCandidatePlace(place2);
            Assert.AreEqual(false, sut.IsPlaceFixed);
            Assert.IsNull(sut.Place);
            CollectionAssert.AreEquivalent(
                new[] { place1, place2 },
                sut.PlaceCandidates.Select(c => c.Place).ToList());
        }

        [TestMethod]
        public void 会に場所の候補を複数設定後1件になるように削除する()
        {
            sut.AddMainGuest(user1);
            sut.AddCandidatePlace(place1);
            sut.AddCandidatePlace(place2);
            sut.RemoveCandidatePlace(new MeetingPlace(place2.PlaceId, place2.DetailInfo));
            Assert.AreEqual(true, sut.IsPlaceFixed);
            Assert.AreEqual(place1, sut.Place);
            CollectionAssert.AreEquivalent(
                new[] { place1 },
                sut.PlaceCandidates.Select(c => c.Place).ToList());
        }

        [TestMethod]
        public void 会に場所の候補を複数設定後1つを選択する()
        {
            sut.AddMainGuest(user1);
            sut.AddCandidatePlace(place1);
            sut.AddCandidatePlace(place2);
            sut.SelectCandidatePlace(place1);
            Assert.AreEqual(true, sut.IsPlaceFixed);
            Assert.AreEqual(place1, sut.Place);
            CollectionAssert.AreEquivalent(
                new[] { place1, place2 },
                sut.PlaceCandidates.Select(c => c.Place).ToList());
        }

        [TestMethod]
        public void 会を確定する()
        {
            sut.AddMainGuest(user1);
            sut.AddGuest(user2);
            sut.AddCandidateSchedule(schedule1);
            sut.AddCandidateSchedule(schedule2);
            sut.SelectCandidateSchedule(schedule1);
            sut.AddCandidatePlace(place1);
            sut.AddCandidatePlace(place2);
            sut.SelectCandidatePlace(place1);
            var meeting = sut.Fix();

            Assert.AreEqual(true, sut.IsFixed);
            Assert.AreEqual(sut.Id, meeting.Id);
            Assert.AreEqual("Meeting1", meeting.Name);
            Assert.AreEqual(schedule1, meeting.Schedule);
            Assert.AreEqual(place1, meeting.Place);
            CollectionAssert.AreEquivalent(new[]
            {
                new Participant(ParticipantRole.Host, host),
                new Participant(ParticipantRole.MainGuest, user1),
                new Participant(ParticipantRole.Guest, user2),
            },
            meeting.Participants);
        }
    }
}
