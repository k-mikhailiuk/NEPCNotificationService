using ControlPanel.App.Models.ViewModels.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ControlPanel.App.Controllers;

/// <summary>
/// Контроллер для управления входом и выходом из системы.
/// </summary>
public class AccountController(SignInManager<IdentityUser> signInManager) : Controller
{
    /// <summary>
    /// Отображает страницу входа в систему.
    /// </summary>
    /// <param name="returnUrl">URL, на который будет выполнен редирект после успешного входа.</param>
    /// <returns>Представление страницы входа с моделью <see cref="LoginViewModel"/>.</returns>
    [HttpGet]
    public IActionResult Login(string returnUrl = null)
    {
        var model = new LoginViewModel
        {
            ReturnUrl = returnUrl
        };
        return View(model);
    }
    
    /// <summary>
    /// Обрабатывает POST-запрос на вход в систему.
    /// </summary>
    /// <param name="model">Модель данных для входа.</param>
    /// <returns>
    /// Если вход успешен, выполняется редирект на указанный returnUrl или на действие Index контроллера NotificationMessageKeyWords;
    /// в противном случае, возвращается представление с моделью и сообщением об ошибке.
    /// </returns>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        var result = await signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, lockoutOnFailure: false);

        if (result.Succeeded)
        {
            if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                return Redirect(model.ReturnUrl);
            else
                return RedirectToAction("Index", "NotificationMessageKeyWords");
        }

        ModelState.AddModelError(string.Empty, "Неверная попытка входа.");
        return View(model);
    }
    
    /// <summary>
    /// Обрабатывает POST-запрос на выход из системы.
    /// </summary>
    /// <returns>Выполняет редирект на действие Index контроллера NotificationMessageKeyWords после выхода.</returns>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
        await signInManager.SignOutAsync();
        return RedirectToAction("Index", "NotificationMessageKeyWords");
    }
}