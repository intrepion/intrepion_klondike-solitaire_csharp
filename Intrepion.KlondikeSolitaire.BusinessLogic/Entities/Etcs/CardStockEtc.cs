using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApplicationNamePlaceholder.BusinessLogic.Entities.Configuration;

public class CardStockEtc : IEntityTypeConfiguration<CardStock>
{
    public void Configure(EntityTypeBuilder<CardStock> builder)
    {
        builder.ToTable("TableNamePlaceholder", x => x.IsTemporal());

        builder.HasOne(x => x.ApplicationUserUpdatedBy)
            .WithMany(x => x.UpdatedTableNamePlaceholder)
            .OnDelete(DeleteBehavior.Restrict);

        // EntityConfigurationCodePlaceholder
    }
}
