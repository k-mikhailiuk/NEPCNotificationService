using Aggregator.Core.Commands;
using Aggregator.Core.Handlers;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Aggregator.Core;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCommands(this IServiceCollection services)
    {
        services.AddSingleton<IMediator, Mediator>();
        
        services.AddTransient<IRequestHandler<ProcessInboxMessageCommand>, ProcessInboxMessageHandler>();
        
        return services;
    }
}