using ControlPanel.Core;
using ControlPanel.DataAccess;
using Microsoft.AspNetCore.Identity;
using OptionsConfiguration;

namespace ControlPanel.App;

/// <summary>
/// Класс для конфигурации DI контейнера и регистрации сервисов приложения ControlPanel.
/// </summary>
public static class DIConfigure
{
    /// <summary>
    /// Регистрирует кастомные сервисы в DI контейнере.
    /// </summary>
    /// <param name="services">Коллекция сервисов для регистрации зависимостей.</param>
    /// <param name="configuration">Конфигурация приложения.</param>
    public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDatabaseConnectionString(configuration);
        services.AddControlPanelDbContext(configuration);
        services.AddAdminUserOptions(configuration);
        services.AddControllersWithViews();
        services.ConfigureIdentity(configuration);
        services.AddDataSeeders();
        services.AddRepositories();
        services.AddServices();
    }

    /// <summary>
    /// Конфигурирует Identity для приложения.
    /// </summary>
    /// <param name="services">Коллекция сервисов для регистрации зависимостей.</param>
    /// <param name="configuration">Конфигурация приложения.</param>
    private static void ConfigureIdentity(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDefaultIdentity<IdentityUser>(options => 
            {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 3;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
            })
            .AddEntityFrameworkStores<ControlPanelDbContext>();
        
        services.ConfigureApplicationCookie(options =>
        {
            options.LoginPath = "/Account/Login";
            options.LogoutPath = "/Account/Logout";
        });
        
        services.AddAuthorization();
    }
}