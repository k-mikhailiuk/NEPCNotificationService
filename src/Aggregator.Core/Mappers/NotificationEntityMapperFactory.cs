using Aggregator.Core.Mappers.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace Aggregator.Core.Mappers;

/// <summary>
/// Фабрика для получения маппера уведомлений.
/// </summary>
public class NotificationEntityMapperFactory(IServiceProvider serviceProvider)
{
    /// <summary>
    /// Возвращает маппер для преобразования объекта DTO в сущность.
    /// </summary>
    /// <typeparam name="TEntity">Тип целевой сущности.</typeparam>
    /// <typeparam name="TDto">Тип исходного DTO.</typeparam>
    /// <returns>
    /// Инстанс <see cref="INotificationMapper{TEntity, TDto}"/>, который осуществляет маппинг от <typeparamref name="TDto"/> к <typeparamref name="TEntity"/>.
    /// </returns>
    /// <exception cref="InvalidOperationException">
    /// Выбрасывается, если не удается разрешить зависимость для указанного маппера.
    /// </exception>
    public INotificationMapper<TEntity, TDto> GetMapper<TEntity, TDto>()
    {
        var mapper = serviceProvider.GetService<INotificationMapper<TEntity, TDto>>();
        if (mapper == null)
            throw new InvalidOperationException($"No mapper found for {typeof(TEntity).Name} -> {typeof(TDto).Name}");
        
        return mapper;
    }

}