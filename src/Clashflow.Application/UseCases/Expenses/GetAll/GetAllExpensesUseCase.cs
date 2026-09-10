using AutoMapper;
using Cashflow.Communication.Responses;
using Cashflow.Domain.Repositories.Expense;
using Cashflow.Domain.Services.LoggedUser;

namespace Cashflow.Application.UseCases.Expenses.GetAll;
public class GetAllExpensesUseCase : IGetAllExpensesUseCase
{
    private readonly IExpensesReadFromRepository _repository;
    private readonly IMapper _mapper;
    private readonly ILoggedUser _LoggedUser;
    public GetAllExpensesUseCase(
        IExpensesReadFromRepository repository,
        IMapper mapper,
        ILoggedUser loggedUser
        )
    {
        _repository = repository;
        _mapper = mapper;
        _LoggedUser = loggedUser;
    }
    public async Task<ResponseExpensesJson> Execute()
    {
        var loggedUser =await _LoggedUser.Get();

        var result = await _repository.GetAll(loggedUser);
        return new ResponseExpensesJson()
        {
            AllExpenses = _mapper.Map<List<ResponseShortExpenseJson>>(result)
        };
    }
}
