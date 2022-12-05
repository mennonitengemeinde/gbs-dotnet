using Gbs.Application.Common.Entities;
using Gbs.Domain.Entities;
using Gbs.Shared.Common.Enums;

namespace Gbs.Infrastructure.Persistence.Configurations;

public class StudentConfiguration : IEntityTypeConfiguration<Student>
{
    public void Configure(EntityTypeBuilder<Student> builder)
    {
        builder.Property(s => s.FirstName)
            .HasMaxLength(100)
            .IsRequired();
        
        builder.Property(s => s.LastName)
            .HasMaxLength(100)
            .IsRequired();
        
        builder.Property(s => s.DateOfBirth)
            .HasColumnType("timestamp without time zone")
            .IsRequired();

        builder.Property(s => s.Address)
            .HasMaxLength(100);
        
        builder.Property(s => s.Province)
            .HasMaxLength(50)
            .IsRequired();
        
        builder.Property(s => s.Country)
            .HasMaxLength(50)
            .IsRequired();
        
        builder.Property(s => s.PostalCode)
            .HasMaxLength(10);
        
        builder.Property(s => s.MaritalStatus)
            .HasConversion<int>();
        
        builder.Property(s => s.Email)
            .HasMaxLength(100);
        
        builder.Property(s => s.Phone)
            .HasMaxLength(50);

        builder.Property(s => s.EnrollmentStatus)
            .HasConversion<int>()
            .HasDefaultValue(EnrollmentState.Active);

        builder.HasOne(s => s.Church)
            .WithMany(c => c.Students)
            .HasForeignKey(s => s.ChurchId)
            .IsRequired();

        builder.HasOne(s => s.Generation)
            .WithMany(g => g.Students)
            .HasForeignKey(s => s.GenerationId)
            .IsRequired();

        builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(s => s.UserId);
    }
}