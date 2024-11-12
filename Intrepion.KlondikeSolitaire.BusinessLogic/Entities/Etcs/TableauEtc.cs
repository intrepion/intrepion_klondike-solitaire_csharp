using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApplicationNamePlaceholder.BusinessLogic.Entities.Configuration;

public class TableauEtc : IEntityTypeConfiguration<Tableau>
{
    public void Configure(EntityTypeBuilder<Tableau> builder)
    {
        builder.ToTable("Tableaus", x => x.IsTemporal());

        builder.HasOne(x => x.ApplicationUserUpdatedBy)
            .WithMany(x => x.UpdatedTableaus)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.GameId)
            .WithMany(x => x.Tableaus)
            .OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(x => x.PuzzleId)
            .WithMany(x => x.Tableaus)
            .OnDelete(DeleteBehavior.Restrict);
        // EntityConfigurationCodePlaceholder
    }
}
