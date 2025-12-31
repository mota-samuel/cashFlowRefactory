using Cashflow.Communication.Requests;
using Cashflow.Communication.Responses;
using Cashflow.Domain.Entities;
using Cashflow.Domain.Enum;
using Cashflow.Exception.ExceptionBase;
using Cashflow.Infrastructure.DataAccess;

namespace Cashflow.Application.UseCases.Expenses.Register;
public class RegisterExpensesUseCase
{
    public ResponsResgisterExpenseJson Execute(RequestRegisterExepenseJson request)
    {
        Validate(request);

        var dbContext = new CashFlowDbContext();
        var entity = new Expense
        {
            Title = request.Title,
            Description = request.Description,
            Date = request.Date,
            Amount = request.Amount,
            PaymentType = (PaymentType)request.PaymentType
        };

        dbContext.Expenses.Add(entity);
        dbContext.SaveChanges();    


        return new ResponsResgisterExpenseJson() { Title = request.Title};
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
