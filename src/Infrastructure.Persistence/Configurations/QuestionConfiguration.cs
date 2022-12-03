using Gbs.Application.Common.Entities;
using Gbs.Domain.Entities;

namespace Gbs.Infrastructure.Persistence.Configurations;

public class QuestionConfiguration : IEntityTypeConfiguration<Question>
{
    public void Configure(EntityTypeBuilder<Question> builder)
    {
        builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(q => q.UserId);
    }
}