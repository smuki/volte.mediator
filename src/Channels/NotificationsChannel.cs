using Volte.Mediator.Abstractions;
using Volte.Mediator.Contracts;

namespace Volte.Mediator.Channels;

/// <inheritdoc cref="Volte.Mediator.Contracts.INotificationsChannel" />
public class NotificationsChannel : ChannelBase<INotification>, INotificationsChannel
{
}