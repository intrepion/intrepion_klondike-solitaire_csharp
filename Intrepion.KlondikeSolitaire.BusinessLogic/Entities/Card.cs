using System.ComponentModel.DataAnnotations;

namespace ApplicationNamePlaceholder.BusinessLogic.Entities;

public class Card
{
    public ApplicationUser? ApplicationUserUpdatedBy { get; set; }
    public Guid Id { get; set; }

    public Rank? RankId { get; set; }
    public Suit? SuitId { get; set; }
    public ICollection<CardFoundation> CardFoundations { get; set; } = [];
    public ICollection<CardStock> CardStocks { get; set; } = [];
    public ICollection<CardTableau> CardTableaus { get; set; } = [];
    // ActualPropertyPlaceholder
}
