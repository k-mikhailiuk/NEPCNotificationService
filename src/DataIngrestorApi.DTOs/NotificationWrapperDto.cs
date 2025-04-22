using System.Text.Json.Serialization;
using DataIngrestorApi.DTOs.AcctBalChange;
using DataIngrestorApi.DTOs.AcqFinAuth;
using DataIngrestorApi.DTOs.AcsOtp;
using DataIngrestorApi.DTOs.CardStatusChange;
using DataIngrestorApi.DTOs.IssFinAuth;
using DataIngrestorApi.DTOs.OwiUserAction;
using DataIngrestorApi.DTOs.PinChange;
using DataIngrestorApi.DTOs.TokenStatusChange;
using DataIngrestorApi.DTOs.Unhold;

namespace DataIngrestorApi.DTOs;

/// <summary>
/// Тип - контейнер уведомлений
/// </summary>
public record NotificationWrapperDto
{
    /// <summary>
    /// Уведомление о финансовой авторизации по номеру счета банка-эмитента
    /// </summary>
    public IssFinAuthDto? IssFinAuth { get; init; }

    /// <summary>
    /// Уведомление об эквайринговой финансовой авторизации по карте
    /// </summary>
    public AcqFinAuthDto? AcqFinAuth { get; init; }
    
    /// <summary>
    /// Уведомление об изменении статуса карты
    /// </summary>
    public CardStatusChangeDto? CardStatusChange { get; init; }

    /// <summary>
    /// Уведомление об изменении PIN-кода
    /// </summary>
    public PinChangeDto? PinChange { get; init; }
    
    /// <summary>
    /// Уведомление об изменении статуса токена
    /// </summary>
    public TokenStatusChangeDto? TokenStatusChange { get; init; }
    
    /// <summary>
    /// Уведомление о снятии холда
    /// </summary>
    public UnholdDto? Unhold { get; init; }
    
    /// <summary>
    /// Уведомление о действии пользователя в OWI
    /// </summary>
    public OwiUserActionDto? OwiUserAction { get; init; }
    
    /// <summary>
    /// Уведомление об изменении лимита авторизации по факту финансовой обработки
    /// </summary>
    public AcctBalChangeDto? AcctBalChange { get; init; }
    
    /// <summary>
    /// Уведомление об изменении лимита авторизации по факту финансовой обработки
    /// </summary>
    public AcsOtpDto? AcsOtp { get; init; }
}