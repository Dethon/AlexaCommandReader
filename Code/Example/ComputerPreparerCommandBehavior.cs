using System.Text;
using System.Threading.Tasks;
using System;
using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.ServiceBus.Core;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using AlexaCommandReader;

namespace AlexaCommandReaderExample {
    public class ComputerPreparerCommandBehavior : ICommandBehavior {
        private const string prepareForWork = "prepareForWork";

        private const string console = "Console";
        private const string prepareForWorkConsoleCommand = "PrepareForWorkConsoleCommand";

        public async Task MessageBehavior(Message message, MessageReceiver messageReceiver, ILogger logger) {
            var parsedMessage = JsonConvert.DeserializeObject<AlexaMessageDTO>(Encoding.UTF8.GetString(message.Body));
            logger.LogInformation($"Received message: {parsedMessage.Skill} - {parsedMessage.Intent}");

            switch (parsedMessage.Intent) {
                case prepareForWork:
                    ProcessLauncher.LaunchProcess(Environment.GetEnvironmentVariable(console), Environment.GetEnvironmentVariable(prepareForWorkConsoleCommand));
                    break;
            }
            await messageReceiver.CompleteAsync(message.SystemProperties.LockToken);
        }   
    }
}
 