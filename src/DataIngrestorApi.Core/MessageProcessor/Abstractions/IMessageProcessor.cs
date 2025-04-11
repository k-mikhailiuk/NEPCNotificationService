using DataIngrestorApi.DTOs;

namespace DataIngrestorApi.Core.MessageProcessor.Abstractions;

/// <summary>
/// Интерфейс для обработки пакета уведомлений.
/// Определяет метод для асинхронной обработки входящего запроса уведомлений.
/// </summary>
public interface IMessageProcessor
{
    /// <summary>
    /// Асинхронно обрабатывает пакет уведомлений, представленный объектом <see cref="NotificationRequestDto"/>.
    /// </summary>
    /// <param name="request">Объект <see cref="NotificationRequestDto"/>, содержащий данные уведомлений для обработки.</param>
    /// <returns>Задача, представляющая асинхронную операцию обработки уведомлений.</returns>
    Task ProcessBatchAsync(NotificationRequestDto request);
}