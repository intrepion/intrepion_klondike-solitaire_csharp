namespace Intrepion.KlondikeSolitaire.BusinessLogic.Grid.Admin.CardFoundationGrid;

// State of grid filters.
public class CardFoundationGridControls(IPageHelper pageHelper) : ICardFoundationFilters
{
    // Keep state of paging.
    public IPageHelper PageHelper { get; set; } = pageHelper;

    // Avoid multiple concurrent requests.
    public bool Loading { get; set; }

    // Column to sort by.
    public CardFoundationFilterColumns SortColumn { get; set; } = CardFoundationFilterColumns.Id;

    // True when sorting ascending, otherwise sort descending.
    public bool SortAscending { get; set; } = true;

    // Column filtered text is against.
    public CardFoundationFilterColumns FilterColumn { get; set; } = CardFoundationFilterColumns.Id;

    // Text to filter on.
    public string? FilterText { get; set; }
}
