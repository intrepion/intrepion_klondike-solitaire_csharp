using System.ComponentModel.DataAnnotations;

namespace Intrepion.KlondikeSolitaire.BusinessLogic.Entities;

public class Game
{
    public ApplicationUser? ApplicationUserUpdatedBy { get; set; }
    public Guid Id { get; set; }

    public DateTime EndTime { get; set; }
    public Player? PlayerId { get; set; }
    public Puzzle? PuzzleId { get; set; }
    public DateTime StartTime { get; set; }
    public Status? StatusId { get; set; }
    public ICollection<CardStock> CardStocks { get; set; } = [];
    public ICollection<CardWaste> CardWastes { get; set; } = [];
    public ICollection<Foundation> Foundations { get; set; } = [];
    public ICollection<Move> Moves { get; set; } = [];
    public ICollection<Tableau> Tableaus { get; set; } = [];
    // ActualPropertyPlaceholder
}
