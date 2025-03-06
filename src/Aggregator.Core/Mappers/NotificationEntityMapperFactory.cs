using Aggregator.Core.Mappers.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace Aggregator.Core.Mappers;

public class NotificationEntityMapperFactory
{
    private readonly IServiceProvider _serviceProvider;

    public NotificationEntityMapperFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public INotificationMapper<TEntity, TDto> GetMapper<TEntity, TDto>()
    {
        var mapper = _serviceProvider.GetService<INotificationMapper<TEntity, TDto>>();
        if (mapper == null)
            throw new InvalidOperationException($"No mapper found for {typeof(TEntity).Name} -> {typeof(TDto).Name}");
        
        return mapper;
    }

}