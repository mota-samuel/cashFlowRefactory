using Cashflow.Communication.Requests;
using Cashflow.Exception;
using FluentValidation;

namespace Cashflow.Application.UseCases.User;
public class UserValidator : AbstractValidator<RequestRegisterUserJson>
{
    public UserValidator()
    {
        RuleFor(user => user.Name).NotEmpty().WithMessage(ResourceErrorMessages.NAME_EMPTY);
        RuleFor(user => user.Email).NotEmpty().EmailAddress().WithMessage(ResourceErrorMessages.INVALID_EMAIL);
        RuleFor(user => user.Password).NotEmpty().MinimumLength(8).WithMessage(ResourceErrorMessages.PASSWORD_8CHAR).Matches("[A-Z]").WithMessage(ResourceErrorMessages.PASSWORD_1UPPER).Matches("[^a-zA-Z0-9]").WithMessage(ResourceErrorMessages.PASSWORD_SPECIALCHAR);
        RuleFor(user => user.Role).IsInEnum().WithMessage(ResourceErrorMessages.INVALID_ROLE);
    }
}
