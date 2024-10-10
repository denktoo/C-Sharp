using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using StudentRecords.Data;

namespace StudentRecords.Models;

public static class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new StudentRecordsContext(
            serviceProvider.GetRequiredService<
                DbContextOptions<StudentRecordsContext>>()))
        {
            if (context == null || context.Student == null)
            {
                throw new ArgumentNullException("Null RazorPagesMovieContext");
            }

            // Look for any movies.
            if (context.Student.Any())
            {
                return;   // DB has been seeded
            }
            context.Student.AddRange(
                new Student
                {
                    StudentRegNumber = "SCM211-0749/2019",
                    Name = "Denis Kiprotich",
                    Gender = "Male",
                    Email = "denis.kiprotich@students.jkuat.ac.ke",
                    DateOfBirth = DateTime.Parse("2000-11-24"),
                    DateOfAdmission = DateTime.Parse("2019-9-12"),
                    Status = "Graduated",
                    PhoneNumber = "+254712456890"
                }
             );
            context.SaveChanges();
        }
    }
}
