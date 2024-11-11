using System.Diagnostics;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using ApplicationNamePlaceholder.BusinessLogic.Entities;

namespace ApplicationNamePlaceholder.BusinessLogic.Grid.Admin.ApplicationUserGrid;

// Creates the correct expressions to filter and sort.
public class ApplicationUserGridQueryAdapter
{
    // Holds state of the grid.
    private readonly IApplicationUserFilters controls;

    // Expressions for sorting.
    private readonly Dictionary<ApplicationUserFilterColumns, Expression<Func<ApplicationUser, string>>> expressions =
        new()
        {
            { ApplicationUserFilterColumns.Email, c => c != null && c.Email != null ? c.Email : string.Empty },
            { ApplicationUserFilterColumns.PhoneNumber, c => c != null && c.PhoneNumber != null ? c.PhoneNumber : string.Empty },
            { ApplicationUserFilterColumns.UserName, c => c != null && c.UserName != null ? c.UserName : string.Empty },
        };

    // Queryables for filtering.
    private readonly Dictionary<ApplicationUserFilterColumns, Func<IQueryable<ApplicationUser>, IQueryable<ApplicationUser>>> filterQueries = [];

    // Creates a new instance of the GridQueryAdapter class.
    // controls: The IApplicationUserFilters" to use.
    public ApplicationUserGridQueryAdapter(IApplicationUserFilters controls)
    {
        this.controls = controls;

        // Set up queries.
        filterQueries =
            new()
            {
                { ApplicationUserFilterColumns.Email, cs => cs.Where(c => c != null && c.Email != null && this.controls.FilterText != null && c.Email.Contains(this.controls.FilterText) ) },
                { ApplicationUserFilterColumns.PhoneNumber, cs => cs.Where(c => c != null && c.PhoneNumber != null && this.controls.FilterText != null && c.PhoneNumber.Contains(this.controls.FilterText) ) },
                { ApplicationUserFilterColumns.UserName, cs => cs.Where(c => c != null && c.UserName != null && this.controls.FilterText != null && c.UserName.Contains(this.controls.FilterText) ) },
            };
    }

    // Uses the query to return a count and a page.
    // query: The IQueryable{ApplicationUser} to work from.
    // Returns the resulting ICollection{ApplicationUser}.
    public async Task<ICollection<ApplicationUser>> FetchAsync(IQueryable<ApplicationUser> query)
    {
        query = FilterAndQuery(query);
        await CountAsync(query);
        var collection = await FetchPageQuery(query).ToListAsync();
        controls.PageHelper.PageItems = collection.Count;
        return collection;
    }

    // Get total filtered items count.
    // query: The IQueryable{ApplicationUser} to use.
    public async Task CountAsync(IQueryable<ApplicationUser> query) =>
        controls.PageHelper.TotalItemCount = await query.CountAsync();

    // Build the query to bring back a single page.
    // query: The <see IQueryable{ApplicationUser} to modify.
    // Returns the new IQueryable{ApplicationUser} for a page.
    public IQueryable<ApplicationUser> FetchPageQuery(IQueryable<ApplicationUser> query) =>
        query
            .Skip(controls.PageHelper.Skip)
            .Take(controls.PageHelper.PageSize)
            .AsNoTracking();

    // Builds the query.
    // root: The IQueryable{ApplicationUser} to start with.
    // Returns the resulting IQueryable{ApplicationUser} with sorts and filters applied.
    private IQueryable<ApplicationUser> FilterAndQuery(IQueryable<ApplicationUser> root)
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

        // Fix name.
        if (controls.SortColumn == ApplicationUserFilterColumns.UserName && controls.ShowUserNameFirst)
        {
            sb.Append("(name first) ");
            expression = c => c != null && c.UserName != null ? c.UserName : string.Empty;
        }

        var sortDir = controls.SortAscending ? "ASC" : "DESC";
        sb.Append(sortDir);

        Debug.WriteLine(sb.ToString());

        // Return the unfiltered query for total count, and the filtered for fetching.
        return controls.SortAscending ? root.OrderBy(expression) : root.OrderByDescending(expression);
    }
}
