using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace JobprtalsWebAPI.Models
{
    public partial class JobsPortalDbContext : DbContext
    {
        public JobsPortalDbContext()
        {
        }

        public JobsPortalDbContext(DbContextOptions<JobsPortalDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CompanyTable> CompanyTables { get; set; } = null!;
        public virtual DbSet<EmployeesTable> EmployeesTables { get; set; } = null!;
        public virtual DbSet<JobCategoryTable> JobCategoryTables { get; set; } = null!;
        public virtual DbSet<JobNatureTable> JobNatureTables { get; set; } = null!;
        public virtual DbSet<JobRequirementDetailsTable> JobRequirementDetailsTables { get; set; } = null!;
        public virtual DbSet<JobRequirementsTable> JobRequirementsTables { get; set; } = null!;
        public virtual DbSet<JobStatusTable> JobStatusTables { get; set; } = null!;
        public virtual DbSet<PostJobTable> PostJobTables { get; set; } = null!;
        public virtual DbSet<UserTable> UserTables { get; set; } = null!;
        public virtual DbSet<UserTypeTable> UserTypeTables { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CompanyTable>(entity =>
            {
                entity.HasKey(e => e.CompanyId);

                entity.HasIndex(e => e.UserId, "IX_FK_CompanyTable_UserTable");

                entity.Property(e => e.CompanyId).HasColumnName("CompanyID");

                entity.Property(e => e.CompanyName).HasMaxLength(500);

                entity.Property(e => e.ContactNo).HasMaxLength(20);

                entity.Property(e => e.Description).HasMaxLength(1000);

                entity.Property(e => e.EmailAddress).HasMaxLength(500);

                entity.Property(e => e.PhoneNo).HasMaxLength(150);

                entity.Property(e => e.UserId).HasColumnName("UserID");

                //entity.HasOne(d => d.User)
                //    .WithMany(p => p.CompanyTables)
                //    .HasForeignKey(d => d.UserId)
                //    .OnDelete(DeleteBehavior.ClientSetNull)
                //    .HasConstraintName("FK_CompanyTable_UserTable");
            });

            modelBuilder.Entity<EmployeesTable>(entity =>
            {
                entity.HasKey(e => e.EmployeeId);

                entity.HasIndex(e => e.UserId, "IX_FK_EmployeesTable_UserTable");

                entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");

                entity.Property(e => e.Description).HasMaxLength(4000);

                entity.Property(e => e.Dob)
                    .HasColumnType("datetime")
                    .HasColumnName("DOB");

                entity.Property(e => e.Education)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.EmailAddress).HasMaxLength(150);

                entity.Property(e => e.EmployeeName).HasMaxLength(150);

                entity.Property(e => e.Gender).HasMaxLength(20);

                entity.Property(e => e.JobReference).HasMaxLength(250);

                entity.Property(e => e.PermanentAddress).HasMaxLength(500);

                entity.Property(e => e.Qualification).HasMaxLength(150);

                entity.Property(e => e.Skills).HasMaxLength(500);

                entity.Property(e => e.WorkExperience)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                //entity.HasOne(d => d.User)
                //    .WithMany(p => p.EmployeesTables)
                //    .HasForeignKey(d => d.UserId)
                //    .OnDelete(DeleteBehavior.ClientSetNull)
                //    .HasConstraintName("FK_EmployeesTable_UserTable");
            });

            modelBuilder.Entity<JobCategoryTable>(entity =>
            {
                entity.HasKey(e => e.JobCategoryId);

                entity.Property(e => e.JobCategoryId).HasColumnName("JobCategoryID");

                entity.Property(e => e.Description).HasMaxLength(500);
            });

            modelBuilder.Entity<JobNatureTable>(entity =>
            {
                entity.HasKey(e => e.JobNatureId);

                entity.Property(e => e.JobNatureId).HasColumnName("JobNatureID");

                entity.Property(e => e.JobNature).HasMaxLength(100);
            });

            modelBuilder.Entity<JobRequirementDetailsTable>(entity =>
            {
                entity.HasKey(e => e.JobRequirementDetailsId);

                entity.HasIndex(e => e.JobRequirementId, "IX_FK_JobRequirementDetailsTable_JobRequirementsTable");

                entity.HasIndex(e => e.PostJobId, "IX_FK_JobRequirementDetailsTable_PostJobTable");

                entity.Property(e => e.JobRequirementDetailsId).HasColumnName("JobRequirementDetailsID");

                entity.Property(e => e.JobRequirementDetails).HasMaxLength(2000);

                entity.Property(e => e.JobRequirementId).HasColumnName("JobRequirementID");

                entity.Property(e => e.PostJobId).HasColumnName("PostJobID");

                //entity.HasOne(d => d.JobRequirement)
                //    .WithMany(p => p.JobRequirementDetailsTables)
                //    .HasForeignKey(d => d.JobRequirementId)
                //    .HasConstraintName("FK_JobRequirementDetailsTable_JobRequirementsTable");

                //entity.HasOne(d => d.PostJob)
                //    .WithMany(p => p.JobRequirementDetailsTables)
                //    .HasForeignKey(d => d.PostJobId)
                //    .HasConstraintName("FK_JobRequirementDetailsTable_PostJobTable");
            });

            modelBuilder.Entity<JobRequirementsTable>(entity =>
            {
                entity.HasKey(e => e.JobRequirementId);

                entity.Property(e => e.JobRequirementId).HasColumnName("JobRequirementID");

                entity.Property(e => e.JobRequirement).HasMaxLength(1000);
            });

            modelBuilder.Entity<JobStatusTable>(entity =>
            {
                entity.HasKey(e => e.JobStatusId);

                entity.Property(e => e.JobStatusId).HasColumnName("JobStatusID");

                entity.Property(e => e.JobStatus).HasMaxLength(150);

                entity.Property(e => e.StatusMessage).HasMaxLength(2000);
            });

            modelBuilder.Entity<PostJobTable>(entity =>
            {
                entity.HasKey(e => e.PostJobId);

                entity.HasIndex(e => e.CompanyId, "IX_FK_PostJobTable_CompanyTable");

                entity.HasIndex(e => e.JobCategoryId, "IX_FK_PostJobTable_JobCategoryTable");

                entity.HasIndex(e => e.JobNatureId, "IX_FK_PostJobTable_JobNatureTable");

                entity.HasIndex(e => e.JobStatusId, "IX_FK_PostJobTable_JobStatusTable");

                entity.HasIndex(e => e.UserId, "IX_FK_PostJobTable_UserTable");

                entity.Property(e => e.PostJobId).HasColumnName("PostJobID");

                entity.Property(e => e.ApplicationDeadline).HasColumnType("datetime");

                entity.Property(e => e.CompanyId).HasColumnName("CompanyID");

                entity.Property(e => e.JobCategoryId).HasColumnName("JobCategoryID");

                entity.Property(e => e.JobDescription).HasMaxLength(2000);

                entity.Property(e => e.JobNatureId).HasColumnName("JobNatureID");

                entity.Property(e => e.JobStatusId).HasColumnName("JobStatusID");

                entity.Property(e => e.JobTitle).HasMaxLength(500);

                entity.Property(e => e.LastDate).HasColumnType("datetime");

                entity.Property(e => e.Location).HasMaxLength(500);

                entity.Property(e => e.PostDate).HasColumnType("datetime");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                //entity.HasOne(d => d.Company)
                //    .WithMany(p => p.PostJobTables)
                //    .HasForeignKey(d => d.CompanyId)
                //    .OnDelete(DeleteBehavior.ClientSetNull)
                //    .HasConstraintName("FK_PostJobTable_CompanyTable");

                //entity.HasOne(d => d.JobCategory)
                //    .WithMany(p => p.PostJobTables)
                //    .HasForeignKey(d => d.JobCategoryId)
                //    .OnDelete(DeleteBehavior.ClientSetNull)
                //    .HasConstraintName("FK_PostJobTable_JobCategoryTable");

                //entity.HasOne(d => d.JobNature)
                //    .WithMany(p => p.PostJobTables)
                //    .HasForeignKey(d => d.JobNatureId)
                //    .OnDelete(DeleteBehavior.ClientSetNull)
                //    .HasConstraintName("FK_PostJobTable_JobNatureTable");

                //entity.HasOne(d => d.JobStatus)
                //    .WithMany(p => p.PostJobTables)
                //    .HasForeignKey(d => d.JobStatusId)
                //    .OnDelete(DeleteBehavior.ClientSetNull)
                //    .HasConstraintName("FK_PostJobTable_JobStatusTable");

                //entity.HasOne(d => d.User)
                //    .WithMany(p => p.PostJobTables)
                //    .HasForeignKey(d => d.UserId)
                //    .OnDelete(DeleteBehavior.ClientSetNull)
                //    .HasConstraintName("FK_PostJobTable_UserTable");
            });

            modelBuilder.Entity<UserTable>(entity =>
            {
                entity.HasKey(e => e.UserId);

                entity.HasIndex(e => e.UserTypeId, "IX_FK_UserTypeTable_UserTable");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.ContactNo).HasMaxLength(20);

                entity.Property(e => e.EmailAddress).HasMaxLength(150);

                entity.Property(e => e.Password).HasMaxLength(20);

                entity.Property(e => e.UserName).HasMaxLength(150);

                entity.Property(e => e.UserTypeId).HasColumnName("UserTypeID");

                //entity.HasOne(d => d.UserType)
                //    .WithMany(p => p.UserTables)
                //    .HasForeignKey(d => d.UserTypeId)
                //    .OnDelete(DeleteBehavior.ClientSetNull)
                //    .HasConstraintName("FK_UserTypeTable_UserTable");
            });

            modelBuilder.Entity<UserTypeTable>(entity =>
            {
                entity.HasKey(e => e.UserTypeId);

                entity.Property(e => e.UserTypeId).HasColumnName("UserTypeID");

                entity.Property(e => e.UserType).HasMaxLength(150);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
