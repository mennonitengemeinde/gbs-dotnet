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
        var teacher = await _context.Teachers.FirstOrDefaultAsync(t => t.Id == id);
        return teacher == null ? TeacherNotFound() : new ServiceResponse<Teacher> { Data = teacher };
    }

    public async Task<ServiceResponse<Teacher>> AddTeacher(TeacherCreateDto teacherCreateDto)
    {
        if (await TeacherNameExists(teacherCreateDto.Name))
        {
            return ServiceResponse<Teacher>.BadRequest("Teacher already exists");
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
        if (await TeacherNameExists(teacherDto.Name, teacherId))
        {
            return ServiceResponse<Teacher>.BadRequest("Teacher already exists");
        }

        var teacher = await _context.Teachers.FirstOrDefaultAsync(t => t.Id == teacherId);
        if (teacher == null)
            return TeacherNotFound();

        teacher.Name = teacherDto.Name;
        await _context.SaveChangesAsync();
        return new ServiceResponse<Teacher> { Data = teacher };
    }

    private async Task<bool> TeacherNameExists(string name, int? id = null)
    {
        return id != null
            ? await _context.Teachers.AnyAsync(t => t.Name == name && t.Id != id)
            : await _context.Teachers.AnyAsync(t => t.Name == name);
    }

    private static ServiceResponse<Teacher> TeacherNotFound()
    {
        return ServiceResponse<Teacher>.BadRequest("Teacher already exists");
    }
}