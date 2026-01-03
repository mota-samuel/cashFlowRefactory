namespace Cashflow.Exception.ExceptionBase;
public class ErrorOnValidationException : CashflowException
{
    public List<string> Errors { get; }
    public ErrorOnValidationException(List<string> errors) : base(string.Empty)
    {
        Errors = errors;
    }

}
