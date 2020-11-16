using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using AlexaCommandReader;

namespace AlexaCommandReaderExample {
    public class Program {
        static async Task Main() {
            await HostBuilderFactory.GetHostBuilder<ComputerIgniterCommanBehavior>().RunConsoleAsync();
        }
    }
}
