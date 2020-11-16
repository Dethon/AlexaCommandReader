using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.ServiceBus.Core;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using AlexaCommandReader;

namespace AlexaCommandReaderExample {
    public class ComputerIgniterCommanBehavior : ICommandBehavior {
        public async Task MessageBehavior(Message message, MessageReceiver messageReceiver, ILogger logger) {
            var parsedMessage = JsonConvert.DeserializeObject<AlexaMessageDTO>(Encoding.UTF8.GetString(message.Body));
            logger.LogInformation($"Received message: {parsedMessage.Skill} - {parsedMessage.Intent}");
            logger.LogInformation($"----------------------------------------------------------------");
            await messageReceiver.CompleteAsync(message.SystemProperties.LockToken);
        }
    }
}
