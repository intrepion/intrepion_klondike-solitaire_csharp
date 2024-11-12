using System.Diagnostics;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Intrepion.KlondikeSolitaire.BusinessLogic.Entities;

namespace Intrepion.KlondikeSolitaire.BusinessLogic.Grid.Admin.CardTableauGrid;

// Creates the correct expressions to filter and sort.
public class CardTableauGridQueryAdapter
{
    // Holds state of the grid.
    private readonly ICardTableauFilters controls;

    // Expressions for sorting.
    private readonly Dictionary<CardTableauFilterColumns, Expression<Func<CardTableau, string>>> expressions =
        new()
        {
            { CardTableauFilterColumns.Id, x => !x.Id.Equals(Guid.Empty) ? x.Id.ToString() : string.Empty },

            // SortExpressionCodePlaceholder
        };

    // Queryables for filtering.
    private readonly Dictionary<CardTableauFilterColumns, Func<IQueryable<CardTableau>, IQueryable<CardTableau>>> filterQueries = [];

    // Creates a new instance of the GridQueryAdapter class.
    // controls: The ICardTableauFilters" to use.
    public CardTableauGridQueryAdapter(ICardTableauFilters controls)
    {
        this.controls = controls;

        // Set up queries.
        filterQueries =
            new()
            {
                { CardTableauFilterColumns.Id, x => x.Where(y => y != null && !y.Id.Equals(Guid.Empty) && this.controls.FilterText != null && y.Id.ToString().Contains(this.controls.FilterText) ) },

                // QueryExpressionCodePlaceholder
            };
    }

    // Uses the query to return a count and a page.
    // query: The IQueryable{CardTableau} to work from.
    // Returns the resulting ICollection{CardTableau}.
    public async Task<ICollection<CardTableau>> FetchAsync(IQueryable<CardTableau> query)
    {
        query = FilterAndQuery(query);
        await CountAsync(query);
        var collection = await FetchPageQuery(query).ToListAsync();
        controls.PageHelper.PageItems = collection.Count;
        return collection;
    }

    // Get total filtered items count.
    // query: The IQueryable{CardTableau} to use.
    public async Task CountAsync(IQueryable<CardTableau> query) =>
        controls.PageHelper.TotalItemCount = await query.CountAsync();

    // Build the query to bring back a single page.
    // query: The <see IQueryable{CardTableau} to modify.
    // Returns the new IQueryable{CardTableau} for a page.
    public IQueryable<CardTableau> FetchPageQuery(IQueryable<CardTableau> query) =>
        query
            .Skip(controls.PageHelper.Skip)
            .Take(controls.PageHelper.PageSize)
            .AsNoTracking();

    // Builds the query.
    // root: The IQueryable{CardTableau} to start with.
    // Returns the resulting IQueryable{CardTableau} with sorts and filters applied.
    private IQueryable<CardTableau> FilterAndQuery(IQueryable<CardTableau> root)
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
