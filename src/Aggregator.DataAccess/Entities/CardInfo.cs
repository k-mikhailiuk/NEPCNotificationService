using System.ComponentModel.DataAnnotations;
using Aggregator.DataAccess.Entities.OwnedEntities;

namespace Aggregator.DataAccess.Entities;

/// <summary>
/// Информация о карте и ее лимитах на момент формирования уведомления
/// </summary>
public class CardInfo
{
    /// <summary>
    /// Уникальный идентификатор
    /// </summary>
    public long Id { get; set; }
    
    /// <summary>
    /// Срок действия карты (YYMM)
    /// </summary>
    public string ExpDate { get; set; }
    
    /// <summary>
    /// Ссылка на номер карты
    /// </summary>
    public string RefPan  { get; set; }
    
    /// <summary>
    /// Идентификатор контракта
    /// </summary>
    public string ContractId { get; set; }
    
    /// <summary>
    /// Номер телефона владельца карты
    /// </summary>
    public string? MobilePhone { get; set; }
    
    /// <summary>
    /// Список лимитов
    /// </summary>
    public List<CardInfoLimitWrapper>? Limits { get; set; }
    
    /// <inheritdoc cref="CardIdentifier" />
    public CardIdentifier? CardIdentifier { get; set; }
}