
using Cashflow.Domain.Entities;

public interface IExpensesRepositories
{
    public Task Add(Expense expense);
    public Task<List<Expense>> GetAll();
    public Task<Expense> GetById(Guid id);
    
}
 