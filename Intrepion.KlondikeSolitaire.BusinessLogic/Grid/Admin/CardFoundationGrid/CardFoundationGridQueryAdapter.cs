using System.Diagnostics;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Intrepion.KlondikeSolitaire.BusinessLogic.Entities;

namespace Intrepion.KlondikeSolitaire.BusinessLogic.Grid.Admin.CardFoundationGrid;

// Creates the correct expressions to filter and sort.
public class CardFoundationGridQueryAdapter
{
    // Holds state of the grid.
    private readonly ICardFoundationFilters controls;

    // Expressions for sorting.
    private readonly Dictionary<CardFoundationFilterColumns, Expression<Func<CardFoundation, string>>> expressions =
        new()
        {
            { CardFoundationFilterColumns.Id, x => !x.Id.Equals(Guid.Empty) ? x.Id.ToString() : string.Empty },

            // SortExpressionCodePlaceholder
        };

    // Queryables for filtering.
    private readonly Dictionary<CardFoundationFilterColumns, Func<IQueryable<CardFoundation>, IQueryable<CardFoundation>>> filterQueries = [];

    // Creates a new instance of the GridQueryAdapter class.
    // controls: The ICardFoundationFilters" to use.
    public CardFoundationGridQueryAdapter(ICardFoundationFilters controls)
    {
        this.controls = controls;

        // Set up queries.
        filterQueries =
            new()
            {
                { CardFoundationFilterColumns.Id, x => x.Where(y => y != null && !y.Id.Equals(Guid.Empty) && this.controls.FilterText != null && y.Id.ToString().Contains(this.controls.FilterText) ) },

                // QueryExpressionCodePlaceholder
            };
    }

    // Uses the query to return a count and a page.
    // query: The IQueryable{CardFoundation} to work from.
    // Returns the resulting ICollection{CardFoundation}.
    public async Task<ICollection<CardFoundation>> FetchAsync(IQueryable<CardFoundation> query)
    {
        query = FilterAndQuery(query);
        await CountAsync(query);
        var collection = await FetchPageQuery(query).ToListAsync();
        controls.PageHelper.PageItems = collection.Count;
        return collection;
    }

    // Get total filtered items count.
    // query: The IQueryable{CardFoundation} to use.
    public async Task CountAsync(IQueryable<CardFoundation> query) =>
        controls.PageHelper.TotalItemCount = await query.CountAsync();

    // Build the query to bring back a single page.
    // query: The <see IQueryable{CardFoundation} to modify.
    // Returns the new IQueryable{CardFoundation} for a page.
    public IQueryable<CardFoundation> FetchPageQuery(IQueryable<CardFoundation> query) =>
        query
            .Skip(controls.PageHelper.Skip)
            .Take(controls.PageHelper.PageSize)
            .AsNoTracking();

    // Builds the query.
    // root: The IQueryable{CardFoundation} to start with.
    // Returns the resulting IQueryable{CardFoundation} with sorts and filters applied.
    private IQueryable<CardFoundation> FilterAndQuery(IQueryable<CardFoundation> root)
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
