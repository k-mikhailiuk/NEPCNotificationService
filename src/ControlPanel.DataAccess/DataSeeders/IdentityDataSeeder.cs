using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using OptionsConfiguration;

namespace ControlPanel.DataAccess.DataSeeders;

public class IdentityDataSeeder(IOptions<AdminUserOptions> adminUserOptions)
{
    private readonly AdminUserOptions _adminUserOptions = adminUserOptions.Value;

    public async Task SeedDefaultUserAsync(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

        var adminUser = await userManager.FindByNameAsync(_adminUserOptions.UserName);
        if (adminUser == null)
        {
            adminUser = new IdentityUser
            {
                UserName = _adminUserOptions.UserName,
                Email = _adminUserOptions.Email,
                EmailConfirmed = true
            };

            var result = await userManager.CreateAsync(adminUser, _adminUserOptions.Password);
            if (!result.Succeeded)
            {
                throw new Exception("Не удалось создать пользователя admin: " +
                                    string.Join(", ", result.Errors.Select(e => e.Description)));
            }
        }
    }
}