using System.Text;
using System.Text.Json;
using Azure.Messaging.ServiceBus;
using Microsoft.Azure.WebJobs.ServiceBus;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using AlexaCommandReader;

namespace AlexaCommandReaderExample {
    public class ComputerIgniterCommandBehavior : ICommandBehavior {
        private const string computerOn = "computerOn";
        private const string computerOff = "computerOff";
        private const string prepareForWork = "prepareForWork";

        private readonly ServiceBusSender m_sender;
        private readonly ServiceBusClient m_client;
        private readonly ComputerIgniterSettings m_settings;
        private readonly ServiceBusSettings m_serviceBusSettings;

        public ComputerIgniterCommandBehavior(IOptions<ComputerIgniterSettings> settings, IOptions<ServiceBusSettings> serviceBusSettings) {
            m_settings = settings.Value;
            m_serviceBusSettings = serviceBusSettings.Value;
            m_client = new ServiceBusClient(m_serviceBusSettings.ServiceBusConnection);
            m_sender = m_client.CreateSender(m_settings.ReceiverQueueName);
        }

        public async Task MessageBehavior(ServiceBusReceivedMessage message, ServiceBusMessageActions messageActions, ILogger logger) {
            var parsedMessage = JsonSerializer.Deserialize<AlexaMessageDTO>(Encoding.UTF8.GetString(message.Body));
            logger.LogInformation($"Received message: {parsedMessage?.Skill} - {parsedMessage?.Intent}");

            switch (parsedMessage?.Intent) {
                case computerOn:
                    ProcessLauncher.LaunchProcess(m_settings.StartupCommand);
                    break;
                case computerOff:
                    ProcessLauncher.LaunchProcess(m_settings.ShutdownCommand);
                    break;
                case prepareForWork:
                    ProcessLauncher.LaunchProcess(m_settings.StartupCommand);
                    await m_sender.SendMessageAsync(new ServiceBusMessage(message.Body));
                    break;
            }
            await messageActions.CompleteMessageAsync(message);
        }
    }
}
 