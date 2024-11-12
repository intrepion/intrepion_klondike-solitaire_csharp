namespace ApplicationNamePlaceholder.BusinessLogic.Grid.Admin.ApplicationUserGrid;

// State of grid filters.
public class ApplicationUserGridControls(IPageHelper pageHelper) : IApplicationUserFilters
{
    // Keep state of paging.
    public IPageHelper PageHelper { get; set; } = pageHelper;

    // Avoid multiple concurrent requests.
    public bool Loading { get; set; }

    // Firstname Lastname, or Lastname, Firstname.
    public bool ShowUserNameFirst { get; set; }

    // Column to sort by.
    public ApplicationUserFilterColumns SortColumn { get; set; } = ApplicationUserFilterColumns.UserName;

    // True when sorting ascending, otherwise sort descending.
    public bool SortAscending { get; set; } = true;

    // Column filtered text is against.
    public ApplicationUserFilterColumns FilterColumn { get; set; } = ApplicationUserFilterColumns.UserName;

    // Text to filter on.
    public string? FilterText { get; set; }
}
