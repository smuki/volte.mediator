using Volte.Mediator.Contexts;
using Volte.Mediator.Contracts;
using Volte.Mediator.Extensions;

namespace Volte.Mediator.PublishingStrategies;

/// <summary>
/// Invokes event handlers in parallel and waits for the result.
/// </summary>
public class ParallelProcessingStrategy : IEventPublishingStrategy
{
    /// <inheritdoc />
    public async Task PublishAsync(NotificationStrategyContext context)
    {
        var notification = context.Notification;
        var cancellationToken = context.CancellationToken;
        var notificationType = notification.GetType();
        var handleMethod = notificationType.GetNotificationHandlerMethod();
        var tasks = context.Handlers.Select(handler => handler.InvokeAsync(handleMethod, notification, cancellationToken)).ToList();

        await Task.WhenAll(tasks);
    }
}
