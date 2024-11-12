using System.ComponentModel.DataAnnotations;

namespace ApplicationNamePlaceholder.BusinessLogic.Entities;

public class Move
{
    public ApplicationUser? ApplicationUserUpdatedBy { get; set; }
    public Guid Id { get; set; }

    public Card? Card1Id { get; set; }
    public Card? Card2Id { get; set; }
    public Card? Card3Id { get; set; }
    // ActualPropertyPlaceholder
}
