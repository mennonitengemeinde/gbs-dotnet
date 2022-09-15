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
}