using ControlPanel.Core.DTOs.LimitIdDescription;
using ControlPanel.DataAccess.Entities;

namespace ControlPanel.Core.Services.Contracts;

/// <summary>
/// Сервис управления справочником описаний лимитов.
/// </summary>
public interface ILimitIdDescriptionDirectoriesService
{
    /// <summary>
    /// Возвращает список записей справочника описаний лимитов.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Список объектов <see cref="LimitIdDescriptionDirectory"/>.</returns>
    Task<List<LimitIdDescriptionDirectory>> GetLimitIdDescriptionDirectoriesAsync(CancellationToken cancellationToken);
    
    /// <summary>
    /// Создает новую запись описания лимита.
    /// </summary>
    /// <param name="dto">DTO для добавления записи.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    Task CreateLimitIdDescription(AddLimitIdDescriptionDto dto, CancellationToken cancellationToken);
    
    /// <summary>
    /// Обновляет запись описания лимита.
    /// </summary>
    /// <param name="dto">DTO для редактирования записи.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    Task EditLimitIdDescription(EditLimitIdDescriptionDto dto, CancellationToken cancellationToken);
    
    /// <summary>
    /// Удаляет запись описания лимита по коду лимита.
    /// </summary>
    /// <param name="limitId">Код лимита.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    Task DeleteLimitIdDescription(int limitId, CancellationToken cancellationToken);
}