using Cashflow.Communication.Requests;
using Cashflow.Exception;
using FluentValidation;

namespace Cashflow.Application.UseCases.User;
public class UserValidator : AbstractValidator<RequestRegisterUserJson>
{
    public UserValidator()
    {
        RuleFor(user => user.Name).NotEmpty().WithMessage(ResourceErrorMessages.NAME_EMPTY);
        RuleFor(user => user.Email)
            .NotEmpty()
            .WithMessage(ResourceErrorMessages.INVALID_EMAIL)
            .EmailAddress()
            .When(user => !string.IsNullOrWhiteSpace(user.Email), ApplyConditionTo.CurrentValidator)
            .WithMessage(ResourceErrorMessages.INVALID_EMAIL);
        RuleFor(user => user.Password).SetValidator(new PasswordValidator<RequestRegisterUserJson>());
    }
}
