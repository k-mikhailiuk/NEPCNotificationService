using System.ComponentModel.DataAnnotations;

namespace ControlPanel.App.Models.ViewModels.Account;

public class LoginViewModel
{
    [Required]
    public string UserName { get; set; }
        
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }
        
    [Display(Name = "Запомнить меня")]
    public bool RememberMe { get; set; }
        
    public string? ReturnUrl { get; set; }
}