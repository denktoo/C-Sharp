﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
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
        public DbSet<Student> Students { get; set; } // DbSet for Student entity
        public DbSet<Admin> Admins { get; set; } // DbSet for Admin Entity

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Admin>()
                .HasKey(a => a.EmpNo); // Explicitly configure RegNo as the primary key

            modelBuilder.Entity<Student>()
                .HasKey(s => s.RegNo); // Explicitly configure RegNo as the primary key

            modelBuilder.Entity<Course>()
                .HasKey(c => c.CourseID); // Explicitly configure CourseID as the primary key

            modelBuilder.Entity<Enrollment>()
                .HasKey(e => e.EnrollmentID); // Explicitly configure EnrollmentID as the primary key

            modelBuilder.Entity<Enrollment>()
                .HasOne(e => e.Course)
                .WithMany(c => c.Enrollments)
                .HasForeignKey(e => e.CourseID);

            modelBuilder.Entity<Enrollment>()
                .HasOne(e => e.Student)
                .WithMany(s => s.Enrollments)
                .HasForeignKey(e => e.RegNo);

            // Explicitly declare Role shadow property and set Role for the Student
            //modelBuilder.Entity<Student>()
            //    .Property<string>("Role")
            //    .HasDefaultValue("Student");

            // Seed Data and Explicitly declare Role shadow property and set Role for the Admin
            modelBuilder.Entity<Admin>().Property<string>("Role");
            modelBuilder.Entity<Admin>().HasData(
                new { EmpNo = "ADM0001", Username = "admin", Email = "admin@gmail.com", Password = "admin#", Role = "Admin" }
            );
        }
    }
}