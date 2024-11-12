namespace Intrepion.KlondikeSolitaire.BusinessLogic.Grid.Admin.CardWasteGrid;

// Interface for filtering.
public interface ICardWasteFilters
{
    // The CardWasteFilterColumns being filtered on.
    CardWasteFilterColumns FilterColumn { get; set; }

    // Loading indicator.
    bool Loading { get; set; }

    // The text of the filter.
    string? FilterText { get; set; }

    // Paging state in PageHelper.
    IPageHelper PageHelper { get; set; }

    // Gets or sets a value indicating if the sort is ascending or descending.
    bool SortAscending { get; set; }

    // The CardWasteFilterColumns being sorted.
    CardWasteFilterColumns SortColumn { get; set; }
}
