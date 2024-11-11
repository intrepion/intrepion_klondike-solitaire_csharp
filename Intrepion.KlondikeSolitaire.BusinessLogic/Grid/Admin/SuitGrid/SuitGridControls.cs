namespace Intrepion.KlondikeSolitaire.BusinessLogic.Grid.Admin.SuitGrid;

// State of grid filters.
public class SuitGridControls(IPageHelper pageHelper) : ISuitFilters
{
    // Keep state of paging.
    public IPageHelper PageHelper { get; set; } = pageHelper;

    // Avoid multiple concurrent requests.
    public bool Loading { get; set; }

    // Column to sort by.
    public SuitFilterColumns SortColumn { get; set; } = SuitFilterColumns.Id;

    // True when sorting ascending, otherwise sort descending.
    public bool SortAscending { get; set; } = true;

    // Column filtered text is against.
    public SuitFilterColumns FilterColumn { get; set; } = SuitFilterColumns.Id;

    // Text to filter on.
    public string? FilterText { get; set; }
}
