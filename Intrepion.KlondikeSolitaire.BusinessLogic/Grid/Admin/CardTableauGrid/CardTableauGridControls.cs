namespace Intrepion.KlondikeSolitaire.BusinessLogic.Grid.Admin.CardTableauGrid;

// State of grid filters.
public class CardTableauGridControls(IPageHelper pageHelper) : ICardTableauFilters
{
    // Keep state of paging.
    public IPageHelper PageHelper { get; set; } = pageHelper;

    // Avoid multiple concurrent requests.
    public bool Loading { get; set; }

    // Column to sort by.
    public CardTableauFilterColumns SortColumn { get; set; } = CardTableauFilterColumns.Id;

    // True when sorting ascending, otherwise sort descending.
    public bool SortAscending { get; set; } = true;

    // Column filtered text is against.
    public CardTableauFilterColumns FilterColumn { get; set; } = CardTableauFilterColumns.Id;

    // Text to filter on.
    public string? FilterText { get; set; }
}
