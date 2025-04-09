using ControlPanel.Core.DTOs;
using ControlPanel.Core.DTOs.Currency;
using ControlPanel.Core.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ControlPanel.App.Controllers;

[Authorize]
public class CurrenciesController(ICurrenciesService currenciesService) : Controller
{
    public async Task<IActionResult> Index(CancellationToken cancellationToken = default)
    {
        var viewModels = await currenciesService.GetCurrenciesAsync(cancellationToken);
        
        return View(viewModels);
    }
    
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] AddCurrencyDto dto, CancellationToken cancellationToken = default)
    {
        await currenciesService.CreateCurrency(dto, cancellationToken);
        return Ok(new { success = true });
    }
    
    [HttpPost]
    public async Task<IActionResult> Edit([FromBody] EditCurrencyDto dto, CancellationToken cancellationToken = default)
    {
        await currenciesService.EditCurrency(dto, cancellationToken);
        return Ok(new { success = true });
    }
    
    [HttpPost]
    public async Task<IActionResult> Delete([FromBody] DeleteCurrencyDto dto, CancellationToken cancellationToken = default)
    {
        await currenciesService.DeleteCurrency(dto.CurrencyCode, cancellationToken);
        return Ok(new { success = true });
    }
}