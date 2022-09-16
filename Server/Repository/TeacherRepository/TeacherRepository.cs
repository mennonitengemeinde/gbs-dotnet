namespace gbs.Server.Repository.TeacherRepository;

public class TeacherRepository : ITeacherRepository
{
    private readonly DataContext _context;

    public TeacherRepository(DataContext context)
    {
        _context = context;
    }

    public async Task<ServiceResponse<List<Teacher>>> GetTeachers()
    {
        var teachers = await _context.Teachers.ToListAsync();
        return new ServiceResponse<List<Teacher>> { Data = teachers };
    }

    public async Task<ServiceResponse<Teacher>> GetTeacherById(int id)
    {
        var response = new ServiceResponse<Teacher>();
        var teacher = await _context.Teachers.FirstOrDefaultAsync(t => t.Id == id);
        if (teacher == null)
        {
            response.Success = false;
            response.Message = "Teacher not found.";
            return response;
        }
        response.Data = teacher;
        return response;
    }

    public async Task<ServiceResponse<Teacher>> AddTeacher(TeacherCreateDto teacherCreateDto)
    {
        if (await _context.Teachers.AnyAsync(t => t.Name == teacherCreateDto.Name))
        {
            return new ServiceResponse<Teacher> { Success = false, Message = "Teacher already exists" };
        }

        var teacher = new Teacher
        {
            Name = teacherCreateDto.Name
        };
        _context.Teachers.Add(teacher);
        await _context.SaveChangesAsync();
        return new ServiceResponse<Teacher> { Data = teacher };
    }

    public async Task<ServiceResponse<Teacher>> UpdateTeacher(int teacherId, TeacherCreateDto teacherDto)
    {
        var response = new ServiceResponse<Teacher>();
        var teacher = await _context.Teachers.FirstOrDefaultAsync(t => t.Id == teacherId);
        if (teacher == null)
        {
            response.Success = false;
            response.Message = "Teacher not found.";
            return response;
        }
        teacher.Name = teacherDto.Name;
        await _context.SaveChangesAsync();
        response.Data = teacher;
        return response;
    }
}