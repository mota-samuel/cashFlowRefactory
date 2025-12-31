using Cashflow.Application.UseCases.Expenses.Register;
using Cashflow.Communication.Enums;
using Cashflow.Exception;
using CommonTestsUtilities.Requests;
using FluentAssertions;

namespace Validators.Tests.Expenses.Register;
public class RegisterExpenseValidatorTest
{

    [Fact]
    public void Success()
    {
        //Arrange
        var validator = new RegisterExpenseValidator();
        //como a funcao Build é static, nao precisa instanciar a classe RequestRegisterExepenseJsonBuilder para acessar a funcao Build
        var request = RequestRegisterExepenseJsonBuilder.Build();

        //Act
        var result = validator.Validate(request);
        //Assert
        result.IsValid.Should().BeTrue();
    }

    [Theory]
    [InlineData("")]//testa string vazia
    [InlineData(null)]//testa valor nulo
    [InlineData("   ")]//testa string com apenas espaços em branco
    [InlineData("\t")]//testa string com tabulação
    [InlineData("\n")]//testa string com nova linha
    [InlineData(" \t\n")]//testa string com combinação de espaços, tabulação e nova linha
    public void Error_TitleIsEmpty(string title)
    {
        //Arrange
        var validator = new RegisterExpenseValidator();
        var request = RequestRegisterExepenseJsonBuilder.Build();
        request.Title = title;
        //Act
        var result = validator.Validate(request);
        //Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle()
            .Which.
            ErrorMessage.
            Should().Be(ResourceErrorMessages.REQUIRED_TITLE);
    }

    [Theory]//testa varios valores
    [InlineData(0)]//testa o valor 0
    [InlineData(-1.99)]//testa o valor -1
    [InlineData(-100)]//testa o valor -100
    public void Error_ValueGreatherThanZero(decimal amount)
    {
        //Arrange
        var validator = new RegisterExpenseValidator();
        var request = RequestRegisterExepenseJsonBuilder.Build();
        request.Amount = amount;
        //Act
        var result = validator.Validate(request);
        //Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle()
            .Which.
            ErrorMessage.
            Should().Be(ResourceErrorMessages.VALUE_GREATER_THAN_0);
    }

    [Fact]
    public void Error_PaymentType()
    {
        //Arrange
        var validator = new RegisterExpenseValidator();
        var request = RequestRegisterExepenseJsonBuilder.Build();
        request.PaymentType = (PaymentType)10;
        //Act
        var result = validator.Validate(request);
        //Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle()
            .Which.
            ErrorMessage.
            Should().Be(ResourceErrorMessages.INVALID_PAYMENT_TYPE);
    }

    [Fact]
    public void Error_DateFuture()
    {
        //Arrange
        var validator = new RegisterExpenseValidator();
        var request = RequestRegisterExepenseJsonBuilder.Build();
        request.Date = DateTime.UtcNow.AddDays(2);
        //Act
        var result = validator.Validate(request);
        //Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle()
            .Which.
            ErrorMessage.
            Should().Be(ResourceErrorMessages.DATE_IN_THE_FUTURE);
    }
}
