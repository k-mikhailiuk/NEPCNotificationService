using Aggregator.DataAccess.Entities;
using Aggregator.DataAccess.Entities.Abstract;
using Aggregator.DataAccess.Entities.Enum;
using Aggregator.DataAccess.Entities.OwnedEntities;
using Aggregator.DTOs;
using Common;

namespace Aggregator.Core.Mappers;

public static class ConversionExtensionsHelper
{
    public static DateTimeOffset SafeConvertTime(string time)
    {
        try
        {
            return TimeZoneConverter.ConvertFromStringToUtc(time);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"SafeConvertTime: Failed to convert time={time}. Error: {ex.Message}");
            return DateTimeOffset.MinValue;
        }
    }

    public static DateTimeOffset SafeConvertFromLocalToUtc(string time)
    {
        try
        {
            return TimeZoneConverter.ConvertLocalToUtc(time);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"SafeConvertTime: Failed to convert time={time}. Error: {ex.Message}");
            return DateTimeOffset.MinValue;
        }
    }
    
    public static T ConvertMoneyDtoToEntity<T>(AggregatorMoneyDto moneyDto) where T : ICurrencyAmount, new()
    {
        var moneyEntity = new T();

        if (moneyDto == null)
        {
            moneyEntity = new T { Amount = null, Currency = null };
            Console.WriteLine($"{moneyEntity.GetType().Name} is null");
            return moneyEntity;
        }

        moneyEntity.Amount = decimal.Round(moneyDto.Amount, 2);
        moneyEntity.Currency = moneyDto.Currency;

        Console.WriteLine($"{moneyEntity} was converted to {moneyEntity.GetType().Name}");

        return moneyEntity;
    }

    public static CardIdentifier MapCardIdentifier(List<AggregatorCardIdentifierDto>? dto)
    {
        if(dto == null)
            return new CardIdentifier
            {
                CardIdentifierType = null,
                CardIdentifierValue = null
            };
        
        return dto.Select(x => new CardIdentifier
            {
                CardIdentifierType =
                    Enum.TryParse<CardIdentifierType>(x.Type.ToString(), out var parsedType)
                        ? parsedType
                        : null,
                CardIdentifierValue = x.Value
            })
            .FirstOrDefault()!;
    }
    
    public static List<NotificationExtension> MapExtensions(List<AggregatorExtensionDto> dto, long notificationId, NotificationType notificationType)
    {
        if (dto == null || dto.Count == 0)
        {
            Console.WriteLine("NotificationExtension is null");
            return null;
        }

        Console.WriteLine("Mapping NotificationExtension");
        
        return dto.Select(x => new NotificationExtension
        {
            ExtensionId = x.Id,
            NotificationType = notificationType,
            Critical = Convert.ToBoolean(x.Critical),
            NotificationId = notificationId,
            ExtensionParameters = x.Parameters?.Select(p => new ExtensionParameter
            {
                Name = p.Name,
                Value = p.Value
            }).ToList()
        }).ToList();
    }
}