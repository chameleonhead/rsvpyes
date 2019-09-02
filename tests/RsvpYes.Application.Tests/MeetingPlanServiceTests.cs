using Microsoft.VisualStudio.TestTools.UnitTesting;
using RsvpYes.Application.Tests.Utils;
using RsvpYes.Domain;
using System.Threading.Tasks;

namespace RsvpYes.Application.Tests
{
    [TestClass]
    public class MeetingPlanServiceTests
    {
        private MeetingPlanService sut;
        private IMeetingPlanRepository repo;
        private MeetingPlanCreateCommand createCommand;
        private MeetingId meetingId;

        [TestInitialize]
        public async Task Initialize()
        {
            repo = new InMemoryMeetingPlanRepository();
            sut = new MeetingPlanService(repo);
            createCommand = new MeetingPlanCreateCommand("Meeting1", new UserId());
            meetingId = await sut.CreateAsync(createCommand).ConfigureAwait(false);
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
        public async Task 会予定の更新コマンドのテスト()
        {
            var command = new MeetingPlanUpdateCommand(meetingId, "Meeting2");
            await sut.UpdateAsync(command).ConfigureAwait(false);
            var meetingPlan = await repo.FindByIdAsync(meetingId).ConfigureAwait(false);
            Assert.AreEqual(command.MeetingName, meetingPlan.Name);
        }
    }
}
