namespace Intrepion.KlondikeSolitaire.BusinessLogic.Grid.Admin.PlayerGrid;

// State of grid filters.
public class PlayerGridControls(IPageHelper pageHelper) : IPlayerFilters
{
    // Keep state of paging.
    public IPageHelper PageHelper { get; set; } = pageHelper;

    // Avoid multiple concurrent requests.
    public bool Loading { get; set; }

    // Column to sort by.
    public PlayerFilterColumns SortColumn { get; set; } = PlayerFilterColumns.Id;

    // True when sorting ascending, otherwise sort descending.
    public bool SortAscending { get; set; } = true;

    // Column filtered text is against.
    public PlayerFilterColumns FilterColumn { get; set; } = PlayerFilterColumns.Id;

    // Text to filter on.
    public string? FilterText { get; set; }
}
