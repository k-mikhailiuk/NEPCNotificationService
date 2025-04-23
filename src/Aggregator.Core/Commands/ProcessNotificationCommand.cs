using Aggregator.DTOs.Abstractions;
using MediatR;

namespace Aggregator.Core.Commands;

/// <summary>
/// Команда для передачи списка DTO уведомлений в обработку.
/// </summary>
/// <typeparam name="T">Тип DTO уведомления, реализующий <see cref="INotificationAggregatorDto"/>.</typeparam>
/// <param name="Notifications">Список DTO уведомлений для обработки.</param>
/// <returns>Список идентификаторов (NotificationId) успешно обработанных уведомлений.</returns>
public record ProcessNotificationCommand<T>(List<T> Notifications) : IRequest<List<long>> where T : INotificationAggregatorDto;