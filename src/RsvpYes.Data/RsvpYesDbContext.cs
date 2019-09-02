using Microsoft.EntityFrameworkCore;
using RsvpYes.Data.MeetingPlans;

namespace RsvpYes.Data
{
    public class RsvpYesDbContext : DbContext
    {
        public RsvpYesDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<MeetingPlanEntity> MeetingPlans { get; internal set; }
        public DbSet<MeetingPlanParticipantEntity> MeetingPlanParticipants { get; internal set; }
        public DbSet<MeetingPlanPlaceCandidateEntity> MeetingPlanPlaceCandidates { get; internal set; }
        public DbSet<MeetingPlanScheduleCandidateEntity> MeetingPlanScheduleCandidates { get; internal set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MeetingPlanParticipantEntity>().HasKey(p => new { p.MeetingId, p.UserId });
            modelBuilder.Entity<MeetingPlanPlaceCandidateEntity>().HasKey(p => new { p.MeetingId, p.Seq });
            modelBuilder.Entity<MeetingPlanScheduleCandidateEntity>().HasKey(p => new { p.MeetingId, p.Seq });
        }
    }
}
