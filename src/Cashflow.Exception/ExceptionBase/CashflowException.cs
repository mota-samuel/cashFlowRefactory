namespace Cashflow.Exception.ExceptionBase;
public abstract class CashflowException : SystemException
{
    protected CashflowException(string message) : base(message)
    {
        
    }
}
