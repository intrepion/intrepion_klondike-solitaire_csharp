using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApplicationNamePlaceholder.BusinessLogic.Entities.Configuration;

public class CardWasteEtc : IEntityTypeConfiguration<CardWaste>
{
    public void Configure(EntityTypeBuilder<CardWaste> builder)
    {
        builder.ToTable("CardWastes", x => x.IsTemporal());

        builder.HasOne(x => x.ApplicationUserUpdatedBy)
            .WithMany(x => x.UpdatedCardWastes)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.CardId)
            .WithMany(x => x.CardWastes)
            .OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(x => x.GameId)
            .WithMany(x => x.CardWastes)
            .OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(x => x.PuzzleId)
            .WithMany(x => x.CardWastes)
            .OnDelete(DeleteBehavior.Restrict);
        // EntityConfigurationCodePlaceholder
    }
}
