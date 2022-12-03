using Gbs.Domain.Common.Wrapper;
using Gbs.Shared.Teachers;

namespace Gbs.Application.Teachers;

public class TeacherCommands : ITeacherCommands
{
    private readonly IGbsDbContext _context;
    private readonly IMapper _mapper;

    public TeacherCommands(IGbsDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public async Task<Result<TeacherDto>> Add(TeacherCreateDto request)
    {
        if (await NameExists(request.Name))
        {
            return Result.BadRequest<TeacherDto>("Teacher already exists");
        }

        var teacher = new Teacher
        {
            Name = request.Name
        };
        _context.Teachers.Add(teacher);
        await _context.SaveChangesAsync();

        return Result.Ok(_mapper.Map<TeacherDto>(teacher));
    }

    public async Task<Result<TeacherDto>> Update(int id, TeacherCreateDto request)
    {
        if (await NameExists(request.Name, id))
            return Result.BadRequest<TeacherDto>("Teacher already exists");

        var teacher = await _context.Teachers
            .FirstOrDefaultAsync(t => t.Id == id);

        if (teacher == null)
            return Result.NotFound<TeacherDto>("Teacher not found");

        teacher.Name = request.Name;
        await _context.SaveChangesAsync();

        return Result.Ok(_mapper.Map<TeacherDto>(teacher));
    }

    public async Task<Result<bool>> Delete(int id)
    {
        var dbResult = await _context.Teachers.FindAsync(id);
        if (dbResult == null)
            return Result.NotFound<bool>("Lesson not found");

        _context.Teachers.Remove(dbResult);
        await _context.SaveChangesAsync();
        return Result.Ok(true);
    }
    
    private async Task<bool> NameExists(string name, int? id = null)
    {
        return id != null
            ? await _context.Teachers.AnyAsync(t => t.Name == name && t.Id != id)
            : await _context.Teachers.AnyAsync(t => t.Name == name);
    }
}