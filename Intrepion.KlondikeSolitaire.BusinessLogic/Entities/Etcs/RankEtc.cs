using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Intrepion.KlondikeSolitaire.BusinessLogic.Entities.Configuration;

public class RankEtc : IEntityTypeConfiguration<Rank>
{
    public void Configure(EntityTypeBuilder<Rank> builder)
    {
        builder.ToTable("Ranks", x => x.IsTemporal());

        builder.HasOne(x => x.ApplicationUserUpdatedBy)
            .WithMany(x => x.UpdatedRanks)
            .OnDelete(DeleteBehavior.Restrict);

        // EntityConfigurationCodePlaceholder
    }
}
