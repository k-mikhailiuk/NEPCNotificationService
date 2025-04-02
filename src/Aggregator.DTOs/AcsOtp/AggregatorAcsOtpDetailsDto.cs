namespace Aggregator.DTOs.AcsOtp;

public class AggregatorAcsOtpDetailsDto
{
    /// <summary>
    /// Информация о разовом пароле
    /// </summary>
    public AggregatorOtpInfo OtpInfo { get; set; }

    /// <summary>
    /// Сумма операции. Может отсутствовать для нефинансовых операций
    /// </summary>
    public AggregatorMoneyDto? AuthMoney { get; set; }

    /// <summary>
    /// Время операции (YYYYMMDDHH24MISS) во временной зоне ПЦ. Может отсутствовать для нефинансовых операций
    /// </summary>
    public string TransactionTime { get; set; }
}