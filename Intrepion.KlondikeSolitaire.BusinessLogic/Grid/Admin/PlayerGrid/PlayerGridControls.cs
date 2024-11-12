namespace ApplicationNamePlaceholder.BusinessLogic.Grid.Admin.EntityNamePlaceholderGrid;

// State of grid filters.
public class EntityNamePlaceholderGridControls(IPageHelper pageHelper) : IEntityNamePlaceholderFilters
{
    // Keep state of paging.
    public IPageHelper PageHelper { get; set; } = pageHelper;

    // Avoid multiple concurrent requests.
    public bool Loading { get; set; }

    // Column to sort by.
    public EntityNamePlaceholderFilterColumns SortColumn { get; set; } = EntityNamePlaceholderFilterColumns.Id;

    // True when sorting ascending, otherwise sort descending.
    public bool SortAscending { get; set; } = true;

    // Column filtered text is against.
    public EntityNamePlaceholderFilterColumns FilterColumn { get; set; } = EntityNamePlaceholderFilterColumns.Id;

    // Text to filter on.
    public string? FilterText { get; set; }
}
