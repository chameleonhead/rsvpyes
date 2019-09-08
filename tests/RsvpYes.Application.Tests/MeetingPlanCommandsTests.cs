using Microsoft.VisualStudio.TestTools.UnitTesting;
using RsvpYes.Application.Meetings;
using RsvpYes.Application.Tests.Utils;
using RsvpYes.Domain.Meetings;
using RsvpYes.Domain.Places;
using RsvpYes.Domain.Users;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace RsvpYes.Application.Tests
{
    [TestClass]
    public class MeetingPlanCommandsTests
    {
        private IMeetingPlanRepository repo;
        private MeetingPlanCreateCommand createCommand;
        private MeetingId meetingId;

        [TestInitialize]
        public async Task Initialize()
        {
            repo = new InMemoryMeetingPlanRepository();
            var sut = new MeetingPlanCreateCommandHandler(repo);
            createCommand = new MeetingPlanCreateCommand("Meeting1", new UserId());
            createCommand.Schedules.Add(new MeetingSchedule(new DateTime(2019, 9, 10, 19, 20, 00, DateTimeKind.Utc), TimeSpan.FromHours(2)));
            createCommand.Places.Add(new MeetingPlace(null, "PLACE"));
            meetingId = await sut.Handle(createCommand).ConfigureAwait(false);
        }

        [TestMethod]
        public async Task 会予定の作成コマンドのテスト()
        {
            var meetingPlan = await repo.FindByIdAsync(meetingId).ConfigureAwait(false);
            Assert.AreEqual(createCommand.MeetingName, meetingPlan.Name);
            Assert.AreEqual(createCommand.Host, meetingPlan.HostedBy);
            Assert.AreEqual(createCommand.Timestamp, meetingPlan.CreatedAt);
        }

        [TestMethod]
        public async Task 会予定の名前更新コマンドのテスト()
        {
            var command = new MeetingPlanUpdateMeetingNameCommand(meetingId, "Meeting2");
            var sut = new MeetingPlanUpdateMeetingNameCommandHandler(repo);
            await sut.Handle(command).ConfigureAwait(false);
            var meetingPlan = await repo.FindByIdAsync(meetingId).ConfigureAwait(false);
            Assert.AreEqual(command.MeetingName, meetingPlan.Name);
        }

        [TestMethod]
        public async Task 会予定の参加者更新コマンドのテスト()
        {
            var command = new MeetingPlanUpdateParticipantsCommand(meetingId);
            command.ParticipantsToRemove.Add(createCommand.Host);
            var guest = new UserId();
            command.Guests.Add(guest);
            var sut = new MeetingPlanUpdateParticipantsCommandHandler(repo);
            await sut.Handle(command).ConfigureAwait(false);
            var meetingPlan = await repo.FindByIdAsync(meetingId).ConfigureAwait(false);
            CollectionAssert.AreEquivalent(
                new[] { guest },
                meetingPlan.Participants.Select(p => p.UserId).ToList());
        }

        [TestMethod]
        public async Task 会予定のスケジュール更新コマンドのテスト()
        {
            var schedule = new MeetingSchedule(new DateTime(2019, 10, 3, 18, 20, 30, DateTimeKind.Utc), TimeSpan.FromHours(2));
            var command = new MeetingPlanUpdateSchedulesCommand(meetingId);
            command.SchedulesToRemove.AddRange(createCommand.Schedules);
            command.SchedulesToAdd.Add(schedule);
            var sut = new MeetingPlanUpdateSchedulesCommandHandler(repo);
            await sut.Handle(command).ConfigureAwait(false);
            var meetingPlan = await repo.FindByIdAsync(meetingId).ConfigureAwait(false);
            CollectionAssert.AreEquivalent(
                new[] { schedule },
                meetingPlan.ScheduleCandidates.Select(p => p.Schedule).ToList());
        }

        [TestMethod]
        public async Task 会予定の候補場所更新コマンドのテスト()
        {
            var place = new MeetingPlace(new PlaceId(), "");
            var command = new MeetingPlanUpdatePlacesCommand(meetingId);
            command.PlacesToRemove.AddRange(createCommand.Places);
            command.PlacesToAdd.Add(place);
            var sut = new MeetingPlanUpdatePlacesCommandHandler(repo);
            await sut.Handle(command).ConfigureAwait(false);
            var meetingPlan = await repo.FindByIdAsync(meetingId).ConfigureAwait(false);
            CollectionAssert.AreEquivalent(
                new[] { place },
                meetingPlan.PlaceCandidates.Select(p => p.Place).ToList());
        }
    }
}
