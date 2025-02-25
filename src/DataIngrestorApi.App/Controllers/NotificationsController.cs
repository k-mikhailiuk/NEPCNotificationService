using DataIngrestorApi.DTOs;
using DataIngrestorApi.Services.MessageProcessor.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace DataIngrestorApi.App.Controllers;

[ApiController]
[Route("[controller]")]
public class NotificationsController : ControllerBase
{
    private readonly IMessageProcessor _messageProcessor;
    
    public NotificationsController(IMessageProcessor messageProcessor)
    {
        _messageProcessor = messageProcessor;
    }
    
    [HttpPost("receive")]
    public async Task<IActionResult> Receive([FromBody] NotificationRequestDto  request)
    {
        try
        {
            await _messageProcessor.ProcessBatchAsync(request);
            return Ok();
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error. Exception: {ex}");
        }
    }
}