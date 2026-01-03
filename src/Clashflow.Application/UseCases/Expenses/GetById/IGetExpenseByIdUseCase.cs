using Cashflow.Communication.Responses;

namespace Cashflow.Application.UseCases.Expenses.GetById;
public interface IGetExpenseByIdUseCase
{
    public Task<ResponseShortExpenseJson> Execute(Guid id);
}
