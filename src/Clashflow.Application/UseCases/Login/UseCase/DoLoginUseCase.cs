using Cashflow.Communication.Requests;
using Cashflow.Communication.Responses;
using Cashflow.Domain.Repositories.User;
using Cashflow.Domain.Security.Cryptography;
using Cashflow.Domain.Security.Tokens;
using Cashflow.Exception.ExceptionBase;

namespace Cashflow.Application.UseCases.Login.UseCase;
public class DoLoginUseCase : IDoLoginUseCase
{
    private readonly IUserReadOnlyRepository _repository;
    private readonly IPasswordEncripter _passwordEncripter;
    private readonly IAccessTokenGenerator _accessTokenGenerator;

    public DoLoginUseCase(IUserReadOnlyRepository repository, IPasswordEncripter passwordEncripter, IAccessTokenGenerator accessTokenGenerator)
    {
        _repository = repository;
        _passwordEncripter = passwordEncripter;
        _accessTokenGenerator = accessTokenGenerator;
    }

    public async Task<ResponseRegisteredUserJson> Execute(RequestLoginJson request)
    {
        var user = await _repository.GetByEmail(request.Email);

        if (user is null)
            throw new InvalidLoginException();

        Validate(request);

        var passwordMatch = _passwordEncripter.Verify(request.Password, user.Password);

        if (!passwordMatch)
            throw new InvalidLoginException();

        return new ResponseRegisteredUserJson
        {
            Name = user.Name,
            Token = _accessTokenGenerator.GenerateToken(user)
        };

    }

    private void Validate(RequestLoginJson request)
    {
        var validator = new LoginPasswordValidator().Validate(request);
        if (!validator.IsValid)
        {
            var errors = validator.Errors.Select(e => e.ErrorMessage).ToList();
            throw new ErrorOnValidationException(errors);
        }
    }
}
