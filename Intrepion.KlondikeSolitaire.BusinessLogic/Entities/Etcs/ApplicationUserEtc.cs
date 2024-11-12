using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Intrepion.KlondikeSolitaire.BusinessLogic.Entities.Configuration;

public class ApplicationUserEtc : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.ToTable("AspNetUsers", x => x.IsTemporal());

        builder.HasOne(x => x.ApplicationUserUpdatedBy)
            .WithMany(x => x.UpdatedApplicationUsers)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(x => x.Player)
            .WithOne(x => x.ApplicationUserId)
            .OnDelete(DeleteBehavior.Restrict);
        // EntityConfigurationCodePlaceholder
    }
}
