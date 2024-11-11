using System.Diagnostics;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Intrepion.KlondikeSolitaire.BusinessLogic.Entities;

namespace Intrepion.KlondikeSolitaire.BusinessLogic.Grid.Admin.SuitGrid;

// Creates the correct expressions to filter and sort.
public class SuitGridQueryAdapter
{
    // Holds state of the grid.
    private readonly ISuitFilters controls;

    // Expressions for sorting.
    private readonly Dictionary<SuitFilterColumns, Expression<Func<Suit, string>>> expressions =
        new()
        {
            { SuitFilterColumns.Id, x => !x.Id.Equals(Guid.Empty) ? x.Id.ToString() : string.Empty },

            // SortExpressionCodePlaceholder
        };

    // Queryables for filtering.
    private readonly Dictionary<SuitFilterColumns, Func<IQueryable<Suit>, IQueryable<Suit>>> filterQueries = [];

    // Creates a new instance of the GridQueryAdapter class.
    // controls: The ISuitFilters" to use.
    public SuitGridQueryAdapter(ISuitFilters controls)
    {
        this.controls = controls;

        // Set up queries.
        filterQueries =
            new()
            {
                { SuitFilterColumns.Id, x => x.Where(y => y != null && !y.Id.Equals(Guid.Empty) && this.controls.FilterText != null && y.Id.ToString().Contains(this.controls.FilterText) ) },

                // QueryExpressionCodePlaceholder
            };
    }

    // Uses the query to return a count and a page.
    // query: The IQueryable{Suit} to work from.
    // Returns the resulting ICollection{Suit}.
    public async Task<ICollection<Suit>> FetchAsync(IQueryable<Suit> query)
    {
        query = FilterAndQuery(query);
        await CountAsync(query);
        var collection = await FetchPageQuery(query).ToListAsync();
        controls.PageHelper.PageItems = collection.Count;
        return collection;
    }

    // Get total filtered items count.
    // query: The IQueryable{Suit} to use.
    public async Task CountAsync(IQueryable<Suit> query) =>
        controls.PageHelper.TotalItemCount = await query.CountAsync();

    // Build the query to bring back a single page.
    // query: The <see IQueryable{Suit} to modify.
    // Returns the new IQueryable{Suit} for a page.
    public IQueryable<Suit> FetchPageQuery(IQueryable<Suit> query) =>
        query
            .Skip(controls.PageHelper.Skip)
            .Take(controls.PageHelper.PageSize)
            .AsNoTracking();

    // Builds the query.
    // root: The IQueryable{Suit} to start with.
    // Returns the resulting IQueryable{Suit} with sorts and filters applied.
    private IQueryable<Suit> FilterAndQuery(IQueryable<Suit> root)
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
