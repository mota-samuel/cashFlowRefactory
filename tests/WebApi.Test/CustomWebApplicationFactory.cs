using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;

namespace WebApi.Test;
public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    override protected void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Test").
            ConfigureServices(services =>
            {
                services.AddDbContext<CashFlowDbContext>
            });
    }


}
