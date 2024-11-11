using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ApplicationNamePlaceholder.BusinessLogic.Data;

public static class DatabaseUtility
{
    public static async Task EnsureDbCreatedAndSeedAsync(DbContextOptions<ApplicationDbContext> options, IServiceProvider serviceProvider)
    {
        var factory = new LoggerFactory();
        var builder = new DbContextOptionsBuilder<ApplicationDbContext>(options)
            .UseLoggerFactory(factory);

        using var applicationDbContext = new ApplicationDbContext(builder.Options);

        if (await applicationDbContext.Database.EnsureCreatedAsync())
        {
            await FakeData.SeedAsync(applicationDbContext, serviceProvider);
        }
    }
}
