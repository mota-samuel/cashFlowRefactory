using Cashflow.Communication.Requests;
using Cashflow.Exception;
using FluentValidation;

namespace Cashflow.Application.UseCases.Expenses;
public class ExpenseValidator : AbstractValidator<RequestExepenseJson>
{
    public ExpenseValidator()
    {
        RuleFor(expense => expense.Title).NotEmpty().WithMessage(ResourceErrorMessages.REQUIRED_TITLE);
        RuleFor(expense => expense.Amount).GreaterThan(0).WithMessage(ResourceErrorMessages.VALUE_GREATER_THAN_0);
        RuleFor(expense => expense.Date).LessThanOrEqualTo(DateTime.Now).WithMessage(ResourceErrorMessages.DATE_IN_THE_FUTURE);
        RuleFor(expense => expense.PaymentType).IsInEnum().WithMessage(ResourceErrorMessages.INVALID_PAYMENT_TYPE);
    }
}
