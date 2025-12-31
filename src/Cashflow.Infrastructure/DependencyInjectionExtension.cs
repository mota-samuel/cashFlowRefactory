using Cashflow.Infrastructure.DataAccess.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Cashflow.Infrastructure;
public static class DependencyInjectionExtension
{
    public static void AddInfrastructure(this IServiceCollection builder)
    {
        builder.AddScoped<IExpensesRepositories, RepositoriesExpenses>();
    }
}


/*
 * quando eu tenho um clase estatica, uma funcao estatica e eu utilizo a palavra reservada this, eu consigo acessar a funcao diretamente no program.cs como se fosse uma funcao defualt do builder.services
 */