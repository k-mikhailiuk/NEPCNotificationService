using MediatR;

namespace Aggregator.Core.Commands;

public record ProcessNotificationCommand<T>(List<T> Notifications) : IRequest<List<long>> where T : class;