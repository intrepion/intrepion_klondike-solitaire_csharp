using System.Diagnostics;
using System.Linq.Expressions;
using Intrepion.KlondikeSolitaire.BusinessLogic.Entities;
using Microsoft.EntityFrameworkCore;

namespace Intrepion.KlondikeSolitaire.BusinessLogic.Grid.Admin.PuzzleGrid;

// Creates the correct expressions to filter and sort.
public class PuzzleGridQueryAdapter
{
    // Holds state of the grid.
    private readonly IPuzzleFilters controls;

    // Expressions for sorting.
    private readonly Dictionary<PuzzleFilterColumns, Expression<Func<Puzzle, string>>> expressions =
        new()
        {
            { PuzzleFilterColumns.Id, x => !x.Id.Equals(Guid.Empty) ? x.Id.ToString() : string.Empty },

            { PuzzleFilterColumns.Description, x => x != null && x.Description != null ? x.Description : string.Empty },
            { PuzzleFilterColumns.IsPublic, x => x != null ? x.IsPublic.ToString() : string.Empty },
            { PuzzleFilterColumns.Name, x => x != null && x.Name != null ? x.Name : string.Empty },
            { PuzzleFilterColumns.PublishTime, x => x != null ? x.PublishTime.ToString() : string.Empty },
            // SortExpressionCodePlaceholder
        };

    // Queryables for filtering.
    private readonly Dictionary<PuzzleFilterColumns, Func<IQueryable<Puzzle>, IQueryable<Puzzle>>> filterQueries = [];

    // Creates a new instance of the GridQueryAdapter class.
    // controls: The IPuzzleFilters" to use.
    public PuzzleGridQueryAdapter(IPuzzleFilters controls)
    {
        this.controls = controls;

        // Set up queries.
        filterQueries =
            new()
            {
                { PuzzleFilterColumns.Id, x => x.Where(y => y != null && !y.Id.Equals(Guid.Empty) && this.controls.FilterText != null && y.Id.ToString().Contains(this.controls.FilterText) ) },

                // QueryExpressionCodePlaceholder
            };
    }

    // Uses the query to return a count and a page.
    // query: The IQueryable{Puzzle} to work from.
    // Returns the resulting ICollection{Puzzle}.
    public async Task<ICollection<Puzzle>> FetchAsync(IQueryable<Puzzle> query)
    {
        query = FilterAndQuery(query);
        await CountAsync(query);
        var collection = await FetchPageQuery(query).ToListAsync();
        controls.PageHelper.PageItems = collection.Count;
        return collection;
    }

    // Get total filtered items count.
    // query: The IQueryable{Puzzle} to use.
    public async Task CountAsync(IQueryable<Puzzle> query) =>
        controls.PageHelper.TotalItemCount = await query.CountAsync();

    // Build the query to bring back a single page.
    // query: The <see IQueryable{Puzzle} to modify.
    // Returns the new IQueryable{Puzzle} for a page.
    public IQueryable<Puzzle> FetchPageQuery(IQueryable<Puzzle> query) =>
        query
            .Skip(controls.PageHelper.Skip)
            .Take(controls.PageHelper.PageSize)
            .AsNoTracking();

    // Builds the query.
    // root: The IQueryable{Puzzle} to start with.
    // Returns the resulting IQueryable{Puzzle} with sorts and filters applied.
    private IQueryable<Puzzle> FilterAndQuery(IQueryable<Puzzle> root)
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
