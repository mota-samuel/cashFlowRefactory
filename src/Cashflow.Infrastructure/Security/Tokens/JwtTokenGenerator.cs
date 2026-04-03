using Cashflow.Domain.Entities;
using Cashflow.Domain.Security.Tokens;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Cashflow.Infrastructure.Security.Tokens;
public class JwtTokenGenerator : IAccessTokenGenerator
{
    private readonly uint _expirationTimeInMinutes;
    private readonly string _secretKey;
    public JwtTokenGenerator(uint expirationTimeInMinutes, string secretKey)
    {
        _expirationTimeInMinutes = expirationTimeInMinutes;
        _secretKey = secretKey;
    }

    public string GenerateToken(User user)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Name),
            new Claim(ClaimTypes.Sid, user.UserId.ToString())
        }; 

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Expires = DateTime.UtcNow.AddMinutes(_expirationTimeInMinutes),
            SigningCredentials = new SigningCredentials(SecurityKey(), SecurityAlgorithms.HmacSha256Signature),
            Subject = new ClaimsIdentity()
        };

       var tokenHandler = new JwtSecurityTokenHandler();
       var securityToken = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(securityToken);
    }

    private SymmetricSecurityKey SecurityKey()
    {
        var key = System.Text.Encoding.UTF8.GetBytes(_secretKey);
        return new SymmetricSecurityKey(key);
    }   
}
