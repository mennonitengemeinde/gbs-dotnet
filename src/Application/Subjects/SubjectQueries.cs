using Gbs.Domain.Common.Wrapper;
using Gbs.Shared.Subjects;

namespace Gbs.Application.Subjects;

public class SubjectQueries : ISubjectQueries
{
    private readonly IGbsDbContext _context;
    private readonly IMapper _mapper;

    public SubjectQueries(IGbsDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Result<List<SubjectDto>>> GetAll()
    {
        var subjects = await _context.Subjects
            .ProjectTo<SubjectDto>(_mapper.ConfigurationProvider)
            .ToListAsync();
        return Result.Ok(subjects);
    }

    public async Task<Result<SubjectDto>> GetById(int id)
    {
        var subject = await _context.Subjects
            .ProjectTo<SubjectDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();
        return subject == null
            ? Result.NotFound<SubjectDto>("Subject not found")
            : Result.Ok(subject);
    }
}