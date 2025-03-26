using System.Text.Json;
using DataIngrestorApi.Core.MessageProcessor.Abstractions;
using DataIngrestorApi.DataAccess;
using DataIngrestorApi.DataAccess.Entities;
using DataIngrestorApi.DTOs;
using Microsoft.Extensions.Logging;

namespace DataIngrestorApi.Core.MessageProcessor;

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
    public MessageProcessor(ILogger<MessageProcessor> logger, IngressApiDbContext context,
        JsonSerializerOptions jsonOptions)
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

        var messages = (from batch in request.Batch
            where batch.IssFinAuth != null || batch.AcqFinAuth != null || batch.CardStatusChange != null ||
                  batch.PinChange != null || batch.Unhold != null || batch.OwiUserAction != null ||
                  batch.TokenStatusChange != null || batch.AcctBalChange != null
            select InboxMessage.Create(batch, _jsonOptions)).ToList();

        await using var transaction = await _context.Database.BeginTransactionAsync();
        
        await _context.InboxMessages.AddRangeAsync(messages);
        await _context.SaveChangesAsync();
        await transaction.CommitAsync();
    }
}