using Cashflow.Communication.Requests;
using Cashflow.Communication.Responses;

namespace Cashflow.Application.UseCases.Expenses.Register;
public interface IRegisterExpensesUseCase
{
    public ResponsResgisterExpenseJson Execute(RequestRegisterExepenseJson request);
}
