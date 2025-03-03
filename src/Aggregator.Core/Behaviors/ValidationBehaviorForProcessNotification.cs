using Aggregator.Core.Commands;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Aggregator.Core.Behaviors;

public class ValidationBehaviorForProcessNotification<T>
    : IPipelineBehavior<ProcessNotificationCommand<T>, Unit>
    where T : class
{
    private readonly IEnumerable<IValidator<T>> _validators;
    private readonly ILogger<ValidationBehaviorForProcessNotification<T>> _logger;

    public ValidationBehaviorForProcessNotification(
        IEnumerable<IValidator<T>> validators,
        ILogger<ValidationBehaviorForProcessNotification<T>> logger)
    {
        _validators = validators;
        _logger = logger;
    }

    public async Task<Unit> Handle(ProcessNotificationCommand<T> request, RequestHandlerDelegate<Unit> next, CancellationToken cancellationToken)
    {
        if (!_validators.Any())
            return await next();

        var invalidItems = new List<T>();

        foreach (var notification in request.Notifications)
        {
            var validationFailures = new List<ValidationFailure>();
        
            foreach (var validator in _validators)
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
                _logger.LogWarning(
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