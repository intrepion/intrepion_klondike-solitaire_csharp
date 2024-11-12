using System.ComponentModel.DataAnnotations;

namespace Intrepion.KlondikeSolitaire.BusinessLogic.Entities;

public class Player
{
    public ApplicationUser? ApplicationUserUpdatedBy { get; set; }
    public Guid Id { get; set; }

    public ApplicationUser? ApplicationUserId { get; set; }
    public ICollection<Game> Games { get; set; } = [];
    public ICollection<Puzzle> Puzzles { get; set; } = [];
    // ActualPropertyPlaceholder
}
