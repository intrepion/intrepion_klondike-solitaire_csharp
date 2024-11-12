using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Intrepion.KlondikeSolitaire.BusinessLogic.Entities.Configuration;

public class FoundationEtc : IEntityTypeConfiguration<Foundation>
{
    public void Configure(EntityTypeBuilder<Foundation> builder)
    {
        builder.ToTable("Foundations", x => x.IsTemporal());

        builder.HasOne(x => x.ApplicationUserUpdatedBy)
            .WithMany(x => x.UpdatedFoundations)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.GameId)
            .WithMany(x => x.Foundations)
            .OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(x => x.PuzzleId)
            .WithMany(x => x.Foundations)
            .OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(x => x.SuitId)
            .WithMany(x => x.Foundations)
            .OnDelete(DeleteBehavior.Restrict);
        // EntityConfigurationCodePlaceholder
    }
}
