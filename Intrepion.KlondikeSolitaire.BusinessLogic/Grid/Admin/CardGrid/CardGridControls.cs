namespace Intrepion.KlondikeSolitaire.BusinessLogic.Grid.Admin.CardGrid;

// State of grid filters.
public class CardGridControls(IPageHelper pageHelper) : ICardFilters
{
    // Keep state of paging.
    public IPageHelper PageHelper { get; set; } = pageHelper;

    // Avoid multiple concurrent requests.
    public bool Loading { get; set; }

    // Column to sort by.
    public CardFilterColumns SortColumn { get; set; } = CardFilterColumns.Id;

    // True when sorting ascending, otherwise sort descending.
    public bool SortAscending { get; set; } = true;

    // Column filtered text is against.
    public CardFilterColumns FilterColumn { get; set; } = CardFilterColumns.Id;

    // Text to filter on.
    public string? FilterText { get; set; }
}
