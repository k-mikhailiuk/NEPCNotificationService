using System.Text.Json.Serialization;
using DataIngrestorApi.DTOs.AcctBalChange;
using DataIngrestorApi.DTOs.AcqFinAuth;
using DataIngrestorApi.DTOs.CardStatusChange;
using DataIngrestorApi.DTOs.IssFinAuth;
using DataIngrestorApi.DTOs.OwiUserAction;
using DataIngrestorApi.DTOs.PinChange;
using DataIngrestorApi.DTOs.TokenStausChange;
using DataIngrestorApi.DTOs.Unhold;

namespace DataIngrestorApi.DTOs;

/// <summary>
/// Тип - контейнер уведомлений
/// </summary>
public class NotificationWrapperDto
{
    /// <summary>
    /// Уведомление о финансовой авторизации по номеру счета банка-эмитента
    /// </summary>
    public IssFinAuthDto? IssFinAuth { get; set; }

    /// <summary>
    /// Уведомление об эквайринговой финансовой авторизации по карте
    /// </summary>
    public AcqFinAuthDto? AcqFinAuth { get; set; }
    
    /// <summary>
    /// Уведомление об изменении статуса карты
    /// </summary>
    public CardStatusChangeDto? CardStatusChange { get; set; }

    /// <summary>
    /// Уведомление об изменении PIN-кода
    /// </summary>
    public PinChangeDto? PinChange { get; set; }
    
    /// <summary>
    /// Уведомление об изменении статуса токена
    /// </summary>
    public TokenStausChangeDto? TokenStatusChange { get; set; }
    
    /// <summary>
    /// Уведомление о снятии холда
    /// </summary>
    public UnholdDto? Unhold { get; set; }
    
    /// <summary>
    /// Уведомление о действии пользователя в OWI
    /// </summary>
    public OwiUserActionDto? OwiUserAction { get; set; }
    
    /// <summary>
    /// Уведомление об изменении лимита авторизации по факту финансовой обработки
    /// </summary>
    public AcctBalChangeDto? AcctBalChange { get; set; }
}