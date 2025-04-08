namespace Aggregator.Core.Mappers.Abstractions;

public interface INotificationMapper<out TEntity, in TDto>
{
    TEntity Map(TDto dto);
}