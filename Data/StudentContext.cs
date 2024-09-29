using Microsoft.EntityFrameworkCore;
using StudentDetails.Models;

namespace StudentDetails.Data
{
    public class StudentContext : DbContext
    {
        public StudentContext(DbContextOptions<StudentContext> options) : base(options) { }
   public DbSet<Student> Students { get; set; }

    }
}
    
