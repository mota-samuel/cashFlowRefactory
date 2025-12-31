namespace Cashflow.Application;

using Cashflow.Application.UseCases.Expenses.Register;
using Microsoft.Extensions.DependencyInjection;
public static class DependencyInjectionExtension
{
    public static void AddApplication (this IServiceCollection builder)
    {
        builder.AddScoped<IRegisterExpensesUseCase, RegisterExpensesUseCase>();
    }
}
