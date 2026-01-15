
using Cashflow.Domain.Repositories;
using Cashflow.Domain.Repositories.Expense;
using Cashflow.Exception;
using Cashflow.Exception.ExceptionBase;

namespace Cashflow.Application.UseCases.Expenses.Delete;
public class DeleteExpenseUseCase : IDeleteExpenseUseCase
{   
    private readonly IExpensesWriteRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteExpenseUseCase(IExpensesWriteRepository repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }
    public async Task Execute(Guid id)
    {
        var result = await _repository.Delete(id);
        
        if (!result)
        {
            throw new NotFoundException(ResourceErrorMessages.EXPENSE_NOT_FOUND);
        }

        await _unitOfWork.Commit();
    }
}
