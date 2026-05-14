using Cashflow.Application.UseCases.User.Register;
using CommonTestsUtilities.Mapper;
using CommonTestsUtilities.Repositories;
using CommonTestsUtilities.Requests;
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
        result.Token.Should().NotBeNullOrEmpty();
    }

    private RegisterUserUseCase FakeUseCase()
    {
        var mapper = MapperBuilder.Build();
        var unitOfWork = UnitOfWorkBuilde.Build();
        var readRepository = UserReadOnlyRepositoryBuilder.Build();

        return new RegisterUserUseCase(readRepository, unitOfWork, mapper, null, null, null);
    }
}
