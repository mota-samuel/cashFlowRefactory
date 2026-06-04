using Cashflow.Domain.Security.Cryptography;
using Moq;

namespace CommonTestsUtilities.Cryptography;
public class PasswordEncrypterBuilder
{
    private  readonly Mock<IPasswordEncripter> _mock;

    public PasswordEncrypterBuilder()
    {
        _mock = new Mock<IPasswordEncripter>();
        _mock.Setup(config => config.Encrypt(It.IsAny<string>())).Returns("!jADej3%knm$@knm34%");
    }

    public PasswordEncrypterBuilder Verify(string? password)
    {
        if(!string.IsNullOrEmpty(password))
        { 
            _mock.Setup(config => config.Verify(password, It.IsAny<string>())).Returns(true);
        }
        return this;
    }

    public IPasswordEncripter Build() => _mock.Object;  
}
