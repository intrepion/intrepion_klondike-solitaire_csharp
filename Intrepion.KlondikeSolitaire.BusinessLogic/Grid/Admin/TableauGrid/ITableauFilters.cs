﻿namespace Intrepion.KlondikeSolitaire.BusinessLogic.Grid.Admin.TableauGrid;

// Interface for filtering.
public interface ITableauFilters
{
    // The TableauFilterColumns being filtered on.
    TableauFilterColumns FilterColumn { get; set; }

    // Loading indicator.
    bool Loading { get; set; }

    // The text of the filter.
    string? FilterText { get; set; }

    // Paging state in PageHelper.
    IPageHelper PageHelper { get; set; }

    // Gets or sets a value indicating if the sort is ascending or descending.
    bool SortAscending { get; set; }

    // The TableauFilterColumns being sorted.
    TableauFilterColumns SortColumn { get; set; }
}