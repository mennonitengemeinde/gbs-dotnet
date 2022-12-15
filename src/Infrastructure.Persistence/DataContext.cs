using System.Reflection;
using Gbs.Application.Common.Interfaces;
using Gbs.Application.Entities;
using Microsoft.AspNetCore.DataProtection.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Gbs.Infrastructure.Persistence;

public class DataContext :
    IdentityDbContext<User, Role, string, IdentityUserClaim<string>, UserRole, IdentityUserLogin<string>,
        IdentityRoleClaim<string>, IdentityUserToken<string>>, IDataProtectionKeyContext, IGbsDbContext
{
    public DbSet<DataProtectionKey> DataProtectionKeys { get; set; } = null!;

    public DbSet<Generation> Generations { get; set; } = null!;
    public DbSet<Church> Churches { get; set; } = null!;
    public DbSet<Student> Students { get; set; } = null!;
    public DbSet<Teacher> Teachers { get; set; } = null!;
    public DbSet<LiveStream> Streams { get; set; } = null!;
    public DbSet<LiveStreamTeacher> StreamTeachers { get; set; } = null!;
    public DbSet<Subject> Subjects { get; set; } = null!;
    public DbSet<SubjectDocument> SubjectDocuments { get; set; } = null!;
    public DbSet<Lesson> Lessons { get; set; } = null!;
    public DbSet<Grade> Grades { get; set; } = null!;
    public DbSet<GradeType> GradeTypes { get; set; } = null!;
    public DbSet<Question> Questions { get; set; } = null!;
    public DbSet<Message> Messages { get; set; } = null!;
    public DbSet<WatchList> WatchLists { get; set; } = null!;

    public DataContext(DbContextOptions<DataContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}