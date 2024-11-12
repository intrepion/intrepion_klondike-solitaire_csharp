using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApplicationNamePlaceholder.BusinessLogic.Entities.Configuration;

public class MoveEtc : IEntityTypeConfiguration<Move>
{
    public void Configure(EntityTypeBuilder<Move> builder)
    {
        builder.ToTable("Moves", x => x.IsTemporal());

        builder.HasOne(x => x.ApplicationUserUpdatedBy)
            .WithMany(x => x.UpdatedMoves)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Card1Id)
            .WithMany(x => x.MovesAsCard1)
            .OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(x => x.Card2Id)
            .WithMany(x => x.MovesAsCard2)
            .OnDelete(DeleteBehavior.Restrict);
        // EntityConfigurationCodePlaceholder
    }
}
