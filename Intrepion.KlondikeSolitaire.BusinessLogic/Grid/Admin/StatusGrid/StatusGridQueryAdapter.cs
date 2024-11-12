using System.Diagnostics;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Intrepion.KlondikeSolitaire.BusinessLogic.Entities;

namespace Intrepion.KlondikeSolitaire.BusinessLogic.Grid.Admin.StatusGrid;

// Creates the correct expressions to filter and sort.
public class StatusGridQueryAdapter
{
    // Holds state of the grid.
    private readonly IStatusFilters controls;

    // Expressions for sorting.
    private readonly Dictionary<StatusFilterColumns, Expression<Func<Status, string>>> expressions =
        new()
        {
            { StatusFilterColumns.Id, x => !x.Id.Equals(Guid.Empty) ? x.Id.ToString() : string.Empty },

            // SortExpressionCodePlaceholder
        };

    // Queryables for filtering.
    private readonly Dictionary<StatusFilterColumns, Func<IQueryable<Status>, IQueryable<Status>>> filterQueries = [];

    // Creates a new instance of the GridQueryAdapter class.
    // controls: The IStatusFilters" to use.
    public StatusGridQueryAdapter(IStatusFilters controls)
    {
        this.controls = controls;

        // Set up queries.
        filterQueries =
            new()
            {
                { StatusFilterColumns.Id, x => x.Where(y => y != null && !y.Id.Equals(Guid.Empty) && this.controls.FilterText != null && y.Id.ToString().Contains(this.controls.FilterText) ) },

                // QueryExpressionCodePlaceholder
            };
    }

    // Uses the query to return a count and a page.
    // query: The IQueryable{Status} to work from.
    // Returns the resulting ICollection{Status}.
    public async Task<ICollection<Status>> FetchAsync(IQueryable<Status> query)
    {
        query = FilterAndQuery(query);
        await CountAsync(query);
        var collection = await FetchPageQuery(query).ToListAsync();
        controls.PageHelper.PageItems = collection.Count;
        return collection;
    }

    // Get total filtered items count.
    // query: The IQueryable{Status} to use.
    public async Task CountAsync(IQueryable<Status> query) =>
        controls.PageHelper.TotalItemCount = await query.CountAsync();

    // Build the query to bring back a single page.
    // query: The <see IQueryable{Status} to modify.
    // Returns the new IQueryable{Status} for a page.
    public IQueryable<Status> FetchPageQuery(IQueryable<Status> query) =>
        query
            .Skip(controls.PageHelper.Skip)
            .Take(controls.PageHelper.PageSize)
            .AsNoTracking();

    // Builds the query.
    // root: The IQueryable{Status} to start with.
    // Returns the resulting IQueryable{Status} with sorts and filters applied.
    private IQueryable<Status> FilterAndQuery(IQueryable<Status> root)
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