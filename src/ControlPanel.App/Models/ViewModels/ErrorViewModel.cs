namespace ControlPanel.App.Models.ViewModels;

/// <summary>
/// Модель представления для отображения ошибок.
/// </summary>
public class ErrorViewModel
{
    /// <summary>
    /// Получает или задаёт идентификатор запроса, который вызвал ошибку.
    /// </summary>
    public string? RequestId { get; set; }

    /// <summary>
    /// Возвращает значение, указывающее, следует ли отображать идентификатор запроса.
    /// </summary>
    public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
}