using Cashflow.Domain.Services.LoggedUser;
using Moq;

namespace UseCases.Test.Expense.Register;

public class LoggedUserBuilder
{
    public static ILoggedUser Build(Cashflow.Domain.Entities.User user)
    {
        var mock = new Mock<ILoggedUser>();

        mock.Setup(loggedUser => loggedUser.Get()).ReturnsAsync(user);

        return mock.Object;
    }
}