
using Cashflow.Domain.Repositories;
using Cashflow.Domain.Repositories.Expense;
using Cashflow.Domain.Services.LoggedUser;
using Cashflow.Exception;
using Cashflow.Exception.ExceptionBase;

namespace Cashflow.Application.UseCases.Expenses.Delete;
public class DeleteExpenseUseCase : IDeleteExpenseUseCase
{   
    private readonly IExpensesWriteRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILoggedUser _loggedUser;
    private readonly IExpensesReadFromRepository _ExpenseReadOnly;

    public DeleteExpenseUseCase(
        IExpensesWriteRepository repository,
        IUnitOfWork unitOfWork,
        ILoggedUser loggedUser,
        IExpensesReadFromRepository expensesReadOnly
        )
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _loggedUser = loggedUser;
        _ExpenseReadOnly = expensesReadOnly;
    }
    public async Task Execute(long id)
    {
        var loggedUser = await _loggedUser.Get();

        var expense = _ExpenseReadOnly.GetById(id, loggedUser);
        if (expense is null)
        {
            throw new NotFoundException(ResourceErrorMessages.EXPENSE_NOT_FOUND);
        }

        var result = _repository.Delete(id);

        await _unitOfWork.Commit();
    }
}
