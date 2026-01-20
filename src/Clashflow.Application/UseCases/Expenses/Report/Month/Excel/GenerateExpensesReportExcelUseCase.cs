using ClosedXML.Excel;
using Cashflow.Domain.Reports;
using Cashflow.Domain.Repositories.Expense;
using AutoMapper;
using Cashflow.Domain.Entities;
using Cashflow.Domain.Enum;
using Cashflow.Exception.ExceptionBase;
using Cashflow.Exception;
using System.Threading.Tasks;

namespace Cashflow.Application.UseCases.Expenses.Report.Month.Excel;
public class GenerateExpensesReportExcelUseCase : IGenerateExpensesReportExcelUseCase
{
    private readonly IExpensesReadFromRepository _repository;
    private const string CURRENCY_SYMBOL = "R$";
    public GenerateExpensesReportExcelUseCase(IExpensesReadFromRepository repository)
    {
        _repository = repository;
    }

    public async Task<byte[]> Execute(DateOnly date)
    {

        var listOfExpenses = _repository.FilterByMonth(date);
        if (listOfExpenses is null)
            return [];

        var workbook = new XLWorkbook();
        workbook.Author = "Samuka Mota";
        workbook.Style.Font.FontSize = 14;
        workbook.Style.Font.FontName = "Times New Roman";

        var worksheet = workbook.Worksheets.Add(date.ToString("Y"));

        CreateHeader(worksheet);
        _ = FillTable(listOfExpenses, worksheet);

        worksheet.Columns().AdjustToContents();

        var fileStream = new MemoryStream();//cria o stream de memoria
        workbook.SaveAs(fileStream);
        return fileStream.ToArray();
    }

    private void CreateHeader(IXLWorksheet worksheet)
    {
        worksheet.Cell("A1").Value = ResourceReportMessages.TITLE;
        worksheet.Cell("B1").Value = ResourceReportMessages.DATE;
        worksheet.Cell("C1").Value = ResourceReportMessages.PAYMENT_TYPE;
        worksheet.Cell("D1").Value = ResourceReportMessages.AMOUNT;
        worksheet.Cell("E1").Value = ResourceReportMessages.DESCRIPTION;

        worksheet.Cells("A1:E1").Style.Font.Bold = true;
        worksheet.Cells("A1:E1").Style.Fill.BackgroundColor = XLColor.Azure;// PARA UTILIZAR CORES NAO PREDEFINIDAS, USAR XLColor.htmlColor("#FFFFFF")
        worksheet.Cells("A1:C1").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
        worksheet.Cell("D1").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
        worksheet.Cell("D1").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;

    }

    private async Task FillTable(Task<List<Expense>> list, IXLWorksheet worksheet)
    {
        var expenses = await list; // essa linha aguarda a conclusão da tarefa para obter a lista de despesas

        int n = 2;
        foreach (var item in expenses)
        {
            worksheet.Cell($"A{n}").Value = item.Title;
            worksheet.Cell($"B{n}").Value = item.Date.ToString("dd/MM/yyyy");
            worksheet.Cell($"C{n}").Value = TranslatePaymentType(item.PaymentType);
            worksheet.Cell($"D{n}").Value = item.Amount;
            worksheet.Cell($"D{n}").Style.NumberFormat.Format = $"-{CURRENCY_SYMBOL} #,##0.00";
            worksheet.Cell($"E{n}").Value = item.Description;

            n++;
        }

    }

    private string TranslatePaymentType(PaymentType paymentType)
    {
        return paymentType switch
        {
            PaymentType.Cash => ResourceReportMessages.CASH,
            PaymentType.CreditCard => ResourceReportMessages.CREDIT_CARD,
            PaymentType.DebitCard => ResourceReportMessages.DEBIT_CARD,
            PaymentType.EletronicTransfer => ResourceReportMessages.ELECTRONIC_TRANSFER,
            _ => string.Empty
        };
    }

}
