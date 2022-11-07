using AutoMapper;
using Gbs.Core.Domain.Entities;
using Gbs.Server.Application.Common;
using Gbs.Server.Persistence;
using Gbs.Server.Persistence.Repository;
using Microsoft.EntityFrameworkCore;

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
        
        var church1 = new Church { Id = 1, Name = "Church 1", Country = "Canada" };
        var church2 = new Church { Id = 2, Name = "Church 2", Country = "Canada" };
        var church3 = new Church { Id = 3, Name = "Church 3", Country = "Canada" };

        _context.Churches.Add(church1);
        _context.Churches.Add(church2);
        _context.Churches.Add(church3);
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
}