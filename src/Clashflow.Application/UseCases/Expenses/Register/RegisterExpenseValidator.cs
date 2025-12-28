using Cashflow.Communication.Requests;
using FluentValidation;

namespace Cashflow.Application.UseCases.Expenses.Register;
public class RegisterExpenseValidator : AbstractValidator<RequestRegisterExepenseJson>
{
    public RegisterExpenseValidator()
    {
        RuleFor(expense => expense.Title).NotEmpty().WithMessage("Title is required.");
        RuleFor(expense => expense.Amount).GreaterThan(0).WithMessage("Amount must be greater than zero.");
        RuleFor(expense => expense.Date).LessThanOrEqualTo(DateTime.Now).WithMessage("Date can`t be in the future.");
        RuleFor(expense => expense.PaymentType).IsInEnum().WithMessage("Invalid payment type.");
    }
}
