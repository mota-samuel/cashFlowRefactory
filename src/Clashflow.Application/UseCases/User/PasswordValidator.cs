using Cashflow.Exception;
using FluentValidation;
using FluentValidation.Validators;
using System.Text.RegularExpressions;

namespace Cashflow.Application.UseCases.User;
public partial class PasswordValidator<T> : PropertyValidator<T, string>
{

    private const string ERROR_MESSAGE_KEY = "ErrorMessage";
    public override string Name => "PasswordValidator";

    public override bool IsValid(ValidationContext<T> context, string password)
    {
        if (string.IsNullOrWhiteSpace(password))
        {
            context.MessageFormatter.AppendArgument(ERROR_MESSAGE_KEY, ResourceErrorMessages.INVALID_PASSWORD);
            return false;
        }

        if(password.Length <= 8)
        {
            context.MessageFormatter.AppendArgument(ERROR_MESSAGE_KEY, ResourceErrorMessages.INVALID_PASSWORD);
            return false;
        }

        if(!UpperCaseLetter().IsMatch(password))
        {
            context.MessageFormatter.AppendArgument(ERROR_MESSAGE_KEY, ResourceErrorMessages.INVALID_PASSWORD);
            return false;
        }
        if(!Numbers().IsMatch(password))
        {
            context.MessageFormatter.AppendArgument(ERROR_MESSAGE_KEY, ResourceErrorMessages.INVALID_PASSWORD);
            return false;
        }
        if(!Regex.IsMatch(password, @"[\!\@\#\$\%\&\*\?\.]+"))
        {
            context.MessageFormatter.AppendArgument(ERROR_MESSAGE_KEY, ResourceErrorMessages.INVALID_PASSWORD);
            return false;
        }

        return true;
    }

    [GeneratedRegex(@"[A-Z]+")]
    private static partial Regex UpperCaseLetter();
    [GeneratedRegex(@"[0-9]+")]
    private static partial Regex Numbers();
}
