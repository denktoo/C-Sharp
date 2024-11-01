using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using KenyattaUniversity.Models;

namespace KenyattaUniversity.Data
{
    public class KUContext : IdentityDbContext<ApplicationUser>
    {
        public KUContext(DbContextOptions<KUContext> options)
            : base(options)
        {
        }

        public DbSet<Course> Courses { get; set; } // DbSet for Course entity
        public DbSet<Enrollment> Enrollments { get; set; } // DbSet for Enrollment entity
        public DbSet<Student> Students { get; set; } // DbSet for Student entity

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Enrollment>()
                .HasKey(e => e.EnrollmentID);

            modelBuilder.Entity<Enrollment>()
                .HasOne(e => e.Course)
                .WithMany(c => c.Enrollments)
                .HasForeignKey(e => e.CourseID);

            modelBuilder.Entity<Enrollment>()
                .HasOne(e => e.Student)
                .WithMany(s => s.Enrollments)
                .HasForeignKey(e => e.StudentID);
        }
    }
}