using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.ServiceBus.Core;
using Microsoft.Azure.WebJobs;
using Newtonsoft.Json;

namespace AlexaCommandReader {
    public class CommandReader {
        private readonly ICommandBehavior m_commandBehavior;

        public CommandReader(ICommandBehavior commandBehavior) {
            m_commandBehavior = commandBehavior;
        }

        public async Task ProcessServiceBus([ServiceBusTrigger("%" + VariableName.queue + "%", Connection = VariableName.serviceBusUri)] Message message, MessageReceiver messageReceiver, ILogger logger) {
            var parsedMessage = JsonConvert.DeserializeObject<AlexaMessageDTO>(Encoding.UTF8.GetString(message.Body));
            await m_commandBehavior.MessageBehavior(parsedMessage, message, messageReceiver, logger);
        }
    }
}
