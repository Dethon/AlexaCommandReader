using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using AlexaCommandReader;
using System.Runtime.InteropServices;

namespace AlexaCommandReaderExample {
    public class Program {
        static async Task Main() {
            if (System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(OSPlatform.Linux)) {
                await HostBuilderFactory.GetHostBuilder<ComputerIgniterCommandBehavior>().RunConsoleAsync();
            }
            else if(System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(OSPlatform.Windows)) {
                await HostBuilderFactory.GetHostBuilder<ComputerPreparerCommandBehavior>().RunConsoleAsync();
            }
        }
    }
}
