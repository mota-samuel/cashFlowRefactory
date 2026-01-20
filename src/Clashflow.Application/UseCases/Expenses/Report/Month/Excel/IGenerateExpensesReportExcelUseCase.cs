namespace Cashflow.Application.UseCases.Expenses.Report.Month.Excel;
public interface IGenerateExpensesReportExcelUseCase
{
    public Task<byte[]> Execute(DateOnly date);
}
