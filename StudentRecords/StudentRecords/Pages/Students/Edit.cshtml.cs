using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StudentRecords.Data;
using StudentRecords.Models;

namespace StudentRecords.Pages.Students
{
    public class EditModel : PageModel
    {
        private readonly StudentRecords.Data.StudentRecordsContext _context;

        public EditModel(StudentRecords.Data.StudentRecordsContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Student Student { get; set; } = default!;
        public string? SearchString { get; private set; }

        //public async Task<IActionResult> OnGetAsync(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var student =  await _context.Student.FirstOrDefaultAsync(m => m.Id == id);
        //    if (student == null)
        //    {
        //        return NotFound();
        //    }
        //    Student = student;
        //    return Page();
        //}
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

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Student).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentExists(Student.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool StudentExists(int id)
        {
            return _context.Student.Any(e => e.Id == id);
        }
    }
}
