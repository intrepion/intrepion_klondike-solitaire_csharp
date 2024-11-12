using System.Diagnostics;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Intrepion.KlondikeSolitaire.BusinessLogic.Entities;

namespace Intrepion.KlondikeSolitaire.BusinessLogic.Grid.Admin.CardGrid;

// Creates the correct expressions to filter and sort.
public class CardGridQueryAdapter
{
    // Holds state of the grid.
    private readonly ICardFilters controls;

    // Expressions for sorting.
    private readonly Dictionary<CardFilterColumns, Expression<Func<Card, string>>> expressions =
        new()
        {
            { CardFilterColumns.Id, x => !x.Id.Equals(Guid.Empty) ? x.Id.ToString() : string.Empty },

            // SortExpressionCodePlaceholder
        };

    // Queryables for filtering.
    private readonly Dictionary<CardFilterColumns, Func<IQueryable<Card>, IQueryable<Card>>> filterQueries = [];

    // Creates a new instance of the GridQueryAdapter class.
    // controls: The ICardFilters" to use.
    public CardGridQueryAdapter(ICardFilters controls)
    {
        this.controls = controls;

        // Set up queries.
        filterQueries =
            new()
            {
                { CardFilterColumns.Id, x => x.Where(y => y != null && !y.Id.Equals(Guid.Empty) && this.controls.FilterText != null && y.Id.ToString().Contains(this.controls.FilterText) ) },

                // QueryExpressionCodePlaceholder
            };
    }

    // Uses the query to return a count and a page.
    // query: The IQueryable{Card} to work from.
    // Returns the resulting ICollection{Card}.
    public async Task<ICollection<Card>> FetchAsync(IQueryable<Card> query)
    {
        query = FilterAndQuery(query);
        await CountAsync(query);
        var collection = await FetchPageQuery(query).ToListAsync();
        controls.PageHelper.PageItems = collection.Count;
        return collection;
    }

    // Get total filtered items count.
    // query: The IQueryable{Card} to use.
    public async Task CountAsync(IQueryable<Card> query) =>
        controls.PageHelper.TotalItemCount = await query.CountAsync();

    // Build the query to bring back a single page.
    // query: The <see IQueryable{Card} to modify.
    // Returns the new IQueryable{Card} for a page.
    public IQueryable<Card> FetchPageQuery(IQueryable<Card> query) =>
        query
            .Skip(controls.PageHelper.Skip)
            .Take(controls.PageHelper.PageSize)
            .AsNoTracking();

    // Builds the query.
    // root: The IQueryable{Card} to start with.
    // Returns the resulting IQueryable{Card} with sorts and filters applied.
    private IQueryable<Card> FilterAndQuery(IQueryable<Card> root)
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
