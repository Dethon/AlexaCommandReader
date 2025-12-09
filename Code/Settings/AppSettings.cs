namespace AlexaCommandReader;

public class ServiceBusSettings {
    public string ServiceBusConnection { get; set; } = string.Empty;
    public string QueueName { get; set; } = string.Empty;
}

public class ComputerIgniterSettings {
    public string StartupCommand { get; set; } = string.Empty;
    public string ShutdownCommand { get; set; } = string.Empty;
    public string ReceiverQueueName { get; set; } = string.Empty;
}

public class ComputerPreparerSettings {
    public string Console { get; set; } = string.Empty;
    public string PrepareForWorkConsoleCommand { get; set; } = string.Empty;
}
