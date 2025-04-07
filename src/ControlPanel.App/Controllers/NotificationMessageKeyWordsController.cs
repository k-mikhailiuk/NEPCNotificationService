using ControlPanel.Core.DTOs;
using ControlPanel.Core.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ControlPanel.App.Controllers;

[Authorize]
public class NotificationMessageKeyWordsController(
    INotificationMessageKeyWordsService notificationMessageKeyWordsService)
    : Controller
{
    public async Task<IActionResult> Index(CancellationToken cancellationToken = default)
    {
        var viewModels = await notificationMessageKeyWordsService.GetKeyWordsAsync(cancellationToken);
        
        return View(viewModels);
    }
    
    [HttpPost]
    public async Task<IActionResult> UpdateDescription([FromBody] UpdateNotificationMessageKeyWordsDescriptionDto dto, CancellationToken cancellationToken = default)
    {
        await notificationMessageKeyWordsService.UpdateDescriptionAsync(dto, cancellationToken);
        return Ok(new { success = true });
    }
}