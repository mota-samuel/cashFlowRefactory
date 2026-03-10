using AutoMapper;
using Cashflow.Application.UseCases.Expenses;
using Cashflow.Communication.Requests;
using Cashflow.Communication.Responses;
using Cashflow.Domain.Repositories;
using Cashflow.Domain.Repositories.Expense;
using Cashflow.Domain.Security.Cryptography;
using Cashflow.Exception.ExceptionBase;

namespace Cashflow.Application.UseCases.User.Register;
public class RegisterUserUseCase : IRegisterUserUseCase
{
    private readonly IExpensesWriteRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IPasswordEncripter _passwordEncripter;
    public RegisterUserUseCase(IExpensesWriteRepository repositorio, IUnitOfWork unitOfWork, IMapper mapper, IPasswordEncripter passwordEncripter)
    {
        _repository = repositorio;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _passwordEncripter = passwordEncripter;
    }
    public Task<ResponseRegisteredUserJson> Execute(RequestRegisterUserJson request)
    {
        Validate(request);

        var user = _mapper.Map<Domain.Entities.User>(request);

        user.Password = _passwordEncripter.Encrypt(request.Password);
    }

    private static void Validate(RequestRegisterUserJson request)
    {
        var validator = new UserValidator();
        var validationResult = validator.Validate(request);
        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
            throw new ErrorOnValidationException(errors);
        }
    }
}
