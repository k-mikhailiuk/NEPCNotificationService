namespace DataIngrestorApi.DTOs.OwiUserAction;

/// <summary>
/// Подробная информация о действии пользователя в OWI
/// </summary>
public class OwiUserActionDetailsDto
{
    /// <summary>
    /// Время совершения операции в ПЦ (YYYYMMDDHH24MISS)
    /// </summary>
    public string TransactionTime { get; set; }

    /// <summary>
    /// Login в owi (имя пользователя)
    /// </summary>
    public string Login { get; set; }

    /// <summary>
    /// Действие пользователя
    /// </summary>
    public string Action { get; set; }
}