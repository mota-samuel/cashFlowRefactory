using Cashflow.Communication.Requests;

namespace Cashflow.Application.UseCases.Expenses.Update;
public interface IUpdateExpenseUseCase
{
    public Task Execute(long id, RequestExepenseJson request);
}
