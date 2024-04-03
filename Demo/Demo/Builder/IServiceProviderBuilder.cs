using Demo.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Demo.Builder;

public static class IServiceProviderBuilder
{
    public static IServiceProvider BuildServiceProvider()
    {
        IServiceCollection services = new ServiceCollection();
        services.AddDbContext<DemoDbContext>(options => { options.UseInMemoryDatabase("DemoDatabase"); });

        
        
        return services.BuildServiceProvider();
    }
}