﻿using Microsoft.EntityFrameworkCore;
using RsvpYes.Data.Meetings;
using RsvpYes.Data.Messaging;
using RsvpYes.Data.Places;
using RsvpYes.Data.Users;
using System;
using System.Linq;

namespace RsvpYes.Data
{
    public class RsvpYesDbContext : DbContext
    {
        public RsvpYesDbContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
            if (!Identities.Any())
            {
                Identities.Add(new IdentityEntity()
                {
                    Id = Guid.NewGuid(),
                    AccountName = "admin",
                    PasswordHash = "AQAAAAEAACcQAAAAEMAL57tk5x40NvsLkLQOAlShnSg7Bf8zpFtlcyG6/R9MA0TLXChQQ3ApbfPVpwS1NA==" // Passw0rd
                });
                SaveChanges();
            }
        }

        internal DbSet<MeetingPlanEntity> MeetingPlans { get; set; }
        internal DbSet<MeetingPlanParticipantEntity> MeetingPlanParticipants { get; set; }
        internal DbSet<MeetingPlanPlaceCandidateEntity> MeetingPlanPlaceCandidates { get; set; }
        internal DbSet<MeetingPlanScheduleCandidateEntity> MeetingPlanScheduleCandidates { get; set; }

        internal DbSet<SentMessageEntity> SentMessages { get; set; }

        internal DbSet<PlaceEntity> Places { get; set; }

        internal DbSet<IdentityEntity> Identities { get; set; }
        internal DbSet<OrganizationEntity> Organizations { get; set; }
        internal DbSet<UserEntity> Users { get; set; }
        internal DbSet<UserMetadataEntity> UserMetadata { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MeetingPlanEntity>().HasKey(e => e.Id);
            modelBuilder.Entity<MeetingPlanParticipantEntity>().HasKey(e => new { e.MeetingId, e.UserId });
            modelBuilder.Entity<MeetingPlanPlaceCandidateEntity>().HasKey(e => new { e.MeetingId, e.Seq });
            modelBuilder.Entity<MeetingPlanScheduleCandidateEntity>().HasKey(e => new { e.MeetingId, e.Seq });
            modelBuilder.Entity<SentMessageEntity>().HasKey(e => e.Id);
            modelBuilder.Entity<PlaceEntity>().HasKey(e => e.Id);
            modelBuilder.Entity<OrganizationEntity>().HasKey(e => e.Id);
            modelBuilder.Entity<UserEntity>().HasKey(e => e.Id);
            modelBuilder.Entity<UserMetadataEntity>().HasKey(e => new { e.UserId, e.Key });
        }
    }
}
