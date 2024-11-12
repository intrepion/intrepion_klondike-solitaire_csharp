namespace Intrepion.KlondikeSolitaire.BusinessLogic.Grid.Admin.FoundationGrid;

// State of grid filters.
public class FoundationGridControls(IPageHelper pageHelper) : IFoundationFilters
{
    // Keep state of paging.
    public IPageHelper PageHelper { get; set; } = pageHelper;

    // Avoid multiple concurrent requests.
    public bool Loading { get; set; }

    // Column to sort by.
    public FoundationFilterColumns SortColumn { get; set; } = FoundationFilterColumns.Id;

    // True when sorting ascending, otherwise sort descending.
    public bool SortAscending { get; set; } = true;

    // Column filtered text is against.
    public FoundationFilterColumns FilterColumn { get; set; } = FoundationFilterColumns.Id;

    // Text to filter on.
    public string? FilterText { get; set; }
}
