using Cashflow.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Cashflow.Infrastructure.Migrations;
public static class DataBaseMigration
{
    public async static Task MigrateDataBase(IServiceProvider serviceProvider)
    {
        var dbContext = serviceProvider.GetRequiredService<CashFlowDbContext>();

        await dbContext.Database.MigrateAsync();
    }
}
