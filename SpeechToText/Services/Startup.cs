using Microsoft.Extensions.DependencyInjection;
using Services.IServices;
using Services.Services;

namespace Services
{
    public static class Startup
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IAzureSTTService, AzureSTTService>();
            services.AddTransient<IWatsonSTTService, WatsonSTTService>();
        }

        //public Configure()
    }
}
