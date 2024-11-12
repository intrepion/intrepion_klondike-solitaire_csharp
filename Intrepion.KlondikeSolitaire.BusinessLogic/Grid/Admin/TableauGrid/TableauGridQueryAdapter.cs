using System.Diagnostics;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Intrepion.KlondikeSolitaire.BusinessLogic.Entities;

namespace Intrepion.KlondikeSolitaire.BusinessLogic.Grid.Admin.TableauGrid;

// Creates the correct expressions to filter and sort.
public class TableauGridQueryAdapter
{
    // Holds state of the grid.
    private readonly ITableauFilters controls;

    // Expressions for sorting.
    private readonly Dictionary<TableauFilterColumns, Expression<Func<Tableau, string>>> expressions =
        new()
        {
            { TableauFilterColumns.Id, x => !x.Id.Equals(Guid.Empty) ? x.Id.ToString() : string.Empty },

            { TableauFilterColumns.PileIndex, x => x != null ? x.PileIndex.ToString() : string.Empty },
            // SortExpressionCodePlaceholder
        };

    // Queryables for filtering.
    private readonly Dictionary<TableauFilterColumns, Func<IQueryable<Tableau>, IQueryable<Tableau>>> filterQueries = [];

    // Creates a new instance of the GridQueryAdapter class.
    // controls: The ITableauFilters" to use.
    public TableauGridQueryAdapter(ITableauFilters controls)
    {
        this.controls = controls;

        // Set up queries.
        filterQueries =
            new()
            {
                { TableauFilterColumns.Id, x => x.Where(y => y != null && !y.Id.Equals(Guid.Empty) && this.controls.FilterText != null && y.Id.ToString().Contains(this.controls.FilterText) ) },

                // QueryExpressionCodePlaceholder
            };
    }

    // Uses the query to return a count and a page.
    // query: The IQueryable{Tableau} to work from.
    // Returns the resulting ICollection{Tableau}.
    public async Task<ICollection<Tableau>> FetchAsync(IQueryable<Tableau> query)
    {
        query = FilterAndQuery(query);
        await CountAsync(query);
        var collection = await FetchPageQuery(query).ToListAsync();
        controls.PageHelper.PageItems = collection.Count;
        return collection;
    }

    // Get total filtered items count.
    // query: The IQueryable{Tableau} to use.
    public async Task CountAsync(IQueryable<Tableau> query) =>
        controls.PageHelper.TotalItemCount = await query.CountAsync();

    // Build the query to bring back a single page.
    // query: The <see IQueryable{Tableau} to modify.
    // Returns the new IQueryable{Tableau} for a page.
    public IQueryable<Tableau> FetchPageQuery(IQueryable<Tableau> query) =>
        query
            .Skip(controls.PageHelper.Skip)
            .Take(controls.PageHelper.PageSize)
            .AsNoTracking();

    // Builds the query.
    // root: The IQueryable{Tableau} to start with.
    // Returns the resulting IQueryable{Tableau} with sorts and filters applied.
    private IQueryable<Tableau> FilterAndQuery(IQueryable<Tableau> root)
    {
        var sb = new System.Text.StringBuilder();

        // Apply a filter?
        if (!string.IsNullOrWhiteSpace(controls.FilterText))
        {
            var filter = filterQueries[controls.FilterColumn];
            sb.Append($"Filter: '{controls.FilterColumn}' ");
            root = filter(root);
        }

        // Apply the expression.
        var expression = expressions[controls.SortColumn];
        sb.Append($"Sort: '{controls.SortColumn}' ");

        var sortDir = controls.SortAscending ? "ASC" : "DESC";
        sb.Append(sortDir);

        Debug.WriteLine(sb.ToString());

        // Return the unfiltered query for total count, and the filtered for fetching.
        return controls.SortAscending ? root.OrderBy(expression) : root.OrderByDescending(expression);
    }
}
