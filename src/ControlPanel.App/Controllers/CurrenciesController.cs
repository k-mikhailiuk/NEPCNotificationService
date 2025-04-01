using ControlPanel.Core.DTOs;
using ControlPanel.Core.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ControlPanel.App.Controllers;

[Authorize]
public class CurrenciesController : Controller
{
    private readonly ICurrenciesService _currenciesService;
    
    public CurrenciesController(ICurrenciesService currenciesService)
    {
        _currenciesService = currenciesService;
    }

    public async Task<IActionResult> Index(CancellationToken cancellationToken = default)
    {
        var viewModels = await _currenciesService.GetCurrenciesAsync(cancellationToken);
        
        return View(viewModels);
    }
    
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] AddCurrencyDto dto, CancellationToken cancellationToken = default)
    {
        await _currenciesService.CreateCurrency(dto, cancellationToken);
        return Ok(new { success = true });
    }
    
    [HttpPost]
    public async Task<IActionResult> Edit([FromBody] EditCurrencyDto dto, CancellationToken cancellationToken = default)
    {
        await _currenciesService.EditCurrency(dto, cancellationToken);
        return Ok(new { success = true });
    }
    
    [HttpPost]
    public async Task<IActionResult> Delete([FromBody] DeleteCurrencyDto dto, CancellationToken cancellationToken = default)
    {
        await _currenciesService.DeleteCurrency(dto.CurrencyCode, cancellationToken);
        return Ok(new { success = true });
    }
}