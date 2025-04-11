using MediatR;

namespace Aggregator.Core.Abstractions;

/// <summary>
/// Represents a notification command that can be processed using MediatR.
/// </summary>
/// <remarks>
/// This interface extends <see cref="MediatR.IRequest"/> and encapsulates a notification object 
/// that can be used to trigger notifications or events within the system.
/// </remarks>
public interface INotificationCommand : IRequest
{
    /// <summary>
    /// Gets the notification object to be processed.
    /// </summary>
    object Notification { get; }
}