namespace Cashflow.Domain.Security.Tokens;
public interface ITokenProvider
{
    string GetTokenOnRequest();
}
