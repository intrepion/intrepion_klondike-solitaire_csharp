using System.Diagnostics;
using System.Linq.Expressions;
using Intrepion.KlondikeSolitaire.BusinessLogic.Entities;
using Microsoft.EntityFrameworkCore;

namespace Intrepion.KlondikeSolitaire.BusinessLogic.Grid.Admin.RankGrid;

// Creates the correct expressions to filter and sort.
public class RankGridQueryAdapter
{
    // Holds state of the grid.
    private readonly IRankFilters controls;

    // Expressions for sorting.
    private readonly Dictionary<RankFilterColumns, Expression<Func<Rank, string>>> expressions =
        new()
        {
            { RankFilterColumns.Id, x => !x.Id.Equals(Guid.Empty) ? x.Id.ToString() : string.Empty },

            // SortExpressionCodePlaceholder
        };

    // Queryables for filtering.
    private readonly Dictionary<RankFilterColumns, Func<IQueryable<Rank>, IQueryable<Rank>>> filterQueries = [];

    // Creates a new instance of the GridQueryAdapter class.
    // controls: The IRankFilters" to use.
    public RankGridQueryAdapter(IRankFilters controls)
    {
        this.controls = controls;

        // Set up queries.
        filterQueries =
            new()
            {
                { RankFilterColumns.Id, x => x.Where(y => y != null && !y.Id.Equals(Guid.Empty) && this.controls.FilterText != null && y.Id.ToString().Contains(this.controls.FilterText) ) },

                // QueryExpressionCodePlaceholder
            };
    }

    // Uses the query to return a count and a page.
    // query: The IQueryable{Rank} to work from.
    // Returns the resulting ICollection{Rank}.
    public async Task<ICollection<Rank>> FetchAsync(IQueryable<Rank> query)
    {
        query = FilterAndQuery(query);
        await CountAsync(query);
        var collection = await FetchPageQuery(query).ToListAsync();
        controls.PageHelper.PageItems = collection.Count;
        return collection;
    }

    // Get total filtered items count.
    // query: The IQueryable{Rank} to use.
    public async Task CountAsync(IQueryable<Rank> query) =>
        controls.PageHelper.TotalItemCount = await query.CountAsync();

    // Build the query to bring back a single page.
    // query: The <see IQueryable{Rank} to modify.
    // Returns the new IQueryable{Rank} for a page.
    public IQueryable<Rank> FetchPageQuery(IQueryable<Rank> query) =>
        query
            .Skip(controls.PageHelper.Skip)
            .Take(controls.PageHelper.PageSize)
            .AsNoTracking();

    // Builds the query.
    // root: The IQueryable{Rank} to start with.
    // Returns the resulting IQueryable{Rank} with sorts and filters applied.
    private IQueryable<Rank> FilterAndQuery(IQueryable<Rank> root)
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
