using System.Diagnostics;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using ApplicationNamePlaceholder.BusinessLogic.Entities;

namespace ApplicationNamePlaceholder.BusinessLogic.Grid.Admin.EntityNamePlaceholderGrid;

// Creates the correct expressions to filter and sort.
public class EntityNamePlaceholderGridQueryAdapter
{
    // Holds state of the grid.
    private readonly IEntityNamePlaceholderFilters controls;

    // Expressions for sorting.
    private readonly Dictionary<EntityNamePlaceholderFilterColumns, Expression<Func<EntityNamePlaceholder, string>>> expressions =
        new()
        {
            { EntityNamePlaceholderFilterColumns.Id, x => !x.Id.Equals(Guid.Empty) ? x.Id.ToString() : string.Empty },

            // SortExpressionCodePlaceholder
        };

    // Queryables for filtering.
    private readonly Dictionary<EntityNamePlaceholderFilterColumns, Func<IQueryable<EntityNamePlaceholder>, IQueryable<EntityNamePlaceholder>>> filterQueries = [];

    // Creates a new instance of the GridQueryAdapter class.
    // controls: The IEntityNamePlaceholderFilters" to use.
    public EntityNamePlaceholderGridQueryAdapter(IEntityNamePlaceholderFilters controls)
    {
        this.controls = controls;

        // Set up queries.
        filterQueries =
            new()
            {
                { EntityNamePlaceholderFilterColumns.Id, x => x.Where(y => y != null && !y.Id.Equals(Guid.Empty) && this.controls.FilterText != null && y.Id.ToString().Contains(this.controls.FilterText) ) },

                // QueryExpressionCodePlaceholder
            };
    }

    // Uses the query to return a count and a page.
    // query: The IQueryable{EntityNamePlaceholder} to work from.
    // Returns the resulting ICollection{EntityNamePlaceholder}.
    public async Task<ICollection<EntityNamePlaceholder>> FetchAsync(IQueryable<EntityNamePlaceholder> query)
    {
        query = FilterAndQuery(query);
        await CountAsync(query);
        var collection = await FetchPageQuery(query).ToListAsync();
        controls.PageHelper.PageItems = collection.Count;
        return collection;
    }

    // Get total filtered items count.
    // query: The IQueryable{EntityNamePlaceholder} to use.
    public async Task CountAsync(IQueryable<EntityNamePlaceholder> query) =>
        controls.PageHelper.TotalItemCount = await query.CountAsync();

    // Build the query to bring back a single page.
    // query: The <see IQueryable{EntityNamePlaceholder} to modify.
    // Returns the new IQueryable{EntityNamePlaceholder} for a page.
    public IQueryable<EntityNamePlaceholder> FetchPageQuery(IQueryable<EntityNamePlaceholder> query) =>
        query
            .Skip(controls.PageHelper.Skip)
            .Take(controls.PageHelper.PageSize)
            .AsNoTracking();

    // Builds the query.
    // root: The IQueryable{EntityNamePlaceholder} to start with.
    // Returns the resulting IQueryable{EntityNamePlaceholder} with sorts and filters applied.
    private IQueryable<EntityNamePlaceholder> FilterAndQuery(IQueryable<EntityNamePlaceholder> root)
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
