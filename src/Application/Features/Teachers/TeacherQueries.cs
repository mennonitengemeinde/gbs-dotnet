using Gbs.Application.Features.Teachers.Interfaces;

namespace Gbs.Application.Features.Teachers;

public class TeacherQueries : ITeacherQueries
{
    private readonly IGbsDbContext _context;
    private readonly IMapper _mapper;

    public TeacherQueries(IGbsDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public async Task<Result<List<TeacherResponse>>> GetAll()
    {
        var teachers = await _context.Teachers
            .ProjectTo<TeacherResponse>(_mapper.ConfigurationProvider)
            .ToListAsync();
        return Result.Ok(teachers);
    }

    public async Task<Result<TeacherResponse>> GetById(int id)
    {
        var teacher = await _context.Teachers
            .ProjectTo<TeacherResponse>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(t => t.Id == id);

        return teacher == null
            ? Result.NotFound<TeacherResponse>("Teacher not found")
            : Result.Ok(teacher);
    }
}