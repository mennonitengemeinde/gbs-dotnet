using gbs.Shared.Enums;
using Microsoft.AspNetCore.DataProtection.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace gbs.Server.Data;

public class DataContext :
    IdentityDbContext<User, Role, string, IdentityUserClaim<string>, UserRole, IdentityUserLogin<string>,
        IdentityRoleClaim<string>, IdentityUserToken<string>>, IDataProtectionKeyContext
{
    public DbSet<DataProtectionKey> DataProtectionKeys { get; set; } = null!;

    public DbSet<Generation> Generations { get; set; } = null!;
    public DbSet<Enrollment> Enrollments { get; set; } = null!;
    public DbSet<Church> Churches { get; set; } = null!;
    public DbSet<Student> Students { get; set; } = null!;
    public DbSet<Teacher> Teachers { get; set; } = null!;
    public DbSet<LiveStream> Streams { get; set; } = null!;
    public DbSet<LiveStreamTeacher> StreamTeachers { get; set; } = null!;
    public DbSet<Subject> Subjects { get; set; } = null!;
    public DbSet<SubjectDocument> SubjectDocuments { get; set; } = null!;
    public DbSet<Lesson> Lessons { get; set; } = null!;
    public DbSet<Grade> Grades { get; set; } = null!;
    public DbSet<Question> Questions { get; set; } = null!;
    public DbSet<Message> Messages { get; set; } = null!;
    public DbSet<WatchList> WatchLists { get; set; } = null!;

    public DataContext(DbContextOptions<DataContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>()
            .HasOne<Teacher>()
            .WithOne(t => t.User)
            .HasForeignKey<Teacher>(t => t.UserId);

        modelBuilder.Entity<UserRole>(userRole =>
        {
            userRole.HasKey(ur => new { ur.UserId, ur.RoleId });

            userRole.HasOne(ur => ur.Role)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.RoleId)
                .IsRequired();

            userRole.HasOne(ur => ur.User)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.UserId)
                .IsRequired();
        });

        modelBuilder.Entity<Teacher>()
            .HasOne<User>()
            .WithOne(u => u.Teacher)
            .HasForeignKey<User>(u => u.TeacherId);

        modelBuilder.Entity<Enrollment>()
            .HasKey(e => new { e.StudentId, e.GenerationId });

        modelBuilder.Entity<LiveStreamTeacher>()
            .HasKey(lt => new { lt.LiveStreamId, lt.TeacherId });

        modelBuilder.Entity<Grade>()
            .HasKey(g => new { g.SubjectId, g.EnrollmentId });

        modelBuilder.Entity<WatchList>()
            .HasKey(wl => new { wl.UserId, wl.QuestionId });

        modelBuilder.Entity<Message>()
            .Property(m => m.MessageType)
            .HasConversion<int>();

        modelBuilder.Entity<Student>()
            .Property(s => s.MaritalStatus)
            .HasConversion<int>();

        modelBuilder.Entity<SubjectDocument>()
            .Property(sd => sd.SubjectDocumentType)
            .HasConversion<int>();

        modelBuilder.Entity<Role>()
            .HasData(
                new Role
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = Shared.Const.Roles.SuperAdmin,
                    NormalizedName = Shared.Const.Roles.SuperAdmin.ToUpper(),
                    ConcurrencyStamp = Guid.NewGuid().ToString()
                },
                new Role
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = Shared.Const.Roles.Admin,
                    NormalizedName = Shared.Const.Roles.Admin.ToUpper(),
                    ConcurrencyStamp = Guid.NewGuid().ToString()
                },
                new Role
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = Shared.Const.Roles.Teacher,
                    NormalizedName = Shared.Const.Roles.Teacher.ToUpper(),
                    ConcurrencyStamp = Guid.NewGuid().ToString()
                },
                new Role
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = Shared.Const.Roles.ChurchLeader,
                    NormalizedName = Shared.Const.Roles.ChurchLeader.ToUpper(),
                    ConcurrencyStamp = Guid.NewGuid().ToString()
                },
                new Role
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = Shared.Const.Roles.ChurchTeacher,
                    NormalizedName = Shared.Const.Roles.ChurchTeacher.ToUpper(),
                    ConcurrencyStamp = Guid.NewGuid().ToString()
                },
                new Role
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = Shared.Const.Roles.Sound,
                    NormalizedName = Shared.Const.Roles.Sound.ToUpper(),
                    ConcurrencyStamp = Guid.NewGuid().ToString()
                },
                new Role
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = Shared.Const.Roles.Student,
                    NormalizedName = Shared.Const.Roles.Student.ToUpper(),
                    ConcurrencyStamp = Guid.NewGuid().ToString()
                }
            );
    }
}