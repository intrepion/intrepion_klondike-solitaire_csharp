using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApplicationNamePlaceholder.BusinessLogic.Entities.Configuration;

public class CardFoundationEtc : IEntityTypeConfiguration<CardFoundation>
{
    public void Configure(EntityTypeBuilder<CardFoundation> builder)
    {
        builder.ToTable("CardFoundations", x => x.IsTemporal());

        builder.HasOne(x => x.ApplicationUserUpdatedBy)
            .WithMany(x => x.UpdatedCardFoundations)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Card)
            .WithMany(x => x.CardFoundations)
            .OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(x => x.Foundation)
            .WithMany(x => x.CardFoundations)
            .OnDelete(DeleteBehavior.Restrict);
        // EntityConfigurationCodePlaceholder
    }
}
