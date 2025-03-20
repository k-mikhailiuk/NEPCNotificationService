using ControlPanel.Core.DTOs;
using ControlPanel.Core.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ControlPanel.App.Controllers;

[Authorize]
public class NotificationMessageKeyWordsController : Controller
{
    private readonly INotificationMessageKeyWordsService _notificationMessageKeyWordsService;

    public NotificationMessageKeyWordsController(INotificationMessageKeyWordsService notificationMessageKeyWordsService)
    {
        _notificationMessageKeyWordsService = notificationMessageKeyWordsService;
    }

    public async Task<IActionResult> Index(CancellationToken cancellationToken = default)
    {
        var viewModels = await _notificationMessageKeyWordsService.GetKeyWordsAsync(cancellationToken);
        
        return View(viewModels);
    }
    
    [HttpPost]
    public async Task<IActionResult> UpdateDescription([FromBody] UpdateNotificationMessageKeyWordsDescriptionDto dto, CancellationToken cancellationToken = default)
    {
        await _notificationMessageKeyWordsService.UpdateDescriptionAsync(dto, cancellationToken);
        return Ok(new { success = true });
    }
}