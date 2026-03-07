namespace Cashflow.Domain.Repositories.Expense;
using Cashflow.Domain.Entities;
public interface IExpensesReadFromRepository
{
    public Task<List<Expense>> GetAll();
    public Task<Expense> GetById(long id);

    Task<List<Expense>> FilterByMonth(DateOnly date);
}
