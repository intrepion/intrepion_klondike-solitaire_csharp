using System.ComponentModel.DataAnnotations;

namespace ApplicationNamePlaceholder.BusinessLogic.Entities;

public class CardStock
{
    public ApplicationUser? ApplicationUserUpdatedBy { get; set; }
    public Guid Id { get; set; }

    public Card? CardId { get; set; }
    public Game? GameId { get; set; }
    public Puzzle? PuzzleId { get; set; }
    public int Ordering { get; set; }
    // ActualPropertyPlaceholder
}
