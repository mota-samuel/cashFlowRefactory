using Cashflow.Domain.Security.Cryptography;
using BC = BCrypt.Net.BCrypt;

namespace Cashflow.Infrastructure.Security;
public class BCrypt : IPasswordEncripter
{
    public string Encrypt(string password)
    {
       string passwordHash = BC.HashPassword(password);
           return passwordHash;
    }
}
