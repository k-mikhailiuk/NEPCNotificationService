using ControlPanel.Core.DTOs;
using ControlPanel.Core.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ControlPanel.App.Controllers;

[Authorize]
public class NotificationMessageTextDirectoriesController : Controller
{
    private readonly INotificationMessageTextDirectoriesService _notificationMessageTextDirectoriesService;

    public NotificationMessageTextDirectoriesController(
        INotificationMessageTextDirectoriesService notificationMessageTextDirectoriesService)
    {
        _notificationMessageTextDirectoriesService = notificationMessageTextDirectoriesService;
    }

    public async Task<IActionResult> Index(CancellationToken cancellationToken = default)
    {
        var viewModels =
            await _notificationMessageTextDirectoriesService.GetNotificationsTextAsync(cancellationToken);

        return View(viewModels);
    }

    [HttpPost]
    public async Task<IActionResult> UpdateMessageTexts([FromBody] UpdateNotificationMessageDirectoriesTextDto dto,
        CancellationToken cancellationToken = default)
    {
        await _notificationMessageTextDirectoriesService.UpdateMessageTextsAsync(dto, cancellationToken);
        return Ok(new { success = true });
    }
}