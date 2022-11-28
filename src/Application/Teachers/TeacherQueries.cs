using Gbs.Domain.Common.Wrapper;

namespace Gbs.Application.Teachers;

public class TeacherQueries : ITeacherQueries
{
    private readonly IGbsDbContext _context;
    private readonly IMapper _mapper;

    public TeacherQueries(IGbsDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public async Task<Result<List<TeacherDto>>> GetAll()
    {
        var teachers = await _context.Teachers
            .ProjectTo<TeacherDto>(_mapper.ConfigurationProvider)
            .ToListAsync();
        return Result.Ok(teachers);
    }

    public async Task<Result<TeacherDto>> GetById(int id)
    {
        var teacher = await _context.Teachers
            .ProjectTo<TeacherDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(t => t.Id == id);

        return teacher == null
            ? Result.NotFound<TeacherDto>("Teacher not found")
            : Result.Ok(teacher);
    }
}