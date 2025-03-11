using Aggregator.Core.Behaviors;
using Aggregator.Core.Commands;
using Aggregator.Core.Extensions.Factories;
using Aggregator.Core.Handlers;
using Aggregator.Core.Handlers.Notifications;
using Aggregator.Core.Mappers;
using Aggregator.Core.Mappers.Abstractions;
using Aggregator.Core.Mappers.Notifications;
using Aggregator.Core.Validators.Notifications;
using Aggregator.DTOs.AcctBalChange;
using Aggregator.DTOs.AcqFinAuth;
using Aggregator.DTOs.CardStatusChange;
using Aggregator.DTOs.IssFinAuth;
using Aggregator.DTOs.OwiUserAction;
using Aggregator.DTOs.PinChange;
using Aggregator.DTOs.TokenStausChange;
using Aggregator.DTOs.Unhold;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Aggregator.Core;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCommands(this IServiceCollection services)
    {
        services.AddSingleton<IMediator, Mediator>();

        services.AddTransient<IRequestHandler<ProcessInboxMessageCommand>, ProcessInboxMessageHandler>();
        services
            .AddTransient<IRequestHandler<ProcessNotificationCommand<AggregatorAcctBalChangeDto>, List<long>>,
                AcctBalChangeProcessHandler>();
        services
            .AddTransient<IRequestHandler<ProcessNotificationCommand<AggregatorAcqFinAuthDto>, List<long>>,
                AcqFinAuthProcessHandler>();
        services
            .AddTransient<IRequestHandler<ProcessNotificationCommand<AggregatorCardStatusChangeDto>, List<long>>,
                CardStatusChangeProcessHandler>();
        services
            .AddTransient<IRequestHandler<ProcessNotificationCommand<AggregatorIssFinAuthDto>, List<long>>,
                IssFinAuthProcessHandler>();
        services
            .AddTransient<IRequestHandler<ProcessNotificationCommand<AggregatorOwiUserActionDto>, List<long>>,
                OwiUserActionProcessHandler>();
        services
            .AddTransient<IRequestHandler<ProcessNotificationCommand<AggregatorPinChangeDto>, List<long>>,
                PinChangeProcessHandler>();
        services
            .AddTransient<IRequestHandler<ProcessNotificationCommand<AggregatorTokenStatusChangeDto>, List<long>>,
                TokenStatusChangeProcessHandler>();
        services
            .AddTransient<IRequestHandler<ProcessNotificationCommand<AggregatorUnholdDto>, List<long>>,
                UnholdProcessHandler>();

        return services;
    }

    public static IServiceCollection AddFactories(this IServiceCollection services)
    {
        services.AddSingleton<INotificationCommandFactory, NotificationCommandFactory>();
        services.AddSingleton<NotificationEntityMapperFactory>();


        return services;
    }

    public static IServiceCollection AddBehaviors(this IServiceCollection services)
    {
        services.AddTransient<
            IPipelineBehavior<ProcessNotificationCommand<AggregatorCardStatusChangeDto>, Unit>,
            ValidationBehaviorForProcessNotification<AggregatorCardStatusChangeDto>>();
        services.AddTransient<
            IPipelineBehavior<ProcessNotificationCommand<AggregatorIssFinAuthDto>, Unit>,
            ValidationBehaviorForProcessNotification<AggregatorIssFinAuthDto>>();

        return services;
    }

    public static IServiceCollection AddValidators(this IServiceCollection services)
    {
        services.AddTransient<IValidator<AggregatorCardStatusChangeDto>, CardStatusChangeDtoValidator>();
        services.AddTransient<IValidator<AggregatorIssFinAuthDto>, AggregatorIssFinAuthDtoCommandValidator>();
        return services;
    }

    public static IServiceCollection AddMappers(this IServiceCollection services)
    {
        services.Scan(scan => scan
            .FromAssemblyOf<AcqFinAuthEntityMapper>()
            .AddClasses(classes => classes.AssignableTo(typeof(INotificationMapper<,>)))
            .AsImplementedInterfaces()
            .WithSingletonLifetime());

        return services;
    }
}