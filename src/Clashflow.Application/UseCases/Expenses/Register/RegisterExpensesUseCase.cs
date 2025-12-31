using Cashflow.Communication.Requests;
using Cashflow.Communication.Responses;
using Cashflow.Domain.Entities;
using Cashflow.Domain.Enum;
using Cashflow.Exception.ExceptionBase;

namespace Cashflow.Application.UseCases.Expenses.Register;
public class RegisterExpensesUseCase : IRegisterExpensesUseCase
{
    private readonly IExpensesRepositories _repository;
    public RegisterExpensesUseCase(IExpensesRepositories repositorio)
    {
        _repository = repositorio;
    }
    public ResponsResgisterExpenseJson Execute(RequestRegisterExepenseJson request)
    {
        Validate(request);

        var entity = new Expense
        {
            Title = request.Title,
            Description = request.Description,
            Date = request.Date,
            Amount = request.Amount,
            PaymentType = (PaymentType)request.PaymentType
        };

        _repository.Add(entity);


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
