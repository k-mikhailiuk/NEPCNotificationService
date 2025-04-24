using Aggregator.Core.Behaviors;
using Aggregator.Core.Commands;
using Aggregator.Core.Extensions.Factories;
using Aggregator.Core.Extensions.Factories.Abstractions;
using Aggregator.Core.Handlers;
using Aggregator.Core.Handlers.Notifications;
using Aggregator.Core.Mappers;
using Aggregator.Core.Mappers.Abstractions;
using Aggregator.Core.Mappers.Notifications;
using Aggregator.Core.Services;
using Aggregator.Core.Services.Abstractions;
using Aggregator.Core.Services.KeyWordBuilders;
using Aggregator.Core.Services.MessageBuilders;
using Aggregator.Core.Validators.Notifications;
using Aggregator.DTOs.AcctBalChange;
using Aggregator.DTOs.AcqFinAuth;
using Aggregator.DTOs.AcsOtp;
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

/// <summary>
/// Методы расширения для регистрации зависимостей в DI-контейнере.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Регистрирует командные обработчики и связанные сервисы.
    /// </summary>
    /// <param name="services">Коллекция сервисов.</param>
    /// <returns>Обновленная коллекция сервисов.</returns>
    public static IServiceCollection AddCommands(this IServiceCollection services)
    {
        services.AddSingleton<IMediator, Mediator>();
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
        services
            .AddTransient<IRequestHandler<ProcessNotificationCommand<AggregatorAcsOtpDto>, List<long>>,
                AcsOtpProcessHandler>();

        return services;
    }
    
    /// <summary>
    /// Регистрирует сервисы.
    /// </summary>
    /// <param name="services">Коллекция сервисов.</param>
    /// <returns>Обновленная коллекция сервисов.</returns>
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddTransient<IInboxHandler, InboxHandler>();

        return services;
    }

    /// <summary>
    /// Регистрирует фабрики для создания команд и построителей сообщений уведомлений.
    /// </summary>
    /// <param name="services">Коллекция сервисов.</param>
    /// <returns>Обновленная коллекция сервисов.</returns>
    public static IServiceCollection AddFactories(this IServiceCollection services)
    {
        services.AddSingleton<INotificationCommandFactory, NotificationCommandFactory>();
        services.AddSingleton<INotificationMessageBuilderFactory, NotificationMessageBuilderFactory>();
        services.AddSingleton<NotificationEntityMapperFactory>();

        return services;
    }

    /// <summary>
    /// Регистрирует поведения конвейера обработки (pipeline behaviors).
    /// </summary>
    /// <param name="services">Коллекция сервисов.</param>
    /// <returns>Обновленная коллекция сервисов.</returns>
    public static IServiceCollection AddBehaviors(this IServiceCollection services)
    {
        services.Scan(scan => scan
            .FromAssemblies(typeof(ValidationBehaviorForProcessNotification<>).Assembly)
            .AddClasses(classes => classes.AssignableTo(typeof(IValidator<>)))
            .AsImplementedInterfaces()
            .WithTransientLifetime()
        );
        
        services.AddTransient(
            typeof(IPipelineBehavior<,>),
            typeof(ValidationBehaviorForProcessNotification<>)
        );

        return services;
    }

    /// <summary>
    /// Регистрирует валидаторы DTO уведомлений.
    /// </summary>
    /// <param name="services">Коллекция сервисов.</param>
    /// <returns>Обновленная коллекция сервисов.</returns>
    public static IServiceCollection AddValidators(this IServiceCollection services)
    {
        services.AddTransient<IValidator<AggregatorCardStatusChangeDto>, CardStatusChangeDtoValidator>();
        services.AddTransient<IValidator<AggregatorIssFinAuthDto>, AggregatorIssFinAuthDtoCommandValidator>();
        return services;
    }

    /// <summary>
    /// Регистрирует мапперы уведомлений.
    /// </summary>
    /// <param name="services">Коллекция сервисов.</param>
    /// <returns>Обновленная коллекция сервисов.</returns>
    public static IServiceCollection AddMappers(this IServiceCollection services)
    {
        services.Scan(scan => scan
            .FromAssemblyOf<AcqFinAuthEntityMapper>()
            .AddClasses(classes => classes.AssignableTo(typeof(INotificationMapper<,>)))
            .AsImplementedInterfaces()
            .WithSingletonLifetime());

        return services;
    }

    /// <summary>
    /// Регистрирует построители уведомлений, ключевых слов, и связанные сервисы.
    /// </summary>
    /// <param name="services">Коллекция сервисов.</param>
    /// <returns>Обновленная коллекция сервисов.</returns>
    public static IServiceCollection AddBuilders(this IServiceCollection services)
    {
        services.Scan(scan => scan
            .FromAssemblyOf<IssFinAuthNotificationMessageBuilder>()
            .AddClasses(classes => classes.AssignableTo(typeof(INotificationMessageBuilder<>)))
            .AsImplementedInterfaces()
            .WithTransientLifetime()
        );

        services.Scan(scan => scan
            .FromAssemblyOf<IssFinAuthKeyWordBuilder>()
            .AddClasses(classes => classes.AssignableTo(typeof(IKeyWordBuilder<>)))
            .AsImplementedInterfaces()
            .WithTransientLifetime()
        );

        services.AddTransient<ICurrencyReplacer, CurrencyReplacer>();
        services.AddTransient<ILanguageSelector, LanguageSelector>();
        services.AddTransient<ILimitIdReplacer, LimitIdReplacer>();

        return services;
    }

    /// <summary>
    /// Регистрирует сервисы отправки уведомлений и сохранения истории уведомлений в контейнере зависимостей.
    /// </summary>
    /// <param name="services">Коллекция сервисов.</param>
    /// <returns>Обновлённая коллекция сервисов с зарегистрированными зависимостями.</returns>
    public static IServiceCollection AddNotificationMessageServices(this IServiceCollection services)
    {
        services.AddScoped<INotificationMessageSender, NotificationMessageSender>();
        services.AddScoped<INotificationHistorySaver, NotificationHistorySaver>();
        return services;
    }
}