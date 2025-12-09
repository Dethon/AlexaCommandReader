using Azure.Messaging.ServiceBus;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.ServiceBus;
using Microsoft.Extensions.Logging;

namespace AlexaCommandReader {
    public class CommandReader {
        private readonly ICommandBehavior m_commandBehavior;

        public CommandReader(ICommandBehavior commandBehavior) {
            m_commandBehavior = commandBehavior;
        }

        public async Task ProcessServiceBus(
            [ServiceBusTrigger("%" + VariableName.queue + "%", Connection = VariableName.serviceBusUri)] ServiceBusReceivedMessage message, 
            ServiceBusMessageActions messageActions, 
            ILogger logger) {
            await m_commandBehavior.MessageBehavior(message, messageActions, logger);
        }
    }
}
