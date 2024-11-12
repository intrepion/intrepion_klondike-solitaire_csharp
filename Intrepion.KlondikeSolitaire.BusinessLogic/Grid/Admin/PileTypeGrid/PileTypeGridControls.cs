namespace Intrepion.KlondikeSolitaire.BusinessLogic.Grid.Admin.PileTypeGrid;

// State of grid filters.
public class PileTypeGridControls(IPageHelper pageHelper) : IPileTypeFilters
{
    // Keep state of paging.
    public IPageHelper PageHelper { get; set; } = pageHelper;

    // Avoid multiple concurrent requests.
    public bool Loading { get; set; }

    // Column to sort by.
    public PileTypeFilterColumns SortColumn { get; set; } = PileTypeFilterColumns.Id;

    // True when sorting ascending, otherwise sort descending.
    public bool SortAscending { get; set; } = true;

    // Column filtered text is against.
    public PileTypeFilterColumns FilterColumn { get; set; } = PileTypeFilterColumns.Id;

    // Text to filter on.
    public string? FilterText { get; set; }
}
