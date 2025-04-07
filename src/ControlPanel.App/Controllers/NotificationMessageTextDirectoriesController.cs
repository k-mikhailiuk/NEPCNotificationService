using ControlPanel.Core.DTOs;
using ControlPanel.Core.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ControlPanel.App.Controllers;

[Authorize]
public class NotificationMessageTextDirectoriesController(
    INotificationMessageTextDirectoriesService notificationMessageTextDirectoriesService)
    : Controller
{
    public async Task<IActionResult> Index(CancellationToken cancellationToken = default)
    {
        var viewModels =
            await notificationMessageTextDirectoriesService.GetNotificationsTextAsync(cancellationToken);

        return View(viewModels);
    }

    [HttpPost]
    public async Task<IActionResult> UpdateMessageTexts([FromBody] UpdateNotificationMessageDirectoriesTextDto dto,
        CancellationToken cancellationToken = default)
    {
        await notificationMessageTextDirectoriesService.UpdateMessageTextsAsync(dto, cancellationToken);
        return Ok(new { success = true });
    }
}