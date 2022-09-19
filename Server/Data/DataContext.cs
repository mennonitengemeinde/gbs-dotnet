using gbs.Shared.Enums;
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

        modelBuilder.Entity<User>()
            .HasOne<Teacher>()
            .WithOne(t => t.User)
            .HasForeignKey<Teacher>(t => t.UserId);

        modelBuilder.Entity<Teacher>()
            .HasOne<User>()
            .WithOne(u => u.Teacher)
            .HasForeignKey<User>(u => u.TeacherId);

        modelBuilder.Entity<Enrollment>()
            .HasKey(e => new {e.StudentId, e.GenerationId});

        modelBuilder.Entity<LiveStreamTeacher>()
            .HasKey(lt => new {lt.LiveStreamId, lt.TeacherId});

        modelBuilder.Entity<Grade>()
            .HasKey(g => new {g.SubjectId, g.EnrollmentId});

        modelBuilder.Entity<WatchList>()
            .HasKey(wl => new {wl.UserId, wl.QuestionId});

        modelBuilder.Entity<Message>()
            .Property(m => m.MessageType)
            .HasConversion<int>();

        modelBuilder.Entity<Student>()
            .Property(s => s.MaritalStatus)
            .HasConversion<int>();

        modelBuilder.Entity<SubjectDocument>()
            .Property(sd => sd.SubjectDocumentType)
            .HasConversion<int>();
    }

    public DbSet<Generation> Generations { get; set; } = null!;
    public DbSet<Enrollment> Enrollments { get; set; } = null!;
    public DbSet<Church> Churches { get; set; } = null!;
    public DbSet<User> Users { get; set; } = null!;
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
}