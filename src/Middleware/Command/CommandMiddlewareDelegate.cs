using System.Threading.Tasks;

namespace Volte.Mediator.Middleware.Command;

/// <summary>
/// Represents a command middleware delegate.
/// </summary>
public delegate ValueTask CommandMiddlewareDelegate(CommandContext context);