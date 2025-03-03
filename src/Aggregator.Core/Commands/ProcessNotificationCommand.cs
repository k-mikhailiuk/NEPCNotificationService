using MediatR;

namespace Aggregator.Core.Commands;

public record ProcessNotificationCommand<T>(List<T> Notifications) : IRequest where T : class;