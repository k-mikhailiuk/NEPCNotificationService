namespace Aggregator.Core.Mappers.Abstractions;

/// <summary>
/// Интерфейс маппера уведомлений, который преобразует объект DTO в сущность.
/// </summary>
/// <typeparam name="TEntity">Тип сущности, в которую производится маппинг.</typeparam>
/// <typeparam name="TDto">Тип объекта DTO, который необходимо преобразовать.</typeparam>
public interface INotificationMapper<out TEntity, in TDto>
{
    /// <summary>
    /// Преобразует объект типа <typeparamref name="TDto"/> в сущность типа <typeparamref name="TEntity"/>.
    /// </summary>
    /// <param name="dto">Объект DTO для маппинга.</param>
    /// <returns>Результирующая сущность <typeparamref name="TEntity"/>.</returns>
    TEntity Map(TDto dto);
}