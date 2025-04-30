using DataIngrestorApi.DTOs;
using DataIngrestorApi.Core.MessageProcessor.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace DataIngrestorApi.App.Controllers;

/// <summary>
/// Контроллер для обработки уведомлений.
/// Содержит методы для получения уведомлений и эхо-ответа.
/// </summary>
[ApiController]
public class NotificationsController(IMessageProcessor messageProcessor, ILogger<NotificationsController> logger)
    : ControllerBase
{
    /// <summary>
    /// Принимает пакет уведомлений и обрабатывает его с помощью IMessageProcessor.
    /// </summary>
    /// <param name="request">DTO запроса уведомлений.</param>
    /// <param name="instance">Заголовок X-CS-Instance, идентифицирующий экземпляр.</param>
    /// <param name="requestId">Заголовок X-CS-RequestId, идентификатор запроса.</param>
    /// <param name="requestTime">Заголовок X-CS-RequestTime, время запроса.</param>
    /// <param name="userAgent">Заголовок User-Agent, информация об агенте пользователя.</param>
    /// <param name="host">Заголовок Host, опциональное значение хоста.</param>
    /// <returns>
    /// <see cref="IActionResult"/> со статусом 200 (Ok) при успешной обработке,
    /// 400 (Bad Request) при отсутствии необходимых заголовков или ошибках валидации,
    /// или 500 (Internal Server Error) при возникновении исключительной ситуации.
    /// </returns>
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
            if (string.IsNullOrEmpty(instance) || requestId == 0 || string.IsNullOrEmpty(requestTime) || string.IsNullOrEmpty(userAgent) || string.IsNullOrEmpty(userAgent))
            {
                return BadRequest("Missing requires headers.");
            }

            var contentType = HttpContext.Request.ContentType;
            logger.LogInformation("Processing request: Instance={Instance}, RequestId={RequestId}, RequestTime={RequestTime}, ContentType={ContentType}, UserAgent={UserAgent}, Host={Host}",
                instance, requestId, requestTime, contentType, userAgent, host);

            await messageProcessor.ProcessBatchAsync(request);
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
    
    /// <summary>
    /// Эхо-метод, возвращающий статус 200 (Ok) после получения строки запроса.
    /// Используется для проверки доступности сервиса.
    /// </summary>
    /// <param name="request">Строка, передаваемая в теле запроса.</param>
    /// <param name="contentType">Заголовок Content-Type, указывающий тип содержимого.</param>
    /// <param name="userAgent">Заголовок User-Agent, информация об агенте пользователя.</param>
    /// <returns>
    /// <see cref="IActionResult"/> со статусом 200 (Ok) при успешном получении,
    /// 400 (Bad Request) при отсутствии необходимых заголовков,
    /// или 500 (Internal Server Error) при возникновении исключительной ситуации.
    /// </returns>
    [HttpPost("echo")]
    public IActionResult Echo(
        [FromBody] string? request,
        [FromHeader(Name = "Content-Type")] string? contentType,
        [FromHeader(Name = "User-Agent")] string? userAgent)
    {
        try
        {
            if (string.IsNullOrEmpty(contentType) || string.IsNullOrEmpty(userAgent))
            {
                return BadRequest("Missing requires headers.");
            }
            logger.LogInformation("receivedMessage is : {request}", request);
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