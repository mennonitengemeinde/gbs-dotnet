using Gbs.Application.Features.Churches.Interfaces;

namespace Gbs.Application.Features.Churches;

public class ChurchCommands : IChurchCommands
{
    private readonly IGbsDbContext _context;
    private readonly IMapper _mapper;
    private readonly IValidator<Church> _churchValidator;

    public ChurchCommands(
        IGbsDbContext context, 
        IMapper mapper, 
        IValidator<Church> churchValidator)
    {
        _context = context;
        _mapper = mapper;
        _churchValidator = churchValidator;
    }

    public async Task<Result<ChurchResponse>> Add(CreateChurchRequest request)
    {
        var newChurch = _mapper.Map<Church>(request);
        
        var valResult = await _churchValidator.ValidateAsync(newChurch);
        if (!valResult.IsValid)
            return Result.ValidationError<ChurchResponse>(valResult);

        _context.Churches.Add(newChurch);
        await _context.SaveChangesAsync();
        return Result.Ok(_mapper.Map<ChurchResponse>(newChurch));
    }

    public async Task<Result<ChurchResponse>> Update(UpdateChurchRequest request)
    {
        var dbChurch = await _context.Churches.FirstOrDefaultAsync(c => c.Id == request.Id);
        if (dbChurch == null)
            return Result.NotFound<ChurchResponse>("Church not found");
        
        _mapper.Map(request, dbChurch);
        
        var valResult = await _churchValidator.ValidateAsync(dbChurch);
        if (!valResult.IsValid)
            return Result.ValidationError<ChurchResponse>(valResult);

        _context.Churches.Update(dbChurch);
        await _context.SaveChangesAsync();

        return Result.Ok(_mapper.Map<ChurchResponse>(dbChurch));
    }

    public async Task<Result<bool>> Delete(int id)
    {
        var dbChurch = await _context.Churches.FirstOrDefaultAsync(c => c.Id == id);
        if (dbChurch == null)
            return Result.NotFound<bool>("Church not found");

        _context.Churches.Remove(dbChurch);
        await _context.SaveChangesAsync();
        return Result.Ok(true);
    }
}