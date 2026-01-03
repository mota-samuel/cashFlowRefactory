namespace Cashflow.Application;

using Cashflow.Application.Mapper;
using Cashflow.Application.UseCases.Expenses.GetAll;
using Cashflow.Application.UseCases.Expenses.GetById;
using Cashflow.Application.UseCases.Expenses.Register;
using Microsoft.Extensions.DependencyInjection;
public static class DependencyInjectionExtension
{
    public static void AddApplication (this IServiceCollection builder)
    {
        AddUseCases(builder);
        AddMapper(builder);
        AddGetAllUseCase(builder);
        AddGetByIdUseCase(builder);
    }

    private static void AddUseCases(IServiceCollection builder)
    {
        builder.AddScoped<IRegisterExpensesUseCase, RegisterExpensesUseCase>();
    }
    private static void AddMapper(IServiceCollection builder)
    {
        builder.AddAutoMapper(typeof(AutoMapping));
    }
    public static void AddGetAllUseCase(IServiceCollection builder)
    {
        builder.AddScoped<IGetAllExpensesUseCase, GetAllExpensesUseCase>();
    }
    public static void AddGetByIdUseCase(IServiceCollection builder)
    {
        builder.AddScoped<IGetExpenseByIdUseCase, GetExpenseByIdUseCase>();
    }
}
