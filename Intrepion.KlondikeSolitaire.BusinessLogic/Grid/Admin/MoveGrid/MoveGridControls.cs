namespace Intrepion.KlondikeSolitaire.BusinessLogic.Grid.Admin.MoveGrid;

// State of grid filters.
public class MoveGridControls(IPageHelper pageHelper) : IMoveFilters
{
    // Keep state of paging.
    public IPageHelper PageHelper { get; set; } = pageHelper;

    // Avoid multiple concurrent requests.
    public bool Loading { get; set; }

    // Column to sort by.
    public MoveFilterColumns SortColumn { get; set; } = MoveFilterColumns.Id;

    // True when sorting ascending, otherwise sort descending.
    public bool SortAscending { get; set; } = true;

    // Column filtered text is against.
    public MoveFilterColumns FilterColumn { get; set; } = MoveFilterColumns.Id;

    // Text to filter on.
    public string? FilterText { get; set; }
}
