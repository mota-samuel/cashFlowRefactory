using Cashflow.Domain.Repositories;

namespace Cashflow.Infrastructure.DataAccess;
internal class UnitOfWork : IUnitOfWork
{
    private readonly CashFlowDbContext _dbContext;
    public UnitOfWork(CashFlowDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public void Commit() => _dbContext.SaveChanges();
}
