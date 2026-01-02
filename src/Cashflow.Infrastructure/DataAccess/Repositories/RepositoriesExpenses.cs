using Cashflow.Domain.Entities;

namespace Cashflow.Infrastructure.DataAccess.Repositories;
internal class RepositoriesExpenses : IExpensesRepositories
{
    private readonly CashFlowDbContext _dbContext;
    public RepositoriesExpenses(CashFlowDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task Add(Expense expense)
    {
       await _dbContext.AddAsync(expense);
    }
}
