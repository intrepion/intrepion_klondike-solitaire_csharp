namespace Intrepion.KlondikeSolitaire.BusinessLogic.Grid.Admin.CardGrid;

// Interface for filtering.
public interface ICardFilters
{
    // The CardFilterColumns being filtered on.
    CardFilterColumns FilterColumn { get; set; }

    // Loading indicator.
    bool Loading { get; set; }

    // The text of the filter.
    string? FilterText { get; set; }

    // Paging state in PageHelper.
    IPageHelper PageHelper { get; set; }

    // Gets or sets a value indicating if the sort is ascending or descending.
    bool SortAscending { get; set; }

    // The CardFilterColumns being sorted.
    CardFilterColumns SortColumn { get; set; }
}
