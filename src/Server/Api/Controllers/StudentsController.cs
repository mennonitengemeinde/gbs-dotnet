using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace gbs.Server.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Policy = Policies.RequireAdminsAndTeachers)]
public class StudentsController : ControllerBase
{
    private readonly IStudentRepository _studentRepo;

    public StudentsController(IStudentRepository studentRepo)
    {
        _studentRepo = studentRepo;
    }

    [HttpGet]
    public async Task<ActionResult<Result<List<StudentDto>>>> GetAllStudents()
    {
        var students = await _studentRepo.GetStudents();
        return Ok(students);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Result<StudentDto>>> GetStudentById(int id)
    {
        var student = await _studentRepo.GetStudentById(id);
        return student.Success ? Ok(student) : NotFound(student);
    }

    [HttpPost]
    public async Task<ActionResult<Result<List<StudentDto>>>> AddStudent(StudentCreateDto student)
    {
        var newStudent = await _studentRepo.AddStudent(student);
        return newStudent.Success ? Ok(newStudent) : BadRequest(newStudent);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<Result<List<StudentDto>>>> UpdateStudent(int id, StudentCreateDto student)
    {
        var updatedStudent = await _studentRepo.UpdateStudent(id, student);
        return updatedStudent.Success ? Ok(updatedStudent) : BadRequest(updatedStudent);
    }
}