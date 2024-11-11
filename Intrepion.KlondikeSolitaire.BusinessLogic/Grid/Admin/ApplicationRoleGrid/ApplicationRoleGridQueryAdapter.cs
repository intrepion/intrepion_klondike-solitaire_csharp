using System.Diagnostics;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using ApplicationNamePlaceholder.BusinessLogic.Entities;

namespace ApplicationNamePlaceholder.BusinessLogic.Grid.Admin.ApplicationRoleGrid;

// Creates the correct expressions to filter and sort.
public class ApplicationRoleGridQueryAdapter
{
    // Holds state of the grid.
    private readonly IApplicationRoleFilters controls;

    // Expressions for sorting.
    private readonly Dictionary<ApplicationRoleFilterColumns, Expression<Func<ApplicationRole, string>>> expressions =
        new()
        {
            { ApplicationRoleFilterColumns.Name, c => c != null && c.Name != null ? c.Name : string.Empty },
        };

    // Queryables for filtering.
    private readonly Dictionary<ApplicationRoleFilterColumns, Func<IQueryable<ApplicationRole>, IQueryable<ApplicationRole>>> filterQueries = [];

    // Creates a new instance of the GridQueryAdapter class.
    // controls: The IApplicationRoleFilters" to use.
    public ApplicationRoleGridQueryAdapter(IApplicationRoleFilters controls)
    {
        this.controls = controls;

        // Set up queries.
        filterQueries =
            new()
            {
                { ApplicationRoleFilterColumns.Name, cs => cs.Where(c => c != null && c.Name != null && this.controls.FilterText != null && c.Name.Contains(this.controls.FilterText) ) },
            };
    }

    // Uses the query to return a count and a page.
    // query: The IQueryable{ApplicationRole} to work from.
    // Returns the resulting ICollection{ApplicationRole}.
    public async Task<ICollection<ApplicationRole>> FetchAsync(IQueryable<ApplicationRole> query)
    {
        query = FilterAndQuery(query);
        await CountAsync(query);
        var collection = await FetchPageQuery(query).ToListAsync();
        controls.PageHelper.PageItems = collection.Count;
        return collection;
    }

    // Get total filtered items count.
    // query: The IQueryable{ApplicationRole} to use.
    public async Task CountAsync(IQueryable<ApplicationRole> query) =>
        controls.PageHelper.TotalItemCount = await query.CountAsync();

    // Build the query to bring back a single page.
    // query: The <see IQueryable{ApplicationRole} to modify.
    // Returns the new IQueryable{ApplicationRole} for a page.
    public IQueryable<ApplicationRole> FetchPageQuery(IQueryable<ApplicationRole> query) =>
        query
            .Skip(controls.PageHelper.Skip)
            .Take(controls.PageHelper.PageSize)
            .AsNoTracking();

    // Builds the query.
    // root: The IQueryable{ApplicationRole} to start with.
    // Returns the resulting IQueryable{ApplicationRole} with sorts and filters applied.
    private IQueryable<ApplicationRole> FilterAndQuery(IQueryable<ApplicationRole> root)
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
