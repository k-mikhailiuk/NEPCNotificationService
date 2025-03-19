using ControlPanel.DataAccess;
using Microsoft.AspNetCore.Identity;
using OptionsConfiguration;

namespace ControlPanel.App;

public static class DIConfigure
{
    /// <summary>
    /// Register custom services
    /// </summary>
    public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDatabaseConnectionString(configuration);
        services.AddControlPanelDbContext(configuration);
        services.AddAdminUserOptions(configuration);
        services.AddControllersWithViews();
        services.ConfigureIdentity(configuration);
        services.AddIdentityDataSeeder();
    }

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
    }
}