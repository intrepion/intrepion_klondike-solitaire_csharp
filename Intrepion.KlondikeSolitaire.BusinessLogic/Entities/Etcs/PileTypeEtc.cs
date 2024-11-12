using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Intrepion.KlondikeSolitaire.BusinessLogic.Entities.Configuration;

public class PileTypeEtc : IEntityTypeConfiguration<PileType>
{
    public void Configure(EntityTypeBuilder<PileType> builder)
    {
        builder.ToTable("PileTypes", x => x.IsTemporal());

        builder.HasOne(x => x.ApplicationUserUpdatedBy)
            .WithMany(x => x.UpdatedPileTypes)
            .OnDelete(DeleteBehavior.Restrict);

        // EntityConfigurationCodePlaceholder
    }
}
