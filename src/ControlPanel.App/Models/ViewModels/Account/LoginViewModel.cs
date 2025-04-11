using System.ComponentModel.DataAnnotations;

namespace ControlPanel.App.Models.ViewModels.Account;

/// <summary>
/// Модель представления для входа пользователя в систему.
/// </summary>
public class LoginViewModel
{
    /// <summary>
    /// Получает или задаёт имя пользователя.
    /// </summary>
    [Required]
    public string UserName { get; set; }
        
    /// <summary>
    /// Получает или задаёт пароль пользователя.
    /// </summary>
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }
        
    /// <summary>
    /// Получает или задаёт значение, указывающее, следует ли запомнить пользователя.
    /// </summary>
    [Display(Name = "Запомнить меня")]
    public bool RememberMe { get; set; }
        
    /// <summary>
    /// Получает или задаёт URL для перенаправления после успешного входа.
    /// </summary>
    public string? ReturnUrl { get; set; }
}