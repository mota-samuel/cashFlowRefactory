using AutoMapper;
using Cashflow.Communication.Requests;
using Cashflow.Communication.Responses;
using Cashflow.Domain.Entities;
using Cashflow.Domain.Repositories;
using Cashflow.Exception.ExceptionBase;

namespace Cashflow.Application.UseCases.Expenses.Register;
public class RegisterExpensesUseCase : IRegisterExpensesUseCase
{
    private readonly IExpensesRepositories _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public RegisterExpensesUseCase(IExpensesRepositories repositorio, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _repository = repositorio;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task<ResponsResgisterExpenseJson> Execute(RequestRegisterExepenseJson request)
    {
        Validate(request);
        //entre os sinais <> colocar a instancia de destino e entre os parentes a classe origem que vai ser preenchido o destino
        var entity = _mapper.Map<Expense>(request);
        await _repository.Add(entity);

        await _unitOfWork.Commit();


        return _mapper.Map<ResponsResgisterExpenseJson>(entity);
    }

    private void Validate(RequestRegisterExepenseJson request)
    {
        var validator = new RegisterExpenseValidator();
        var validationResult = validator.Validate(request);
        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
            throw new ErrorOnValidationException(errors);
        }
    }
}
