namespace Intrepion.KlondikeSolitaire.BusinessLogic.Grid.Admin.StatusGrid;

// State of grid filters.
public class StatusGridControls(IPageHelper pageHelper) : IStatusFilters
{
    // Keep state of paging.
    public IPageHelper PageHelper { get; set; } = pageHelper;

    // Avoid multiple concurrent requests.
    public bool Loading { get; set; }

    // Column to sort by.
    public StatusFilterColumns SortColumn { get; set; } = StatusFilterColumns.Id;

    // True when sorting ascending, otherwise sort descending.
    public bool SortAscending { get; set; } = true;

    // Column filtered text is against.
    public StatusFilterColumns FilterColumn { get; set; } = StatusFilterColumns.Id;

    // Text to filter on.
    public string? FilterText { get; set; }
}
