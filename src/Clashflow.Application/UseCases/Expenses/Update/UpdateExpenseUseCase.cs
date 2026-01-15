using AutoMapper;
using Cashflow.Communication.Requests;
using Cashflow.Domain.Repositories;
using Cashflow.Domain.Repositories.Expense;
using Cashflow.Exception;
using Cashflow.Exception.ExceptionBase;

namespace Cashflow.Application.UseCases.Expenses.Update;
public class UpdateExpenseUseCase : IUpdateExpenseUseCase
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IExpenseUpdateRepository _repository;

    public UpdateExpenseUseCase(IMapper mapper, IUnitOfWork unitOfWork, IExpenseUpdateRepository repository)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _repository = repository;
    }


    public async Task Execute(Guid id, RequestExepenseJson request)
    {
        Validator(request);

        var expense = await _repository.GetById(id);

        if(expense is null)
        {
            throw new NotFoundException(ResourceErrorMessages.EXPENSE_NOT_FOUND);
        }

        _mapper.Map(request, expense);
        _repository.Update(expense);

        await _unitOfWork.Commit();
    }

    private void Validator(RequestExepenseJson request)
    {
        var validator = new ExpenseValidator();
        var validation = validator.Validate(request);
        if (!validation.IsValid)
        {
            var errors = validation.Errors.Select(e => e.ErrorMessage).ToList();
            throw new ErrorOnValidationException(errors);
        }

    }
}
