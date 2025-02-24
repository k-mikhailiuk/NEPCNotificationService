namespace MessagingBroker.Abstractions;

public interface IMessageConsumer
{
    Task ConsumeAsync<TMessage>(string destination, Func<TMessage, Task> onMessage, CancellationToken token = default);
}