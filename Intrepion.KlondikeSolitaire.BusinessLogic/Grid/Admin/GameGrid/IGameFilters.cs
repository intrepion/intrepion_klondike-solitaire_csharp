namespace Intrepion.KlondikeSolitaire.BusinessLogic.Grid.Admin.GameGrid;

// Interface for filtering.
public interface IGameFilters
{
    // The GameFilterColumns being filtered on.
    GameFilterColumns FilterColumn { get; set; }

    // Loading indicator.
    bool Loading { get; set; }

    // The text of the filter.
    string? FilterText { get; set; }

    // Paging state in PageHelper.
    IPageHelper PageHelper { get; set; }

    // Gets or sets a value indicating if the sort is ascending or descending.
    bool SortAscending { get; set; }

    // The GameFilterColumns being sorted.
    GameFilterColumns SortColumn { get; set; }
}
