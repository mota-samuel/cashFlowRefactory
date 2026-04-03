
using System.Net;

namespace Cashflow.Exception.ExceptionBase;
public class InvalidLoginException : CashflowException
{
    public InvalidLoginException() : base(ResourceErrorMessages.EMAIL_OU_PASSWORD_INVALID)
    {
        
    }

    public override int StatusCode => (int)HttpStatusCode.Unauthorized;

    public override List<string> GetErrors()
    {
        return [Message];
    }
}
