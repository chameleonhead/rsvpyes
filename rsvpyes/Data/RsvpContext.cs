using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace rsvpyes.Data
{
    public class RsvpContext : DbContext
    {
        public RsvpContext() : base(new DbContextOptionsBuilder<RsvpContext>()
            .UseSqlite("Data Souce=App_Data\\rsvp.db")
            .Options)
        {
        }

        public RsvpContext(DbContextOptions<RsvpContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Meeting> Meetings { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<RsvpRequest> RsvpRequests { get; set; }
        public DbSet<RsvpResponse> RsvpResponses { get; set; }
    }
}
