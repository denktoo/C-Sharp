using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JkuatUniversity.Data;
using JkuatUniversity.Models;

namespace JkuatUniversity.Controllers
{
    public class StudentsController : Controller
    {
        private readonly JkuatContext _context;

        public StudentsController(JkuatContext context)
        {
            _context = context;
        }

        // GET: Students
        public async Task<IActionResult> Index()
        {
            return View(await _context.Students.ToListAsync());
        }

        // GET: Students/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Students/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,FirstMidName,LastName,EnrollmentDate")] Student student)
        {
            if (ModelState.IsValid)
            {
                _context.Add(student);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }

        // Additional actions like Edit, Delete...
    }
}