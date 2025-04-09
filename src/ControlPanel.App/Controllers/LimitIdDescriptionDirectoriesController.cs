using ControlPanel.Core.DTOs.LimitIdDescription;
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
    
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] AddLimitIdDescriptionDto dto, CancellationToken cancellationToken = default)
    {
        await limitIdDescriptionDirectoriesService.CreateLimitIdDescription(dto, cancellationToken);
        return Ok(new { success = true });
    }
    
    [HttpPost]
    public async Task<IActionResult> Edit([FromBody] EditLimitIdDescriptionDto dto, CancellationToken cancellationToken = default)
    {
        await limitIdDescriptionDirectoriesService.EditLimitIdDescription(dto, cancellationToken);
        return Ok(new { success = true });
    }
    
    [HttpPost]
    public async Task<IActionResult> Delete([FromBody] DeleteLimitIdDescriptionDto dto, CancellationToken cancellationToken = default)
    {
        await limitIdDescriptionDirectoriesService.DeleteLimitIdDescription(dto.Id, cancellationToken);
        return Ok(new { success = true });
    }
}