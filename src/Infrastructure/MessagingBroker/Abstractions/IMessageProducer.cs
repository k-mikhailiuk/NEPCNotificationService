namespace MessagingBroker.Abstractions;

public interface IMessageProducer
{
    Task ProduceAsync<T>(
        T obj,
        string destination,
        CancellationToken token = default);
}