using Microsoft.VisualStudio.TestTools.UnitTesting;
using RsvpYes.Domain.Meetings;
using RsvpYes.Domain.Places;
using RsvpYes.Domain.Users;
using System;
using System.Linq;

namespace RsvpYes.Domain.Tests
{
    [TestClass]
    public class MeetingPlanTests
    {
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
            host = new UserId();
            createdAt = DateTime.UtcNow;
            sut = new MeetingPlan(host, "�V���Ј����}��", createdAt);
            user1 = new UserId();
            user2 = new UserId();
            schedule1 = new MeetingSchedule(new DateTime(2019, 09, 20, 18, 30, 00, DateTimeKind.Local), TimeSpan.FromHours(2));
            schedule2 = new MeetingSchedule(new DateTime(2019, 09, 21, 18, 30, 00, DateTimeKind.Local), TimeSpan.FromHours(2));
            place1 = new MeetingPlace(new PlaceId(), "��c���P");
            place2 = new MeetingPlace(new PlaceId(), "��c���Q");
        }

        [TestMethod]
        public void ����쐬����()
        {
            Assert.AreEqual("�V���Ј����}��", sut.Name);
            Assert.AreEqual(host, sut.HostedBy);
            Assert.AreEqual(createdAt, sut.CreatedAt);
            Assert.AreEqual(false, sut.IsScheduleFixed);
        }

        [TestMethod]
        public void ���C���Q�X�g��ݒ肷��()
        {
            sut.AddMainGuest(user1);
            CollectionAssert.AreEquivalent(
                new[] { new Participant(ParticipantRole.Host, host), new Participant(ParticipantRole.MainGuest, user1) },
                sut.Participants.ToList());
        }

        [TestMethod]
        public void ���Ҏ҂�ǉ�����()
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
        public void �Q���҂��폜����()
        {
            sut.AddMainGuest(user1);
            sut.RemoveParticipant(user1);
            CollectionAssert.AreEquivalent(
                new[] { new Participant(ParticipantRole.Host, host) },
                sut.Participants.ToList());
        }

        [TestMethod]
        public void ��Ɏ��Ԃ̌���ݒ肷��()
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
        public void ��Ɏ��Ԃ̌��𕡐��ݒ肷��()
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
        public void ��Ɏ��Ԃ̌��𕡐��ݒ��1���ɂȂ�悤�ɍ폜����()
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
        public void ��Ɏ��Ԃ̌��𕡐��ݒ��1��I������()
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
        public void ��ɏꏊ�̌���ݒ肷��()
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
        public void ��ɏꏊ�̌��𕡐��ݒ肷��()
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
        public void ��ɏꏊ�̌��𕡐��ݒ��1���ɂȂ�悤�ɍ폜����()
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
        public void ��ɏꏊ�̌��𕡐��ݒ��1��I������()
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
        public void ����m�肷��()
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
            Assert.AreEqual("�V���Ј����}��", meeting.Name);
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
