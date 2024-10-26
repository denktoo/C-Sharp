using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using KenyattaUniversity.Models;
using Microsoft.AspNetCore.Identity;

namespace KenyattaUniversity.Data
{
    public class KUContext : IdentityDbContext<ApplicationUser>
    {
        public KUContext(DbContextOptions<KUContext> options)
            : base(options)
        {
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Customize table names if necessary
            modelBuilder.Entity<ApplicationUser>(b =>
            {
                b.ToTable("AspNetUsers"); // Ensure this matches your MySQL table name
            });

            modelBuilder.Entity<IdentityRole>(b =>
            {
                b.ToTable("AspNetRoles");
            });

            modelBuilder.Entity<IdentityUserRole<string>>(b =>
            {
                b.ToTable("AspNetUserRoles");
            });

            modelBuilder.Entity<IdentityUserClaim<string>>(b =>
            {
                b.ToTable("AspNetUserClaims");
            });

            modelBuilder.Entity<IdentityUserLogin<string>>(b =>
            {
                b.ToTable("AspNetUserLogins");
            });

            modelBuilder.Entity<IdentityRoleClaim<string>>(b =>
            {
                b.ToTable("AspNetRoleClaims");
            });
        }
    }
}