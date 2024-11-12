namespace Intrepion.KlondikeSolitaire.BusinessLogic.Grid.Admin.TableauGrid;

// State of grid filters.
public class TableauGridControls(IPageHelper pageHelper) : ITableauFilters
{
    // Keep state of paging.
    public IPageHelper PageHelper { get; set; } = pageHelper;

    // Avoid multiple concurrent requests.
    public bool Loading { get; set; }

    // Column to sort by.
    public TableauFilterColumns SortColumn { get; set; } = TableauFilterColumns.Id;

    // True when sorting ascending, otherwise sort descending.
    public bool SortAscending { get; set; } = true;

    // Column filtered text is against.
    public TableauFilterColumns FilterColumn { get; set; } = TableauFilterColumns.Id;

    // Text to filter on.
    public string? FilterText { get; set; }
}
