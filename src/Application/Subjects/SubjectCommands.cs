using Gbs.Domain.Common.Wrapper;

namespace Gbs.Application.Subjects;

public class SubjectCommands : ISubjectCommands
{
    private readonly IGbsDbContext _context;
    private readonly IMapper _mapper;

    public SubjectCommands(IGbsDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Result<SubjectDto>> Add(SubjectCreateDto request)
    {
        if (await NameExists(request.Name))
            return Result.BadRequest<SubjectDto>("Subject name already exists");
        
        var subject = _mapper.Map<Subject>(request);
        _context.Subjects.Add(subject);
        await _context.SaveChangesAsync();
        return Result.Ok(_mapper.Map<SubjectDto>(subject));
    }

    public async Task<Result<SubjectDto>> Update(int id, SubjectCreateDto request)
    {
        if (await NameExists(request.Name, id))
            return Result.BadRequest<SubjectDto>("Subject name already exists");
        
        var subject = await _context.Subjects.FirstOrDefaultAsync(s => s.Id == id);
        if (subject == null)
            return Result.NotFound<SubjectDto>("Subject not found");
        
        subject.Name = request.Name;
        await _context.SaveChangesAsync();

        return Result.Ok(_mapper.Map<SubjectDto>(subject));
    }

    private async Task<bool> NameExists(string name, int? id = null)
    {
        return id == null
            ? await _context.Subjects.AnyAsync(x => x.Name.ToLower().Equals(name.ToLower()))
            : await _context.Subjects.AnyAsync(x => x.Name.ToLower().Equals(name.ToLower()) && x.Id != id); 
    }
}