using Aggregator.Core.Commands;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Aggregator.Core.Behaviors;

public class ValidationBehaviorForProcessNotification<T>(
    IEnumerable<IValidator<T>> validators,
    ILogger<ValidationBehaviorForProcessNotification<T>> logger)
    : IPipelineBehavior<ProcessNotificationCommand<T>, Unit>
    where T : class
{
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