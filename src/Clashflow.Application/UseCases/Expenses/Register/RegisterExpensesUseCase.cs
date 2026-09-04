using AutoMapper;
using Cashflow.Communication.Requests;
using Cashflow.Communication.Responses;
using Cashflow.Domain.Entities;
using Cashflow.Domain.Repositories;
using Cashflow.Domain.Repositories.Expense;
using Cashflow.Domain.Services.LoggedUser;
using Cashflow.Exception.ExceptionBase;

namespace Cashflow.Application.UseCases.Expenses.Register;
public class RegisterExpensesUseCase : IRegisterExpensesUseCase
{
    private readonly IExpensesWriteRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ILoggedUser _loggedUser;
    public RegisterExpensesUseCase(IExpensesWriteRepository repositorio, IUnitOfWork unitOfWork, IMapper mapper, ILoggedUser logged)
    {
        _repository = repositorio;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _loggedUser = logged;
    }
    public async Task<ResponsResgisterExpenseJson> Execute(RequestExepenseJson request)
    {
        Validate(request);
        var loggedUser = await _loggedUser.Get();

        //entre os sinais <> colocar a instancia de destino e entre os parentes a classe origem que vai ser preenchido o destino
        var expense = _mapper.Map<Expense>(request);
        expense.UserId = loggedUser.UserId;
        await _repository.Add(expense);

        await _unitOfWork.Commit();

        return _mapper.Map<ResponsResgisterExpenseJson>(expense);
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
