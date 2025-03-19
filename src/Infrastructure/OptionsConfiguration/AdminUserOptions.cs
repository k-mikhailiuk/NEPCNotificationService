namespace OptionsConfiguration;

public class AdminUserOptions
{
    public const string AdminUser = nameof(AdminUser); 
    
    public string UserName { get; set; }
    
    public string Password { get; set; }
    
    public string Email { get; set; }
}