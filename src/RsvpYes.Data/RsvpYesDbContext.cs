using Microsoft.EntityFrameworkCore;
using RsvpYes.Data.Meetings;
using RsvpYes.Data.Users;

namespace RsvpYes.Data
{
    public class RsvpYesDbContext : DbContext
    {
        public RsvpYesDbContext(DbContextOptions options) : base(options)
        {
        }

        internal DbSet<MeetingPlanEntity> MeetingPlans { get; set; }
        internal DbSet<MeetingPlanParticipantEntity> MeetingPlanParticipants { get; set; }
        internal DbSet<MeetingPlanPlaceCandidateEntity> MeetingPlanPlaceCandidates { get; set; }
        internal DbSet<MeetingPlanScheduleCandidateEntity> MeetingPlanScheduleCandidates { get; set; }

        internal DbSet<UserEntity> Users { get; set; }
        internal DbSet<UserMetadataEntity> UserMetadata { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MeetingPlanParticipantEntity>().HasKey(p => new { p.MeetingId, p.UserId });
            modelBuilder.Entity<MeetingPlanPlaceCandidateEntity>().HasKey(p => new { p.MeetingId, p.Seq });
            modelBuilder.Entity<MeetingPlanScheduleCandidateEntity>().HasKey(p => new { p.MeetingId, p.Seq });
            modelBuilder.Entity<UserMetadataEntity>().HasKey(p => new { p.UserId, p.Key });
        }
    }
}
