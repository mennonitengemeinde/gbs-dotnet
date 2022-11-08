using AutoMapper.QueryableExtensions;

namespace Gbs.Server.Persistence.Repository;

public class ChurchRepository : IChurchRepository
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public ChurchRepository(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Result<List<ChurchDto>>> GetAllChurches()
    {
        var churches = await _context.Churches
            .AsNoTracking()
            .Include((c) => c.Students)
            .ProjectTo<ChurchDto>(_mapper.ConfigurationProvider)
            .ToListAsync();
            
        return Result.Ok(churches);
    }

    public async Task<Result<ChurchDto>> GetChurchById(int id)
    {
        var church = await _context.Churches
            .AsNoTracking()
            .ProjectTo<ChurchDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(c => c.Id == id);
        
        return church == null
            ? Result.NotFound<ChurchDto>("Church not found")
            : Result.Ok(church);
    }

    public async Task<Result<ChurchDto>> AddChurch(ChurchCreateDto church)
    {
        if (await ChurchExists(church.Name))
        {
            return Result.BadRequest<ChurchDto>("A Church with that name already exists");
        }

        var newChurch = _mapper.Map<Church>(church);
        _context.Churches.Add(newChurch);
        await _context.SaveChangesAsync();
        return Result.Ok(_mapper.Map<ChurchDto>(newChurch));
    }

    public async Task<Result<ChurchDto>> UpdateChurch(int id, ChurchCreateDto churchDto)
    {
        var dbChurch = await _context.Churches.FirstOrDefaultAsync(c => c.Id == id);
        if (dbChurch == null)
        {
            return Result.NotFound<ChurchDto>("Church not found");
        }

        if (await ChurchExists(churchDto.Name, id))
        {
            return Result.BadRequest<ChurchDto>("A Church with that name already exists");
        }

        dbChurch.Name = churchDto.Name;
        dbChurch.Address = churchDto.Address;
        dbChurch.City = churchDto.City;
        dbChurch.State = churchDto.State;
        dbChurch.PostalCode = churchDto.PostalCode;
        dbChurch.Country = churchDto.Country;
        _context.Churches.Update(dbChurch);
        await _context.SaveChangesAsync();

        return Result.Ok(_mapper.Map<ChurchDto>(dbChurch));
    }

    public async Task<Result<bool>> DeleteChurch(int id)
    {
        var dbChurch = await _context.Churches.FirstOrDefaultAsync(c => c.Id == id);
        if (dbChurch == null)
        {
            return Result.NotFound<bool>("Church not found");
        }

        _context.Churches.Remove(dbChurch);
        await _context.SaveChangesAsync();
        return Result.Ok(true);
    }

    private async Task<bool> ChurchExists(string name, int? id = null)
    {
        if (id == null)
        {
            return await _context.Churches.AnyAsync(c => c.Name.ToLower().Equals(name.ToLower()));
        }

        return await _context.Churches.AnyAsync(c => c.Name.ToLower().Equals(name.ToLower()) && c.Id != id);
    }
}