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

    public INotificationMapper<TDto, TEntity> GetMapper<TDto, TEntity>()
    {
        var mapper = _serviceProvider.GetService<INotificationMapper<TDto, TEntity>>();
        if (mapper == null)
            throw new InvalidOperationException($"No mapper found for {typeof(TDto).Name} -> {typeof(TEntity).Name}");
        
        return mapper;
    }

}