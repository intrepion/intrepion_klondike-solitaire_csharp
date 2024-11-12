using System.ComponentModel.DataAnnotations;

namespace Intrepion.KlondikeSolitaire.BusinessLogic.Entities;

public class CardFoundation
{
    public ApplicationUser? ApplicationUserUpdatedBy { get; set; }
    public Guid Id { get; set; }

    public Card? Card { get; set; }
    public Foundation? Foundation { get; set; }
    public int Ordering { get; set; }
    // ActualPropertyPlaceholder
}
