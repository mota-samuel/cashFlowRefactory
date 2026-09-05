namespace Cashflow.Domain.Repositories.Expense;
using Cashflow.Domain.Entities;
public interface IExpenseUpdateRepository
{
    public Task<Expense> GetById(long id, Domain.Entities.User user);
    public void Update(Expense expense);
}
