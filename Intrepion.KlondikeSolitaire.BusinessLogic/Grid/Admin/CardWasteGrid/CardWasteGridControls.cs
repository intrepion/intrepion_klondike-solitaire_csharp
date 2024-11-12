namespace Intrepion.KlondikeSolitaire.BusinessLogic.Grid.Admin.CardWasteGrid;

// State of grid filters.
public class CardWasteGridControls(IPageHelper pageHelper) : ICardWasteFilters
{
    // Keep state of paging.
    public IPageHelper PageHelper { get; set; } = pageHelper;

    // Avoid multiple concurrent requests.
    public bool Loading { get; set; }

    // Column to sort by.
    public CardWasteFilterColumns SortColumn { get; set; } = CardWasteFilterColumns.Id;

    // True when sorting ascending, otherwise sort descending.
    public bool SortAscending { get; set; } = true;

    // Column filtered text is against.
    public CardWasteFilterColumns FilterColumn { get; set; } = CardWasteFilterColumns.Id;

    // Text to filter on.
    public string? FilterText { get; set; }
}
