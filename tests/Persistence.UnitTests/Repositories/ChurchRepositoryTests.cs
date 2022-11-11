namespace Gbs.Persistence.UnitTests.Repositories;

public class ChurchRepositoryTests
{
    private readonly ChurchRepository _churchRepo;

    public ChurchRepositoryTests()
    {
        var options = new DbContextOptionsBuilder<DataContext>()
            .UseInMemoryDatabase(databaseName: "ChurchDatabase")
            .Options;

        var context = new DataContext(options);
        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();

        var churches = ChurchSeed.GetChurches();
        context.Churches.AddRange(churches);
        context.SaveChanges();

        var config = new MapperConfiguration(cfg => { cfg.AddProfile(new MappingProfile()); });
        var mapper = config.CreateMapper();

        _churchRepo = new ChurchRepository(context, mapper);
    }

    [Fact]
    public async Task GetAllChurches_ReturnsAllChurches()
    {
        var churches = await _churchRepo.GetAllChurches();

        Assert.NotNull(churches.Data);
        Assert.Equal(3, churches.Data.Count);
    }

    [Fact]
    public async Task GetChurchById_ReturnsChurch()
    {
        var church = await _churchRepo.GetChurchById(1);

        Assert.NotNull(church.Data);
        Assert.Equal(1, church.Data.Id);
        Assert.Equal("Church 1", church.Data.Name);
    }

    [Fact]
    public async Task GetChurchById_ReturnsNotFound()
    {
        var church = await _churchRepo.GetChurchById(4);

        Assert.False(church.Success);
        Assert.Null(church.Data);
        Assert.Equal(404, church.StatusCode);
    }

    [Fact]
    public async Task AddChurch_ReturnsChurch()
    {
        var church = new ChurchCreateDto { Name = "Church 4", Country = "Canada" };

        var result = await _churchRepo.AddChurch(church);

        Assert.NotNull(result.Data);
        Assert.Equal(4, result.Data.Id);
        Assert.Equal("Church 4", result.Data.Name);
    }

    [Fact]
    public async Task AddChurch_ReturnsBadRequest()
    {
        var church = new ChurchCreateDto { Name = "Church 1", Country = "Canada" };

        var result = await _churchRepo.AddChurch(church);

        Assert.False(result.Success);
        Assert.Null(result.Data);
        Assert.Equal(400, result.StatusCode);
    }

    [Fact]
    public async Task UpdateChurch_ReturnsChurch()
    {
        var church = new ChurchCreateDto { Name = "Church 1 Updated", Country = "Canada" };

        var result = await _churchRepo.UpdateChurch(1, church);

        Assert.NotNull(result.Data);
        Assert.Equal(1, result.Data.Id);
        Assert.Equal("Church 1 Updated", result.Data.Name);
    }

    [Fact]
    public async Task UpdateChurch_ReturnsNotFound()
    {
        var church = new ChurchCreateDto { Name = "Church 1 Updated", Country = "Canada" };

        var result = await _churchRepo.UpdateChurch(4, church);

        Assert.False(result.Success);
        Assert.Null(result.Data);
        Assert.Equal(404, result.StatusCode);
    }

    [Fact]
    public async Task UpdateChurch_ChurchNameAlreadyExists_ReturnsBadRequest()
    {
        var church = new ChurchCreateDto { Name = "Church 2", Country = "Canada" };

        var result = await _churchRepo.UpdateChurch(1, church);

        Assert.False(result.Success);
        Assert.Null(result.Data);
        Assert.Equal(400, result.StatusCode);
    }

    [Fact]
    public async Task DeleteChurch_ReturnsSuccess()
    {
        var result = await _churchRepo.DeleteChurch(1);

        Assert.True(result.Success);
        Assert.True(result.Data);
    }

    [Fact]
    public async Task DeleteChurch_ReturnsNotFound()
    {
        var result = await _churchRepo.DeleteChurch(4);

        Assert.False(result.Success);
        Assert.Equal(404, result.StatusCode);
    }
}