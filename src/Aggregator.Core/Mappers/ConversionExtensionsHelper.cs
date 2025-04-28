using Aggregator.DataAccess.Entities;
using Aggregator.DataAccess.Entities.Abstract;
using Aggregator.DataAccess.Entities.Enum;
using Aggregator.DataAccess.Entities.OwnedEntities;
using Aggregator.DTOs;
using Microsoft.Extensions.Logging;

namespace Aggregator.Core.Mappers;

/// <summary>
/// Класс-расширение для безопасного преобразования времени, конвертации денежных значений и маппинга идентификаторов карт и расширений.
/// </summary>
public class ConversionExtensionsHelper(ILogger<ConversionExtensionsHelper> logger)
{
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
    public T ConvertMoneyDtoToEntity<T>(AggregatorMoneyDto? moneyDto) where T : ICurrencyAmount, new()
    {
        var moneyEntity = new T();

        if (moneyDto == null)
        {
            moneyEntity = new T { Amount = null, Currency = null };
            logger.LogInformation("{moneyEntity.GetType().Name} is null", moneyEntity.GetType().Name);
            return moneyEntity;
        }

        moneyEntity.Amount = decimal.Round(moneyDto.Amount, 2) / 100;
        moneyEntity.Currency = moneyDto.Currency;

        logger.LogInformation("{moneyEntity} was converted to {moneyEntity.GetType().Name}", moneyEntity, moneyEntity.GetType().Name);

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
    public CardIdentifier MapCardIdentifier(List<AggregatorCardIdentifierDto>? dto)
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
    public List<NotificationExtension>? MapExtensions(List<AggregatorExtensionDto>? dto, long notificationId, NotificationType notificationType)
    {
        if (dto == null || dto.Count == 0)
        {
            logger.LogInformation("NotificationExtension is null");
            return null;
        }

        logger.LogInformation("Mapping NotificationExtension");
        
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