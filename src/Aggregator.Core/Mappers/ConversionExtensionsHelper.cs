using Aggregator.DataAccess.Entities;
using Aggregator.DataAccess.Entities.Abstract;
using Aggregator.DataAccess.Entities.Enum;
using Aggregator.DataAccess.Entities.OwnedEntities;
using Aggregator.DTOs;
using Common;

namespace Aggregator.Core.Mappers;

/// <summary>
/// Класс-расширение для безопасного преобразования времени, конвертации денежных значений и маппинга идентификаторов карт и расширений.
/// </summary>
public static class ConversionExtensionsHelper
{
    /// <summary>
    /// Безопасно преобразует строку времени в значение <see cref="DateTimeOffset"/> в UTC.
    /// </summary>
    /// <param name="time">Строковое представление времени.</param>
    /// <returns>
    /// Преобразованное время в UTC; в случае неудачи возвращает <see cref="DateTimeOffset.MinValue"/>.
    /// </returns>
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

    /// <summary>
    /// Безопасно преобразует локальное строковое время в значение <see cref="DateTimeOffset"/> в UTC.
    /// </summary>
    /// <param name="time">Строковое представление локального времени.</param>
    /// <returns>
    /// Преобразованное время в UTC; в случае неудачи возвращает <see cref="DateTimeOffset.MinValue"/>.
    /// </returns>
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
    
    /// <summary>
    /// Конвертирует DTO-объект денежных средств в сущность, реализующую интерфейс <see cref="ICurrencyAmount"/>.
    /// </summary>
    /// <typeparam name="T">
    /// Тип сущности, реализующей <see cref="ICurrencyAmount"/> и имеющей публичный конструктор.
    /// </typeparam>
    /// <param name="moneyDto">DTO-объект, содержащий данные о денежных средствах.</param>
    /// <returns>
    /// Сущность типа <typeparamref name="T"/> с заполненными полями <c>Amount</c> и <c>Currency</c>.
    /// </returns>
    public static T ConvertMoneyDtoToEntity<T>(AggregatorMoneyDto moneyDto) where T : ICurrencyAmount, new()
    {
        var moneyEntity = new T();

        if (moneyDto == null)
        {
            moneyEntity = new T { Amount = null, Currency = null };
            Console.WriteLine($"{moneyEntity.GetType().Name} is null");
            return moneyEntity;
        }

        moneyEntity.Amount = decimal.Round(moneyDto.Amount, 2) / 100;
        moneyEntity.Currency = moneyDto.Currency;

        Console.WriteLine($"{moneyEntity} was converted to {moneyEntity.GetType().Name}");

        return moneyEntity;
    }

    /// <summary>
    /// Маппит список DTO идентификаторов карт в объект <see cref="CardIdentifier"/>.
    /// </summary>
    /// <param name="dto">
    /// Список DTO-объектов для идентификаторов карт. Может быть null.
    /// </param>
    /// <returns>
    /// Объект <see cref="CardIdentifier"/> с заполненными полями <c>CardIdentifierType</c> и <c>CardIdentifierValue</c>;
    /// если входной параметр равен null, возвращает объект с обоими значениями равными null.
    /// </returns>
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
    
    /// <summary>
    /// Маппит список DTO расширений в список сущностей <see cref="NotificationExtension"/>.
    /// </summary>
    /// <param name="dto">Список DTO-объектов для расширений уведомлений.</param>
    /// <param name="notificationId">Идентификатор уведомления.</param>
    /// <param name="notificationType">Тип уведомления.</param>
    /// <returns>
    /// Список сущностей <see cref="NotificationExtension"/> с заполненными полями.
    /// Если входной список dto равен null или пуст, выводится сообщение и возвращается null.
    /// </returns>
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