using Gbs.Application.Features.Teachers.Interfaces;

namespace Gbs.Application.Features.Teachers;

public class TeacherCommands : ITeacherCommands
{
    private readonly IGbsDbContext _context;
    private readonly IMapper _mapper;
    private readonly IValidator<Teacher> _validator;

    public TeacherCommands(IGbsDbContext context, IMapper mapper, IValidator<Teacher> validator)
    {
        _context = context;
        _mapper = mapper;
        _validator = validator;
    }

    public async Task<Result<TeacherResponse>> Add(CreateTeacherRequest request)
    {
        var teacher = new Teacher { Name = request.Name.Trim() };
        
        var valResult = await _validator.ValidateAsync(teacher);
        if (!valResult.IsValid)
            return Result.ValidationError<TeacherResponse>(valResult);

        _context.Teachers.Add(teacher);
        await _context.SaveChangesAsync();

        return Result.Ok(_mapper.Map<TeacherResponse>(teacher));
    }

    public async Task<Result<TeacherResponse>> Update(UpdateTeacherRequest request)
    {
        var teacher = await _context.Teachers
            .FirstOrDefaultAsync(t => t.Id == request.Id);
        
        if (teacher == null)
            return Result.NotFound<TeacherResponse>("Teacher not found");
        
        teacher.Name = request.Name.Trim();
        
        var valResult = await _validator.ValidateAsync(teacher);
        if (!valResult.IsValid)
            return Result.ValidationError<TeacherResponse>(valResult);

        await _context.SaveChangesAsync();

        return Result.Ok(_mapper.Map<TeacherResponse>(teacher));
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