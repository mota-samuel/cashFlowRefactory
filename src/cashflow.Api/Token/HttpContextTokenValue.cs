using Cashflow.Domain.Security.Tokens;

namespace cashflow.Api.Token;

public class HttpContextTokenValue : ITokenProvider
{
    private readonly IHttpContextAccessor _contextAccessor;

    public HttpContextTokenValue(IHttpContextAccessor accessor)
    {
        _contextAccessor = accessor;
    }
    public string GetTokenOnRequest()
    {
        var authorization = _contextAccessor.HttpContext!.Request.Headers.Authorization.ToString();

        return authorization["Bearer ".Length..].Trim();

    }
}
