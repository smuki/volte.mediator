namespace Volte.Mediator.Middleware.Command.Contracts;
using System.Threading.Tasks;
using System.Threading;
using System;
using System.Collections.Generic;

/// <summary>
/// Represents a command middleware.
/// </summary>
public interface ICommandMiddleware
{
    /// <summary>
    /// Invokes the middleware.
    /// </summary>
    /// <param name="context">The command context.</param>
    ValueTask InvokeAsync(CommandContext context);
}