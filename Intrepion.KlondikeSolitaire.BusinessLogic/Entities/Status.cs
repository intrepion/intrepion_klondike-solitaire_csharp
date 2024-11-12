using System.ComponentModel.DataAnnotations;

namespace Intrepion.KlondikeSolitaire.BusinessLogic.Entities;

public class Status
{
    public ApplicationUser? ApplicationUserUpdatedBy { get; set; }
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;
    public ICollection<Game> Games { get; set; } = [];
    // ActualPropertyPlaceholder
}
