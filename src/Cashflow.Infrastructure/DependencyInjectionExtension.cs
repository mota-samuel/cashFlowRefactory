using Cashflow.Domain.Repositories;
using Cashflow.Domain.Repositories.Expense;
using Cashflow.Infrastructure.DataAccess;
using Cashflow.Infrastructure.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cashflow.Infrastructure;
public static class DependencyInjectionExtension
{
    public static void AddInfrastructure(this IServiceCollection builder, IConfiguration config)
    {
        AddRepository(builder);
        AddDbContext(builder, config);
    }
    
    private static void AddRepository(IServiceCollection builder)
    {
        builder.AddScoped<IUnitOfWork, UnitOfWork>();
        builder.AddScoped<IExpensesWriteRepository, RepositoriesExpenses>();
        builder.AddScoped<IExpensesReadFromRepository, RepositoriesExpenses>();
    }

    private static void AddDbContext(IServiceCollection builder, IConfiguration config)
    {
        var connectionString = config.GetConnectionString("DefaultConnection"); ;
        builder.AddDbContext<CashFlowDbContext>(config => config.UseSqlServer(connectionString));
    }
}


/*
 * quando eu tenho um clase estatica, uma funcao estatica e eu utilizo a palavra reservada this, eu consigo acessar a funcao diretamente no program.cs como se fosse uma funcao defualt do builder.services
 */