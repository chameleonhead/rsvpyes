using System.Threading.Tasks;

namespace RsvpYes.Domain.Meetings
{
    public interface IMeetingRepository
    {
        Task<Meeting> FindByIdAsync(MeetingId meetingId);
        Task SaveAsync(Meeting meeting);
    }
}
