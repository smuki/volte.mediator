using Volte.Mediator.Abstractions;
using Volte.Mediator.Contracts;

namespace Volte.Mediator.Channels;

/// <inheritdoc cref="Volte.Mediator.Contracts.ICommandsChannel" />
public class CommandsChannel : ChannelBase<ICommand>, ICommandsChannel
{
}
