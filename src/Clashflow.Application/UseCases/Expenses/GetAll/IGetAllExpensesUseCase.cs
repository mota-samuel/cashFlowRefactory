using Cashflow.Communication.Responses;

namespace Cashflow.Application.UseCases.Expenses.GetAll;
public interface IGetAllExpensesUseCase
{
    public Task<ResponseExpensesJson> Execute();
}
