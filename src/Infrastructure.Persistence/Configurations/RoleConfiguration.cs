using Gbs.Application.Common.Entities;
using Gbs.Shared.Common.Const;

namespace Gbs.Infrastructure.Persistence.Configurations;

public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.HasData(
            new Role
            {
                Id = "c537702f-d9e1-4863-b932-55aeaf0819de",
                Name = Roles.SuperAdmin,
                NormalizedName = Roles.SuperAdmin.ToUpper(),
                ConcurrencyStamp = "8f7a655c-48b5-45f7-aa19-c813a35f3f3b"
            },
            new Role
            {
                Id = "a5c03823-1a98-44f2-bb83-2121617b4973",
                Name = Roles.Admin,
                NormalizedName = Roles.Admin.ToUpper(),
                ConcurrencyStamp = "9d9a88e9-5df4-4cc3-a9a4-0cf675f80475"
            },
            new Role
            {
                Id = "6f3a0cb0-f911-4110-a0ac-d97969b0bc59",
                Name = Roles.Teacher,
                NormalizedName = Roles.Teacher.ToUpper(),
                ConcurrencyStamp = "49d61614-d4dc-47d0-9d8e-dbe885213268"
            },
            new Role
            {
                Id = "7ef200ac-ef8c-4cdf-96af-4f076ea50db5",
                Name = Roles.ChurchLeader,
                NormalizedName = Roles.ChurchLeader.ToUpper(),
                ConcurrencyStamp = "2873935d-7b59-41a0-a031-4dae102e4a63"
            },
            new Role
            {
                Id = "ebecf4ec-24c2-4778-9e84-3f36c1d58ffd",
                Name = Roles.ChurchTeacher,
                NormalizedName = Roles.ChurchTeacher.ToUpper(),
                ConcurrencyStamp = "938923c8-d7e2-4480-b45a-4c6f8cf44d01"
            },
            new Role
            {
                Id = "868472b5-6a89-43d0-bc44-377133f3f3b6",
                Name = Roles.Sound,
                NormalizedName = Roles.Sound.ToUpper(),
                ConcurrencyStamp = "00b8a084-abfc-4886-a410-6acdadf60599"
            },
            new Role
            {
                Id = "0c04959c-6363-4709-a24c-45e4b995afce",
                Name = Roles.Student,
                NormalizedName = Roles.Student.ToUpper(),
                ConcurrencyStamp = "5b82514b-810d-416b-8306-b6e48fac12ca"
            }
        );
    }
}