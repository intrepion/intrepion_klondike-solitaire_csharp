namespace Intrepion.KlondikeSolitaire.BusinessLogic.Grid.Admin.CardStockGrid;

// State of grid filters.
public class CardStockGridControls(IPageHelper pageHelper) : ICardStockFilters
{
    // Keep state of paging.
    public IPageHelper PageHelper { get; set; } = pageHelper;

    // Avoid multiple concurrent requests.
    public bool Loading { get; set; }

    // Column to sort by.
    public CardStockFilterColumns SortColumn { get; set; } = CardStockFilterColumns.Id;

    // True when sorting ascending, otherwise sort descending.
    public bool SortAscending { get; set; } = true;

    // Column filtered text is against.
    public CardStockFilterColumns FilterColumn { get; set; } = CardStockFilterColumns.Id;

    // Text to filter on.
    public string? FilterText { get; set; }
}
