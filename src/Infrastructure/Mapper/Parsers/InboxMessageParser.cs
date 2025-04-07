using System.Text.Json;
using System.Text.Json.Nodes;
using Aggregator.DataAccess.Entities.Enum;
using Aggregator.DTOs.Abstractions;

namespace Mapper.Parsers;

public static class InboxMessageParser
{
    public static INotificationAggregatorDto? ParseInboxMessage(string payload)
    {
        try
        {
            var jsonObject = JsonNode.Parse(payload)?.AsObject();
            if (jsonObject == null || jsonObject.Count == 0)
            {
                throw new InvalidOperationException("Payload JSON is empty or invalid.");
            }

            var rootKey = ((IDictionary<string, JsonNode?>)jsonObject).Keys.FirstOrDefault();
            if (rootKey == null)
            {
                throw new InvalidOperationException("Failed to determine root JSON object.");
            }

            if (!Enum.TryParse(rootKey, out NotificationType detectedType) || 
                !NotificationTypeMapper.EnumAggregatorTypeMapping.TryGetValue(detectedType, out var targetType))
            {
                throw new InvalidOperationException($"Unknown notification type: {rootKey}");
            }

            var deserializedObject = (INotificationAggregatorDto)JsonSerializer.Deserialize(jsonObject[rootKey]!.ToJsonString(), targetType);
        
            return deserializedObject;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error parsing Inbox message: {ex.Message}");
            return null;
        }
    }
}