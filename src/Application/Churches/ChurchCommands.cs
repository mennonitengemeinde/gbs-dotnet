namespace Gbs.Application.Churches;

public class ChurchCommands : IChurchCommands
{
    private readonly IGbsDbContext _context;
    private readonly IMapper _mapper;

    public ChurchCommands(IGbsDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Result<ChurchDto>> Add(ChurchCreateDto request)
    {
        if (await NameExists(request.Name))
        {
            return Result.BadRequest<ChurchDto>("A Church with that name already exists");
        }

        var newChurch = _mapper.Map<Church>(request);
        _context.Churches.Add(newChurch);
        await _context.SaveChangesAsync();
        return Result.Ok(_mapper.Map<ChurchDto>(newChurch));
    }

    public async Task<Result<ChurchDto>> Update(int id, ChurchCreateDto request)
    {
        var dbChurch = await _context.Churches.FirstOrDefaultAsync(c => c.Id == id);
        if (dbChurch == null)
        {
            return Result.NotFound<ChurchDto>("Church not found");
        }

        if (await NameExists(request.Name, id))
        {
            return Result.BadRequest<ChurchDto>("A Church with that name already exists");
        }

        dbChurch.Name = request.Name;
        dbChurch.Address = request.Address;
        dbChurch.City = request.City;
        dbChurch.State = request.State;
        dbChurch.PostalCode = request.PostalCode;
        dbChurch.Country = request.Country;
        _context.Churches.Update(dbChurch);
        await _context.SaveChangesAsync();

        return Result.Ok(_mapper.Map<ChurchDto>(dbChurch));
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

    private async Task<bool> NameExists(string name, int? id = null)
    {
        return id == null
            ? await _context.Churches.AnyAsync(c => c.Name.ToLower().Equals(name.ToLower()))
            : await _context.Churches.AnyAsync(c => c.Name.ToLower().Equals(name.ToLower()) && c.Id != id);
    }
}