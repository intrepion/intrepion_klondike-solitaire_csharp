namespace Intrepion.KlondikeSolitaire.BusinessLogic.Grid.Admin.CardStockGrid;

// Interface for filtering.
public interface ICardStockFilters
{
    // The CardStockFilterColumns being filtered on.
    CardStockFilterColumns FilterColumn { get; set; }

    // Loading indicator.
    bool Loading { get; set; }

    // The text of the filter.
    string? FilterText { get; set; }

    // Paging state in PageHelper.
    IPageHelper PageHelper { get; set; }

    // Gets or sets a value indicating if the sort is ascending or descending.
    bool SortAscending { get; set; }

    // The CardStockFilterColumns being sorted.
    CardStockFilterColumns SortColumn { get; set; }
}
