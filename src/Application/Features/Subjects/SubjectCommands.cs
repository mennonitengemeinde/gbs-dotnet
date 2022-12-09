using Gbs.Application.Features.Subjects.Interfaces;

namespace Gbs.Application.Features.Subjects;

public class SubjectCommands : ISubjectCommands
{
    private readonly IGbsDbContext _context;
    private readonly IMapper _mapper;

    public SubjectCommands(IGbsDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Result<SubjectResponse>> Add(CreateSubjectRequest request)
    {
        if (await NameExists(request.Name))
            return Result.BadRequest<SubjectResponse>("Subject name already exists");
        
        var subject = _mapper.Map<Subject>(request);
        _context.Subjects.Add(subject);
        await _context.SaveChangesAsync();
        return Result.Ok(_mapper.Map<SubjectResponse>(subject));
    }

    public async Task<Result<SubjectResponse>> Update(int id, UpdateSubjectRequest request)
    {
        if (await NameExists(request.Name, id))
            return Result.BadRequest<SubjectResponse>("Subject name already exists");
        
        var subject = await _context.Subjects.FirstOrDefaultAsync(s => s.Id == id);
        if (subject == null)
            return Result.NotFound<SubjectResponse>("Subject not found");
        
        subject.Name = request.Name;
        await _context.SaveChangesAsync();

        return Result.Ok(_mapper.Map<SubjectResponse>(subject));
    }

    private async Task<bool> NameExists(string name, int? id = null)
    {
        return id == null
            ? await _context.Subjects.AnyAsync(x => x.Name.ToLower().Equals(name.ToLower()))
            : await _context.Subjects.AnyAsync(x => x.Name.ToLower().Equals(name.ToLower()) && x.Id != id); 
    }
}