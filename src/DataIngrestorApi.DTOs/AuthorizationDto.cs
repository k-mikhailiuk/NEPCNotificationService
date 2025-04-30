namespace DataIngrestorApi.DTOs;

/// <summary>
/// Информация об авторизации
/// </summary>
public record AuthorizationDto
{
    /// <summary>
    /// Внутренний идентификатор авторизации (utrnno)
    /// </summary>
    public long Id { get; init; }
    
    /// <summary>
    /// Признак отмены (0 - false, 1 - true)
    /// </summary>
    public int Reversal { get; init; }
}