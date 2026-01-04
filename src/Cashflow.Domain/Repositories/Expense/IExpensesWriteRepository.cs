namespace Cashflow.Domain.Repositories.Expense;
using Cashflow.Domain.Entities;
public interface IExpensesWriteRepository
{
    public Task Add(Expense expense);
}
