using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aggregator.DataAccess.Entities;

/// <summary>
/// Параметры расширения
/// </summary>
public class ExtensionParameter
{
    /// <summary>
    /// Уникальный идентификатор
    /// </summary>
    public long Id { get; set; }
    
    /// <summary>
    /// Идентификатор расширения, которому принадлежит параметр
    /// </summary>
    public long ExtensionId { get; set; }
    
    /// <inheritdoc cref="NotificationExtension" />
    public NotificationExtension Extension { get; set; }
    
    /// <summary>
    /// Имя параметра
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    /// Значение параметра
    /// </summary>
    public string Value { get; set; }
}