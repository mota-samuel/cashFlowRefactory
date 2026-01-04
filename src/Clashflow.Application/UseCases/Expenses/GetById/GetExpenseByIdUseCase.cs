using AutoMapper;
using Cashflow.Communication.Responses;
using Cashflow.Domain.Repositories.Expense;
using Cashflow.Exception;
using Cashflow.Exception.ExceptionBase;

namespace Cashflow.Application.UseCases.Expenses.GetById;
public class GetExpenseByIdUseCase : IGetExpenseByIdUseCase
{
    private readonly IExpensesReadFromRepository _repository;
    private readonly IMapper _mapper;
    public GetExpenseByIdUseCase(IExpensesReadFromRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    public async Task<ResponseShortExpenseJson> Execute(Guid id)
    {
        var result = await _repository.GetById(id);

        return result is null
            ? throw new NotFoundException(ResourceErrorMessages.EXPENSE_NOT_FOUND)
            : _mapper.Map<ResponseShortExpenseJson>(result);
    }

}