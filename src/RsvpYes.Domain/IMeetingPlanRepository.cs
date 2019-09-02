using System.Threading.Tasks;

namespace RsvpYes.Domain
{
    public interface IMeetingPlanRepository
    {
        Task<MeetingPlan> FindByIdAsync(MeetingId meetingId);
        Task SaveAsync(MeetingPlan meetingPlan);
    }
}
