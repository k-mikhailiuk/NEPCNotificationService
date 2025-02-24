using MessagingBroker.Abstractions;

namespace MessagingBroker;

public sealed class KafkaMessageConsumer : IMessageConsumer
{
    public Task ConsumeAsync<TMessage>(string destination, Func<TMessage, Task> onMessage,
        CancellationToken token = default)
    {
        throw new NotImplementedException();
    }
}