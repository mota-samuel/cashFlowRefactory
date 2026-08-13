using Cashflow.Domain.Entities;
using Cashflow.Domain.Security.Cryptography;
using Cashflow.Infrastructure.DataAccess;
using CommonTestsUtilities.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace WebApi.Test;
public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    private User _user;
    private string _password;

    public string GetEmailUser() => _user.Email;
    public string GetNameUser() => _user.Name;
    public string GetPasswordUser() => _password;
    override protected void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Test").
            ConfigureServices(services =>
            {
                var provider = services.AddEntityFrameworkInMemoryDatabase().BuildServiceProvider();

                services.AddDbContext<CashFlowDbContext>(config =>
                {
                    config.UseInMemoryDatabase("InMemoryDbForTesting");
                    config.UseInternalServiceProvider(provider);
                });

                var scope = services.BuildServiceProvider().CreateScope();
                var dbContext = scope.ServiceProvider.GetRequiredService<CashFlowDbContext>();
                var passwordEncripter = scope.ServiceProvider.GetRequiredService<IPasswordEncripter>();

                StartDb(dbContext, passwordEncripter);
            });
    }

    private void StartDb(CashFlowDbContext dbContext, IPasswordEncripter passwordEncripter)
    {
        _user = UserBuilder.Build();

        _password = _user.Password;

        _user.Password = passwordEncripter.Encrypt(_user.Password);

        dbContext.Users.Add(_user);

        dbContext.SaveChanges();

    }

}
