using System.ComponentModel.DataAnnotations;

namespace ApplicationNamePlaceholder.BusinessLogic.Entities;

public class Puzzle
{
    public ApplicationUser? ApplicationUserUpdatedBy { get; set; }
    public Guid Id { get; set; }

    public Player? CreatorId { get; set; }
    public string Description { get; set; } = string.Empty;
    public bool IsPublic { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateTime PublishTime { get; set; }
    public ICollection<CardStock> CardStocks { get; set; } = [];
    public ICollection<CardWaste> CardWastes { get; set; } = [];
    public ICollection<Foundation> Foundations { get; set; } = [];
    public ICollection<Game> Games { get; set; } = [];
    public ICollection<Tableau> Tableaus { get; set; } = [];
    // ActualPropertyPlaceholder
}
