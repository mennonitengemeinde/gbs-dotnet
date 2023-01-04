using Gbs.Application.Features.GradeTypes.Interfaces;
using Gbs.Shared.GradeTypes;

namespace Gbs.Application.Features.GradeTypes;

public class GradeTypeQueries : IGradeTypeQueries
{
    private readonly IGbsDbContext _context;
    private readonly IMapper _mapper;

    public GradeTypeQueries(IGbsDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Result<List<GradeTypeResponse>>> GetAll()
    {
        var gradeTypes = await _context.GradeTypes
            .ProjectTo<GradeTypeResponse>(_mapper.ConfigurationProvider)
            .ToListAsync();
        return Result.Ok(gradeTypes);
    }

    public async Task<Result<GradeTypeResponse>> GetById(int id)
    {
        var gradeType = await _context.GradeTypes
            .ProjectTo<GradeTypeResponse>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(x => x.Id == id);
        return gradeType == null
            ? Result.NotFound<GradeTypeResponse>("Grade type not found")
            : Result.Ok(gradeType);
    }
}