using System.Threading.Channels;
using Volte.Mediator.Models;

namespace Volte.Mediator.Contracts;

/// <summary>
/// A channel that can be used to enqueue jobs.
/// </summary>
public interface IJobsChannel
{
    /// <summary>
    /// Gets the writer for the job queue.
    /// </summary>
    ChannelWriter<EnqueuedJob> Writer { get; }
    
    /// <summary>
    /// Gets the reader for the job queue.
    /// </summary>
    ChannelReader<EnqueuedJob> Reader { get; }
}
