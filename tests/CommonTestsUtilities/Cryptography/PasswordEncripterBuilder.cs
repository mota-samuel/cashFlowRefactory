using Cashflow.Domain.Security.Cryptography;
using Moq;

namespace CommonTestsUtilities.Cryptography;
public class PasswordEncripterBuilder
{
    public static IPasswordEncripter Build()
    {
        var mock = new Mock<IPasswordEncripter>();

        mock.Setup(config => config.Encrypt(It.IsAny<string>())).Returns("!jadej3%knm$@knm34%");

        return mock.Object;
    }
}
