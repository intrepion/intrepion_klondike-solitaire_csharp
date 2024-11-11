using System.ComponentModel.DataAnnotations;

namespace ApplicationNamePlaceholder.BusinessLogic.Entities;

public class Player
{
    public ApplicationUser? ApplicationUserUpdatedBy { get; set; }
    public Guid Id { get; set; }

    public ApplicationUser? ApplicationUser { get; set; }
    public ICollection<Game> Games { get; set; } = [];
    // ActualPropertyPlaceholder
}
