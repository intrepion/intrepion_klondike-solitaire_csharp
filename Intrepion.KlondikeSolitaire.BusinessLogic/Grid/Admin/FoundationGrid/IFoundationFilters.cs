﻿namespace Intrepion.KlondikeSolitaire.BusinessLogic.Grid.Admin.FoundationGrid;

// Interface for filtering.
public interface IFoundationFilters
{
    // The FoundationFilterColumns being filtered on.
    FoundationFilterColumns FilterColumn { get; set; }

    // Loading indicator.
    bool Loading { get; set; }

    // The text of the filter.
    string? FilterText { get; set; }

    // Paging state in PageHelper.
    IPageHelper PageHelper { get; set; }

    // Gets or sets a value indicating if the sort is ascending or descending.
    bool SortAscending { get; set; }

    // The FoundationFilterColumns being sorted.
    FoundationFilterColumns SortColumn { get; set; }
}
