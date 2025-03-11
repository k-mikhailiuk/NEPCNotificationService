using MediatR;

namespace Aggregator.Core.Extensions.Factories;

public interface INotificationCommandFactory
{
    IRequest<List<long>> CreateCommand(List<object> notification);
}