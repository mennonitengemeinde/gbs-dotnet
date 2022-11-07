using gbs.Core.Shared.Const;

namespace Gbs.Server.Persistence.Configurations;

public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.HasData(
            new Role
            {
                Id = Guid.NewGuid().ToString(),
                Name = Roles.SuperAdmin,
                NormalizedName = Roles.SuperAdmin.ToUpper(),
                ConcurrencyStamp = Guid.NewGuid().ToString()
            },
            new Role
            {
                Id = Guid.NewGuid().ToString(),
                Name = Roles.Admin,
                NormalizedName = Roles.Admin.ToUpper(),
                ConcurrencyStamp = Guid.NewGuid().ToString()
            },
            new Role
            {
                Id = Guid.NewGuid().ToString(),
                Name = Roles.Teacher,
                NormalizedName = Roles.Teacher.ToUpper(),
                ConcurrencyStamp = Guid.NewGuid().ToString()
            },
            new Role
            {
                Id = Guid.NewGuid().ToString(),
                Name = Roles.ChurchLeader,
                NormalizedName = Roles.ChurchLeader.ToUpper(),
                ConcurrencyStamp = Guid.NewGuid().ToString()
            },
            new Role
            {
                Id = Guid.NewGuid().ToString(),
                Name = Roles.ChurchTeacher,
                NormalizedName = Roles.ChurchTeacher.ToUpper(),
                ConcurrencyStamp = Guid.NewGuid().ToString()
            },
            new Role
            {
                Id = Guid.NewGuid().ToString(),
                Name = Roles.Sound,
                NormalizedName = Roles.Sound.ToUpper(),
                ConcurrencyStamp = Guid.NewGuid().ToString()
            },
            new Role
            {
                Id = Guid.NewGuid().ToString(),
                Name = Roles.Student,
                NormalizedName = Roles.Student.ToUpper(),
                ConcurrencyStamp = Guid.NewGuid().ToString()
            }
        );
    }
}