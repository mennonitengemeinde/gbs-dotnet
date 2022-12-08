using Gbs.Application.Features.Churches.Interfaces;

namespace Gbs.Application.Features.Churches;

public class ChurchCommands : IChurchCommands
{
    private readonly IGbsDbContext _context;
    private readonly IMapper _mapper;
    private readonly IValidator<CreateChurchRequest> _createChurchValidator;
    private readonly IValidator<UpdateChurchRequest> _updateChurchValidator;

    public ChurchCommands(
        IGbsDbContext context, 
        IMapper mapper, 
        IValidator<CreateChurchRequest> createChurchValidator,
        IValidator<UpdateChurchRequest> updateChurchValidator)
    {
        _context = context;
        _mapper = mapper;
        _createChurchValidator = createChurchValidator;
        _updateChurchValidator = updateChurchValidator;
    }

    public async Task<Result<ChurchResponse>> Add(CreateChurchRequest request)
    {
        var valResult = await _createChurchValidator.ValidateAsync(request);
        if (!valResult.IsValid)
            return Result.ValidationError<ChurchResponse>(valResult);

        var newChurch = _mapper.Map<Church>(request);
        _context.Churches.Add(newChurch);
        await _context.SaveChangesAsync();
        return Result.Ok(_mapper.Map<ChurchResponse>(newChurch));
    }

    public async Task<Result<ChurchResponse>> Update(UpdateChurchRequest request)
    {
        var dbChurch = await _context.Churches.FirstOrDefaultAsync(c => c.Id == request.Id);
        if (dbChurch == null)
            return Result.NotFound<ChurchResponse>("Church not found");
        
        var valResult = await _updateChurchValidator.ValidateAsync(request);
        if (!valResult.IsValid)
            return Result.ValidationError<ChurchResponse>(valResult);

        dbChurch.Name = request.Name;
        dbChurch.Address = request.Address;
        dbChurch.City = request.City;
        dbChurch.State = request.State;
        dbChurch.PostalCode = request.PostalCode;
        dbChurch.Country = request.Country;
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