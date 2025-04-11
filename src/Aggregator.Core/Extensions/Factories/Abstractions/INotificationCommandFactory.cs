using MediatR;

namespace Aggregator.Core.Extensions.Factories.Abstractions;

/// <summary>
/// Фабрика команд уведомлений.
/// </summary>
/// <remarks>
/// Интерфейс предназначен для создания команд уведомлений, реализующих <see cref="IRequest{TResponse}"/>.
/// Созданная команда возвращает список идентификаторов (типа <see cref="long"/>), связанных с уведомлениями.
/// </remarks>
public interface INotificationCommandFactory
{
    /// <summary>
    /// Создаёт команду уведомлений на основе переданного списка уведомлений.
    /// </summary>
    /// <param name="notification">Список уведомлений, для которых необходимо создать команду.</param>
    /// <returns>
    /// Команда, реализующая <see cref="IRequest{TResponse}"/>, которая при выполнении возвращает список идентификаторов уведомлений.
    /// </returns>
    IRequest<List<long>> CreateCommand(List<object> notification);
}