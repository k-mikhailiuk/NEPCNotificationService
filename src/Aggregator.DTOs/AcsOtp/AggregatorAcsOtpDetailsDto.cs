namespace Aggregator.DTOs.AcsOtp;

/// <summary>
/// Детали разовых паролей, отправляемых ACS банка-эмитента карты
/// </summary>
public record AggregatorAcsOtpDetailsDto
{
    /// <summary>
    /// Информация о разовом пароле
    /// </summary>
    public AggregatorOtpInfo OtpInfo { get; init; }

    /// <summary>
    /// Сумма операции. Может отсутствовать для нефинансовых операций
    /// </summary>
    public AggregatorMoneyDto? AuthMoney { get; init; }

    /// <summary>
    /// Время операции (YYYYMMDDHH24MISS) во временной зоне ПЦ. Может отсутствовать для нефинансовых операций
    /// </summary>
    public string TransactionTime { get; init; }
}