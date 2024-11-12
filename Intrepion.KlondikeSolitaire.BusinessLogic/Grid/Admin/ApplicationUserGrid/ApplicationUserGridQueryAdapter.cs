﻿using System.Diagnostics;
using System.Linq.Expressions;
using Intrepion.KlondikeSolitaire.BusinessLogic.Entities;
using Microsoft.EntityFrameworkCore;

namespace Intrepion.KlondikeSolitaire.BusinessLogic.Grid.Admin.ApplicationUserGrid;

// Creates the correct expressions to filter and sort.
public class ApplicationUserGridQueryAdapter
{
    // Holds state of the grid.
    private readonly IApplicationUserFilters controls;

    // Expressions for sorting.
    private readonly Dictionary<ApplicationUserFilterColumns, Expression<Func<ApplicationUser, string>>> expressions =
        new()
        {
            { ApplicationUserFilterColumns.Email, x => x != null && x.Email != null ? x.Email : string.Empty },
            { ApplicationUserFilterColumns.PhoneNumber, x => x != null && x.PhoneNumber != null ? x.PhoneNumber : string.Empty },
            { ApplicationUserFilterColumns.UserName, x => x != null && x.UserName != null ? x.UserName : string.Empty },
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
                { ApplicationUserFilterColumns.Email, x => x.Where(y => y != null && y.Email != null && this.controls.FilterText != null && y.Email.Contains(this.controls.FilterText) ) },
                { ApplicationUserFilterColumns.PhoneNumber, x => x.Where(y => y != null && y.PhoneNumber != null && this.controls.FilterText != null && y.PhoneNumber.Contains(this.controls.FilterText) ) },
                { ApplicationUserFilterColumns.UserName, x => x.Where(y => y != null && y.UserName != null && this.controls.FilterText != null && y.UserName.Contains(this.controls.FilterText) ) },
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
            expression = x => x != null && x.UserName != null ? x.UserName : string.Empty;
        }

        var sortDir = controls.SortAscending ? "ASC" : "DESC";
        sb.Append(sortDir);

        Debug.WriteLine(sb.ToString());

        // Return the unfiltered query for total count, and the filtered for fetching.
        return controls.SortAscending ? root.OrderBy(expression) : root.OrderByDescending(expression);
    }
}
