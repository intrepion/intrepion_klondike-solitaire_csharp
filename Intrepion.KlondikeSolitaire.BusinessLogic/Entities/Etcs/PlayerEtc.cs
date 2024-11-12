using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Intrepion.KlondikeSolitaire.BusinessLogic.Entities.Configuration;

public class PlayerEtc : IEntityTypeConfiguration<Player>
{
    public void Configure(EntityTypeBuilder<Player> builder)
    {
        builder.ToTable("Players", x => x.IsTemporal());

        builder.HasOne(x => x.ApplicationUserUpdatedBy)
            .WithMany(x => x.UpdatedPlayers)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.ApplicationUserId)
            .WithMany(x => x.Player)
            .OnDelete(DeleteBehavior.Restrict);
        // EntityConfigurationCodePlaceholder
    }
}
