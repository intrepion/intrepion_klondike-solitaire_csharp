namespace ApplicationNamePlaceholder.BusinessLogic.Grid.Admin.ApplicationRoleGrid;

// State of grid filters.
public class ApplicationRoleGridControls(IPageHelper pageHelper) : IApplicationRoleFilters
{
    // Keep state of paging.
    public IPageHelper PageHelper { get; set; } = pageHelper;

    // Avoid multiple concurrent requests.
    public bool Loading { get; set; }

    // Column to sort by.
    public ApplicationRoleFilterColumns SortColumn { get; set; } = ApplicationRoleFilterColumns.Name;

    // True when sorting ascending, otherwise sort descending.
    public bool SortAscending { get; set; } = true;

    // Column filtered text is against.
    public ApplicationRoleFilterColumns FilterColumn { get; set; } = ApplicationRoleFilterColumns.Name;

    // Text to filter on.
    public string? FilterText { get; set; }
}
