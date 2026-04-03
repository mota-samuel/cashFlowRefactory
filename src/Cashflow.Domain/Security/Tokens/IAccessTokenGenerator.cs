using Cashflow.Domain.Entities;

namespace Cashflow.Domain.Security.Tokens;
public interface IAccessTokenGenerator
{
    public string GenerateToken(User user);
}
