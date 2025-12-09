using Azure.Messaging.ServiceBus;
using Microsoft.Azure.WebJobs.ServiceBus;
using Microsoft.Extensions.Logging;

namespace AlexaCommandReader {
    public interface ICommandBehavior {
        public Task MessageBehavior(ServiceBusReceivedMessage message, ServiceBusMessageActions messageActions, ILogger logger);
    }
}
