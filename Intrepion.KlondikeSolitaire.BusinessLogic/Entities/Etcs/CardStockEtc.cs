using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApplicationNamePlaceholder.BusinessLogic.Entities.Configuration;

public class CardStockEtc : IEntityTypeConfiguration<CardStock>
{
    public void Configure(EntityTypeBuilder<CardStock> builder)
    {
        builder.ToTable("CardStocks", x => x.IsTemporal());

        builder.HasOne(x => x.ApplicationUserUpdatedBy)
            .WithMany(x => x.UpdatedCardStocks)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.CardId)
            .WithMany(x => x.CardStocks)
            .OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(x => x.GameId)
            .WithMany(x => x.CardStocks)
            .OnDelete(DeleteBehavior.Restrict);
        // EntityConfigurationCodePlaceholder
    }
}
