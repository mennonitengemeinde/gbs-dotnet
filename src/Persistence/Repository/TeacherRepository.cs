using AutoMapper.QueryableExtensions;

namespace Gbs.Infrastructure.Persistence.Repository;

public class TeacherRepository : ITeacherRepository
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public TeacherRepository(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Result<List<TeacherDto>>> GetTeachers()
    {
        var teachers = await _context.Teachers
            .ProjectTo<TeacherDto>(_mapper.ConfigurationProvider)
            .ToListAsync();
        return Result.Ok(teachers);
    }

    public async Task<Result<TeacherDto>> GetTeacherById(int id)
    {
        var teacher = await _context.Teachers
            .ProjectTo<TeacherDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(t => t.Id == id);

        return teacher == null
            ? Result.NotFound<TeacherDto>("Teacher not found")
            : Result.Ok(teacher);
    }

    public async Task<Result<TeacherDto>> AddTeacher(TeacherCreateDto teacherCreateDto)
    {
        if (await TeacherNameExists(teacherCreateDto.Name))
        {
            return Result.BadRequest<TeacherDto>("Teacher already exists");
        }

        var teacher = new Teacher
        {
            Name = teacherCreateDto.Name
        };
        _context.Teachers.Add(teacher);
        await _context.SaveChangesAsync();

        return Result.Ok(_mapper.Map<TeacherDto>(teacher));
    }

    public async Task<Result<TeacherDto>> UpdateTeacher(int teacherId, TeacherCreateDto teacherDto)
    {
        if (await TeacherNameExists(teacherDto.Name, teacherId))
            return Result.BadRequest<TeacherDto>("Teacher already exists");

        var teacher = await _context.Teachers
            .FirstOrDefaultAsync(t => t.Id == teacherId);

        if (teacher == null)
            return Result.NotFound<TeacherDto>("Teacher not found");

        teacher.Name = teacherDto.Name;
        await _context.SaveChangesAsync();

        return Result.Ok(_mapper.Map<TeacherDto>(teacher));
    }

    private async Task<bool> TeacherNameExists(string name, int? id = null)
    {
        return id != null
            ? await _context.Teachers.AnyAsync(t => t.Name == name && t.Id != id)
            : await _context.Teachers.AnyAsync(t => t.Name == name);
    }
}