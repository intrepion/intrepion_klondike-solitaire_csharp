using System.Diagnostics;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Intrepion.KlondikeSolitaire.BusinessLogic.Entities;

namespace Intrepion.KlondikeSolitaire.BusinessLogic.Grid.Admin.CardWasteGrid;

// Creates the correct expressions to filter and sort.
public class CardWasteGridQueryAdapter
{
    // Holds state of the grid.
    private readonly ICardWasteFilters controls;

    // Expressions for sorting.
    private readonly Dictionary<CardWasteFilterColumns, Expression<Func<CardWaste, string>>> expressions =
        new()
        {
            { CardWasteFilterColumns.Id, x => !x.Id.Equals(Guid.Empty) ? x.Id.ToString() : string.Empty },

            { CardWasteFilterColumns.Ordering, x => x != null ? x.Ordering.ToString() : string.Empty },
            // SortExpressionCodePlaceholder
        };

    // Queryables for filtering.
    private readonly Dictionary<CardWasteFilterColumns, Func<IQueryable<CardWaste>, IQueryable<CardWaste>>> filterQueries = [];

    // Creates a new instance of the GridQueryAdapter class.
    // controls: The ICardWasteFilters" to use.
    public CardWasteGridQueryAdapter(ICardWasteFilters controls)
    {
        this.controls = controls;

        // Set up queries.
        filterQueries =
            new()
            {
                { CardWasteFilterColumns.Id, x => x.Where(y => y != null && !y.Id.Equals(Guid.Empty) && this.controls.FilterText != null && y.Id.ToString().Contains(this.controls.FilterText) ) },

                // QueryExpressionCodePlaceholder
            };
    }

    // Uses the query to return a count and a page.
    // query: The IQueryable{CardWaste} to work from.
    // Returns the resulting ICollection{CardWaste}.
    public async Task<ICollection<CardWaste>> FetchAsync(IQueryable<CardWaste> query)
    {
        query = FilterAndQuery(query);
        await CountAsync(query);
        var collection = await FetchPageQuery(query).ToListAsync();
        controls.PageHelper.PageItems = collection.Count;
        return collection;
    }

    // Get total filtered items count.
    // query: The IQueryable{CardWaste} to use.
    public async Task CountAsync(IQueryable<CardWaste> query) =>
        controls.PageHelper.TotalItemCount = await query.CountAsync();

    // Build the query to bring back a single page.
    // query: The <see IQueryable{CardWaste} to modify.
    // Returns the new IQueryable{CardWaste} for a page.
    public IQueryable<CardWaste> FetchPageQuery(IQueryable<CardWaste> query) =>
        query
            .Skip(controls.PageHelper.Skip)
            .Take(controls.PageHelper.PageSize)
            .AsNoTracking();

    // Builds the query.
    // root: The IQueryable{CardWaste} to start with.
    // Returns the resulting IQueryable{CardWaste} with sorts and filters applied.
    private IQueryable<CardWaste> FilterAndQuery(IQueryable<CardWaste> root)
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
