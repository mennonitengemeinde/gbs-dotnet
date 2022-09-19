namespace gbs.Server.Repository.ChurchRepository;

public class ChurchRepository : IChurchRepository
{
    public async Task<ServiceResponse<List<Church>>> GetAllChurches()
    {
        throw new NotImplementedException();
    }

    public async Task<ServiceResponse<Church>> GetChurchById(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<ServiceResponse<List<Church>>> AddChurch(ChurchCreateDto church)
    {
        throw new NotImplementedException();
    }

    public async Task<ServiceResponse<List<Church>>> UpdateChurch(ChurchCreateDto church)
    {
        throw new NotImplementedException();
    }

    public async Task<ServiceResponse<List<Church>>> DeleteChurch(int id)
    {
        throw new NotImplementedException();
    }
}