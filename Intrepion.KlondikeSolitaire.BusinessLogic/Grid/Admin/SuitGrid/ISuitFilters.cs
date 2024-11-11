namespace Intrepion.KlondikeSolitaire.BusinessLogic.Grid.Admin.SuitGrid;

// Interface for filtering.
public interface ISuitFilters
{
    // The SuitFilterColumns being filtered on.
    SuitFilterColumns FilterColumn { get; set; }

    // Loading indicator.
    bool Loading { get; set; }

    // The text of the filter.
    string? FilterText { get; set; }

    // Paging state in PageHelper.
    IPageHelper PageHelper { get; set; }

    // Gets or sets a value indicating if the sort is ascending or descending.
    bool SortAscending { get; set; }

    // The SuitFilterColumns being sorted.
    SuitFilterColumns SortColumn { get; set; }
}
