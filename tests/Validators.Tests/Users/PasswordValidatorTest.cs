using Cashflow.Application.UseCases.User;
using Cashflow.Communication.Requests;
using FluentAssertions;
using FluentValidation;

namespace Validators.Tests.Users;
public class PasswordValidatorTest
{
    [Theory]
    [InlineData(" ")]
    [InlineData("")]
    [InlineData(null)]
    [InlineData("a")]
    [InlineData("aa")]
    [InlineData("aaa")]
    [InlineData("aaaa")]
    [InlineData("aaaaa")]
    [InlineData("aaaaaa")]
    [InlineData("aaaaaaa")]
    [InlineData("aaaaaaaa")]
    [InlineData("AAAAAAAA")]
    [InlineData("Aaaaaaaa")]
    [InlineData("Aaaaaa$A")]
    public void Error_NameWithSpaceOrBlank(string password)
    {
        //arrange
        var validator = new PasswordValidator<RequestRegisterUserJson>();


        //act
        var result = validator.IsValid(new ValidationContext<RequestRegisterUserJson>(new RequestRegisterUserJson()), password);

        //assert
        result.Should().BeFalse();
    }
}
