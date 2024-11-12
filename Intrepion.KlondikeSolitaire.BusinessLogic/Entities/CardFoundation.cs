using System.ComponentModel.DataAnnotations;

namespace ApplicationNamePlaceholder.BusinessLogic.Entities;

public class CardFoundation
{
    public ApplicationUser? ApplicationUserUpdatedBy { get; set; }
    public Guid Id { get; set; }

    public Card? Card { get; set; }
    public Foundation? Foundation { get; set; }
    // ActualPropertyPlaceholder
}
