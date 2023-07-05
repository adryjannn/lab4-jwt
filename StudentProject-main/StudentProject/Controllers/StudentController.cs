using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentProject.Models;

namespace WebApplication1.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StudentController : ControllerBase
{
    private readonly StudentContext _context;

    public StudentController(StudentContext context)
    {
        _context = context;
    }

    // GET: api/Student
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Student>>> GetStudents()
    {
        if (_context.Students == null) return NotFound();
        return await _context.Students.ToListAsync();
    }

    // GET: api/Student/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Student>> GetStudent(int id)
    {
        if (_context.Students == null) return NotFound();
        var student = await _context.Students.FindAsync(id);

        if (student == null) return NotFound();

        return student;
    }

    // PUT: api/Student/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutStudent(int id, Student student)
    {
        if (id != student.Id) return BadRequest();

        _context.Entry(student).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!StudentExists(id))
                return NotFound();
            throw;
        }

        return NoContent();
    }

    /// <summary>
    ///     Wprowadź studenta
    /// </summary>
    /// <param name="student"></param>
    /// <returns>A newly created Student</returns>
    /// <remarks>
    ///     Sample request:
    ///     POST /Student
    ///     {
    ///     "id": 1,
    ///     "name": "Anna",
    ///     "surnname": "Zablotni"
    ///     }
    /// </remarks>
    // POST: api/Students
    // POST: api/Student
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost] 
    [Authorize(Policy = "Bearer")]
    public async Task<ActionResult<Student>> PostStudent(Student student)
    {
        if (_context.Students == null) return Problem("Entity set 'StudentContext.Students'  is null.");
        _context.Students.Add(student);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetStudent", new {id = student.Id}, student);
    }

    /// <summary>
    ///     Usuwanie Studentow z listy.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    // DELETE: api/Student/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteStudent(int id)
    {
        if (_context.Students == null) return NotFound();
        var student = await _context.Students.FindAsync(id);
        if (student == null) return NotFound();

        _context.Students.Remove(student);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool StudentExists(int id)
    {
        return (_context.Students?.Any(e => e.Id == id)).GetValueOrDefault();
    }
}