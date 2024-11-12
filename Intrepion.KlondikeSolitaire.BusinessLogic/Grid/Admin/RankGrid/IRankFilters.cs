namespace ApplicationNamePlaceholder.BusinessLogic.Grid.Admin.EntityNamePlaceholderGrid;

// Interface for filtering.
public interface IEntityNamePlaceholderFilters
{
    // The EntityNamePlaceholderFilterColumns being filtered on.
    EntityNamePlaceholderFilterColumns FilterColumn { get; set; }

    // Loading indicator.
    bool Loading { get; set; }

    // The text of the filter.
    string? FilterText { get; set; }

    // Paging state in PageHelper.
    IPageHelper PageHelper { get; set; }

    // Gets or sets a value indicating if the sort is ascending or descending.
    bool SortAscending { get; set; }

    // The EntityNamePlaceholderFilterColumns being sorted.
    EntityNamePlaceholderFilterColumns SortColumn { get; set; }
}
