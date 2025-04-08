using Aggregator.Core.Mappers.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace Aggregator.Core.Mappers;

public class NotificationEntityMapperFactory(IServiceProvider serviceProvider)
{
    public INotificationMapper<TEntity, TDto> GetMapper<TEntity, TDto>()
    {
        var mapper = serviceProvider.GetService<INotificationMapper<TEntity, TDto>>();
        if (mapper == null)
            throw new InvalidOperationException($"No mapper found for {typeof(TEntity).Name} -> {typeof(TDto).Name}");
        
        return mapper;
    }

}