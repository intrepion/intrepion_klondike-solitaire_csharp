using ApplicationNamePlaceholder.BusinessLogic.Entities;
using ApplicationNamePlaceholder.BusinessLogic.Entities.Configuration;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ApplicationNamePlaceholder.BusinessLogic.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser, ApplicationRole, Guid, ApplicationUserClaim, ApplicationUserRole, ApplicationUserLogin, ApplicationRoleClaim, ApplicationUserToken>(options)
{
    public DbSet<Card> Cards { get; set; }
    public DbSet<Game> Games { get; set; }
    public DbSet<Player> Players { get; set; }
    public DbSet<Rank> Ranks { get; set; }
    public DbSet<Suit> Suits { get; set; }
    // DbSetCodePlaceholder

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        new ApplicationRoleClaimEtc().Configure(builder.Entity<ApplicationRoleClaim>());
        new ApplicationRoleEtc().Configure(builder.Entity<ApplicationRole>());
        new ApplicationUserClaimEtc().Configure(builder.Entity<ApplicationUserClaim>());
        new ApplicationUserEtc().Configure(builder.Entity<ApplicationUser>());
        new ApplicationUserLoginEtc().Configure(builder.Entity<ApplicationUserLogin>());
        new ApplicationUserRoleEtc().Configure(builder.Entity<ApplicationUserRole>());
        new ApplicationUserTokenEtc().Configure(builder.Entity<ApplicationUserToken>());

        new CardEtc().Configure(builder.Entity<Card>());
        new GameEtc().Configure(builder.Entity<Game>());
        new PlayerEtc().Configure(builder.Entity<Player>());
        new RankEtc().Configure(builder.Entity<Rank>());
        // EntityTypeCfgCodePlaceholder
    }
}
