using System.Text;
using System.Diagnostics;
using System.Threading.Tasks;
using System;
using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.ServiceBus.Core;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using AlexaCommandReader;

namespace AlexaCommandReaderExample {
    public class ComputerIgniterCommanBehavior : ICommandBehavior {
        private const string computerOn = "computerOn";
        private const string computerOff = "computerOff";
        private const string prepareForWork = "prepareForWork";

        private const string shell = "Shell";
        private const string startupCommand = "StartupCommand";
        private const string shutdownCommand = "ShutdownCommand";

        public async Task MessageBehavior(Message message, MessageReceiver messageReceiver, ILogger logger) {
            var parsedMessage = JsonConvert.DeserializeObject<AlexaMessageDTO>(Encoding.UTF8.GetString(message.Body));
            logger.LogInformation($"Received message: {parsedMessage.Skill} - {parsedMessage.Intent}");

            switch (parsedMessage.Intent) {
                case computerOn:
                    LaunchScript(Environment.GetEnvironmentVariable(shell), Environment.GetEnvironmentVariable(startupCommand));
                    break;
                case computerOff:
                    LaunchScript(Environment.GetEnvironmentVariable(shell), Environment.GetEnvironmentVariable(shutdownCommand));
                    break;
                case prepareForWork:
                    break;
            }

            await messageReceiver.CompleteAsync(message.SystemProperties.LockToken);
        }

        private void LaunchScript(string executable, string args) {
            Process proc = new Process() { 
                StartInfo = new ProcessStartInfo() { 
                    FileName = executable,
                    Arguments = args
                } 
            };
            proc.Start();
        }
    }
}
 