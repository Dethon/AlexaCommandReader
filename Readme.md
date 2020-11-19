# Alexa Command Reader

A companion to the Alexa Enqueuer Azure Function. 

## Objective

This reads an Azure Service Bus queue and performs custom actions when messages arrive. The aim was to be able to control my devices through Alexa.

To add custo behavior to yur messages you need to implement the `MessageBehavior()` method from `ICommandBehavior` in a subclass and inject it into the service. You can see an example in the `Code/Example` folder.

## Settings

The function uses some settings that must be configured for your use case:
    `QueueName`: The name of your Azure Service Bus queue.
    `ServiceBusConnection`: The connection string to your Azure Service Bus namespace.

These setting names are defined as constants within `VariableName`.
