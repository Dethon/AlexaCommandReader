using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace AlexaCommandReader {
    public static class HostBuilderFactory {
        public static IHostBuilder GetHostBuilder<T>() where T : class, ICommandBehavior {
            return new HostBuilder()
                .ConfigureServices((hostingContext, services) => {
                    services.AddScoped<CommandReader, CommandReader>();
                    services.AddScoped<ICommandBehavior, T>();
                }).ConfigureWebJobs(webJobs => {
                    webJobs.AddServiceBus(sbOptions => {
                        sbOptions.MessageHandlerOptions.AutoComplete = false;
                        sbOptions.MessageHandlerOptions.MaxConcurrentCalls = 1;
                        sbOptions.SessionHandlerOptions.MessageWaitTimeout = TimeSpan.FromMinutes(5);
                    });
                }).ConfigureAppConfiguration((hostingContext, config) => {
                    config.AddEnvironmentVariables();
                }).ConfigureLogging((hostingContext, logging) => {
                    logging.AddConsole();
                });
        }
    }
}
