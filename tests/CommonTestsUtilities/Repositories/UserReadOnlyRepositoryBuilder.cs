using Cashflow.Domain.Repositories.User;
using Moq;

namespace CommonTestsUtilities.Repositories;
public class UserReadOnlyRepositoryBuilder
{
    public static IUserReadOnlyRepository Build()
    {
        var mock = new Mock<IUserReadOnlyRepository>();
        return mock.Object;
    }
}
