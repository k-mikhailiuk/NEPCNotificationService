using System.Text.Json;
using DataIngrestorApi.DataAccess;
using DataIngrestorApi.DataAccess.Entities;
using DataIngrestorApi.DTOs;
using DataIngrestorApi.Services.MessageProcessor.Abstractions;
using Microsoft.Extensions.Logging;

namespace DataIngrestorApi.Services.MessageProcessor;

/// <summary>
/// Класс-обработчик полученных сообщений
/// </summary>
public class MessageProcessor : IMessageProcessor
{
    private readonly ILogger<MessageProcessor> _logger;
    private readonly IngressApiDbContext _context;
    private readonly JsonSerializerOptions _jsonOptions;

    /// <summary>
    /// Инициализирует новый экземпляр класса <see cref="MessageProcessor"/>.
    /// </summary>
    /// <param name="logger">Логгер для записи событий и ошибок</param>
    /// <param name="context">Экземпляр <see cref="IngressApiDbContext"/> для работы с базой данных.</param>
    /// <param name="jsonOptions">Настройки сериализации JSON.</param>
    public MessageProcessor(ILogger<MessageProcessor> logger, IngressApiDbContext context, JsonSerializerOptions jsonOptions)
    {
        _logger = logger;
        _context = context;
        _jsonOptions = jsonOptions;
    }

    /// <summary>
    /// Обрабатывает пакет уведомлений и сохраняет их в базу данных.
    /// </summary>
    /// <param name="request">Объект, содержащий пакет уведомлений.</param>
    public async Task ProcessBatchAsync(NotificationRequestDto request)
    {
        if (request.Batch.Length == 0)
        {
            _logger.LogWarning("Received empty batch.");
            return;
        }

        var messages = request.Batch
            .Select(batchItem => InboxMessage.Create(batchItem, _jsonOptions))
            .ToList();

        await using var transaction = await _context.Database.BeginTransactionAsync();
        await _context.InboxMessages.AddRangeAsync(messages);
        await _context.SaveChangesAsync();
        await transaction.CommitAsync();
    }
}