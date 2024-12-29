using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using KenyattaUniversity.Models;

namespace KenyattaUniversity.Data
{
    public class KUContext : DbContext
    {
        public KUContext(DbContextOptions<KUContext> options)
            : base(options)
        {
        }

        public DbSet<Course> Courses { get; set; } // DbSet for Course entity
        public DbSet<Enrollment> Enrollments { get; set; } // DbSet for Enrollment entity
        public DbSet<User> Users { get; set; } // DbSet for User entity

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .HasKey(s => s.SchoolID); // Explicitly configure SchoolID as the primary key

            modelBuilder.Entity<Course>()
                .HasKey(c => c.CourseID); // Explicitly configure CourseID as the primary key

            modelBuilder.Entity<Enrollment>()
                .HasKey(e => e.EnrollmentID); // Explicitly configure EnrollmentID as the primary key

            modelBuilder.Entity<Enrollment>()
                .HasOne(e => e.Course)
                .WithMany(c => c.Enrollments)
                .HasForeignKey(e => e.CourseID);

            modelBuilder.Entity<Enrollment>()
                .HasOne(e => e.User)
                .WithMany(s => s.Enrollments)
                .HasForeignKey(e => e.SchoolID);

            // Seed Data and Explicitly declare Role shadow property and set Role for the Admin
            modelBuilder.Entity<User>().Property<string>("Role");
            modelBuilder.Entity<User>().HasData(
                new { SchoolID = "ADM0001", Username = "admin", Email = "admin@gmail.com", Password = "admin#", Role = "Admin" }
            );
        }
    }
}