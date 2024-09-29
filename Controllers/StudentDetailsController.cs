using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentDetails.Data;
using StudentDetails.Models;
namespace StudentDetails.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class StudentDetailsController : ControllerBase
    {
        private readonly StudentContext _appDbContext;
       public StudentDetailsController(StudentContext appDbContext) {

            _appDbContext = appDbContext;
        }


        /*    [HttpPost]
            public async Task<IActionResult> AddStudent(Student  student) {
                _appDbContext.Students.Add(student);
                await _appDbContext.SaveChangesAsync();
                return Ok(student);
            }*/

        [HttpPost]
        public async Task<IActionResult> AddStudent([FromForm] Student student)
        {
            if (ModelState.IsValid)
            {
                _appDbContext.Students.Add(student);
                await _appDbContext.SaveChangesAsync();
                return Ok(student);
            }

            return BadRequest("Invalid student data.");
        }

        [HttpGet]
        public async Task<IActionResult> GetAllStudents() {
            var students = await _appDbContext.Students.ToListAsync();
            return Ok(students);
        }


        [HttpGet("id")]
        
        public async Task<IActionResult> GetStudentById(int id)
        {
            var student = await _appDbContext.Students.FindAsync(id);
            return student == null ? NotFound() : Ok(student);
        }


        /*    [HttpPut("{id}")]
            public async Task<IActionResult> UpdateStudent([FromForm]Student student)
            {
                //if (id != student.Id) return BadRequest();

                _appDbContext.Entry(student).State = EntityState.Modified;
                await _appDbContext.SaveChangesAsync();
                return Ok();
            }

            [HttpDelete("{id}")]
            public async Task<IActionResult> Delete([FromForm] int id)
            {
                var student = await _appDbContext.Students.FindAsync(id);
                if (student == null) return NotFound();

                _appDbContext.Students.Remove(student);
                await _appDbContext.SaveChangesAsync();
                return Ok();
            }
    */
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStudent(int id, [FromForm] Student student)
        {
            if (id != student.Id) return BadRequest("Student ID mismatch.");

            // Optional: Fetch the existing student to verify
            var existingStudent = await _appDbContext.Students.FindAsync(id);
            if (existingStudent == null) return NotFound();

            // Update the existing student details
            existingStudent.Name = student.Name; // Update properties as necessary
            existingStudent.Age = student.Age;
            existingStudent.Course = student.Course;

            _appDbContext.Entry(existingStudent).State = EntityState.Modified;
            await _appDbContext.SaveChangesAsync();
            return Ok(existingStudent);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var student = await _appDbContext.Students.FindAsync(id);
            if (student == null) return NotFound();

            _appDbContext.Students.Remove(student);
            await _appDbContext.SaveChangesAsync();
            return Ok();
        }
    }
}
