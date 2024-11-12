namespace Intrepion.KlondikeSolitaire.BusinessLogic.Grid.Admin.GameGrid;

// State of grid filters.
public class GameGridControls(IPageHelper pageHelper) : IGameFilters
{
    // Keep state of paging.
    public IPageHelper PageHelper { get; set; } = pageHelper;

    // Avoid multiple concurrent requests.
    public bool Loading { get; set; }

    // Column to sort by.
    public GameFilterColumns SortColumn { get; set; } = GameFilterColumns.Id;

    // True when sorting ascending, otherwise sort descending.
    public bool SortAscending { get; set; } = true;

    // Column filtered text is against.
    public GameFilterColumns FilterColumn { get; set; } = GameFilterColumns.Id;

    // Text to filter on.
    public string? FilterText { get; set; }
}
