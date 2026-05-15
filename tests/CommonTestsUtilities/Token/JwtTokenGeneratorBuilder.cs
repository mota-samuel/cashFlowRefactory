using Cashflow.Domain.Entities;
using Cashflow.Domain.Security.Tokens;
using Moq;

namespace CommonTestsUtilities.Token;
public class JwtTokenGeneratorBuilder
{
    public static IAccessTokenGenerator Build()
    {
        var mock = new Mock<IAccessTokenGenerator>();

        mock.Setup(configToken => configToken.GenerateToken(It.IsAny<User>())).Returns("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYmYiOjE3Nzg4MDE4NDIsImV4cCI6MTc3ODgwMjE0MiwiaWF0IjoxNzc4ODAxODQyfQ.hNnwwe2QLxeIU2hCBxvv_6h6HIJBN6ge3qgw-I9SQFc");

        return mock.Object;
    }
}
