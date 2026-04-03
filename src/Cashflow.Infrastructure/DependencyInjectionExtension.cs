using Cashflow.Domain.Repositories;
using Cashflow.Domain.Repositories.Expense;
using Cashflow.Domain.Repositories.User;
using Cashflow.Domain.Security.Cryptography;
using Cashflow.Domain.Security.Tokens;
using Cashflow.Infrastructure.DataAccess;
using Cashflow.Infrastructure.DataAccess.Repositories;
using Cashflow.Infrastructure.Security.Tokens;
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
        AddSecurity(builder);
        AddToken(builder, config);
    }

    private static void AddToken(IServiceCollection services, IConfiguration configuration)
    {
        var expirationTimeMinutes = configuration.GetValue<uint>("Settings:Jwt:ExpireMinutes");
        var signingKey = configuration.GetValue<string>("Settings:Jwt:SigningKey");
        services.AddScoped<IAccessTokenGenerator>(config =>
            new JwtTokenGenerator(expirationTimeMinutes, signingKey!));
    }

    private static void AddRepository(IServiceCollection builder)
    {
        builder.AddScoped<IUnitOfWork, UnitOfWork>();
        builder.AddScoped<IExpensesWriteRepository, RepositoriesExpenses>();
        builder.AddScoped<IExpensesReadFromRepository, RepositoriesExpenses>();
        builder.AddScoped<IExpenseUpdateRepository, RepositoriesExpenses>();
        builder.AddScoped<IUserReadOnlyRepository, UserRepository>();
        builder.AddScoped<IUserWriteOnlyRepository, UserRepository>();
    }

    private static void AddDbContext(IServiceCollection builder, IConfiguration config)
    {
        var connectionString = config.GetConnectionString("DefaultConnection"); ;
        builder.AddDbContext<CashFlowDbContext>(config => config.UseSqlServer(connectionString));
    }

    private static void AddSecurity(IServiceCollection builder)
    {
        builder.AddScoped<IPasswordEncripter, Security.Cryptography.BCrypt>();
    }
}


/*
 * quando eu tenho um clase estatica, uma funcao estatica e eu utilizo a palavra reservada this, eu consigo acessar a funcao diretamente no program.cs como se fosse uma funcao defualt do builder.services
 */