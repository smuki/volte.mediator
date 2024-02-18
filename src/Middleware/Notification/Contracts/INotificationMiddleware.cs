namespace Volte.Mediator.Middleware.Notification.Contracts;
using System.Threading.Tasks;
using System.Threading;
using System;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Represents a notification middleware component.
/// </summary>
public interface INotificationMiddleware
{
    /// <summary>
    /// Invokes the notification middleware.
    /// </summary>
    /// <param name="context">The notification context.</param>
    ValueTask InvokeAsync(NotificationContext context);
}