namespace DataIngrestorApi.DTOs.AcsOtp;

/// <summary>
/// Детали разовых паролей, отправляемых ACS банка-эмитента карты
/// </summary>
public record AcsOtpDetailsDto
{
    /// <summary>
    /// Информация о разовом пароле
    /// </summary>
    public OtpInfo OtpInfo { get; init; }
    
    /// <summary>
    /// Сумма операции. Может отсутствовать для нефинансовых операций
    /// </summary>
    public MoneyDto? AuthMoney { get; init; }
    
    /// <summary>
    /// Время операции (YYYYMMDDHH24MISS) во временной зоне ПЦ. Может отсутствовать для нефинансовых операций
    /// </summary>
    public string TransactionTime { get; init; }
}