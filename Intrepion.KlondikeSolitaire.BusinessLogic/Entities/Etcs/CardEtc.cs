using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Intrepion.KlondikeSolitaire.BusinessLogic.Entities.Configuration;

public class CardEtc : IEntityTypeConfiguration<Card>
{
    public void Configure(EntityTypeBuilder<Card> builder)
    {
        builder.ToTable("Cards", x => x.IsTemporal());

        builder.HasOne(x => x.ApplicationUserUpdatedBy)
            .WithMany(x => x.UpdatedCards)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Rank)
            .WithMany(x => x.Cards)
            .OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(x => x.Suit)
            .WithMany(x => x.Cards)
            .OnDelete(DeleteBehavior.Restrict);
        // EntityConfigurationCodePlaceholder
    }
}
