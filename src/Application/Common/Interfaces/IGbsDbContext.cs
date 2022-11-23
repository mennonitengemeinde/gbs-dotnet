using Microsoft.AspNetCore.DataProtection.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace Gbs.Application.Common.Interfaces;

public interface IGbsDbContext
{
    DbSet<DataProtectionKey> DataProtectionKeys { get; set; }
    DbSet<Generation> Generations { get; set; }
    DbSet<Enrollment> Enrollments { get; set; }
    DbSet<Church> Churches { get; set; }
    DbSet<Student> Students { get; set; }
    DbSet<Teacher> Teachers { get; set; }
    DbSet<LiveStream> Streams { get; set; }
    DbSet<LiveStreamTeacher> StreamTeachers { get; set; }
    DbSet<Subject> Subjects { get; set; }
    DbSet<SubjectDocument> SubjectDocuments { get; set; }
    DbSet<Lesson> Lessons { get; set; }
    DbSet<Grade> Grades { get; set; }
    DbSet<Question> Questions { get; set; }
    DbSet<Message> Messages { get; set; }
    DbSet<WatchList> WatchLists { get; set; }
    DbSet<User> Users { get; set; }
    DbSet<IdentityUserClaim<string>> UserClaims { get; set; }
    DbSet<IdentityUserLogin<string>> UserLogins { get; set; }
    DbSet<IdentityUserToken<string>> UserTokens { get; set; }
    DbSet<UserRole> UserRoles { get; set; }
    DbSet<Role> Roles { get; set; }
    DbSet<IdentityRoleClaim<string>> RoleClaims { get; set; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));
}