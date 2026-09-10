namespace Cashflow.Domain.Repositories.Expense;
using Cashflow.Domain.Entities;
public interface IExpensesReadFromRepository
{
    public Task<List<Expense>> GetAll(Entities.User loggedUser);
    public Task<Expense> GetById(long id, Entities.User loggedUser);

    Task<List<Expense>> FilterByMonth(DateOnly date);
}
