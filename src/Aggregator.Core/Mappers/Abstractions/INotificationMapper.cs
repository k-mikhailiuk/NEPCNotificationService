namespace Aggregator.Core.Mappers.Abstractions;

public interface INotificationMapper<TEntity, TDto>
{
    TEntity Map(TDto dto);
}