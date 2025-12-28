using Cashflow.Communication.Requests;
using Cashflow.Communication.Responses;
using Cashflow.Exception.ExceptionBase;

namespace Cashflow.Application.UseCases.Expenses.Register;
public class RegisterExpensesUseCase
{
    public ResponsResgisterExpenseJson Execute(RequestRegisterExepenseJson request)
    {
        Validate(request);

        return new ResponsResgisterExpenseJson();
    }

    private void Validate(RequestRegisterExepenseJson request)
    {
        var validator = new RegisterExpenseValidator();
        var validationResult = validator.Validate(request);
        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
            throw new ErrorOnValidationException(errors);
        }
    }
}
