using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCCORE_AJAX.Models;

namespace MVCCORE_AJAX.Controllers
{
    public class StudentController : Controller
{
    private readonly StudentContext _context;

    public StudentController(StudentContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    [HttpGet]
public async Task<JsonResult> GetStudents()
{
    var students = await _context.Students.ToListAsync();
    return Json(new { Data = students.Select(s => new { s.Id, s.Name, s.Address }) });
}


    [HttpPost]
    public async Task<JsonResult> CreateStudent([FromBody] Student student)
    {
        if (student == null || string.IsNullOrWhiteSpace(student.Name) || string.IsNullOrWhiteSpace(student.Address))
            return Json(new { Message = "INVALID DATA" });

        _context.Students.Add(student);
        await _context.SaveChangesAsync();
        return Json(new { Message = "SUCCESS", Student = new { student.Id, student.Name, student.Address } });
    }

    [HttpPost]
    public async Task<JsonResult> EditStudent([FromBody] Student student)
    {
        var existing = await _context.Students.FindAsync(student.Id);
        if (existing == null) return Json(new { Message = "STUDENT NOT FOUND" });

        existing.Name = student.Name;
        existing.Address = student.Address;

        await _context.SaveChangesAsync();
        return Json(new { Message = "UPDATED", Student = new { existing.Id, existing.Name, existing.Address } });
    }

    [HttpPost]
    public async Task<JsonResult> DeleteStudent([FromBody] int id)
    {
        var student = await _context.Students.FindAsync(id);
        if (student == null) return Json(new { Message = "STUDENT NOT FOUND" });

        _context.Students.Remove(student);
        await _context.SaveChangesAsync();
        return Json(new { Message = "DELETED", StudentId = id });
    }
}
 
}
