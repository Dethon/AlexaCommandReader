using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using AlexaCommandReader;
using System.Runtime.InteropServices;

namespace AlexaCommandReaderExample {
    public class Program {
        static async Task Main() {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux)) {
                await HostBuilderFactory.GetHostBuilder<ComputerIgniterCommandBehavior>().Build().RunAsync();
            }
            else if(RuntimeInformation.IsOSPlatform(OSPlatform.Windows)) {
                await HostBuilderFactory.GetHostBuilder<ComputerPreparerCommandBehavior>().Build().RunAsync();
            }
        }
    }
}
