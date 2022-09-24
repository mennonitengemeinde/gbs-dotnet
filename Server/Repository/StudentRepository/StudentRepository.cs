namespace gbs.Server.Repository.StudentRepository;

public class StudentRepository : IStudentRepository
{
    private readonly DataContext _context;
    private readonly IAuthRepository _authRepo;
    private readonly IUserRepository _userRepo;

    public StudentRepository(DataContext context, IAuthRepository authRepo, IUserRepository userRepo)
    {
        _context = context;
        _authRepo = authRepo;
        _userRepo = userRepo;
    }

    public async Task<ServiceResponse<List<Student>>> GetStudents()
    {
        var role = _authRepo.GetUserRole();
        if (role is Roles.SuperAdmin or Roles.Admin)
        {
            return ServiceResponse<List<Student>>.Ok(await _context.Students.ToListAsync());
        }

        var user = await _userRepo.GetUserById(_authRepo.GetUserId());
        var students = await _context.Students
            .Where(s => s.ChurchId == user.Data.ChurchId)
            .ToListAsync();
        return ServiceResponse<List<Student>>.Ok(students);
    }

    public async Task<ServiceResponse<Student>> GetStudentById(int id)
    {
        var role = _authRepo.GetUserRole();
        if (role is Roles.SuperAdmin or Roles.Admin)
        {
            var student = await _context.Students.FirstOrDefaultAsync(s => s.Id == id);
            return student == null
                ? ServiceResponse<Student>.BadRequest("Student not found")
                : ServiceResponse<Student>.Ok(student);
        }
        else
        {
            var user = await _userRepo.GetUserById(_authRepo.GetUserId());
            var student = await _context.Students
                .FirstOrDefaultAsync(s => s.Id == id && s.ChurchId == user.Data.ChurchId);
            return student == null
                ? ServiceResponse<Student>.BadRequest("Student not found")
                : ServiceResponse<Student>.Ok(student);
        }
    }

    public async Task<ServiceResponse<List<Student>>> AddStudent(StudentCreateDto studentDto)
    {
        var student = new Student
        {
            Name = studentDto.Name,
            Address = studentDto.Address,
            City = studentDto.City,
            State = studentDto.State,
            Country = studentDto.Country,
            PostalCode = studentDto.PostalCode,
            DateOfBirth = studentDto.DateOfBirth,
            MaritalStatus = studentDto.MaritalStatus,
            Email = studentDto.Email,
            Phone = studentDto.Phone,
            ChurchId = studentDto.ChurchId
        };
        _context.Students.Add(student);
        await _context.SaveChangesAsync();
        return await GetStudents();
    }

    public async Task<ServiceResponse<List<Student>>> UpdateStudent(int studentId, StudentCreateDto studentDto)
    {
        var result = await GetStudentById(studentId);
        if (!result.Success)
            return ServiceResponse<List<Student>>.BadRequest("Student not found");
        var student = result.Data;

        student.Name = studentDto.Name;
        student.Address = studentDto.Address;
        student.City = studentDto.City;
        student.State = studentDto.State;
        student.Country = studentDto.Country;
        student.PostalCode = studentDto.PostalCode;
        student.DateOfBirth = studentDto.DateOfBirth;
        student.MaritalStatus = studentDto.MaritalStatus;
        student.Email = studentDto.Email;
        student.Phone = studentDto.Phone;
        student.ChurchId = studentDto.ChurchId;

        _context.Students.Update(student);
        await _context.SaveChangesAsync();
        return await GetStudents();
    }
}