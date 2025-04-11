using DataIngrestorApi.DataAccess.Entities;
using MediatR;

namespace Aggregator.Core.Commands;

public record ProcessInboxMessageCommand(IEnumerable<InboxMessage> Messages) : IRequest;