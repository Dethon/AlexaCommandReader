using System.Text;
using System.Text.Json;
using Azure.Messaging.ServiceBus;
using Microsoft.Azure.WebJobs.ServiceBus;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using AlexaCommandReader;

namespace AlexaCommandReaderExample {
    public class ComputerPreparerCommandBehavior : ICommandBehavior {
        private const string prepareForWork = "prepareForWork";

        private readonly ComputerPreparerSettings m_settings;

        public ComputerPreparerCommandBehavior(IOptions<ComputerPreparerSettings> settings) {
            m_settings = settings.Value;
        }

        public async Task MessageBehavior(ServiceBusReceivedMessage message, ServiceBusMessageActions messageActions, ILogger logger) {
            var parsedMessage = JsonSerializer.Deserialize<AlexaMessageDTO>(Encoding.UTF8.GetString(message.Body));
            logger.LogInformation($"Received message: {parsedMessage?.Skill} - {parsedMessage?.Intent}");

            switch (parsedMessage?.Intent) {
                case prepareForWork:
                    ProcessLauncher.LaunchProcess(m_settings.Console, m_settings.PrepareForWorkConsoleCommand);
                    break;
            }
            await messageActions.CompleteMessageAsync(message);
        }   
    }
}
 