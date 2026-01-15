namespace Cashflow.Application.UseCases.Expenses.Delete;
public interface IDeleteExpenseUseCase
{
    public Task Execute(Guid id);
}
