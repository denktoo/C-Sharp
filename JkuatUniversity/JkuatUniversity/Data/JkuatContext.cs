using Microsoft.EntityFrameworkCore;
using JkuatUniversity.Models;

namespace JkuatUniversity.Data
{
    public class JkuatContext : DbContext
    {
        public JkuatContext(DbContextOptions<JkuatContext> options)
            : base(options)
        {
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
    }
}