using Gbs.Server.Persistence.Seeds;

namespace gbs.Server.Persistence.UnitTests.Repositories;

public class ChurchRepositoryTests
{
    private DataContext _context = null!;
    private ChurchRepository _churchRepository = null!;

    [SetUp]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<DataContext>()
            .UseInMemoryDatabase(databaseName: "ChurchDatabase")
            .Options;

        _context = new DataContext(options);
        _context.Database.EnsureDeleted();
        _context.Database.EnsureCreated();

        var churches = ChurchSeed.GetChurches();
        _context.Churches.AddRange(churches);
        _context.SaveChanges();

        var config = new MapperConfiguration(cfg => { cfg.AddProfile(new MappingProfile()); });
        var mapper = config.CreateMapper();

        _churchRepository = new ChurchRepository(_context, mapper);
    }

    [Test]
    public async Task GetAllChurches_ReturnsAllChurches()
    {
        var churches = await _churchRepository.GetAllChurches();

        Assert.That(churches.Data, Is.Not.Null);
        Assert.That(churches.Data, Has.Count.EqualTo(3));
    }
    
    [Test]
    public async Task GetChurchById_ReturnsChurch()
    {
        var church = await _churchRepository.GetChurchById(1);

        Assert.That(church.Data, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(church.Data.Id, Is.EqualTo(1));
            Assert.That(church.Data.Name, Is.EqualTo("Church 1"));
        });
    }
    
    [Test]
    public async Task GetChurchById_ReturnsNotFound()
    {
        var church = await _churchRepository.GetChurchById(4);
        Assert.Multiple(() =>
        {
            Assert.That(church.Success, Is.False);
            Assert.That(church.Data, Is.Null);
            Assert.That(church.StatusCode, Is.EqualTo(404));
        });
    }

    [Test]
    public async Task AddChurch_ReturnsChurch()
    {
        var church = new ChurchCreateDto { Name = "Church 4", Country = "Canada" };

        var result = await _churchRepository.AddChurch(church);

        Assert.That(result.Data, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(result.Data.Id, Is.EqualTo(4));
            Assert.That(result.Data.Name, Is.EqualTo("Church 4"));
        });
    }
    
    [Test]
    public async Task AddChurch_ReturnsBadRequest()
    {
        var church = new ChurchCreateDto { Name = "Church 1", Country = "Canada" };

        var result = await _churchRepository.AddChurch(church);

        Assert.Multiple(() =>
        {
            Assert.That(result.Data, Is.Null);
            Assert.That(result.Success, Is.False);
            Assert.That(result.StatusCode, Is.EqualTo(400));
        });
    }
    
    [Test]
    public async Task UpdateChurch_ReturnsChurch()
    {
        var church = new ChurchCreateDto { Name = "Church 1 Updated", Country = "Canada" };

        var result = await _churchRepository.UpdateChurch(1, church);

        Assert.That(result.Data, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(result.Data.Id, Is.EqualTo(1));
            Assert.That(result.Data.Name, Is.EqualTo("Church 1 Updated"));
        });
    }
    
    [Test]
    public async Task UpdateChurch_ReturnsNotFound()
    {
        var church = new ChurchCreateDto { Name = "Church 1 Updated", Country = "Canada" };

        var result = await _churchRepository.UpdateChurch(4, church);

        Assert.Multiple(() =>
        {
            Assert.That(result.Data, Is.Null);
            Assert.That(result.Success, Is.False);
            Assert.That(result.StatusCode, Is.EqualTo(404));
        });
    }
    
    [Test]
    public async Task UpdateChurch_ChurchNameAlreadyExists_ReturnsBadRequest()
    {
        var church = new ChurchCreateDto { Name = "Church 2", Country = "Canada" };

        var result = await _churchRepository.UpdateChurch(1, church);

        Assert.Multiple(() =>
        {
            Assert.That(result.Data, Is.Null);
            Assert.That(result.Success, Is.False);
            Assert.That(result.StatusCode, Is.EqualTo(400));
        });
    }
    
    [Test]
    public async Task DeleteChurch_ReturnsSuccess()
    {
        var result = await _churchRepository.DeleteChurch(1);

        Assert.That(result.Success, Is.True);
    }
    
    [Test]
    public async Task DeleteChurch_ReturnsNotFound()
    {
        var result = await _churchRepository.DeleteChurch(4);

        Assert.Multiple(() =>
        {
            Assert.That(result.Data, Is.Null);
            Assert.That(result.Success, Is.False);
            Assert.That(result.StatusCode, Is.EqualTo(404));
        });
    }
}