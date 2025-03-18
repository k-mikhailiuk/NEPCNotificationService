using DataIngrestorApi.DTOs;
using DataIngrestorApi.Services.MessageProcessor.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace DataIngrestorApi.App.Controllers;

[ApiController]
public class NotificationsController : ControllerBase
{
    private readonly IMessageProcessor _messageProcessor;
    private readonly ILogger<NotificationsController> _logger;

    public NotificationsController(IMessageProcessor messageProcessor, ILogger<NotificationsController> logger)
    {
        _messageProcessor = messageProcessor;
        _logger = logger;
    }

    [HttpPost("receive")]
    public async Task<IActionResult> Receive([FromBody] NotificationRequestDto request,
        [FromHeader(Name = "X-CS-Instance")] string instance,
        [FromHeader(Name = "X-CS-RequestId")] long requestId,
        [FromHeader(Name = "X-CS-RequestTime")] string requestTime,
        [FromHeader(Name = "User-Agent")] string userAgent,
        [FromHeader(Name = "Host")] string? host)
    {
        try
        {
            if (string.IsNullOrEmpty(instance) || requestId == null || string.IsNullOrEmpty(requestTime) || string.IsNullOrEmpty(userAgent) || string.IsNullOrEmpty(userAgent))
            {
                return BadRequest("Missing requires headers.");
            }

            var contentType = HttpContext.Request.ContentType;
            _logger.LogInformation("Processing request: Instance={Instance}, RequestId={RequestId}, RequestTime={RequestTime}, ContentType={ContentType}, UserAgent={UserAgent}, Host={Host}",
                instance, requestId, requestTime, contentType, userAgent, host);

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
    
    [HttpPost("echo")]
    public IActionResult Echo([FromBody] string request,
        [FromHeader(Name = "Content-Type")] string contentType,
        [FromHeader(Name = "User-Agent")] string userAgent)
    {
        try
        {
            if (string.IsNullOrEmpty(contentType) || string.IsNullOrEmpty(userAgent))
            {
                return BadRequest("Missing requires headers.");
            }
            _logger.LogInformation("receivedMessage is : {request}", request);
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