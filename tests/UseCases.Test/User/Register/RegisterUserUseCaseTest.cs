using Cashflow.Application.UseCases.User.Register;
using Cashflow.Exception;
using Cashflow.Exception.ExceptionBase;
using CommonTestsUtilities.Cryptography;
using CommonTestsUtilities.Mapper;
using CommonTestsUtilities.Repositories;
using CommonTestsUtilities.Requests;
using CommonTestsUtilities.Token;
using FluentAssertions;

namespace UseCases.Test.User.Register;
public class RegisterUserUseCaseTest
{
    [Fact]
    public async Task Success()
    {
        var request = RequestRegisterUserJsonBuilder.Build();
        var useCase = FakeUseCase();

        var result = await useCase.Execute(request);

        result.Should().NotBeNull();
        result.Name.Should().Be(request.Name);
        result.Token.Should().NotBeNullOrWhiteSpace();
    }
    [Fact]
    public async Task Error_Name_Empty()
    {
        var request = RequestRegisterUserJsonBuilder.Build();
        request.Name = string.Empty;
        var useCase = FakeUseCase();

        var act = async () => await useCase.Execute(request);

       var result = await act.Should().ThrowAsync<ErrorOnValidationException>();

        result.Where(ex => ex.GetErrors().Count == 1 && ex.GetErrors().Contains(ResourceErrorMessages.NAME_EMPTY));
    }

    private RegisterUserUseCase FakeUseCase()
    {
        var mapper = MapperBuilder.Build();
        var unitOfWork = UnitOfWorkBuilde.Build();
        var readRepository = new UserReadOnlyRepositoryBuilder().Build();
        var encripty = PasswordEncripterBuilder.Build();
        var token = JwtTokenGeneratorBuilder.Build();
        var writeRepository = UserWriteOnlyRepositoryBuilder.Build();

        return new RegisterUserUseCase(readRepository, unitOfWork, mapper, encripty, writeRepository, token);
    }
}
