namespace Intrepion.KlondikeSolitaire.BusinessLogic.Grid.Admin.MoveGrid;

// Interface for filtering.
public interface IMoveFilters
{
    // The MoveFilterColumns being filtered on.
    MoveFilterColumns FilterColumn { get; set; }

    // Loading indicator.
    bool Loading { get; set; }

    // The text of the filter.
    string? FilterText { get; set; }

    // Paging state in PageHelper.
    IPageHelper PageHelper { get; set; }

    // Gets or sets a value indicating if the sort is ascending or descending.
    bool SortAscending { get; set; }

    // The MoveFilterColumns being sorted.
    MoveFilterColumns SortColumn { get; set; }
}
