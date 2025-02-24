namespace DataIngrestorApi.DTOs;

/// <summary>
/// Информация об авторизации
/// </summary>
public class AuthorizationDto
{
    /// <summary>
    /// Внутренний идентификатор авторизации (utrnno)
    /// </summary>
    public long Id { get; set; }
    
    /// <summary>
    /// Признак отмены (0 - false, 1 - true)
    /// </summary>
    public int Reversal { get; set; }
}