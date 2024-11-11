namespace Intrepion.KlondikeSolitaire.BusinessLogic.Grid.Admin.RankGrid;

// State of grid filters.
public class RankGridControls(IPageHelper pageHelper) : IRankFilters
{
    // Keep state of paging.
    public IPageHelper PageHelper { get; set; } = pageHelper;

    // Avoid multiple concurrent requests.
    public bool Loading { get; set; }

    // Column to sort by.
    public RankFilterColumns SortColumn { get; set; } = RankFilterColumns.Id;

    // True when sorting ascending, otherwise sort descending.
    public bool SortAscending { get; set; } = true;

    // Column filtered text is against.
    public RankFilterColumns FilterColumn { get; set; } = RankFilterColumns.Id;

    // Text to filter on.
    public string? FilterText { get; set; }
}
