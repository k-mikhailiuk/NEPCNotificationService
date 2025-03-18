using MediatR;

namespace Aggregator.Core.Extensions.Factories.Abstractions;

public interface INotificationCommandFactory
{
    IRequest<List<long>> CreateCommand(List<object> notification);
}