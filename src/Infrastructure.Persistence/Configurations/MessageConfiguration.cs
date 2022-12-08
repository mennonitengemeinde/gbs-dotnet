using Gbs.Application.Common.Entities;
using Gbs.Application.Entities;

namespace Gbs.Infrastructure.Persistence.Configurations;

public class MessageConfiguration : IEntityTypeConfiguration<Message>
{
    public void Configure(EntityTypeBuilder<Message> builder)
    {
        builder.Property(m => m.MessageType)
            .HasConversion<int>();

        builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(m => m.UserId);
    }
}