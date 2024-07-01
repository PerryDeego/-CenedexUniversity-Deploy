using CenedexUniversityWebSystem.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CenedexUniversityWebSystem.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            /* Users authentication and authorization model builders. */
            modelBuilder.HasDefaultSchema("Identity");

            modelBuilder.Entity<Course>().ToTable("Course");
            modelBuilder.Entity<Lecturer>().ToTable("Lecturer");
            modelBuilder.Entity<Student>().ToTable("Student");
            modelBuilder.Entity<StudentGrade>().ToTable("StudentGrade");
            modelBuilder.Entity<MainMenu>().Property(e => e.MenuName).IsUnicode(false);
            modelBuilder.Entity<SubMenu>().ToTable("SubMenu");
            modelBuilder.Entity<CourseAfterDeleteAuditLog>().ToTable("CourseAfterDeleteAuditLog");
            modelBuilder.Entity<CourseAfterUpdateAuditLog>().ToTable("CourseAfterUpdateAuditLog");
            modelBuilder.Entity<LecturerAfterDeleteAuditLog>().ToTable("LecturerAfterDeleteAuditLog");
            modelBuilder.Entity<LecturerAfterDeleteAuditLog>().ToTable("LecturerAfterDeleteAuditLog");
            modelBuilder.Entity<StudentAfterDeleteAuditLog>().ToTable("StudentAfterDeleteAuditLog");
            modelBuilder.Entity<StudentAfterUpdateAuditLog>().ToTable("StudentAfterUpdateAuditLog");
            modelBuilder.Entity<StudentGradeAfterDeleteAuditLog>().ToTable("StudentGradeAfterDeleteAuditLog");

            // Indicates that the entity has no key attriblute
            modelBuilder.Entity<CourseReportViewModel>(entity => entity.HasNoKey());

            modelBuilder.Entity<IdentityUser>(entity =>
            {
                entity.ToTable(name: "User");
            });

            modelBuilder.Entity<IdentityRole>(entity =>
            {
                entity.ToTable(name: "Role");
            });
            modelBuilder.Entity<IdentityUserRole<string>>(entity =>
            {
                entity.ToTable("UserRoles");
            });

            modelBuilder.Entity<IdentityUserClaim<string>>(entity =>
            {
                entity.ToTable("UserClaims");
            });

            modelBuilder.Entity<IdentityUserLogin<string>>(entity =>
            {
                entity.ToTable("UserLogins");
            });

            modelBuilder.Entity<IdentityRoleClaim<string>>(entity =>
            {
                entity.ToTable("RoleClaims");

            });

            modelBuilder.Entity<IdentityUserToken<string>>(entity =>
            {
                entity.ToTable("UserTokens");
            });
        }

        public virtual DbSet<CenedexUniversityWebSystem.Models.CourseReportViewModel> CourseReportViewModel { get; set; }
        public virtual DbSet<CenedexUniversityWebSystem.Models.Course> Course { get; set; }
        public DbSet<CenedexUniversityWebSystem.Models.Lecturer> Lecturer { get; set; }
        public DbSet<CenedexUniversityWebSystem.Models.Student> Student { get; set; }
        public DbSet<CenedexUniversityWebSystem.Models.StudentGrade> StudentGrade { get; set; }
        public DbSet<CenedexUniversityWebSystem.Models.SubMenu> SubMenu { get; set; }
        public DbSet<CenedexUniversityWebSystem.Models.MainMenu> MainMenu { get; set; }

        public DbSet<CenedexUniversityWebSystem.Models.CourseAfterUpdateAuditLog> CourseAfterUpdateAuditLog { get; set; }
        public DbSet<CenedexUniversityWebSystem.Models.CourseAfterDeleteAuditLog> CourseAfterDeleteAuditLog { get; set; }
        public DbSet<CenedexUniversityWebSystem.Models.LecturerAfterUpdateAuditLog> LecturerAfterUpdateAuditLog { get; set; }
        public DbSet<CenedexUniversityWebSystem.Models.LecturerAfterDeleteAuditLog> LecturerAfterDeleteAuditLog { get; set; }
        public DbSet<CenedexUniversityWebSystem.Models.StudentAfterUpdateAuditLog> StudentAfterUpdateAuditLog { get; set; }
        public DbSet<CenedexUniversityWebSystem.Models.StudentAfterDeleteAuditLog> StudentAfterDeleteAuditLog { get; set; }
        public DbSet<CenedexUniversityWebSystem.Models.StudentGradeAfterUpdateAuditLog> StudentGradeAfterUpdateAuditLog { get; set; }
        public DbSet<CenedexUniversityWebSystem.Models.StudentGradeAfterDeleteAuditLog> StudentGradeAfterDeleteAuditLog { get; set; }
        public DbSet<CenedexUniversityWebSystem.Models.MainMenuAfterUpdateAuditLog> MainMenuAfterUpdateAuditLog { get; set; }
        public DbSet<CenedexUniversityWebSystem.Models.MainMenuAfterDeleteAuditLog> MainMenuAfterDeleteAuditLog { get; set; }
        public DbSet<CenedexUniversityWebSystem.Models.SubMenuAfterUpdateAuditLog> SubMenuAfterUpdateAuditLog { get; set; }
        public DbSet<CenedexUniversityWebSystem.Models.SubMenuAfterDeleteAuditLog> SubMenuAfterDeleteAuditLog { get; set; }
    }
}
