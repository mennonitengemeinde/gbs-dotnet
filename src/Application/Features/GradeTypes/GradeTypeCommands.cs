using Gbs.Application.Features.GradeTypes.Interfaces;
using Gbs.Shared.GradeTypes;

namespace Gbs.Application.Features.GradeTypes;

public class GradeTypeCommands : IGradeTypeCommands
{
    private readonly IGbsDbContext _context;
    private readonly IMapper _mapper;
    private readonly IValidator<GradeType> _validator;

    public GradeTypeCommands(IGbsDbContext context, IMapper mapper, IValidator<GradeType> validator)
    {
        _context = context;
        _mapper = mapper;
        _validator = validator;
    }

    public async Task<Result<GradeTypeResponse>> Add(CreateGradeTypeRequest request)
    {
        var gradeType = new GradeType { Name = request.Name.Trim(), GenerationId = request.GenerationId };
        var resultVal = await _validator.ValidateAsync(gradeType);
        if (!resultVal.IsValid)
            return Result.ValidationError<GradeTypeResponse>(resultVal);

        await _context.GradeTypes.AddAsync(gradeType);
        await _context.SaveChangesAsync();
        return Result.Ok(_mapper.Map<GradeTypeResponse>(gradeType));
    }

    public async Task<Result<GradeTypeResponse>> Update(int id, CreateGradeTypeRequest request)
    {
        var dbGradeType = await _context.GradeTypes.FirstOrDefaultAsync(u => u.Id == id);
        if (dbGradeType == null)
            return Result.NotFound<GradeTypeResponse>("GradeType not found");
        
        dbGradeType.Name = request.Name.Trim();
        dbGradeType.GenerationId = request.GenerationId;
        
        var resultVal = await _validator.ValidateAsync(dbGradeType);
        if (!resultVal.IsValid)
            return Result.ValidationError<GradeTypeResponse>(resultVal);

        await _context.SaveChangesAsync();

        return Result.Ok(_mapper.Map<GradeTypeResponse>(dbGradeType));
    }

    public async Task<Result<bool>> Delete(int id)
    {
        var dbGradeType = await _context.GradeTypes.FirstOrDefaultAsync(u => u.Id == id);
        if (dbGradeType == null)
            return Result.NotFound<bool>("GradeType not found");

        _context.GradeTypes.Remove(dbGradeType);
        await _context.SaveChangesAsync();

        return Result.Ok(true);
    }
}