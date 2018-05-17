using Amazon.S3;
using Amazon.TranscribeService;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Services.IServices;
using Services.Services;

namespace Services
{
    public static class Startup
    {
        public static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IAzureSTTService, AzureSTTService>();
            services.AddTransient<IWatsonSTTService, WatsonSTTService>();
            services.AddTransient<IAWSService, AWSService>();

            services.AddTransient<IAmazonUploader, AmazonUploader>();

            var x = configuration.GetAWSOptions();
            services.AddDefaultAWSOptions(configuration.GetAWSOptions());
            services.AddAWSService<IAmazonS3>();
            services.AddAWSService<IAmazonTranscribeService>();
        }

    }
}
