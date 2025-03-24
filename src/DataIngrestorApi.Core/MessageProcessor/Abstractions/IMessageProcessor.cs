using DataIngrestorApi.DTOs;

namespace DataIngrestorApi.Core.MessageProcessor.Abstractions;

public interface IMessageProcessor
{
    Task ProcessBatchAsync(NotificationRequestDto request);
}