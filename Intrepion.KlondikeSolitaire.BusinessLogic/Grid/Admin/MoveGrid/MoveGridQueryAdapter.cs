using System.Diagnostics;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Intrepion.KlondikeSolitaire.BusinessLogic.Entities;

namespace Intrepion.KlondikeSolitaire.BusinessLogic.Grid.Admin.MoveGrid;

// Creates the correct expressions to filter and sort.
public class MoveGridQueryAdapter
{
    // Holds state of the grid.
    private readonly IMoveFilters controls;

    // Expressions for sorting.
    private readonly Dictionary<MoveFilterColumns, Expression<Func<Move, string>>> expressions =
        new()
        {
            { MoveFilterColumns.Id, x => !x.Id.Equals(Guid.Empty) ? x.Id.ToString() : string.Empty },

            { MoveFilterColumns.FromPileIndex, x => x != null ? x.FromPileIndex.ToString() : string.Empty },
            { MoveFilterColumns.MoveTime, x => x != null ? x.MoveTime.ToString() : string.Empty },
            // SortExpressionCodePlaceholder
        };

    // Queryables for filtering.
    private readonly Dictionary<MoveFilterColumns, Func<IQueryable<Move>, IQueryable<Move>>> filterQueries = [];

    // Creates a new instance of the GridQueryAdapter class.
    // controls: The IMoveFilters" to use.
    public MoveGridQueryAdapter(IMoveFilters controls)
    {
        this.controls = controls;

        // Set up queries.
        filterQueries =
            new()
            {
                { MoveFilterColumns.Id, x => x.Where(y => y != null && !y.Id.Equals(Guid.Empty) && this.controls.FilterText != null && y.Id.ToString().Contains(this.controls.FilterText) ) },

                // QueryExpressionCodePlaceholder
            };
    }

    // Uses the query to return a count and a page.
    // query: The IQueryable{Move} to work from.
    // Returns the resulting ICollection{Move}.
    public async Task<ICollection<Move>> FetchAsync(IQueryable<Move> query)
    {
        query = FilterAndQuery(query);
        await CountAsync(query);
        var collection = await FetchPageQuery(query).ToListAsync();
        controls.PageHelper.PageItems = collection.Count;
        return collection;
    }

    // Get total filtered items count.
    // query: The IQueryable{Move} to use.
    public async Task CountAsync(IQueryable<Move> query) =>
        controls.PageHelper.TotalItemCount = await query.CountAsync();

    // Build the query to bring back a single page.
    // query: The <see IQueryable{Move} to modify.
    // Returns the new IQueryable{Move} for a page.
    public IQueryable<Move> FetchPageQuery(IQueryable<Move> query) =>
        query
            .Skip(controls.PageHelper.Skip)
            .Take(controls.PageHelper.PageSize)
            .AsNoTracking();

    // Builds the query.
    // root: The IQueryable{Move} to start with.
    // Returns the resulting IQueryable{Move} with sorts and filters applied.
    private IQueryable<Move> FilterAndQuery(IQueryable<Move> root)
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
