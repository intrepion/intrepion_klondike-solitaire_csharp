namespace Intrepion.KlondikeSolitaire.BusinessLogic.Grid.Admin.PuzzleGrid;

// State of grid filters.
public class PuzzleGridControls(IPageHelper pageHelper) : IPuzzleFilters
{
    // Keep state of paging.
    public IPageHelper PageHelper { get; set; } = pageHelper;

    // Avoid multiple concurrent requests.
    public bool Loading { get; set; }

    // Column to sort by.
    public PuzzleFilterColumns SortColumn { get; set; } = PuzzleFilterColumns.Id;

    // True when sorting ascending, otherwise sort descending.
    public bool SortAscending { get; set; } = true;

    // Column filtered text is against.
    public PuzzleFilterColumns FilterColumn { get; set; } = PuzzleFilterColumns.Id;

    // Text to filter on.
    public string? FilterText { get; set; }
}
