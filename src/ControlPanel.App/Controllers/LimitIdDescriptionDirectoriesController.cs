using ControlPanel.Core.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace ControlPanel.App.Controllers;

public class LimitIdDescriptionDirectoriesController(ILimitIdDescriptionDirectoriesService limitIdDescriptionDirectoriesService) : Controller
{
    public async Task<IActionResult> Index(CancellationToken cancellationToken = default)
    {
        var viewModels = await limitIdDescriptionDirectoriesService.GetLimitIdDescriptionDirectoriesAsync(cancellationToken);
        
        return View(viewModels);
    }
}