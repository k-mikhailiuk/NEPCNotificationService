namespace OptionsConfiguration;

/// <summary>
/// Класс конфигурации параметров администратора, используется для привязки значений из конфигурационного файла.
/// </summary>
public class AdminUserOptions
{
    /// <summary>
    /// Имя секции конфигурации.
    /// </summary>
    public const string AdminUser = nameof(AdminUser); 
    
    /// <summary>
    /// Имя пользователя администратора.
    /// </summary>
    public string UserName { get; set; }
    
    /// <summary>
    /// Пароль администратора.
    /// </summary>
    public string Password { get; set; }
    
    /// <summary>
    /// Электронная почта администратора.
    /// </summary>
    public string Email { get; set; }
}