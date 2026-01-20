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
        _dbContext.Expenses.Update(expense);
    }

    public async Task<List<Expense>> FilterByMonth(DateOnly date)
    {
        var dateStart = new DateTime(date.Year, date.Month,1).Date;
        var lastDay = DateTime.DaysInMonth(date.Year, date.Month);
        var dateEnd = new DateTime(date.Year, date.Month, lastDay, hour: 23, minute:59, second:59);

        return await _dbContext.Expenses
            .AsNoTracking()
            .Where(expense => expense.Date >= dateStart && expense.Date <= dateEnd)
            .OrderBy(expense => expense.Date)
            .ThenBy(expense => expense.Title)
            .ToListAsync();
    }
}
