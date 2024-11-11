using System.Diagnostics;
using System.Linq.Expressions;
using Intrepion.KlondikeSolitaire.BusinessLogic.Entities;
using Microsoft.EntityFrameworkCore;

namespace Intrepion.KlondikeSolitaire.BusinessLogic.Grid.Admin.PlayerGrid;

// Creates the correct expressions to filter and sort.
public class PlayerGridQueryAdapter
{
    // Holds state of the grid.
    private readonly IPlayerFilters controls;

    // Expressions for sorting.
    private readonly Dictionary<PlayerFilterColumns, Expression<Func<Player, string>>> expressions =
        new()
        {
            { PlayerFilterColumns.Id, x => !x.Id.Equals(Guid.Empty) ? x.Id.ToString() : string.Empty },

            // SortExpressionCodePlaceholder
        };

    // Queryables for filtering.
    private readonly Dictionary<PlayerFilterColumns, Func<IQueryable<Player>, IQueryable<Player>>> filterQueries = [];

    // Creates a new instance of the GridQueryAdapter class.
    // controls: The IPlayerFilters" to use.
    public PlayerGridQueryAdapter(IPlayerFilters controls)
    {
        this.controls = controls;

        // Set up queries.
        filterQueries =
            new()
            {
                { PlayerFilterColumns.Id, x => x.Where(y => y != null && !y.Id.Equals(Guid.Empty) && this.controls.FilterText != null && y.Id.ToString().Contains(this.controls.FilterText) ) },

                // QueryExpressionCodePlaceholder
            };
    }

    // Uses the query to return a count and a page.
    // query: The IQueryable{Player} to work from.
    // Returns the resulting ICollection{Player}.
    public async Task<ICollection<Player>> FetchAsync(IQueryable<Player> query)
    {
        query = FilterAndQuery(query);
        await CountAsync(query);
        var collection = await FetchPageQuery(query).ToListAsync();
        controls.PageHelper.PageItems = collection.Count;
        return collection;
    }

    // Get total filtered items count.
    // query: The IQueryable{Player} to use.
    public async Task CountAsync(IQueryable<Player> query) =>
        controls.PageHelper.TotalItemCount = await query.CountAsync();

    // Build the query to bring back a single page.
    // query: The <see IQueryable{Player} to modify.
    // Returns the new IQueryable{Player} for a page.
    public IQueryable<Player> FetchPageQuery(IQueryable<Player> query) =>
        query
            .Skip(controls.PageHelper.Skip)
            .Take(controls.PageHelper.PageSize)
            .AsNoTracking();

    // Builds the query.
    // root: The IQueryable{Player} to start with.
    // Returns the resulting IQueryable{Player} with sorts and filters applied.
    private IQueryable<Player> FilterAndQuery(IQueryable<Player> root)
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
