using System.Text;
using System.Threading.Tasks;
using System;
using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.ServiceBus.Core;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using AlexaCommandReader;

namespace AlexaCommandReaderExample {
    public class ComputerIgniterCommandBehavior : ICommandBehavior {
        private const string computerOn = "computerOn";
        private const string computerOff = "computerOff";
        private const string prepareForWork = "prepareForWork";

        private const string startupCommand = "StartupCommand";
        private const string shutdownCommand = "ShutdownCommand";

        private const string receiverQueueName = "ReceiverQueueName";

        private readonly QueueClient m_queue;

        public ComputerIgniterCommandBehavior() {
            m_queue = new QueueClient(
                Environment.GetEnvironmentVariable(VariableName.serviceBusUri), 
                Environment.GetEnvironmentVariable(receiverQueueName));
        }

        ~ComputerIgniterCommandBehavior() {
            m_queue.CloseAsync().Wait();
        }

        public async Task MessageBehavior(Message message, MessageReceiver messageReceiver, ILogger logger) {
            var parsedMessage = JsonConvert.DeserializeObject<AlexaMessageDTO>(Encoding.UTF8.GetString(message.Body));
            logger.LogInformation($"Received message: {parsedMessage.Skill} - {parsedMessage.Intent}");

            switch (parsedMessage.Intent) {
                case computerOn:
                    ProcessLauncher.LaunchProcess(Environment.GetEnvironmentVariable(startupCommand));
                    break;
                case computerOff:
                    ProcessLauncher.LaunchProcess(Environment.GetEnvironmentVariable(shutdownCommand));
                    break;
                case prepareForWork:
                    ProcessLauncher.LaunchProcess(Environment.GetEnvironmentVariable(startupCommand));
                    await m_queue.SendAsync(new Message(message.Body));
                    break;
            }
            await messageReceiver.CompleteAsync(message.SystemProperties.LockToken);
        }
    }
}
 