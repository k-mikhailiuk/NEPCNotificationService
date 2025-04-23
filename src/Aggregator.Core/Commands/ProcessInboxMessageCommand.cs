using DataIngrestorApi.DataAccess.Entities;
using MediatR;

namespace Aggregator.Core.Commands;

/// <summary>
/// Команда для обработки списка входящих сообщений из таблицы Inbox.
/// </summary>
/// <param name="Messages">Коллекция сущностей <see cref="InboxMessage"/>, которые необходимо обработать.</param>
public record ProcessInboxMessageCommand(IEnumerable<InboxMessage> Messages) : IRequest;