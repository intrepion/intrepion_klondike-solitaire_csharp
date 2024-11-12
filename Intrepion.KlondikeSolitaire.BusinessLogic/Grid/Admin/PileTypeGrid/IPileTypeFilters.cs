namespace Intrepion.KlondikeSolitaire.BusinessLogic.Grid.Admin.PileTypeGrid;

// Interface for filtering.
public interface IPileTypeFilters
{
    // The PileTypeFilterColumns being filtered on.
    PileTypeFilterColumns FilterColumn { get; set; }

    // Loading indicator.
    bool Loading { get; set; }

    // The text of the filter.
    string? FilterText { get; set; }

    // Paging state in PageHelper.
    IPageHelper PageHelper { get; set; }

    // Gets or sets a value indicating if the sort is ascending or descending.
    bool SortAscending { get; set; }

    // The PileTypeFilterColumns being sorted.
    PileTypeFilterColumns SortColumn { get; set; }
}
