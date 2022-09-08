using Microsoft.EntityFrameworkCore;

namespace gbs.Server.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }
}