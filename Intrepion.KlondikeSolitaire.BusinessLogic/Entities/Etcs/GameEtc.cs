using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApplicationNamePlaceholder.BusinessLogic.Entities.Configuration;

public class GameEtc : IEntityTypeConfiguration<Game>
{
    public void Configure(EntityTypeBuilder<Game> builder)
    {
        builder.ToTable("Games", x => x.IsTemporal());

        builder.HasOne(x => x.ApplicationUserUpdatedBy)
            .WithMany(x => x.UpdatedGames)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.PlayerId)
            .WithMany(x => x.Games)
            .OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(x => x.PuzzleId)
            .WithMany(x => x.Games)
            .OnDelete(DeleteBehavior.Restrict);
        // EntityConfigurationCodePlaceholder
    }
}
