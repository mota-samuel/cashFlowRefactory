using Cashflow.Domain.Entities;
using Cashflow.Domain.Repositories.Expense;
using Microsoft.EntityFrameworkCore;

namespace Cashflow.Infrastructure.DataAccess.Repositories;
internal class RepositoriesExpenses : IExpensesReadFromRepository, IExpensesWriteRepository
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

    public async Task<Expense> GetById(Guid id)
    {
        return await _dbContext.Expenses.AsNoTracking().FirstOrDefaultAsync(item => item.Id.Equals(id));
    }
}
