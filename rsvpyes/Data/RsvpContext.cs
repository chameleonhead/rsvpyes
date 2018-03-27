using Microsoft.EntityFrameworkCore;

namespace rsvpyes.Data
{
    public class RsvpContext : DbContext
    {
        public RsvpContext(DbContextOptions<RsvpContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Meeting> Meetings { get; set; }
        public DbSet<RsvpRequest> RsvpRequests { get; set; }
        public DbSet<RsvpResponse> RsvpResponses { get; set; }
    }
}
