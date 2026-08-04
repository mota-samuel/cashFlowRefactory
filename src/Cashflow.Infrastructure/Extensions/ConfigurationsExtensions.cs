using Microsoft.Extensions.Configuration;

namespace Cashflow.Infrastructure.Extensions;
public static class ConfigurationsExtensions
{
    public static bool IsTestEnvironment(this IConfiguration configuration)
    {
        return configuration.GetValue<bool>("InMemoryTest");
         
    }
}
