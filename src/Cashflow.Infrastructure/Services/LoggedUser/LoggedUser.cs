using Cashflow.Domain.Entities;
using Cashflow.Domain.Security.Tokens;
using Cashflow.Domain.Services.LoggedUser;
using Cashflow.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Cashflow.Infrastructure.Services.LoggedUser;
public class LoggedUser : ILoggedUser
{
    private readonly CashFlowDbContext _dbContext;
    private readonly ITokenProvider _tokenProvider;

    public LoggedUser(CashFlowDbContext dbContext, ITokenProvider tokenProvider)
    {
        _dbContext = dbContext;
        _tokenProvider = tokenProvider;

    }
    public Task<User> Get()
    {
        string token = _tokenProvider.GetTokenOnRequest();

        var tokenHandler = new JwtSecurityTokenHandler();

        var jwtSecurityToken = tokenHandler.ReadJwtToken(token);

        var identifier = jwtSecurityToken.Claims.First(claim => claim.Type == ClaimTypes.Sid).Value;

        return _dbContext
            .Users
            .AsNoTracking()
            .FirstAsync(user => user.UserId.Equals(Guid.Parse(identifier)));

    }
}
