using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.ServiceBus.Core;
using Microsoft.Extensions.Logging;

namespace AlexaCommandReader {
    public interface ICommandBehavior {
        public Task MessageBehavior(Message message, MessageReceiver messageReceiver, ILogger logger);
    }
}
