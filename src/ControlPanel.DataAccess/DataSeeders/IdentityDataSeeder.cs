using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using OptionsConfiguration;

namespace ControlPanel.DataAccess.DataSeeders;

/// <summary>
/// Класс для сидирования данных Identity.
/// </summary>
public class IdentityDataSeeder(IOptions<AdminUserOptions> adminUserOptions)
{
    private readonly AdminUserOptions _adminUserOptions = adminUserOptions.Value;

    /// <summary>
    /// Создает пользователя по умолчанию, если он не существует.
    /// </summary>
    /// <param name="serviceProvider">Провайдер сервисов для получения зависимостей.</param>
    /// <returns>Задача асинхронного выполнения операции.</returns>
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