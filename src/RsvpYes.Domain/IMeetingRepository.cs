using System.Threading.Tasks;

namespace RsvpYes.Domain
{
    public interface IMeetingRepository
    {
        Task<Meeting> FindByIdAsync(MeetingId meetingId);
        Task SaveAsync(Meeting meeting);
    }
}
