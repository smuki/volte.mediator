using System.Threading.Tasks;

namespace Volte.Mediator.Middleware.Notification;

/// <summary>
/// Represents a delegate for a notification middleware.
/// </summary>
public delegate ValueTask NotificationMiddlewareDelegate(NotificationContext context);