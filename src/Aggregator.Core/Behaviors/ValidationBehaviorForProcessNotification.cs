using Aggregator.Core.Commands;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Aggregator.Core.Behaviors;

/// <summary>
/// Поведение для обработки команды уведомлений с валидацией.
/// </summary>
/// <typeparam name="T">Тип уведомления, которое должно быть проверено.</typeparam>
/// <remarks>
/// Данное поведение является частью конвейера обработки (pipeline) MediatR и используется для валидации
/// уведомлений, передаваемых в команде <see cref="ProcessNotificationCommand{T}"/>. Если уведомление не
/// проходит валидацию, оно удаляется из списка уведомлений, а ошибки валидации записываются в лог.
/// </remarks>
public class ValidationBehaviorForProcessNotification<T>(
    IEnumerable<IValidator<T>> validators,
    ILogger<ValidationBehaviorForProcessNotification<T>> logger)
    : IPipelineBehavior<ProcessNotificationCommand<T>, Unit>
    where T : class
{
    
    /// <summary>
    /// Обрабатывает команду уведомлений с выполнением валидации.
    /// </summary>
    /// <param name="request">Запрос, содержащий уведомления для валидации.</param>
    /// <param name="next">Делегат для вызова следующего обработчика в конвейере.</param>
    /// <param name="cancellationToken">Токен для отмены выполнения операции.</param>
    /// <returns>Результат выполнения следующего обработчика в виде <see cref="Unit"/>.</returns>
    public async Task<Unit> Handle(ProcessNotificationCommand<T> request, RequestHandlerDelegate<Unit> next, CancellationToken cancellationToken)
    {
        if (!validators.Any())
            return await next();

        var invalidItems = new List<T>();

        foreach (var notification in request.Notifications)
        {
            var validationFailures = new List<ValidationFailure>();
        
            foreach (var validator in validators)
            {
                var context = new ValidationContext<T>(notification);
                var result = await validator.ValidateAsync(context, cancellationToken);
                if (!result.IsValid)
                {
                    validationFailures.AddRange(result.Errors);
                }
            }

            if (validationFailures.Count <= 0) continue;
            
            foreach (var error in validationFailures)
            {
                logger.LogWarning(
                    "Validation failed for item: {Item}. Error: {Message}", 
                    notification, 
                    error.ErrorMessage
                );
            }

            invalidItems.Add(notification);
        }

        request.Notifications.RemoveAll(item => invalidItems.Contains(item));

        return await next();
    }
}