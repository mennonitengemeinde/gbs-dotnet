using Gbs.Application.Features.Subjects.Interfaces;

namespace Gbs.Application.Features.Subjects;

public class SubjectCommands : ISubjectCommands
{
    private readonly IGbsDbContext _context;
    private readonly IMapper _mapper;
    private readonly IValidator<Subject> _validator;

    public SubjectCommands(IGbsDbContext context, IMapper mapper, IValidator<Subject> validator)
    {
        _context = context;
        _mapper = mapper;
        _validator = validator;
    }

    public async Task<Result<SubjectResponse>> Add(CreateSubjectRequest request)
    {
        var subject = _mapper.Map<Subject>(request);

        var resultVal = await _validator.ValidateAsync(subject);
        if (!resultVal.IsValid)
            return Result.ValidationError<SubjectResponse>(resultVal);

        _context.Subjects.Add(subject);
        await _context.SaveChangesAsync();
        return Result.Ok(_mapper.Map<SubjectResponse>(subject));
    }

    public async Task<Result<SubjectResponse>> Update(int id, CreateSubjectRequest request)
    {
        var subject = await _context.Subjects.FirstOrDefaultAsync(s => s.Id == id);
        if (subject == null)
            return Result.NotFound<SubjectResponse>("Subject not found");

        subject.Name = request.Name;

        var resultVal = await _validator.ValidateAsync(subject);
        if (!resultVal.IsValid)
            return Result.ValidationError<SubjectResponse>(resultVal);

        await _context.SaveChangesAsync();

        return Result.Ok(_mapper.Map<SubjectResponse>(subject));
    }
}