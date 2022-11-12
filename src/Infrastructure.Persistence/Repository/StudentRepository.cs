using AutoMapper.QueryableExtensions;

namespace Gbs.Infrastructure.Persistence.Repository;

public class StudentRepository : IStudentRepository
{
    private readonly DataContext _context;
    private readonly IAuthRepository _authRepo;
    private readonly IUserRepository _userRepo;
    private readonly IAuthenticatedUserService _authUserService;
    private readonly IMapper _mapper;

    public StudentRepository(
        DataContext context, 
        IAuthRepository authRepo, 
        IUserRepository userRepo,
        IAuthenticatedUserService authUserService,
        IMapper mapper
        )
    {
        _context = context;
        _authRepo = authRepo;
        _userRepo = userRepo;
        _authUserService = authUserService;
        _mapper = mapper;
    }

    public async Task<Result<List<StudentDto>>> GetStudents()
    {
        var roles = _authUserService.GetUserRoles();
        if (roles.Contains(Roles.Admin) || roles.Contains(Roles.SuperAdmin))
        {
            var result = await _context.Students
                .ProjectTo<StudentDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
            return Result.Ok(result);
        }

        var user = await _userRepo.GetUserById(_authUserService.GetUserId());
        var students = await _context.Students
            .Where(s => s.ChurchId == user.Data.ChurchId)
            .OrderBy(s => s.Name)
            .ProjectTo<StudentDto>(_mapper.ConfigurationProvider)
            .ToListAsync();
        
        return Result.Ok(students);
    }

    public async Task<Result<StudentDto>> GetStudentById(int id)
    {
        var roles = _authUserService.GetUserRoles();
        if (roles.Contains(Roles.Admin) || roles.Contains(Roles.SuperAdmin))
        {
            var student = await _context.Students
                .ProjectTo<StudentDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(s => s.Id == id);
            
            return student == null
                ? Result.NotFound<StudentDto>("Student not found")
                : Result.Ok(student);
        }
        else
        {
            var user = await _userRepo.GetUserById(_authUserService.GetUserId());
            var student = await _context.Students
                .ProjectTo<StudentDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(s => s.Id == id && s.ChurchId == user.Data.ChurchId);
            return student == null
                ? Result.NotFound<StudentDto>("Student not found")
                : Result.Ok(student);
        }
    }

    public async Task<Result<List<StudentDto>>> AddStudent(StudentCreateDto studentDto)
    {
        var student = _mapper.Map<Student>(studentDto);
        
        if (!_authUserService.UserIsAdmin())
        {
            var user = await _userRepo.GetUserById(_authUserService.GetUserId());
            if (user.Data.ChurchId == null)
                return Result.BadRequest<List<StudentDto>>("You are not assigned to a church");
            
            student.ChurchId = user.Data.ChurchId.Value;
        }
        
        _context.Students.Add(student);
        await _context.SaveChangesAsync();
        return await GetStudents();
    }

    public async Task<Result<List<StudentDto>>> UpdateStudent(int studentId, StudentCreateDto studentDto)
    {
        var student = await _context.Students.FirstOrDefaultAsync(s => s.Id == studentId);
        if (student == null)
            return Result.NotFound<List<StudentDto>>("Student not found");

        student.Name = studentDto.Name;
        student.Address = studentDto.Address;
        student.City = studentDto.City;
        student.State = studentDto.State;
        student.Country = studentDto.Country;
        student.PostalCode = studentDto.PostalCode;
        if (studentDto.DateOfBirth != null) student.DateOfBirth = studentDto.DateOfBirth.Value;
        student.MaritalStatus = studentDto.MaritalStatus;
        student.Email = studentDto.Email;
        student.Phone = studentDto.Phone;
        
        if (_authUserService.UserIsAdmin())
        {
            student.ChurchId = studentDto.ChurchId;
        }

        _context.Students.Update(student);
        await _context.SaveChangesAsync();
        return await GetStudents();
    }
}