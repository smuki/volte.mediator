using System.Threading.Tasks;
using Volte.Mediator.Contexts;
using Volte.Mediator.Contracts;
using Volte.Mediator.Extensions;

namespace Volte.Mediator.PublishingStrategies;

/// <summary>
/// Invokes event handlers in sequence and waits for the result.
/// </summary>
public class SequentialProcessingStrategy : IEventPublishingStrategy
{
    /// <inheritdoc />
    public async Task PublishAsync(NotificationStrategyContext context)
    {
        var notification = context.Notification;
        var cancellationToken = context.CancellationToken;
        var notificationType = notification.GetType();
        var handleMethod = notificationType.GetNotificationHandlerMethod();
        
        foreach (var handler in context.Handlers) 
            await handler.InvokeAsync(handleMethod, notification, cancellationToken);
    }
}