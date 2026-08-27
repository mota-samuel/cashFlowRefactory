using Cashflow.Domain.Entities;

namespace Cashflow.Domain.Services.LoggedUser;
public interface ILoggedUser
{
    Task<User> Get();
}
