namespace Intrepion.KlondikeSolitaire.BusinessLogic.Grid.Admin.CardTableauGrid;

// Interface for filtering.
public interface ICardTableauFilters
{
    // The CardTableauFilterColumns being filtered on.
    CardTableauFilterColumns FilterColumn { get; set; }

    // Loading indicator.
    bool Loading { get; set; }

    // The text of the filter.
    string? FilterText { get; set; }

    // Paging state in PageHelper.
    IPageHelper PageHelper { get; set; }

    // Gets or sets a value indicating if the sort is ascending or descending.
    bool SortAscending { get; set; }

    // The CardTableauFilterColumns being sorted.
    CardTableauFilterColumns SortColumn { get; set; }
}
