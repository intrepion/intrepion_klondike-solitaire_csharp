using System.Diagnostics;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Intrepion.KlondikeSolitaire.BusinessLogic.Entities;

namespace Intrepion.KlondikeSolitaire.BusinessLogic.Grid.Admin.PileTypeGrid;

// Creates the correct expressions to filter and sort.
public class PileTypeGridQueryAdapter
{
    // Holds state of the grid.
    private readonly IPileTypeFilters controls;

    // Expressions for sorting.
    private readonly Dictionary<PileTypeFilterColumns, Expression<Func<PileType, string>>> expressions =
        new()
        {
            { PileTypeFilterColumns.Id, x => !x.Id.Equals(Guid.Empty) ? x.Id.ToString() : string.Empty },

            { PileTypeFilterColumns.Name, x => x != null && x.Name != null ? x.Name : string.Empty },
            // SortExpressionCodePlaceholder
        };

    // Queryables for filtering.
    private readonly Dictionary<PileTypeFilterColumns, Func<IQueryable<PileType>, IQueryable<PileType>>> filterQueries = [];

    // Creates a new instance of the GridQueryAdapter class.
    // controls: The IPileTypeFilters" to use.
    public PileTypeGridQueryAdapter(IPileTypeFilters controls)
    {
        this.controls = controls;

        // Set up queries.
        filterQueries =
            new()
            {
                { PileTypeFilterColumns.Id, x => x.Where(y => y != null && !y.Id.Equals(Guid.Empty) && this.controls.FilterText != null && y.Id.ToString().Contains(this.controls.FilterText) ) },

                // QueryExpressionCodePlaceholder
            };
    }

    // Uses the query to return a count and a page.
    // query: The IQueryable{PileType} to work from.
    // Returns the resulting ICollection{PileType}.
    public async Task<ICollection<PileType>> FetchAsync(IQueryable<PileType> query)
    {
        query = FilterAndQuery(query);
        await CountAsync(query);
        var collection = await FetchPageQuery(query).ToListAsync();
        controls.PageHelper.PageItems = collection.Count;
        return collection;
    }

    // Get total filtered items count.
    // query: The IQueryable{PileType} to use.
    public async Task CountAsync(IQueryable<PileType> query) =>
        controls.PageHelper.TotalItemCount = await query.CountAsync();

    // Build the query to bring back a single page.
    // query: The <see IQueryable{PileType} to modify.
    // Returns the new IQueryable{PileType} for a page.
    public IQueryable<PileType> FetchPageQuery(IQueryable<PileType> query) =>
        query
            .Skip(controls.PageHelper.Skip)
            .Take(controls.PageHelper.PageSize)
            .AsNoTracking();

    // Builds the query.
    // root: The IQueryable{PileType} to start with.
    // Returns the resulting IQueryable{PileType} with sorts and filters applied.
    private IQueryable<PileType> FilterAndQuery(IQueryable<PileType> root)
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
