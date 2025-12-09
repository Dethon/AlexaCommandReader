namespace AlexaCommandReader {
    public static class VariableName {
        // Configuration keys used by WebJobs ServiceBusTrigger attribute
        // These reference paths in appsettings.json using colon notation
        public const string queue = "ServiceBus:QueueName";
        public const string serviceBusUri = "ServiceBus:ServiceBusConnection";
    }
}
