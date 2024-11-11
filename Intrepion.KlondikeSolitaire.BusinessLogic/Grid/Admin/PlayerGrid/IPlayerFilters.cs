namespace Intrepion.KlondikeSolitaire.BusinessLogic.Grid.Admin.PlayerGrid;

// Interface for filtering.
public interface IPlayerFilters
{
    // The PlayerFilterColumns being filtered on.
    PlayerFilterColumns FilterColumn { get; set; }

    // Loading indicator.
    bool Loading { get; set; }

    // The text of the filter.
    string? FilterText { get; set; }

    // Paging state in PageHelper.
    IPageHelper PageHelper { get; set; }

    // Gets or sets a value indicating if the sort is ascending or descending.
    bool SortAscending { get; set; }

    // The PlayerFilterColumns being sorted.
    PlayerFilterColumns SortColumn { get; set; }
}
