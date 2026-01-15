using Cashflow.Communication.Requests;
using Cashflow.Communication.Responses;

namespace Cashflow.Application.UseCases.Expenses.Register;
public interface IRegisterExpensesUseCase
{
    public Task<ResponsResgisterExpenseJson> Execute(RequestExepenseJson request);
}
