using System.ComponentModel.DataAnnotations;

namespace Intrepion.KlondikeSolitaire.BusinessLogic.Entities;

public class CardTableau
{
    public ApplicationUser? ApplicationUserUpdatedBy { get; set; }
    public Guid Id { get; set; }

    public Card? CardId { get; set; }
    public Tableau? TableauId { get; set; }
    public int Ordering { get; set; }
    // ActualPropertyPlaceholder
}
