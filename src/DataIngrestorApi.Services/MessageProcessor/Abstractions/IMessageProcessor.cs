using DataIngrestorApi.DTOs;

namespace DataIngrestorApi.Services.MessageProcessor.Abstractions;

public interface IMessageProcessor
{
    Task ProcessBatchAsync(NotificationRequestDto request);
}