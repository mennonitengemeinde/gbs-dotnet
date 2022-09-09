using Microsoft.EntityFrameworkCore;

namespace gbs.Server.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<Enrolment>()
            .HasKey(e => new { e.StudentId, e.GenerationId });
    }

    public DbSet<Generation> Generations { get; set; } = null!;
    public DbSet<Enrolment> Enrolments { get; set; } = null!;
    public DbSet<Church> Churches { get; set; } = null!;
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Student> Students { get; set; } = null!;
    public DbSet<Teacher> Teachers { get; set; } = null!;
    public DbSet<LiveStream> Streams { get; set; } = null!;
}