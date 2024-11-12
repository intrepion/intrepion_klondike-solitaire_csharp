using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApplicationNamePlaceholder.BusinessLogic.Entities.Configuration;

public class CardTableauEtc : IEntityTypeConfiguration<CardTableau>
{
    public void Configure(EntityTypeBuilder<CardTableau> builder)
    {
        builder.ToTable("CardTableaus", x => x.IsTemporal());

        builder.HasOne(x => x.ApplicationUserUpdatedBy)
            .WithMany(x => x.UpdatedCardTableaus)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.CardId)
            .WithMany(x => x.CardTableaus)
            .OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(x => x.TableauId)
            .WithMany(x => x.CardTableaus)
            .OnDelete(DeleteBehavior.Restrict);
        // EntityConfigurationCodePlaceholder
    }
}
