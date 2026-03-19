using AutoMapper;
using Cashflow.Communication.Requests;
using Cashflow.Communication.Responses;
using Cashflow.Domain.Repositories;
using Cashflow.Domain.Repositories.User;
using Cashflow.Domain.Security.Cryptography;
using Cashflow.Exception;
using Cashflow.Exception.ExceptionBase;
using FluentValidation.Results;

namespace Cashflow.Application.UseCases.User.Register;
public class RegisterUserUseCase : IRegisterUserUseCase
{
    private readonly IUserReadOnlyRepository _userReadOnlyRepository;
    private readonly IUserWriteOnlyRepository _userWriteOnlyRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IPasswordEncripter _passwordEncripter;
    public RegisterUserUseCase(
        IUserReadOnlyRepository repositorio,
        IUnitOfWork unitOfWork,
        IMapper mapper,
        IPasswordEncripter passwordEncripter,
        IUserWriteOnlyRepository userWriteOnlyRepository
        )
    {
        _userReadOnlyRepository = repositorio;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _passwordEncripter = passwordEncripter;
        _userWriteOnlyRepository = userWriteOnlyRepository;
    }
    public async Task<ResponseRegisteredUserJson> Execute(RequestRegisterUserJson request)
    {
        await Validate(request);

        var user = _mapper.Map<Domain.Entities.User>(request);
        user.Password = _passwordEncripter.Encrypt(request.Password);
        user.UserId = Guid.NewGuid();

        await _userWriteOnlyRepository.Add(user);
        await _unitOfWork.Commit();

        return new ResponseRegisteredUserJson
        {
            Name = user.Name,
        };
     }
    private async Task Validate(RequestRegisterUserJson request)
    {
        var validator = new UserValidator().Validate(request);
        var emailExist = await _userReadOnlyRepository.ExistActiveUserWithEmail(request.Email);

        if (emailExist)
        {
           validator.Errors.Add(new ValidationFailure(string.Empty, ResourceErrorMessages.EMAIL_ALREADY_REGISTERED));
        }


        if (!validator.IsValid)
        {
            var errors = validator.Errors.Select(e => e.ErrorMessage).ToList();
            throw new ErrorOnValidationException(errors);
        }
    }
}
