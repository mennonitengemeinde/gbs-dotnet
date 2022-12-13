using Gbs.Application.Features.Students.Interfaces;
using Gbs.Shared.Students;

namespace Gbs.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Policy = Policies.RequireAdminsAndTeachers)]
public class StudentsController : ControllerBase
{
    private readonly IStudentQueries _studentQueries;
    private readonly IStudentCommands _studentCommands;

    public StudentsController(
        IStudentQueries studentQueries,
        IStudentCommands studentCommands)
    {
        _studentQueries = studentQueries;
        _studentCommands = studentCommands;
    }

    [HttpGet]
    public async Task<ActionResult<Result<List<StudentResponse>>>> GetAllStudents()
    {
        var result = await _studentQueries.GetAll();
        return result.ToActionResult();
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Result<StudentResponse>>> GetStudentById(int id)
    {
        var result = await _studentQueries.GetById(id);
        return result.ToActionResult();
    }

    [HttpPost]
    public async Task<ActionResult<Result<StudentResponse>>> AddStudent(CreateStudentRequest student)
    {
        var result = await _studentCommands.Add(student);
        return result.ToActionResult();
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<Result<StudentResponse>>> UpdateStudent(int id, CreateStudentRequest student)
    {
        var result = await _studentCommands.Update(id, student);
        return result.ToActionResult();
    }
}