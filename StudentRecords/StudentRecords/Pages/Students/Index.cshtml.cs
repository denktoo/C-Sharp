using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using StudentRecords.Data;
using StudentRecords.Models;

namespace StudentRecords.Pages.Students
{
    public class IndexModel : PageModel
    {
        private readonly StudentRecords.Data.StudentRecordsContext _context;

        public IndexModel(StudentRecords.Data.StudentRecordsContext context)
        {
            _context = context;
        }

        public IList<Student> Student { get;set; } = default!;

        [BindProperty(SupportsGet = true)]
        public string? SearchString { get; set; }

        public async Task OnGetAsync()
        {
            var students = from m in _context.Student
                         select m;
            if (!string.IsNullOrEmpty(SearchString))
            {
                students = students.Where(s => s.StudentRegNumber.Contains(SearchString));
            }

            Student = await students.ToListAsync();
        }
    }
}
