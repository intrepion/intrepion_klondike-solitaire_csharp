using System.ComponentModel.DataAnnotations;

namespace ApplicationNamePlaceholder.BusinessLogic.Entities;

public class CardTableau
{
    public ApplicationUser? ApplicationUserUpdatedBy { get; set; }
    public Guid Id { get; set; }

    public Card? CardId { get; set; }
    public Tableau? TableauId { get; set; }
    // ActualPropertyPlaceholder
}
