namespace Cashflow.Exception.ExceptionBase;
public abstract class CashflowException : SystemException
{
    public abstract List<string> GetErrors();
    public abstract int StatusCode { get; }
    protected CashflowException(string message) : base(message)
    {
        
    }
}
