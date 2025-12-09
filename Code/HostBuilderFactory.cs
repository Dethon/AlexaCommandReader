using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace AlexaCommandReader {
    public static class HostBuilderFactory {
        public static IHostBuilder GetHostBuilder<T>() where T : class, ICommandBehavior {
            return new HostBuilder()
                .ConfigureAppConfiguration((hostingContext, config) => {
                    config.SetBasePath(AppContext.BaseDirectory);
                    config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                    config.AddJsonFile($"appsettings.{hostingContext.HostingEnvironment.EnvironmentName}.json", optional: true, reloadOnChange: true);
                    config.AddEnvironmentVariables();
                })
                .ConfigureServices((hostingContext, services) => {
                    services.Configure<ServiceBusSettings>(hostingContext.Configuration.GetSection("ServiceBus"));
                    services.Configure<ComputerIgniterSettings>(hostingContext.Configuration.GetSection("ComputerIgniter"));
                    services.Configure<ComputerPreparerSettings>(hostingContext.Configuration.GetSection("ComputerPreparer"));
                    
                    services.AddScoped<CommandReader, CommandReader>();
                    services.AddScoped<ICommandBehavior, T>();
                }).ConfigureWebJobs(webJobs => {
                    webJobs.AddServiceBus(sbOptions => {
                        sbOptions.AutoCompleteMessages = false;
                        sbOptions.MaxConcurrentCalls = 1;
                        sbOptions.MaxConcurrentSessions = 1;
                        sbOptions.SessionIdleTimeout = TimeSpan.FromMinutes(5);
                    });
                }).ConfigureLogging((hostingContext, logging) => {
                    logging.AddConsole();
                });
        }
    }
}
