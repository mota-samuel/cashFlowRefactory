using Cashflow.Communication.Requests;
using Cashflow.Communication.Responses;
using Cashflow.Domain.Entities;
using Cashflow.Domain.Enum;
using Cashflow.Domain.Repositories;
using Cashflow.Exception.ExceptionBase;

namespace Cashflow.Application.UseCases.Expenses.Register;
public class RegisterExpensesUseCase : IRegisterExpensesUseCase
{
    private readonly IExpensesRepositories _repository;
    private readonly IUnitOfWork _unitOfWork;
    public RegisterExpensesUseCase(IExpensesRepositories repositorio, IUnitOfWork unitOfWork)
    {
        _repository = repositorio;
        _unitOfWork = unitOfWork;
    }
    public async Task<ResponsResgisterExpenseJson> Execute(RequestRegisterExepenseJson request)
    {
        Validate(request);

        var entity = new Expense
        {
            Title = request.Title,
            Description = request.Description,
            Date = request.Date,
            Amount = request.Amount,
            PaymentType = (PaymentType)request.PaymentType
        };

        await _repository.Add(entity);

        await _unitOfWork.Commit();


        return new ResponsResgisterExpenseJson() { Title = request.Title};
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
