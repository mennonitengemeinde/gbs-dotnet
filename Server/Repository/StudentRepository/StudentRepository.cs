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

    public async Task<ServiceResponse<List<StudentDto>>> GetStudents()
    {
        var role = _authRepo.GetUserRole();
        if (role is Roles.SuperAdmin or Roles.Admin)
        {
            var result = await GetStudentDtoQuery().ToListAsync();
            return ServiceResponse<List<StudentDto>>.Ok(result);
        }

        var user = await _userRepo.GetUserById(_authRepo.GetUserId());
        var students = await GetStudentDtoQuery()
            .Where(s => s.ChurchId == user.Data.ChurchId)
            .OrderBy(s => s.Name)
            .ToListAsync();
        return ServiceResponse<List<StudentDto>>.Ok(students);
    }

    public async Task<ServiceResponse<StudentDto>> GetStudentById(int id)
    {
        var role = _authRepo.GetUserRole();
        if (role is Roles.SuperAdmin or Roles.Admin)
        {
            var student = await GetStudentDtoQuery().FirstOrDefaultAsync(s => s.Id == id);
            return student == null
                ? ServiceResponse<StudentDto>.BadRequest("Student not found")
                : ServiceResponse<StudentDto>.Ok(student);
        }
        else
        {
            var user = await _userRepo.GetUserById(_authRepo.GetUserId());
            var student = await GetStudentDtoQuery()
                .FirstOrDefaultAsync(s => s.Id == id && s.ChurchId == user.Data.ChurchId);
            return student == null
                ? ServiceResponse<StudentDto>.BadRequest("Student not found")
                : ServiceResponse<StudentDto>.Ok(student);
        }
    }

    public async Task<ServiceResponse<List<StudentDto>>> AddStudent(IStudentCreateDto studentDto)
    {
        if (studentDto.DateOfBirth == null)
            return ServiceResponse<List<StudentDto>>.BadRequest("Date of birth is required");
        
        var student = new Student
        {
            Name = studentDto.Name,
            Address = studentDto.Address,
            City = studentDto.City,
            State = studentDto.State,
            Country = studentDto.Country,
            PostalCode = studentDto.PostalCode,
            DateOfBirth = studentDto.DateOfBirth.Value,
            MaritalStatus = studentDto.MaritalStatus,
            Email = studentDto.Email,
            Phone = studentDto.Phone
        };
        
        if (_authRepo.UserIsAdmin())
        {
            if (studentDto.ChurchId == null)
            {
                return ServiceResponse<List<StudentDto>>.BadRequest("Church is required");
            }
            student.ChurchId = studentDto.ChurchId.Value;
        }
        else
        {
            var user = await _userRepo.GetUserById(_authRepo.GetUserId());
            if (user.Data.ChurchId == null)
            {
                return ServiceResponse<List<StudentDto>>.BadRequest("You are not assigned to a church");
            }
            student.ChurchId = user.Data.ChurchId.Value;
        }
        
        _context.Students.Add(student);
        await _context.SaveChangesAsync();
        return await GetStudents();
    }

    public async Task<ServiceResponse<List<StudentDto>>> UpdateStudent(int studentId, IStudentCreateDto studentDto)
    {
        if (studentDto.DateOfBirth == null)
            return ServiceResponse<List<StudentDto>>.BadRequest("Date of birth is required");

        var student = await _context.Students.FirstOrDefaultAsync(s => s.Id == studentId);
        if (student == null)
            return ServiceResponse<List<StudentDto>>.BadRequest("Student not found");

        student.Name = studentDto.Name;
        student.Address = studentDto.Address;
        student.City = studentDto.City;
        student.State = studentDto.State;
        student.Country = studentDto.Country;
        student.PostalCode = studentDto.PostalCode;
        student.DateOfBirth = studentDto.DateOfBirth.Value;
        student.MaritalStatus = studentDto.MaritalStatus;
        student.Email = studentDto.Email;
        student.Phone = studentDto.Phone;
        
        if (_authRepo.UserIsAdmin())
        {
            if (studentDto.ChurchId != null)
            {
                student.ChurchId = studentDto.ChurchId.Value;
            }
        }

        _context.Students.Update(student);
        await _context.SaveChangesAsync();
        return await GetStudents();
    }

    private IQueryable<StudentDto> GetStudentDtoQuery()
    {
        return _context.Students.Select(s => new StudentDto
        {
            Id = s.Id,
            Name = s.Name,
            Address = s.Address,
            City = s.City,
            State = s.State,
            Country = s.Country,
            Email = s.Email,
            Phone = s.Phone,
            MaritalStatus = s.MaritalStatus,
            PostalCode = s.PostalCode,
            DateOfBirth = s.DateOfBirth,
            ChurchId = s.ChurchId,
            ChurchName = s.Church.Name
        });
    }
}