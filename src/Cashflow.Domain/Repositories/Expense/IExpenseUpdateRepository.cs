namespace Cashflow.Domain.Repositories.Expense;
using Cashflow.Domain.Entities;
public interface IExpenseUpdateRepository
{
    public Task<Expense> GetById(Guid id);
    public void Update(Expense expense);
}
