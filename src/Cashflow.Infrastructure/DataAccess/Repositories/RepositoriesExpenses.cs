using Cashflow.Domain.Entities;
using Cashflow.Domain.Repositories.Expense;
using Microsoft.EntityFrameworkCore;

namespace Cashflow.Infrastructure.DataAccess.Repositories;
internal class RepositoriesExpenses : IExpensesReadFromRepository, IExpensesWriteRepository, IExpenseUpdateRepository
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
    // utilizar AsNoTracking para situacoes onde nao alteramos os dados da consulta
    public async Task<List<Expense>> GetAll()
    {
        return await _dbContext.Expenses.AsNoTracking().ToListAsync();
    }

    async Task<Expense> IExpensesReadFromRepository.GetById(Guid id)
    {
        return await _dbContext.Expenses.AsNoTracking().FirstOrDefaultAsync(item => item.Id.Equals(id));
    }
    async Task<Expense> IExpenseUpdateRepository.GetById(Guid id)
    {
        return await _dbContext.Expenses.FirstOrDefaultAsync(item => item.Id.Equals(id));
    }
    public async Task<bool> Delete(Guid id)
    {
        var result =  await _dbContext.Expenses.AsNoTracking().FirstOrDefaultAsync(item => item.Id.Equals(id));
        if (result is null) return false;

        _dbContext.Expenses.Remove(result);

        return true;
    
    }

    public void Update(Expense expense)
    {
        throw new NotImplementedException();
    }
}
