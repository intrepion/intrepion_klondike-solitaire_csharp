using System.ComponentModel.DataAnnotations;

namespace ApplicationNamePlaceholder.BusinessLogic.Entities;

public class Move
{
    public ApplicationUser? ApplicationUserUpdatedBy { get; set; }
    public Guid Id { get; set; }

    public Card? Card1Id { get; set; }
    public Card? Card2Id { get; set; }
    public Card? Card3Id { get; set; }
    public int FromPileIndex { get; set; }
    public PileType? FromPileTypeId { get; set; }
    public Game? GameId { get; set; }
    public DateTime MoveTime { get; set; }
    public int ToPileIndex { get; set; }
    public PileType? ToPileTypeId { get; set; }
    // ActualPropertyPlaceholder
}
