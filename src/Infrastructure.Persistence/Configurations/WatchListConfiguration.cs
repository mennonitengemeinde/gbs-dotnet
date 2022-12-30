using Gbs.Application.Entities;

namespace Gbs.Infrastructure.Persistence.Configurations;

public class WatchListConfiguration : IEntityTypeConfiguration<WatchList>
{
    public void Configure(EntityTypeBuilder<WatchList> builder)
    {
        builder.HasKey(wl => new { wl.UserId, wl.QuestionId });
    }
}