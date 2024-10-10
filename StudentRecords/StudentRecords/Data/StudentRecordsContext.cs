using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StudentRecords.Models;

namespace StudentRecords.Data
{
    public class StudentRecordsContext : DbContext
    {
        public StudentRecordsContext (DbContextOptions<StudentRecordsContext> options)
            : base(options)
        {
        }

        public DbSet<StudentRecords.Models.Student> Student { get; set; } = default!;
    }
}
