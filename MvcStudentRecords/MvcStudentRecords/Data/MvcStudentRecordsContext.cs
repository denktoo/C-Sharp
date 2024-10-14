using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MvcStudentRecords.Models;

namespace MvcStudentRecords.Data
{
    public class MvcStudentRecordsContext : DbContext
    {
        public MvcStudentRecordsContext (DbContextOptions<MvcStudentRecordsContext> options)
            : base(options)
        {
        }

        public DbSet<MvcStudentRecords.Models.Student> Student { get; set; } = default!;
    }
}
