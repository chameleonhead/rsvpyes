using Microsoft.VisualStudio.TestTools.UnitTesting;
using RsvpYes.Domain.Meetings;
using RsvpYes.Domain.Users;
using System;
using System.Linq;

namespace RsvpYes.Domain.Tests
{
    [TestClass]
    public class MeetingAdjustmentTests
    {
        private MeetingId meetingId;
        private UserId user1;
        private UserId user2;
        private MeetingSchedule schedule1;
        private MeetingSchedule schedule2;
        private MeetingSchedule schedule3;
        private MeetingAdjustment<MeetingSchedule> sut;

        [TestInitialize]
        public void Initialize()
        {
            meetingId = new MeetingId();
            user1 = new UserId();
            user2 = new UserId();
            schedule1 = new MeetingSchedule(new DateTime(2019, 9, 10, 18, 20, 0, DateTimeKind.Utc), TimeSpan.FromHours(2));
            schedule2 = new MeetingSchedule(new DateTime(2019, 9, 11, 18, 20, 0, DateTimeKind.Utc), TimeSpan.FromHours(2));
            schedule3 = new MeetingSchedule(new DateTime(2019, 9, 10, 12, 20, 0, DateTimeKind.Utc), TimeSpan.FromHours(2));

            sut = new MeetingAdjustment<MeetingSchedule>(
                meetingId,
                new[] { user1, user2 },
                new[] { schedule1, schedule2, schedule3 });
        }

        [TestMethod]
        public void 調整を開始する()
        {
            Assert.AreEqual(meetingId, sut.ForMeetingId);
            CollectionAssert.AreEqual(new[] { user1, user2 }, sut.Participants.ToList());
            CollectionAssert.AreEqual(new[] { schedule1, schedule2, schedule3 }, sut.Selections.ToList());
        }
    }
}
