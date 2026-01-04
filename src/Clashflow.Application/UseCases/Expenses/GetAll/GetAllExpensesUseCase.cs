using AutoMapper;
using Cashflow.Communication.Responses;
using Cashflow.Domain.Repositories.Expense;

namespace Cashflow.Application.UseCases.Expenses.GetAll;
public class GetAllExpensesUseCase : IGetAllExpensesUseCase
{
    private readonly IExpensesReadFromRepository _repository;
    private readonly IMapper _mapper;
    public GetAllExpensesUseCase(IExpensesReadFromRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    public async Task<ResponseExpensesJson> Execute()
    {
        var result = await _repository.GetAll();
        return new ResponseExpensesJson()
        {
            AllExpenses = _mapper.Map<List<ResponseShortExpenseJson>>(result)
        };
    }
}
