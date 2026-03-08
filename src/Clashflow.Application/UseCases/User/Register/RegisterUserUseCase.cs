using AutoMapper;
using Cashflow.Application.UseCases.Expenses;
using Cashflow.Communication.Requests;
using Cashflow.Communication.Responses;
using Cashflow.Domain.Repositories;
using Cashflow.Domain.Repositories.Expense;
using Cashflow.Exception.ExceptionBase;

namespace Cashflow.Application.UseCases.User.Register;
public class RegisterUserUseCase : IRegisterUserUseCase
{
    private readonly IExpensesWriteRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public RegisterUserUseCase(IExpensesWriteRepository repositorio, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _repository = repositorio;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public Task<ResponseRegisteredUserJson> Execute(RequestRegisterUserJson request)
    {
        Validate(request);
    }

    private void Validate(RequestExepenseJson request)
    {
        var validator = new ExpenseValidator();
        var validationResult = validator.Validate(request);
        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
            throw new ErrorOnValidationException(errors);
        }
    }
}
