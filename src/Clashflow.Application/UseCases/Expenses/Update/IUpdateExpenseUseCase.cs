using Cashflow.Communication.Requests;

namespace Cashflow.Application.UseCases.Expenses.Update;
public interface IUpdateExpenseUseCase
{
    public Task Execute(Guid id, RequestExepenseJson request);
}
