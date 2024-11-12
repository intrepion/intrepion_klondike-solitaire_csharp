﻿namespace Intrepion.KlondikeSolitaire.BusinessLogic.Grid.Admin.CardFoundationGrid;

// Interface for filtering.
public interface ICardFoundationFilters
{
    // The CardFoundationFilterColumns being filtered on.
    CardFoundationFilterColumns FilterColumn { get; set; }

    // Loading indicator.
    bool Loading { get; set; }

    // The text of the filter.
    string? FilterText { get; set; }

    // Paging state in PageHelper.
    IPageHelper PageHelper { get; set; }

    // Gets or sets a value indicating if the sort is ascending or descending.
    bool SortAscending { get; set; }

    // The CardFoundationFilterColumns being sorted.
    CardFoundationFilterColumns SortColumn { get; set; }
}