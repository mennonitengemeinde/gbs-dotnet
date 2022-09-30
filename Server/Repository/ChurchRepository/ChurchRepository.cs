namespace gbs.Server.Repository.ChurchRepository;

public class ChurchRepository : IChurchRepository
{
    private readonly DataContext _context;

    public ChurchRepository(DataContext context)
    {
        _context = context;
    }

    public async Task<ServiceResponse<List<ChurchDto>>> GetAllChurches()
    {
        var churches = await _context.Churches
            .AsNoTracking()
            .Include((c) => c.Students)
            .Select((c) => new ChurchDto
            {
                Id = c.Id,
                Name = c.Name,
                Address = c.Address,
                City = c.City,
                State = c.State,
                PostalCode = c.PostalCode,
                Country = c.Country,
                StudentCount = c.Students.Count
            })
            .ToListAsync();
        return new ServiceResponse<List<ChurchDto>>
        {
            Data = churches
        };
    }

    public async Task<ServiceResponse<ChurchDto>> GetChurchById(int id)
    {
        var church = await _context.Churches
            .AsNoTracking()
            .Include((c) => c.Students)
            .Select((c) => new ChurchDto
            {
                Id = c.Id,
                Name = c.Name,
                Address = c.Address,
                City = c.City,
                State = c.State,
                PostalCode = c.PostalCode,
                Country = c.Country,
                StudentCount = c.Students.Count
            })
            .FirstOrDefaultAsync(c => c.Id == id);
        return church == null
            ? ServiceResponse<ChurchDto>.BadRequest("Church not found")
            : new ServiceResponse<ChurchDto> { Data = church };
    }

    public async Task<ServiceResponse<List<ChurchDto>>> AddChurch(ChurchCreateDto church)
    {
        if (await ChurchExists(church.Name))
        {
            return ServiceResponse<List<ChurchDto>>.BadRequest("A Church with that name already exists");
        }

        var newChurch = new Church
        {
            Name = church.Name,
            Address = church.Address,
            City = church.City,
            State = church.State,
            PostalCode = church.PostalCode,
            Country = church.Country,
        };
        _context.Churches.Add(newChurch);
        await _context.SaveChangesAsync();
        return await GetAllChurches();
    }

    public async Task<ServiceResponse<List<ChurchDto>>> UpdateChurch(int id, ChurchCreateDto churchDto)
    {
        var dbChurch = await _context.Churches.FirstOrDefaultAsync(c => c.Id == id);
        if (dbChurch == null)
        {
            return ServiceResponse<List<ChurchDto>>.BadRequest("Church not found");
        }

        if (await ChurchExists(churchDto.Name, id))
        {
            return ServiceResponse<List<ChurchDto>>.BadRequest("A Church with that name already exists");
        }

        dbChurch.Name = churchDto.Name;
        dbChurch.Address = churchDto.Address;
        dbChurch.City = churchDto.City;
        dbChurch.State = churchDto.State;
        dbChurch.PostalCode = churchDto.PostalCode;
        dbChurch.Country = churchDto.Country;
        _context.Churches.Update(dbChurch);
        await _context.SaveChangesAsync();

        return await GetAllChurches();
    }

    public async Task<ServiceResponse<List<ChurchDto>>> DeleteChurch(int id)
    {
        var dbChurch = await _context.Churches.FirstOrDefaultAsync(c => c.Id == id);
        if (dbChurch == null)
        {
            return ServiceResponse<List<ChurchDto>>.BadRequest("Church not found");
        }

        _context.Churches.Remove(dbChurch);
        await _context.SaveChangesAsync();
        return await GetAllChurches();
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