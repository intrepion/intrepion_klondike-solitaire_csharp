using System.ComponentModel.DataAnnotations;

namespace ApplicationNamePlaceholder.BusinessLogic.Entities;

public class Game
{
    public ApplicationUser? ApplicationUserUpdatedBy { get; set; }
    public Guid Id { get; set; }

    public DateTime EndTime { get; set; }
    public Player? PlayerId { get; set; }
    public Puzzle? PuzzleId { get; set; }
    public DateTime StartTime { get; set; }
    public Status? StatusId { get; set; }
    // ActualPropertyPlaceholder
}
