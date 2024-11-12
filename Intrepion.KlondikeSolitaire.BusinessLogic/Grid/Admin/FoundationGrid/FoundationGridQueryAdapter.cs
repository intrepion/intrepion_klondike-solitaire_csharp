using System.Diagnostics;
using System.Linq.Expressions;
using Intrepion.KlondikeSolitaire.BusinessLogic.Entities;
using Microsoft.EntityFrameworkCore;

namespace Intrepion.KlondikeSolitaire.BusinessLogic.Grid.Admin.FoundationGrid;

// Creates the correct expressions to filter and sort.
public class FoundationGridQueryAdapter
{
    // Holds state of the grid.
    private readonly IFoundationFilters controls;

    // Expressions for sorting.
    private readonly Dictionary<FoundationFilterColumns, Expression<Func<Foundation, string>>> expressions =
        new()
        {
            { FoundationFilterColumns.Id, x => !x.Id.Equals(Guid.Empty) ? x.Id.ToString() : string.Empty },

            { FoundationFilterColumns.PileIndex, x => x != null ? x.PileIndex.ToString() : string.Empty },
            // SortExpressionCodePlaceholder
        };

    // Queryables for filtering.
    private readonly Dictionary<FoundationFilterColumns, Func<IQueryable<Foundation>, IQueryable<Foundation>>> filterQueries = [];

    // Creates a new instance of the GridQueryAdapter class.
    // controls: The IFoundationFilters" to use.
    public FoundationGridQueryAdapter(IFoundationFilters controls)
    {
        this.controls = controls;

        // Set up queries.
        filterQueries =
            new()
            {
                { FoundationFilterColumns.Id, x => x.Where(y => y != null && !y.Id.Equals(Guid.Empty) && this.controls.FilterText != null && y.Id.ToString().Contains(this.controls.FilterText) ) },

                // QueryExpressionCodePlaceholder
            };
    }

    // Uses the query to return a count and a page.
    // query: The IQueryable{Foundation} to work from.
    // Returns the resulting ICollection{Foundation}.
    public async Task<ICollection<Foundation>> FetchAsync(IQueryable<Foundation> query)
    {
        query = FilterAndQuery(query);
        await CountAsync(query);
        var collection = await FetchPageQuery(query).ToListAsync();
        controls.PageHelper.PageItems = collection.Count;
        return collection;
    }

    // Get total filtered items count.
    // query: The IQueryable{Foundation} to use.
    public async Task CountAsync(IQueryable<Foundation> query) =>
        controls.PageHelper.TotalItemCount = await query.CountAsync();

    // Build the query to bring back a single page.
    // query: The <see IQueryable{Foundation} to modify.
    // Returns the new IQueryable{Foundation} for a page.
    public IQueryable<Foundation> FetchPageQuery(IQueryable<Foundation> query) =>
        query
            .Skip(controls.PageHelper.Skip)
            .Take(controls.PageHelper.PageSize)
            .AsNoTracking();

    // Builds the query.
    // root: The IQueryable{Foundation} to start with.
    // Returns the resulting IQueryable{Foundation} with sorts and filters applied.
    private IQueryable<Foundation> FilterAndQuery(IQueryable<Foundation> root)
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
