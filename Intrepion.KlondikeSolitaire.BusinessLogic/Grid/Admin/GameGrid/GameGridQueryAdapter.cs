using System.Diagnostics;
using System.Linq.Expressions;
using Intrepion.KlondikeSolitaire.BusinessLogic.Entities;
using Microsoft.EntityFrameworkCore;

namespace Intrepion.KlondikeSolitaire.BusinessLogic.Grid.Admin.GameGrid;

// Creates the correct expressions to filter and sort.
public class GameGridQueryAdapter
{
    // Holds state of the grid.
    private readonly IGameFilters controls;

    // Expressions for sorting.
    private readonly Dictionary<GameFilterColumns, Expression<Func<Game, string>>> expressions =
        new()
        {
            { GameFilterColumns.Id, x => !x.Id.Equals(Guid.Empty) ? x.Id.ToString() : string.Empty },

            // SortExpressionCodePlaceholder
        };

    // Queryables for filtering.
    private readonly Dictionary<GameFilterColumns, Func<IQueryable<Game>, IQueryable<Game>>> filterQueries = [];

    // Creates a new instance of the GridQueryAdapter class.
    // controls: The IGameFilters" to use.
    public GameGridQueryAdapter(IGameFilters controls)
    {
        this.controls = controls;

        // Set up queries.
        filterQueries =
            new()
            {
                { GameFilterColumns.Id, x => x.Where(y => y != null && !y.Id.Equals(Guid.Empty) && this.controls.FilterText != null && y.Id.ToString().Contains(this.controls.FilterText) ) },

                // QueryExpressionCodePlaceholder
            };
    }

    // Uses the query to return a count and a page.
    // query: The IQueryable{Game} to work from.
    // Returns the resulting ICollection{Game}.
    public async Task<ICollection<Game>> FetchAsync(IQueryable<Game> query)
    {
        query = FilterAndQuery(query);
        await CountAsync(query);
        var collection = await FetchPageQuery(query).ToListAsync();
        controls.PageHelper.PageItems = collection.Count;
        return collection;
    }

    // Get total filtered items count.
    // query: The IQueryable{Game} to use.
    public async Task CountAsync(IQueryable<Game> query) =>
        controls.PageHelper.TotalItemCount = await query.CountAsync();

    // Build the query to bring back a single page.
    // query: The <see IQueryable{Game} to modify.
    // Returns the new IQueryable{Game} for a page.
    public IQueryable<Game> FetchPageQuery(IQueryable<Game> query) =>
        query
            .Skip(controls.PageHelper.Skip)
            .Take(controls.PageHelper.PageSize)
            .AsNoTracking();

    // Builds the query.
    // root: The IQueryable{Game} to start with.
    // Returns the resulting IQueryable{Game} with sorts and filters applied.
    private IQueryable<Game> FilterAndQuery(IQueryable<Game> root)
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
