using System.Diagnostics;
using System.Linq.Expressions;
using Intrepion.KlondikeSolitaire.BusinessLogic.Entities;
using Microsoft.EntityFrameworkCore;

namespace Intrepion.KlondikeSolitaire.BusinessLogic.Grid.Admin.CardStockGrid;

// Creates the correct expressions to filter and sort.
public class CardStockGridQueryAdapter
{
    // Holds state of the grid.
    private readonly ICardStockFilters controls;

    // Expressions for sorting.
    private readonly Dictionary<CardStockFilterColumns, Expression<Func<CardStock, string>>> expressions =
        new()
        {
            { CardStockFilterColumns.Id, x => !x.Id.Equals(Guid.Empty) ? x.Id.ToString() : string.Empty },

            { CardStockFilterColumns.Ordering, x => x != null ? x.Ordering.ToString() : string.Empty },
            // SortExpressionCodePlaceholder
        };

    // Queryables for filtering.
    private readonly Dictionary<CardStockFilterColumns, Func<IQueryable<CardStock>, IQueryable<CardStock>>> filterQueries = [];

    // Creates a new instance of the GridQueryAdapter class.
    // controls: The ICardStockFilters" to use.
    public CardStockGridQueryAdapter(ICardStockFilters controls)
    {
        this.controls = controls;

        // Set up queries.
        filterQueries =
            new()
            {
                { CardStockFilterColumns.Id, x => x.Where(y => y != null && !y.Id.Equals(Guid.Empty) && this.controls.FilterText != null && y.Id.ToString().Contains(this.controls.FilterText) ) },

                // QueryExpressionCodePlaceholder
            };
    }

    // Uses the query to return a count and a page.
    // query: The IQueryable{CardStock} to work from.
    // Returns the resulting ICollection{CardStock}.
    public async Task<ICollection<CardStock>> FetchAsync(IQueryable<CardStock> query)
    {
        query = FilterAndQuery(query);
        await CountAsync(query);
        var collection = await FetchPageQuery(query).ToListAsync();
        controls.PageHelper.PageItems = collection.Count;
        return collection;
    }

    // Get total filtered items count.
    // query: The IQueryable{CardStock} to use.
    public async Task CountAsync(IQueryable<CardStock> query) =>
        controls.PageHelper.TotalItemCount = await query.CountAsync();

    // Build the query to bring back a single page.
    // query: The <see IQueryable{CardStock} to modify.
    // Returns the new IQueryable{CardStock} for a page.
    public IQueryable<CardStock> FetchPageQuery(IQueryable<CardStock> query) =>
        query
            .Skip(controls.PageHelper.Skip)
            .Take(controls.PageHelper.PageSize)
            .AsNoTracking();

    // Builds the query.
    // root: The IQueryable{CardStock} to start with.
    // Returns the resulting IQueryable{CardStock} with sorts and filters applied.
    private IQueryable<CardStock> FilterAndQuery(IQueryable<CardStock> root)
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
