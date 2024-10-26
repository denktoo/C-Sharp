using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KenyattaUniversity.Data;
using KenyattaUniversity.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace KenyattaUniversity.Controllers
{
    public class EnrollmentsController : Controller
    {
        private readonly KUContext _context;

        public EnrollmentsController(KUContext context)
        {
            _context = context;
        }

        // GET: Enrollments
        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchString)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.StudentSortParm = string.IsNullOrEmpty(sortOrder) ? "student_desc" : "";
            ViewBag.CourseSortParm = sortOrder == "Course" ? "course_desc" : "Course";

            if (searchString != null)
            {
                currentFilter = searchString; // Use current filter if new search
            }
            else
            {
                searchString = currentFilter; // Retain the previous filter
            }

            ViewBag.CurrentFilter = currentFilter; // Set current filter for view

            var enrollments = from e in _context.Enrollments.Include(e => e.Student).Include(e => e.Course)
                              select e;

            // Filter based on search string (e.g., by student or course name)
            if (!string.IsNullOrEmpty(searchString))
            {
                enrollments = enrollments.Where(e => e.Student.Fname.Contains(searchString) || e.Course.Title.Contains(searchString));
            }

            // Sorting logic
            switch (sortOrder)
            {
                case "student_desc":
                    enrollments = enrollments.OrderByDescending(e => e.Student.Fname);
                    break;
                case "Course":
                    enrollments = enrollments.OrderBy(e => e.Course.Title);
                    break;
                case "course_desc":
                    enrollments = enrollments.OrderByDescending(e => e.Course.Title);
                    break;
                default:  // Default sorting by student name ascending 
                    enrollments = enrollments.OrderBy(e => e.Student.Fname);
                    break;
            }

            return View(await enrollments.ToListAsync()); // Return the list to the view without pagination
        }

        // GET: Enrollments/Create
        public IActionResult Create()
        {
            ViewData["CourseID"] = new SelectList(_context.Courses, "CourseID", "Title");
            ViewData["StudentID"] = new SelectList(_context.Students, "StudentID", "Fname");
            return View();
        }

        // POST: Enrollments/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EnrollmentID,CourseID,StudentID,Grade")] Enrollment enrollment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(enrollment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["CourseID"] = new SelectList(_context.Courses, "CourseID", "Title", enrollment.CourseID);
            ViewData["StudentID"] = new SelectList(_context.Students, "StudentID", "Fname", enrollment.StudentID);

            return View(enrollment);
        }

        // GET: Enrollments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enrollment = await _context.Enrollments.Include(e => e.Student).Include(e => e.Course).FirstOrDefaultAsync(m => m.EnrollmentID == id);

            if (enrollment == null)
            {
                return NotFound();
            }

            return View(enrollment);
        }

        // GET: Enrollments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enrollment = await _context.Enrollments.FindAsync(id);

            if (enrollment == null)
            {
                return NotFound();
            }

            ViewData["CourseID"] = new SelectList(_context.Courses, "CourseID", "Title", enrollment.CourseID);
            ViewData["StudentID"] = new SelectList(_context.Students, "StudentID", "Fname", enrollment.StudentID);

            return View(enrollment);
        }

        // POST: Enrollments/Edit/5 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EnrollmentID,CourseID,StudentID,Grade")] Enrollment enrollment)
        {
            if (id != enrollment.EnrollmentID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(enrollment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EnrollmentExists(enrollment.EnrollmentID))
                    {
                        return NotFound();
                    }
                    else { throw; }
                }

                return RedirectToAction(nameof(Index));
            }

            ViewData["CourseID"] = new SelectList(_context.Courses, "CourseID", "Title", enrollment.CourseID);
            ViewData["StudentID"] = new SelectList(_context.Students, "StudentID", "Fname", enrollment.StudentID);

            return View(enrollment);
        }

        // GET: Enrollments/Delete/5  
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enrollment = await _context.Enrollments.Include(e => e.Student).Include(e => e.Course).FirstOrDefaultAsync(m => m.EnrollmentID == id);

            if (enrollment == null)
            {
                return NotFound();
            }

            return View(enrollment);
        }

        // POST: Enrollments/Delete/5  
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var enrollment = await _context.Enrollments.FindAsync(id);
            if (enrollment != null)
            {
                _context.Enrollments.Remove(enrollment);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EnrollmentExists(int id)
        {
            return _context.Enrollments.Any(e => e.EnrollmentID == id);
        }
    }
}