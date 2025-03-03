using MediatR;

namespace Aggregator.Core.Abstractions;

public interface INotificationCommand : IRequest
{
    object Notification { get; }
}