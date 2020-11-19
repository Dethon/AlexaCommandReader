using System.Diagnostics;

namespace AlexaCommandReader {
    public static class ProcessLauncher {
        public static void LaunchProcess(string executable, string args = "") {
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
