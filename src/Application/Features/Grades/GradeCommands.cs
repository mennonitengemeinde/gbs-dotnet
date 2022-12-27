using Gbs.Application.Features.Grades.Interfaces;

namespace Gbs.Application.Features.Grades;

public class GradeCommands : IGradeCommands
{
    private readonly IGbsDbContext _context;
    private readonly IMapper _mapper;
    private readonly IValidator<Grade> _validator;

    public GradeCommands(IGbsDbContext context, IMapper mapper, IValidator<Grade> validator)
    {
        _context = context;
        _mapper = mapper;
        _validator = validator;
    }
    
    public async Task<Result<GradeResponse>> Add(CreateGradeRequest request)
    {
        var newGrade = _mapper.Map<Grade>(request);
        
        var valResult = await _validator.ValidateAsync(newGrade);
        if (!valResult.IsValid)
            return Result.ValidationError<GradeResponse>(valResult);

        _context.Grades.Add(newGrade);
        await _context.SaveChangesAsync();
        return Result.Ok(_mapper.Map<GradeResponse>(newGrade));
    }

    public async Task<Result<GradeResponse>> Update(int id, CreateGradeRequest request)
    {
        var dbGrade = await _context.Grades.FindAsync(id);
        if (dbGrade == null)
            return Result.NotFound<GradeResponse>("Grade not found");
        
        _mapper.Map(request, dbGrade);
        
        var valResult = await _validator.ValidateAsync(dbGrade);
        if (!valResult.IsValid)
            return Result.ValidationError<GradeResponse>(valResult);

        _context.Grades.Update(dbGrade);
        await _context.SaveChangesAsync();
        return Result.Ok(_mapper.Map<GradeResponse>(dbGrade));
    }

    public async Task<Result<bool>> Delete(int id)
    {
        var dbGrade = await _context.Grades.FindAsync(id);
        if (dbGrade == null)
            return Result.NotFound<bool>("Grade not found");
        
        _context.Grades.Remove(dbGrade);
        await _context.SaveChangesAsync();
        return Result.Ok(true);
    }
}