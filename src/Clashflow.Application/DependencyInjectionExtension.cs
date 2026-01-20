namespace Cashflow.Application;

using Cashflow.Application.Mapper;
using Cashflow.Application.UseCases.Expenses.Delete;
using Cashflow.Application.UseCases.Expenses.GetAll;
using Cashflow.Application.UseCases.Expenses.GetById;
using Cashflow.Application.UseCases.Expenses.Register;
using Cashflow.Application.UseCases.Expenses.Report.Month.Excel;
using Cashflow.Application.UseCases.Expenses.Update;
using Microsoft.Extensions.DependencyInjection;
public static class DependencyInjectionExtension
{
    public static void AddApplication (this IServiceCollection builder)
    {
        AddUseCases(builder);
    }

    private static void AddUseCases(IServiceCollection builder)
    {
        builder.AddScoped<IRegisterExpensesUseCase, RegisterExpensesUseCase>();
        builder.AddAutoMapper(typeof(AutoMapping));
        builder.AddScoped<IGetAllExpensesUseCase, GetAllExpensesUseCase>();
        builder.AddScoped<IGetExpenseByIdUseCase, GetExpenseByIdUseCase>();
        builder.AddScoped<IDeleteExpenseUseCase, DeleteExpenseUseCase>();
        builder.AddScoped<IUpdateExpenseUseCase, UpdateExpenseUseCase>();
        builder.AddScoped<IGenerateExpensesReportExcelUseCase, GenerateExpensesReportExcelUseCase>();
    }

}
