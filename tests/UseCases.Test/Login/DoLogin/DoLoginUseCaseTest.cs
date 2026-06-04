using Cashflow.Application.UseCases.Login.UseCase;
using Cashflow.Exception;
using Cashflow.Exception.ExceptionBase;
using CommonTestsUtilities.Cryptography;
using CommonTestsUtilities.Entities;
using CommonTestsUtilities.Repositories;
using CommonTestsUtilities.Requests;
using CommonTestsUtilities.Token;
using FluentAssertions;

namespace UseCases.Test.Login.DoLogin;
public class DoLoginUseCaseTest
{
    [Fact]
        public async Task Success()
        {
            var user = UserBuilder.Build();

            var request = RequestLoginJsonBuilder.Build();
            request.Email = user.Email;  

        var useCase = FakeDoLoginUseCase(user, request.Password);
    
            var result = await useCase.Execute(request);
    
            result.Should().NotBeNull();
            result.Name.Should().Be(user.Name);
            result.Token.Should().NotBeNullOrWhiteSpace();
        }
    [Fact]
        public async Task Error_User_NotFound()
        {
            var user = UserBuilder.Build();
            var request = RequestLoginJsonBuilder.Build();
            var useCase = FakeDoLoginUseCase(user, request.Password);
    
            var act = async() => await useCase.Execute(request);

            var result = await act.Should().ThrowAsync<InvalidLoginException>();

            result.Where(ex => ex.GetErrors().Count == 1 && ex.GetErrors().Contains(ResourceErrorMessages.EMAIL_OU_PASSWORD_INVALID));
        }
    [Fact]
        public async Task Error_Password_NotMatch()
        {
            var user = UserBuilder.Build();
            var request = RequestLoginJsonBuilder.Build();

            request.Email = user.Email;

            var useCase = FakeDoLoginUseCase(user);
    
            var act = async() => await useCase.Execute(request);

            var result = await act.Should().ThrowAsync<InvalidLoginException>();

            result.Where(ex => ex.GetErrors().Count == 1 && ex.GetErrors().Contains(ResourceErrorMessages.EMAIL_OU_PASSWORD_INVALID));
        }

    private DoLoginUseCase FakeDoLoginUseCase(Cashflow.Domain.Entities.User user, string? password = null)
    {
        var passwordEncripter =new PasswordEncrypterBuilder().Verify(password).Build();
        var tokenGenerator = JwtTokenGeneratorBuilder.Build();
        var readRepository = new UserReadOnlyRepositoryBuilder().GetByEmail(user).Build();

        return new DoLoginUseCase(readRepository, passwordEncripter, tokenGenerator);
    }
}
