namespace ApplicationNamePlaceholder.BusinessLogic.Grid.Admin.ApplicationUserGrid;

// Interface for filtering.
public interface IApplicationUserFilters
{
    // The ApplicationUserFilterColumns being filtered on.
    ApplicationUserFilterColumns FilterColumn { get; set; }

    // Loading indicator.
    bool Loading { get; set; }

    // The text of the filter.
    string? FilterText { get; set; }

    // Paging state in PageHelper.
    IPageHelper PageHelper { get; set; }

    // Gets or sets a value indicating if the name is rendered first name first.
    bool ShowUserNameFirst { get; set; }

    // Gets or sets a value indicating if the sort is ascending or descending.
    bool SortAscending { get; set; }

    // The ApplicationUserFilterColumns being sorted.
    ApplicationUserFilterColumns SortColumn { get; set; }
}
