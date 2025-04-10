namespace DataIngrestorApi.DTOs.AcsOtp;

/// <summary>
/// Детали разовых паролей, отправляемых ACS банка-эмитента карты
/// </summary>
public class AcsOtpDetailsDto
{
    /// <summary>
    /// Информация о разовом пароле
    /// </summary>
    public OtpInfo OtpInfo { get; set; }
    
    /// <summary>
    /// Сумма операции. Может отсутствовать для нефинансовых операций
    /// </summary>
    public MoneyDto? AuthMoney { get; set; }
    
    /// <summary>
    /// Время операции (YYYYMMDDHH24MISS) во временной зоне ПЦ. Может отсутствовать для нефинансовых операций
    /// </summary>
    public string TransactionTime { get; set; }
}