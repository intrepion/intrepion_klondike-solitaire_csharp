using System.ComponentModel.DataAnnotations;

namespace ApplicationNamePlaceholder.BusinessLogic.Entities;

public class Card
{
    public ApplicationUser? ApplicationUserUpdatedBy { get; set; }
    public Guid Id { get; set; }

    public Rank? Rank { get; set; }
    public Suit? Suit { get; set; }
    // ActualPropertyPlaceholder
}
