using AutoMapper;
using Cashflow.Communication.Responses;
using Cashflow.Domain.Repositories.Expense;
using Cashflow.Domain.Services.LoggedUser;
using Cashflow.Exception;
using Cashflow.Exception.ExceptionBase;

namespace Cashflow.Application.UseCases.Expenses.GetById;
public class GetExpenseByIdUseCase : IGetExpenseByIdUseCase
{
    private readonly IExpensesReadFromRepository _repository;
    private readonly IMapper _mapper;
    private readonly ILoggedUser _loggedUser;
    public GetExpenseByIdUseCase(
        IExpensesReadFromRepository repository,
        IMapper mapper,
        ILoggedUser loggedUser
        )
    {
        _repository = repository;
        _mapper = mapper;
        _loggedUser = loggedUser;
    }
    public async Task<ResponseShortExpenseJson> Execute(long id)
    {
        var loggedUser =await _loggedUser.Get();
        var result = await _repository.GetById(id, loggedUser);

        return result is null
            ? throw new NotFoundException(ResourceErrorMessages.EXPENSE_NOT_FOUND)
            : _mapper.Map<ResponseShortExpenseJson>(result);
    }

}