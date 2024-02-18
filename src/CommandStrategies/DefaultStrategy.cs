using System.Threading.Tasks;
using Volte.Mediator.Contexts;
using Volte.Mediator.Contracts;
using Volte.Mediator.Extensions;

namespace Volte.Mediator.CommandStrategies;

/// <summary>
/// Invokes command handlers using the default strategy. 
/// </summary>
public class DefaultStrategy : ICommandStrategy
{
    /// <inheritdoc />
    public async Task<TResult> ExecuteAsync<TResult>(CommandStrategyContext context)
    {
        var command = context.Command;
        var cancellationToken = context.CancellationToken;
        var commandType = command.GetType();
        var handleMethod = commandType.GetCommandHandlerMethod();
        var handler = context.Handler;
        
        return await handler.InvokeAsync<TResult>(handleMethod, command, cancellationToken);
    }
}