using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.ServiceBus.Core;
using Microsoft.Extensions.Logging;
using AlexaCommandReader;

namespace AlexaCommandReaderExample {
    public class ComputerIgniterCommanBehavior : ICommandBehavior {
        public async Task MessageBehavior(AlexaMessageDTO parsedMessage, Message message, MessageReceiver messageReceiver, ILogger logger) {
            logger.LogInformation($"Received message: {parsedMessage.Skill} - {parsedMessage.Intent}");
            logger.LogInformation($"----------------------------------------------------------------");
            await messageReceiver.CompleteAsync(message.SystemProperties.LockToken);
        }
    }
}
