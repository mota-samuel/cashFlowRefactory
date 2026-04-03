using Cashflow.Application.UseCases.User;
using Cashflow.Communication.Requests;
using Cashflow.Domain.Entities;
using FluentValidation;

namespace Cashflow.Application.UseCases.Login;
public class LoginPasswordValidator : AbstractValidator<RequestLoginJson>
{
    public LoginPasswordValidator()
    {
        RuleFor(user => user.Password).SetValidator(new PasswordValidator<RequestLoginJson>());
    }
}
