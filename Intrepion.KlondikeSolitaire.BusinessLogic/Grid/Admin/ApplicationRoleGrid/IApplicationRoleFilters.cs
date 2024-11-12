namespace ApplicationNamePlaceholder.BusinessLogic.Grid.Admin.ApplicationRoleGrid;

// Interface for filtering.
public interface IApplicationRoleFilters
{
    // The ApplicationRoleFilterColumns being filtered on.
    ApplicationRoleFilterColumns FilterColumn { get; set; }

    // Loading indicator.
    bool Loading { get; set; }

    // The text of the filter.
    string? FilterText { get; set; }

    // Paging state in PageHelper.
    IPageHelper PageHelper { get; set; }

    // Gets or sets a value indicating if the sort is ascending or descending.
    bool SortAscending { get; set; }

    // The ApplicationRoleFilterColumns being sorted.
    ApplicationRoleFilterColumns SortColumn { get; set; }
}
