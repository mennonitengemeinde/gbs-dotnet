using Gbs.Application.Features.Subjects.Interfaces;

namespace Gbs.Application.Features.Subjects;

public class SubjectQueries : ISubjectQueries
{
    private readonly IGbsDbContext _context;
    private readonly IMapper _mapper;

    public SubjectQueries(IGbsDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Result<List<SubjectResponse>>> GetAll()
    {
        var subjects = await _context.Subjects
            .OrderBy(x => x.Name)
            .ProjectTo<SubjectResponse>(_mapper.ConfigurationProvider)
            .ToListAsync();
        return Result.Ok(subjects);
    }

    public async Task<Result<SubjectResponse>> GetById(int id)
    {
        var subject = await _context.Subjects
            .ProjectTo<SubjectResponse>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();
        return subject == null
            ? Result.NotFound<SubjectResponse>("Subject not found")
            : Result.Ok(subject);
    }
}