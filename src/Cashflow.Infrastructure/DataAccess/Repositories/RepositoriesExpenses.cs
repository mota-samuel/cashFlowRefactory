using Cashflow.Domain.Entities;

namespace Cashflow.Infrastructure.DataAccess.Repositories;
internal class RepositoriesExpenses : IExpensesRepositories
{
    public void Add(Expense expense)
    {
        var dbContext = new CashFlowDbContext();

        dbContext.Add(expense);
        dbContext.SaveChanges();
    }
}
