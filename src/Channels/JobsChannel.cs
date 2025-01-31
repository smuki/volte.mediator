using Volte.Mediator.Abstractions;
using Volte.Mediator.Contracts;
using Volte.Mediator.Models;

namespace Volte.Mediator.Channels;

/// <inheritdoc cref="Volte.Mediator.Contracts.IJobsChannel" />
public class JobsChannel : ChannelBase<EnqueuedJob>, IJobsChannel
{
}
