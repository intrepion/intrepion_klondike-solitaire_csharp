﻿using Intrepion.KlondikeSolitaire.BusinessLogic.Entities;
using Intrepion.KlondikeSolitaire.BusinessLogic.Entities.Configuration;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Intrepion.KlondikeSolitaire.BusinessLogic.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser, ApplicationRole, Guid, ApplicationUserClaim, ApplicationUserRole, ApplicationUserLogin, ApplicationRoleClaim, ApplicationUserToken>(options)
{
    public DbSet<Card> Cards { get; set; }
    public DbSet<CardFoundation> CardFoundations { get; set; }
    public DbSet<CardStock> CardStocks { get; set; }
    public DbSet<CardTableau> CardTableaus { get; set; }
    public DbSet<CardWaste> CardWastes { get; set; }
    public DbSet<Foundation> Foundations { get; set; }
    public DbSet<Game> Games { get; set; }
    public DbSet<Move> Moves { get; set; }
    public DbSet<PileType> PileTypes { get; set; }
    public DbSet<Player> Players { get; set; }
    public DbSet<Puzzle> Puzzles { get; set; }
    public DbSet<Rank> Ranks { get; set; }
    public DbSet<Status> Statuses { get; set; }
    public DbSet<Suit> Suits { get; set; }
    public DbSet<Tableau> Tableaus { get; set; }
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
        new CardFoundationEtc().Configure(builder.Entity<CardFoundation>());
        new CardStockEtc().Configure(builder.Entity<CardStock>());
        new CardTableauEtc().Configure(builder.Entity<CardTableau>());
        new CardWasteEtc().Configure(builder.Entity<CardWaste>());
        new FoundationEtc().Configure(builder.Entity<Foundation>());
        new GameEtc().Configure(builder.Entity<Game>());
        new MoveEtc().Configure(builder.Entity<Move>());
        new PileTypeEtc().Configure(builder.Entity<PileType>());
        new PlayerEtc().Configure(builder.Entity<Player>());
        new PuzzleEtc().Configure(builder.Entity<Puzzle>());
        new RankEtc().Configure(builder.Entity<Rank>());
        new StatusEtc().Configure(builder.Entity<Status>());
        new SuitEtc().Configure(builder.Entity<Suit>());
        new TableauEtc().Configure(builder.Entity<Tableau>());
        // EntityTypeCfgCodePlaceholder
    }
}
