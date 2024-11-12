﻿namespace Intrepion.KlondikeSolitaire.BusinessLogic.Grid.Admin.StatusGrid;

// Interface for filtering.
public interface IStatusFilters
{
    // The StatusFilterColumns being filtered on.
    StatusFilterColumns FilterColumn { get; set; }

    // Loading indicator.
    bool Loading { get; set; }

    // The text of the filter.
    string? FilterText { get; set; }

    // Paging state in PageHelper.
    IPageHelper PageHelper { get; set; }

    // Gets or sets a value indicating if the sort is ascending or descending.
    bool SortAscending { get; set; }

    // The StatusFilterColumns being sorted.
    StatusFilterColumns SortColumn { get; set; }
}