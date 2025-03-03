using MediatR;

namespace Aggregator.Core.Extensions.Factories;

public interface INotificationCommandFactory
{
    IRequest CreateCommand(List<object> notification);
}