using System.ComponentModel.DataAnnotations;

namespace Intrepion.KlondikeSolitaire.BusinessLogic.Entities;

public class Tableau
{
    public ApplicationUser? ApplicationUserUpdatedBy { get; set; }
    public Guid Id { get; set; }

    public Game? GameId { get; set; }
    public int PileIndex { get; set; }
    public Puzzle? PuzzleId { get; set; }
    public ICollection<CardTableau> CardTableaus { get; set; } = [];
    // ActualPropertyPlaceholder
}
