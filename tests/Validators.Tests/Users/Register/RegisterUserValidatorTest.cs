using Cashflow.Application.UseCases.User;
using Cashflow.Exception;
using CommonTestsUtilities.Requests;
using FluentAssertions;

namespace Validators.Tests.Users.Register;
public class RegisterUserValidatorTest
{
    [Fact]
    public void Success()
    {
        //arrange
        var validator = new UserValidator();
        var request = RequestRegisterUserJsonBuilder.Build();

        //act
        var result = validator.Validate(request);

        //assert
        result.IsValid.Should().BeTrue();
    }

    [Theory]
    [InlineData(" ")]
    [InlineData("")]
    [InlineData(null)]
    public void Error_NameWithSpaceOrBlank(string name)
    {
        //arrange
        var validator = new UserValidator();
        var request = RequestRegisterUserJsonBuilder.Build();
        request.Name = name;

        //act
        var result = validator.Validate(request);

        //assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.NAME_EMPTY));
    }

    [Theory]
    [InlineData(" ")]
    [InlineData("")]
    [InlineData(null)]
    public void Error_Email_Empty(string email)
    {
        //arrange
        var validator = new UserValidator();
        var request = RequestRegisterUserJsonBuilder.Build();
        request.Email = email;

        //act
        var result = validator.Validate(request);

        //assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.INVALID_EMAIL));
    }
}