using System.ComponentModel.DataAnnotations;

namespace Intrepion.KlondikeSolitaire.BusinessLogic.Entities;

public class Foundation
{
    public ApplicationUser? ApplicationUserUpdatedBy { get; set; }
    public Guid Id { get; set; }

    public Game? GameId { get; set; }
    public int PileIndex { get; set; }
    public Puzzle? PuzzleId { get; set; }
    public Suit? SuitId { get; set; }
    public ICollection<CardFoundation> CardFoundations { get; set; } = [];
    // ActualPropertyPlaceholder
}
